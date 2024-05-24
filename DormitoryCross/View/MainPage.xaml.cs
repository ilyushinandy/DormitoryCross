using DormitoryCross.View;

namespace DormitoryCross.View
{
    public partial class MainPage : ContentPage
    {
        public string n = "3";
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var a = Shell.Current.CurrentState;

            Routing.UnRegisterRoute(nameof(LoginPage));

            var b = Shell.Current.CurrentState;

            //var stack = Shell.Current.Navigation.NavigationStack.ToArray();

            //for (int i = stack.Length - 1; i > 0; i--)
            //{
            //    Shell.Current.Navigation.RemovePage(stack[i]);
            //}
        }

        protected override bool OnBackButtonPressed()
        {
            
            return base.OnBackButtonPressed();
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
            await Shell.Current.GoToAsync(nameof(DataManagerPage));
        }

        private async void GoToSearchPageAsync(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SearchPage));
        }

        private async void GoToSettingsPageAsync(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SettingsPage)}");
        }

        private async void GoToStatisticsPageAsync(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(StatisticsPage));
        }
    }

}
