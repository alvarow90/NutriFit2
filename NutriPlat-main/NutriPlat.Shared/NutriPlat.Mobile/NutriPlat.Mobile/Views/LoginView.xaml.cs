using NutriPlat.Mobile.ViewModels;

namespace NutriPlat.Mobile.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView(LoginViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm; 
        }
    }
}
