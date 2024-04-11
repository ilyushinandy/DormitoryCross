using DormitoryCross.View;

namespace DormitoryCross
{
    public partial class MainPage : ContentPage
    {
        public string n = "3";
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GoToRoomPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RoomPage));
        }

        private async void GoToAddStudentAsync(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddStudent));
        }

        private async void GoToRoomsPageAsync(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RoomsPage));
        }

        private async void GoToDataManagerPageAsync(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(DataManagerPage)}");
        }
    }

}
