namespace DormitoryCross.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(StudentsDetailsViewModel studentsDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = studentsDetailsViewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}