namespace NutriPlat.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Shell.Current.GoToAsync("//Login");

        }
    }
}
