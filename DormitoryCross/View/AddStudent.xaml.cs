using DormitoryCross.Services;

namespace DormitoryCross.View;

public partial class AddStudent : ContentPage
{
    AddStudentViewModel addStudentViewModel  = new();

    public AddStudent()
	{
		InitializeComponent();

        BindingContext = addStudentViewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}