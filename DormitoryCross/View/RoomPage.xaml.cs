using DormitoryCross.Services;
using Microsoft.Maui.Networking;

namespace DormitoryCross.View;

public partial class RoomPage : ContentPage
{
    static SQLServices sQLServices = new SQLServices();
    StudentsViewModel studentsViewModel = new(sQLServices);

    public RoomPage()
	{
		InitializeComponent();

        BindingContext = studentsViewModel;
	}
}