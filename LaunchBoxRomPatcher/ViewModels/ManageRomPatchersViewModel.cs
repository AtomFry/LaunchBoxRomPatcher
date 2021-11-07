using LaunchBoxRomPatcher.Helpers;
using LaunchBoxRomPatcher.UserInterface.Events;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace LaunchBoxRomPatcher.ViewModels
{
    public sealed class ManageRomPatchersViewModel : ViewModelBase
    {
        private static readonly ManageRomPatchersViewModel instance = new ManageRomPatchersViewModel();

        static ManageRomPatchersViewModel() 
        {
        }

        private ManageRomPatchersViewModel()
        {
            EventAggregatorHelper.Instance.EventAggregator
                .GetEvent<OpenRomPatcherEvent>()
                .Subscribe(OnOpenRomPatcherView);
        }

        private async void OnOpenRomPatcherView(string romPatcherId)
        {
            await ManageRomPatchersDetailViewModel.LoadAsync(romPatcherId);
        }

        public static ManageRomPatchersViewModel Instance
        {
            get
            {
                return instance;
            }
        }

        public async Task LoadAsync()
        {
            await ManageRomPatchersNavigationViewModel.LoadAsync();
        }

        public ManageRomPatchersNavigationViewModel ManageRomPatchersNavigationViewModel => ManageRomPatchersNavigationViewModel.Instance;

        public ManageRomPatchersDetailViewModel ManageRomPatchersDetailViewModel => ManageRomPatchersDetailViewModel.Instance;

        public Image IconImage => Properties.Resources.RomHackingIcon;
    }
}
