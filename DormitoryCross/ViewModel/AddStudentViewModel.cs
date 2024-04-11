using DormitoryCross.Services;
using Microsoft.Maui.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryCross.ViewModel
{
    public partial class AddStudentViewModel: BaseViewModel
    {
        Student student = new();
        SQLServices sQLServices;
        string fullName, numberContract, period, group, telefone, numberRoom;

        public string FullName{ get => fullName; set => SetProperty(ref fullName, value); }
        public string NumberContract { get => numberContract; set => SetProperty(ref numberContract, value); }
        public string Period { get => period; set => SetProperty(ref period, value); }
        public string Group { get => group; set => SetProperty(ref group, value); }
        public string Telefone { get => telefone; set => SetProperty(ref telefone, value); }
        public string NumberRoom { get => numberRoom; set => SetProperty(ref numberRoom, value); }

        public AddStudentViewModel() 
        {
            Title = "Добавить студента";
            sQLServices = new SQLServices();
        }

        [RelayCommand]
        async Task Save()
        {
            if (IsBusy)
                return;

            student.FullName = FullName;
            student.NumberContract = NumberContract;
            student.Period = Period;
            student.Group = Group;
            student.Telefone = Telefone;
            student.NumberRoom = NumberRoom;

            try
            {
                if (student == null)
                {
                    return;
                }

                IsBusy = true;

                await sQLServices.AddStudent(student.FullName, student.NumberContract, student.Period, student.Group, student.Telefone, student.NumberRoom);
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
