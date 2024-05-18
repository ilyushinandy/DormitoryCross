namespace DormitoryCross.View;

public partial class AdminPage : ContentPage
{
	UserViewModel userViewModel = new UserViewModel();

	public AdminPage()
	{
		InitializeComponent();

		BindingContext = userViewModel;
	}
}