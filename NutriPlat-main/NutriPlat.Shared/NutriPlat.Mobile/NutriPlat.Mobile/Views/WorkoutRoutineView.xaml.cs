using NutriPlat.Mobile.ViewModels;

namespace NutriPlat.Mobile.Views
{
    public partial class WorkoutRoutineView : ContentPage
    {
        public WorkoutRoutineView(WorkoutRoutineViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
