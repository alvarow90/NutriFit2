using NutriPlat.Mobile.ViewModels;

namespace NutriPlat.Mobile.Views
{
    public partial class ProgressView : ContentPage
    {
        public ProgressView(ProgressViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
