using DormitoryCross.Services;
using DormitoryCross.View;
using Microsoft.Maui.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryCross.ViewModel
{
    public partial class SearchViewModel : BaseViewModel
    {
        SQLServices sQLServices;

        public ObservableCollection<Student> Students { get; } = new();

        string fullName;

        public string FullName { get => fullName; set => SetProperty(ref fullName, value); }

        [ObservableProperty]
        bool isRefreshing;

        public SearchViewModel()
        {
            Title = "Поиск студента";
            sQLServices = new SQLServices();
        }

        [RelayCommand]
        async Task SearchStudy(Room room)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                await Task.Delay(2000);

                Students.Clear();

                var students = await sQLServices.SearchStudent(FullName);

                foreach (var student in students)
                {
                    Students.Add(student);
                }
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
    }
}
