using DormitoryCross.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryCross.ViewModel
{
    public partial class UserViewModel : BaseViewModel
    {
        SQLServices sQLServices;

        public ObservableCollection<User> Users { get; } = new();

        string id, name, email, password;

        public string Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }

        [ObservableProperty]
        bool isRefreshing;

        public UserViewModel()
        {
            Title = "Статистика";
            sQLServices = new SQLServices();
            GetUsers();
        }

        [RelayCommand]
        async Task GetUsers()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                await Task.Delay(2000);

                Users.Clear();

                var users = await sQLServices.GetUsers();

                foreach (var user in users)
                {
                    Users.Add(user);
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
        async Task AddUser()
        {
            if (IsBusy)
                return;

            try
            {
                await sQLServices.AddUsers(Name, Email, Password);
                Name = "";
                Email = "";
                Password = "";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", $"insert user: {ex.Message}", "Ok");

            }
            finally
            {
                IsBusy = false;
                isRefreshing = false;
            }
        }

        [RelayCommand]
        async Task RemoveUser()
        {
            if (IsBusy)
                return;

            try
            {
                await sQLServices.RemoveUser(int.Parse(Id));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", $"del user: {ex.Message}", "Ok");

            }
            finally
            {
                IsBusy = false;
                isRefreshing = false;
            }
        }

        [RelayCommand]
        async Task UpdateUser()
        {
            if (IsBusy)
                return;

            try
            {
                await sQLServices.UpdateUser(int.Parse(Id), Name, Email, Password);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", $"del user: {ex.Message}", "Ok");

            }
            finally
            {
                IsBusy = false;
                isRefreshing = false;
            }
        }

        [RelayCommand]
        async Task ClickedUser(User user)
        {
            if (IsBusy)
                return;

            try
            {
                Id = user.Id.ToString();
                Name = user.Name;
                Email = user.Email;
                Password = user.Password;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", $"click user: {ex.Message}", "Ok");

            }
            finally
            {
                IsBusy = false;
                isRefreshing = false;
            }
        }
    }
}
