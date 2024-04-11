using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitoryCross.Services;
using DormitoryCross.View;

namespace DormitoryCross.ViewModel
{
    [QueryProperty("NumberRoom", "NumberRoom")]
    public partial class StudentsViewModel : BaseViewModel
    {
        SQLServices sQLServices;

        [ObservableProperty]
        string numberRoom;

        public ObservableCollection<Student> Students { get; } = new();

        IConnectivity connectivity;

        public StudentsViewModel(SQLServices sQLServices) 
        {
            Title = "Комната №" + numberRoom;
            this.sQLServices = sQLServices;
            this.connectivity = Connectivity.Current;
            Refresh();
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GoToDetailsAsync(Student student)
        {
            if (student is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true,
                new Dictionary<string, object>
                {
                    {"Student", student }
                });
        }

        [RelayCommand]
        async Task Add()
        {
            await Shell.Current.GoToAsync(nameof(AddStudent));
        }

        [RelayCommand]
        async Task Remove(Student student)
        {
            await sQLServices.RemoveStudent(13);
            await Refresh();
        }

        [RelayCommand]
        async Task Refresh()
        {
            if (IsBusy)
                return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Internet issue", $"Check internet!", "Ok");
                    return;
                }

                IsBusy = true;

                await Task.Delay(2000);

                Students.Clear();

                var students = await sQLServices.GetStudentRoom(numberRoom);

                foreach (var student in students)
                {
                    Students.Add(student);
                }

                //var students = await serverService.GetStudents();

                //if (Students.Count != 0)
                //    Students.Clear();

                //foreach (var student in students)
                //{
                //    Students.Add(student);
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", $"get study: {ex.Message}", "Ok");

            }
            finally
            {
                IsBusy = false;
                isRefreshing = false;
            }
        }
    }
}
