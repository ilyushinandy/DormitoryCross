//using Android.Content;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using DocumentFormat.OpenXml.Spreadsheet;
using DormitoryCross.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryCross.ViewModel
{
    public partial class DataManagerViewModel : BaseViewModel
    {
        SQLServices sQLServices;
        PickOptions options;

        public DataManagerViewModel()
        {
            this.sQLServices = new SQLServices();

            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // UTType values
                    { DevicePlatform.Android, new[] { "application/comics" } }, // MIME type
                    { DevicePlatform.WinUI, new[] { ".cbr", ".cbz" } }, // file extension
                    { DevicePlatform.Tizen, new[] { "*/*" } },
                    { DevicePlatform.macOS, new[] { "cbr", "cbz" } }, // UTType values
                });

            this.options = new()
            {
                PickerTitle = "Please select a comic file",
                FileTypes = customFileType,
            };
        }

        
        async Task SaveBDToExcel(string path)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                var students = await sQLServices.GetStudent();
                if (students == null)
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage(path + "/Студенты общежития №3 ШАБЛОН.xlsx"))
                    {
                        var sheet = package.Workbook.Worksheets.Add("Студенты");
                        sheet.Cells["A1"].Value = "ФИО";
                        sheet.Cells["B1"].Value = "Номер договора";
                        sheet.Cells["C1"].Value = "Период проживания";
                        sheet.Cells["E1"].Value = "Группа";
                        sheet.Cells["D1"].Value = "Номер Комнаты";
                        sheet.Cells["F1"].Value = "Телефон";

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
                        sheet.Cells["E1"].Value = "Группа";
                        sheet.Cells["D1"].Value = "Номер Комнаты";
                        sheet.Cells["F1"].Value = "Телефон";

                        int i = 2;

                        foreach (var student in students)
                        {
                            sheet.Cells[$"A{i}"].Value = student.FullName;
                            sheet.Cells[$"B{i}"].Value = student.NumberContract;
                            sheet.Cells[$"C{i}"].Value = student.Period;
                            sheet.Cells[$"E{i}"].Value = student.Group;
                            sheet.Cells[$"D{i}"].Value = student.NumberRoom;
                            sheet.Cells[$"F{i}"].Value = student.Telefone;
                            i++;
                        }

                        package.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        [RelayCommand]
        public async Task<FileResult> LoadDBIsExcel()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName.EndsWith("xlsx", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        using var stream = await result.OpenReadAsync();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
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

                    await Toast.Make($"The folder was picked: Name - {result.Folder.Name}, Path - {result.Folder.Path}", ToastDuration.Long).Show(cancellationToken);

                    await SaveBDToExcel(result.Folder.Path);
                    
                }
                else
                {
                    await Toast.Make($"The folder was not picked with error: {result.Exception.Message}").Show(cancellationToken);
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
