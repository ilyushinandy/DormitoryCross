using DormitoryCross.Services;

namespace DormitoryCross.View;

public partial class RoomsPage : ContentPage
{
    static SQLServices sQLServices = new SQLServices();
    RoomsViewModel roomsViewModel = new(sQLServices);

    public RoomsPage()
	{
		InitializeComponent();

        BindingContext = roomsViewModel;
	}
}