using NutriPlat.Mobile.ViewModels;

namespace NutriPlat.Mobile.Views
{
    public partial class NutritionPlanView : ContentPage
    {
        public NutritionPlanView(NutritionPlanViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
