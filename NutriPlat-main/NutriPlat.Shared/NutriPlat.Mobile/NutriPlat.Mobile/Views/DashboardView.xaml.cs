using NutriPlat.Mobile.ViewModels;

namespace NutriPlat.Mobile.Views
{
    public partial class DashboardView : ContentPage
    {
        public DashboardView(DashboardViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
