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
        }
    }
}
