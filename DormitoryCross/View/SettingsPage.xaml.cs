namespace DormitoryCross.View;

public partial class SettingsPage : ContentPage
{
	UserViewModel userViewModel = new UserViewModel();

	public SettingsPage()
	{
		InitializeComponent();

		BindingContext = userViewModel;

        var a = Shell.Current.CurrentState;
    }
}