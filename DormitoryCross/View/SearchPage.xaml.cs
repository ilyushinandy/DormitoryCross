namespace DormitoryCross.View;

public partial class SearchPage : ContentPage
{
	SearchViewModel searchViewModel = new SearchViewModel();

	public SearchPage()
	{
		InitializeComponent();

		BindingContext = searchViewModel;
	}
}