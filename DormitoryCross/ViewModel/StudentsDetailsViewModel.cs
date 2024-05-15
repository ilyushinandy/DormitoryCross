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
        public StudentsDetailsViewModel() 
        {
            
        }

        [ObservableProperty]
        Student student;

        [RelayCommand]
        async Task GoAddStudent()
        {
            await Shell.Current.GoToAsync($"{nameof(AddStudent)}", true,
                new Dictionary<string, object>
                {
                    {"Student", student }
                });
        }
    }
}
