namespace DormitoryCross.View;

public partial class LoginPage : ContentPage
{
	string passwordAdmin = "ilyushinR00t";
    SQLServices sQLServices;

    public LoginPage()
	{
		InitializeComponent();

        this.sQLServices = sQLServices;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		if (password.Equals(passwordAdmin))
		{
			return;
		}
		
    }
}