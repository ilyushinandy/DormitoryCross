namespace DormitoryCross.View;

public partial class StatisticsPage : ContentPage
{
	StatisticsViewModel statisticsViewModel = new StatisticsViewModel();

	public StatisticsPage()
	{
		InitializeComponent();

		BindingContext = statisticsViewModel;
	}
}