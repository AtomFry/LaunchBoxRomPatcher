using LaunchBoxRomPatcher.ViewModels;
using System.Windows;

namespace LaunchBoxRomPatcher.UserInterface
{
    public partial class ManageRomPatchers : Window
    {
        public ManageRomPatchers()
        {
            InitializeComponent();

            // set the data context to our view model 
            DataContext = ManageRomPatchersViewModel.Instance;

            // once the window is loaded, load the data 
            Loaded += ManageRomPatchers_Loaded;
        }

        private async void ManageRomPatchers_Loaded(object sender, RoutedEventArgs e)
        {
            await ManageRomPatchersViewModel.Instance.LoadAsync();

        }
    }
}
