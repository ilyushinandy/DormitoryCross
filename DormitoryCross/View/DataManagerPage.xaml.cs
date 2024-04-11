using CommunityToolkit.Maui.Storage;
using DormitoryCross.Services;

namespace DormitoryCross.View;

public partial class DataManagerPage : ContentPage
{
    DataManagerViewModel dataManagerViewModel = new DataManagerViewModel();

    public DataManagerPage()
	{
		InitializeComponent();

        BindingContext = dataManagerViewModel;
	}
}