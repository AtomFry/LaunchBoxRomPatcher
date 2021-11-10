using LaunchBoxRomPatcher.DataAccess;
using LaunchBoxRomPatcher.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LaunchBoxRomPatcher.Helpers;
using LaunchBoxRomPatcher.UserInterface.Events;
using System.Linq;

namespace LaunchBoxRomPatcher.ViewModels
{
    // public class ManageRomPatchersNavigationViewModel
    public sealed class ManageRomPatchersNavigationViewModel : ViewModelBase
    {
        private static readonly ManageRomPatchersNavigationViewModel instance = new ManageRomPatchersNavigationViewModel();
        public static ManageRomPatchersNavigationViewModel Instance
        {
            get
            {
                return instance;
            }
        }

        // explicit static constructor to tell C# compiler not to mark type as beforefieldinit
        static ManageRomPatchersNavigationViewModel()
        {
        }
        
        private ManageRomPatchersNavigationViewModel()
        {
            RomPatchers = new ObservableCollection<ManageRomPatchersNavigationItemViewModel>();

            EventAggregatorHelper.Instance.EventAggregator
                .GetEvent<RomPatcherSaved>()
                .Subscribe(AfterRomPatcherSaved);
        }

        private void AfterRomPatcherSaved(RomPatcher savedRomPatcher)
        {
            ManageRomPatchersNavigationItemViewModel romPatcher = RomPatchers.Single(rp => rp.Id == savedRomPatcher.RomPatcherId);
            romPatcher.DisplayMember = savedRomPatcher.RomPatcherName;
        }

        public async Task LoadAsync()
        {
            List<RomPatcher> lookup = await RomPatcherRepository.Instance.GetAllAsync();

            RomPatchers.Clear();
            foreach (RomPatcher item in lookup)
            {
                RomPatchers.Add(new ManageRomPatchersNavigationItemViewModel(item.RomPatcherId, item.RomPatcherName));
            }
        }

        // public ObservableCollection<RomPatcher> RomPatchers { get; }
        public ObservableCollection<ManageRomPatchersNavigationItemViewModel> RomPatchers { get; }

        public string ErrorMessage 
        { 
            get
            {
                return RomPatcherRepository.Instance.ErrorMessage;                
            }
        }
    }
}
