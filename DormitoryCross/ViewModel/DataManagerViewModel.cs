//using Android.Content;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using DormitoryCross.Services;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DormitoryCross.ViewModel
{
    public partial class DataManagerViewModel : BaseViewModel
    {
        SQLServices sQLServices;

        ServerServices serverServices;

        PickOptions options;
        
        bool errorLoad = false;
        
        public ObservableCollection<Student> Students { get; } = new();

        public DataManagerViewModel()
        {
            Title = "Управление данными";
            this.sQLServices = new SQLServices();
            this.serverServices =  new ServerServices();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // UTType values
                    { DevicePlatform.Android, new[] { "application/comics/*" } }, // MIME type
                    { DevicePlatform.WinUI, new[] { ".xlsx", ".xlsx" } }, // file extension
                    { DevicePlatform.Tizen, new[] { "*/*" } },
                    { DevicePlatform.macOS, new[] { "xlsx", "xlsx" } }, // UTType values
                });

            this.options = new()
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
        }

        [RelayCommand]
        async Task SaveBDToExcel(string path)
        {
            try
            {
                var students = await sQLServices.GetStudent();
                if (students == null)
                {
                    using (var package = new ExcelPackage(path + "/Студенты общежития №3 ШАБЛОН.xlsx"))
                    {
                        var sheet = package.Workbook.Worksheets.Add("Студенты");
                        sheet.Cells["A1"].Value = "ФИО";
                        sheet.Cells["B1"].Value = "Номер договора";
                        sheet.Cells["C1"].Value = "Период проживания";
                        sheet.Cells["D1"].Value = "Группа";
                        sheet.Cells["E1"].Value = "Номер Комнаты";
                        sheet.Cells["F1"].Value = "Телефон";
                        sheet.Cells["G1"].Value = "ФИО Родителей";
                        sheet.Cells["H1"].Value = "Телефон родителей";

                        package.Save();
                    }
                }
                else
                {
                    using (var package = new ExcelPackage(path + "/Студенты общежития №3.xlsx"))
                    {
                        var sheet = package.Workbook.Worksheets.Add("Студенты");
                        sheet.Cells["A1"].Value = "ФИО";
                        sheet.Cells["B1"].Value = "Номер договора";
                        sheet.Cells["C1"].Value = "Период проживания";
                        sheet.Cells["D1"].Value = "Группа";
                        sheet.Cells["E1"].Value = "Номер Комнаты";
                        sheet.Cells["F1"].Value = "Телефон";
                        sheet.Cells["G1"].Value = "ФИО Родителей";
                        sheet.Cells["H1"].Value = "Телефон родителей";

                        int i = 2;

                        foreach (var student in students)
                        {
                            sheet.Cells[$"A{i}"].Value = student.FullName;
                            sheet.Cells[$"B{i}"].Value = student.NumberContract;
                            sheet.Cells[$"C{i}"].Value = student.Period;
                            sheet.Cells[$"D{i}"].Value = student.Group;
                            sheet.Cells[$"E{i}"].Value = student.NumberRoom;
                            sheet.Cells[$"F{i}"].Value = student.Telefone;
                            sheet.Cells[$"G{i}"].Value = student.FullNameParents;
                            sheet.Cells[$"H{i}"].Value = student.TelefoneParents;
                            i++;
                        }

                        package.Save();
                    }
                }
                await Toast.Make("Сохранение успешно!").Show();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка сохранения!", "Возможно файл с таким именем уже есть в этой папке!", "Ок");
                Debug.WriteLine(ex);
            }
        }

        [RelayCommand]
        public async Task<FileResult> LoadDBIsExcel()
        {
            bool confirm = await Shell.Current.DisplayAlert("Подтвердите загрузку из файла","При загрузки данных из Excel файла, текущие данные будут очищены", "Да", "Нет");
            
            try
            {
                if (confirm)
                {
                    await sQLServices.RemoveAllStudent();

                    var result = await FilePicker.Default.PickAsync(options);
                    if (result != null)
                    {
                        if (result.FileName.EndsWith("xlsx", StringComparison.OrdinalIgnoreCase) ||
                            result.FileName.EndsWith("xlsx", StringComparison.OrdinalIgnoreCase))
                        {
                            using var stream = await result.OpenReadAsync();
                            ExcelPackage package = new ExcelPackage(result.FullPath);
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                            int rows = worksheet.Dimension.Rows;
                            int colums = worksheet.Dimension.Columns;

                            for (int i = 2; i < rows; i++)
                            {

                                if (worksheet.Cells[$"A{i}"].Value != null)
                                {
                                    var numberContract = worksheet.Cells[$"B{i}"].Value == null ? worksheet.Cells[$"B{i}"].Value = "" : worksheet.Cells[$"B{i}"].Value.ToString();
                                    var period = worksheet.Cells[$"C{i}"].Value == null ? worksheet.Cells[$"C{i}"].Value = "" : worksheet.Cells[$"C{i}"].Value.ToString();
                                    var group = worksheet.Cells[$"D{i}"].Value == null ? worksheet.Cells[$"D{i}"].Value = "" : worksheet.Cells[$"D{i}"].Value.ToString();
                                    var numberRoom = worksheet.Cells[$"E{i}"].Value == null ? worksheet.Cells[$"E{i}"].Value = "" : worksheet.Cells[$"E{i}"].Value.ToString();
                                    var telefone = worksheet.Cells[$"F{i}"].Value == null ? worksheet.Cells[$"F{i}"].Value = "" : worksheet.Cells[$"F{i}"].Value.ToString();
                                    var fullNameParents = worksheet.Cells[$"G{i}"].Value == null ? worksheet.Cells[$"G{i}"].Value = "" : worksheet.Cells[$"G{i}"].Value.ToString();
                                    var telefoneParents = worksheet.Cells[$"H{i}"].Value == null ? worksheet.Cells[$"H{i}"].Value = "" : worksheet.Cells[$"H{i}"].Value.ToString();

                                    var student = new Student
                                    {
                                        FullName = worksheet.Cells[$"A{i}"].Value.ToString(),
                                        NumberContract = numberContract.ToString(),
                                        Period = period.ToString(),
                                        Group = group.ToString(),
                                        NumberRoom = numberRoom.ToString().ToUpper(),
                                        Telefone = telefone.ToString(),
                                        FullNameParents = fullNameParents.ToString(),
                                        TelefoneParents = telefoneParents.ToString()
                                    };

                                    if ((student.NumberRoom.ToCharArray().Length > 1))
                                    {
                                        if (student.NumberRoom.ToCharArray().ElementAt(1).Equals('А'))
                                            student.NumberRoom = student.NumberRoom.ToCharArray().ElementAt(0) + "A";
                                    }

                                    if (student.NumberRoom.Equals("")) 
                                    {
                                        student.NumberRoom = student.NumberRoom + "!!!";
                                        errorLoad = true;
                                    }

                                    if (student.FullName.Equals("") || student.NumberContract.Equals("") || student.Period.Equals("") || 
                                        student.Group.Equals("") || student.Telefone.Equals("") || student.FullNameParents.Equals("") || student.TelefoneParents.Equals(""))
                                    {
                                        student.FullName = student.FullName + "!!!";
                                        errorLoad = true;
                                    }

                                    sQLServices.AddStudent(student.FullName, student.NumberContract, student.Period, student.Group, student.NumberRoom, student.Telefone, student.FullNameParents, student.TelefoneParents);
                                }
                            }
                        }
                    }
                    if (errorLoad)
                    {
                        await Shell.Current.DisplayAlert("Загрузка завершена с ошибками в данных!", "Для обнаружения ошибок, сохраните БД" +
                            " в Excel файл, строки с ошибками будут помечены - \"!!!\"", "Ок");
                        return result;
                    }
                    await Toast.Make("Загрузка завершена успешно!").Show();
                    return result;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                await Toast.Make("Ошибка! Загрузка прервана.").Show();
                return null;
            }
        }

        [RelayCommand]
        public async Task PickFolder(CancellationToken cancellationToken)
        {
            try
            {
                var result = await FolderPicker.Default.PickAsync(cancellationToken);
                if (result.IsSuccessful)
                {
                    await Task.Delay(2000);

                    await Toast.Make($"Выбрана папка: Имя - {result.Folder.Name}, Путь - {result.Folder.Path}", ToastDuration.Long).Show(cancellationToken);

                    await SaveBDToExcel(result.Folder.Path);
                }
                else
                {
                    await Toast.Make($"Папка не была выбрана из-за ошибки: {result.Exception.Message}").Show(cancellationToken);
                }
            }
            catch(Exception ex)
            {

            }
        }

        [RelayCommand]
        public async Task LoadBdIsServer()
        {
            bool confirm = await Shell.Current.DisplayAlert("Подтвердите загрузку c сервера", "При загрузке данных из сервера, текущие данные будут очищены", "Да", "Нет");
            try
            {
                if (confirm)
                {
                    

                    await sQLServices.RemoveAllStudent();

                    var students = await serverServices.SelectBD();

                    foreach (var student in students)
                    {
                        sQLServices.AddStudent(student.FullName, student.NumberContract, student.Period, student.Group, student.NumberRoom, student.Telefone, student.FullNameParents, student.TelefoneParents);
                    }
                }
                await Toast.Make($"Загрузка завершена!").Show();
            }
            catch (Exception ex)
            {
                await Toast.Make($"Ошибка!").Show();
            }
        }

        [RelayCommand]
        public async Task InsertBdInServer()
        {
            bool confirm = await Shell.Current.DisplayAlert("Подтвердите отправку данных на сервер", "При отправке данных на сервер, текущие данные на сервере будут очищены", "Да", "Нет");
            try
            {
                if (confirm)
                {
                    ServerServices serverServices = new ServerServices();

                    await serverServices.TruncateBD();

                    var students = await sQLServices.GetStudent();
                    foreach (var student in students)
                    {
                        await serverServices.InsertBD(student.FullName, student.NumberContract, student.Period, student.Group, student.NumberRoom, student.Telefone, student.FullNameParents, student.TelefoneParents);
                    }
                }

                await Toast.Make($"Данные успешно отправлены!").Show();
            }
            catch (Exception ex)
            {
                await Toast.Make($"Ошибка!").Show();
            }
        }
    }
}
