using DocumentFormat.OpenXml.Spreadsheet;
using DormitoryCross.Services;

namespace DormitoryCross.View;

public partial class LoginPage : ContentPage
{
	string passwordAdmin = "ilyushinR00t";
    string logindAdmin = "admin";
    SQLServices sQLServices;
    IEnumerable<User> users;
    public LoginPage()
	{
		InitializeComponent();

        this.sQLServices = new SQLServices();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        users = await sQLServices.GetUsers();
        if (users.Count().Equals(0))
        {
            await Shell.Current.GoToAsync($"/{nameof(MainPage)}");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (password.Text.Equals(passwordAdmin))
            {
                await Shell.Current.GoToAsync($"{nameof(AdminPage)}?Settings={logindAdmin}");
            }
            else if(password.Text.Equals(passwordAdmin) && login.Text.Equals(logindAdmin))
            {
                await Shell.Current.GoToAsync($"/{nameof(MainPage)}");
            }

            foreach (var user in users)
            {
                if (user.Name.Equals(login.Text) & user.Password.Equals(password.Text))
                {
                    await Shell.Current.GoToAsync($"/{nameof(MainPage)}");
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}