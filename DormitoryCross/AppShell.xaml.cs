using DormitoryCross.View;

namespace DormitoryCross
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RoomPage), typeof(RoomPage));
            Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
            Routing.RegisterRoute(nameof(AddStudent), typeof(AddStudent));
            Routing.RegisterRoute(nameof(RoomsPage), typeof(RoomsPage));
            Routing.RegisterRoute(nameof(DataManagerPage), typeof(DataManagerPage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(StatisticsPage), typeof(StatisticsPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}
