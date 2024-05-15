using DormitoryCross.Services;
using Microsoft.Maui.Networking;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryCross.ViewModel
{
    [QueryProperty("Student", "Student")]
    [QueryProperty("roomAdd", "roomAdd")]
    public partial class AddStudentViewModel: BaseViewModel
    {
        SQLServices sQLServices;

        [ObservableProperty]
        Student student;

        [ObservableProperty]
        string roomAdd;

        string fullName, numberContract, period, group, telefone, numberRoom, fullNameParents, telefoneParents;
        int id;
        public int Id { get => id; set => SetProperty(ref id, value); }
        public string FullName{ get => fullName; set => SetProperty(ref fullName, value); }
        public string NumberContract { get => numberContract; set => SetProperty(ref numberContract, value); }
        public string Period { get => period; set => SetProperty(ref period, value); }
        public string Group { get => group; set => SetProperty(ref group, value); }
        public string Telefone { get => telefone; set => SetProperty(ref telefone, value); }
        public string NumberRoom { get => numberRoom;
            set
            {
                var s = value.ToUpper();
                if ((s.ToCharArray().Length > 1))
                {
                    if (s.ToCharArray().ElementAt(1).Equals('А'))
                        s = s.ToCharArray().ElementAt(0) + "A";
                }
                value = s;
                SetProperty(ref numberRoom, value);
            } }
        public string FullNameParents { get => fullNameParents; set => SetProperty(ref fullNameParents, value); }
        public string TelefoneParents { get => telefoneParents; set => SetProperty(ref telefoneParents, value); }

        public AddStudentViewModel() 
        {
            Title = "Добавить студента";
            sQLServices = new SQLServices();
            Load();
        }

        async Task Load()
        {
            await Task.Delay(2000);

            if (student != null)
            {
                Title = "Редактировать";
                Id = student.Id;
                FullName = student.FullName;
                NumberContract = student.NumberContract;
                Period = student.Period;
                Group = student.Group;
                Telefone = student.Telefone;
                NumberRoom = student.NumberRoom;
                FullNameParents = student.FullNameParents;
                TelefoneParents = student.TelefoneParents;
            }

            if (roomAdd != null)
                NumberRoom = roomAdd;
        }

        [RelayCommand]
        async Task Save()
        {
            if (IsBusy)
                return;

            if (FullName != null & NumberContract != null & Period != null & Group != null & Telefone != null &
                NumberRoom != null & FullNameParents != null & telefoneParents != null)
            {
                student = new Student
                {
                    Id = Id,
                    FullName = FullName,
                    NumberContract = NumberContract,
                    Period = Period,
                    Group = Group,
                    Telefone = Telefone,
                    NumberRoom = NumberRoom,
                    FullNameParents = FullNameParents,
                    TelefoneParents = TelefoneParents
                };
            }
            else
            {
                await Shell.Current.DisplayAlert("Не все данные введены", "Есть пустые поля", "Ok");
                return;
            }

            try
            {
                if (student == null)
                {
                    return;
                }

                IsBusy = true;

                if (Title == "Добавить студента")
                {
                    await sQLServices.AddStudent(student.FullName, student.NumberContract, student.Period, student.Group, student.NumberRoom, student.Telefone, student.FullNameParents, student.TelefoneParents);
                    await Shell.Current.DisplayAlert("Добавление студента", "Успешно!", "Ok");
                    FullName = "";
                    NumberContract = "";
                    Period = "";
                    Group = "";
                    Telefone = "";
                    NumberRoom = "";
                    FullNameParents = "";
                    TelefoneParents = "";
                }
                else
                {
                    await sQLServices.UpdateStudent(student.Id, student.FullName, student.NumberContract, student.Period, student.Group, student.NumberRoom, student.Telefone, student.FullNameParents, student.TelefoneParents);
                    await Shell.Current.DisplayAlert("Редактирование студента", "Успешно!", "Ok");

                    await Shell.Current.GoToAsync("..", true,
                        new Dictionary<string, object>
                        {
                             {"Student", Student }
                        });
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", $"add study: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
