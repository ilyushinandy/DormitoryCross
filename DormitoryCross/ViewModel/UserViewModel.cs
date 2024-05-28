using CommunityToolkit.Maui.Alerts;
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
    [QueryProperty("Settings", "Settings")]
    public partial class UserViewModel : BaseViewModel
    {
        SQLServices sQLServices;
        ServerServices serverServices;
        IConnectivity connectivity;

        [ObservableProperty]
        string settings;

        bool title;

        public ObservableCollection<User> Users { get; } = new();

        string id, name, email, password, buttonText;

        public string Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public string ButtonText { get => buttonText; set => SetProperty(ref buttonText, value); }

        [ObservableProperty]
        bool isRefreshing;

        public UserViewModel()
        {
            sQLServices = new SQLServices();
            serverServices = new ServerServices();
            this.connectivity = Connectivity.Current;
            GetUsers();

            if (title)
            {
                Title = "Настройки администратора";
            }
            else
            {
                Title = "Настройки";
            }

            ButtonText = "Сохранить";
        }

        async Task Init()
        {
            await Task.Delay(2000);

            foreach (var user in Users)
            {
                Id = user.Id.ToString();
                Name = user.Name;
                Email = user.Email;
                Password = user.Password;
            }

            ButtonText = "Редактировать";
        }

        [RelayCommand]
        async Task GetUsers()
        {
            if (IsBusy)
                return;

            await Task.Delay(2000);

            if (settings != null)
            {
                title = true;
            }
            else
            {
                title = false;
            }

            try
            {
                IsBusy = true;

                Users.Clear();

                var users = await sQLServices.GetUsers();

                foreach (var user in users)
                {
                    Users.Add(user);
                }

                if (Users.Count > 0 && !title)
                {
                    Init();
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
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Нет подключения к сети", $"Пароль сохранен только локально!", "Ok");
                    return;
                }
                await serverServices.InsertUser(Name, Email, Password);
                ButtonText = "Редактировать";
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
                
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Нет подключения к сети", $"Проверьте интернет!", "Ok");
                    return;
                }
                await serverServices.DeleteUser(Id);
                Id = "";
                Name = "";
                Email = "";
                Password = "";
                ButtonText = "Сохранить";
                await Toast.Make("Удаление успешно!").Show();
                await GetUsers();
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
