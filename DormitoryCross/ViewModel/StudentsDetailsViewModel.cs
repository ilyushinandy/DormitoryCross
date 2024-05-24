using DormitoryCross.Services;
using DormitoryCross.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryCross.ViewModel
{
    [QueryProperty("Student", "Student")]
    public partial class StudentsDetailsViewModel : BaseViewModel
    {
        SQLServices sQLServices;
        public StudentsDetailsViewModel() 
        {
            sQLServices = new SQLServices();
        }

        [ObservableProperty]
        Student student;

        [RelayCommand]
        async Task GoUpdateStudent()
        {
            await Shell.Current.GoToAsync($"{nameof(AddStudent)}", true,
                new Dictionary<string, object>
                {
                    {"Student", student }
                });
        }

        [RelayCommand]
        async Task Remove()
        {
            await sQLServices.RemoveStudent(Student.Id);
            await Shell.Current.DisplayAlert("Удаление студента", "Успешно!", "Ok");
            await Shell.Current.GoToAsync("..");
        }
    }
}
