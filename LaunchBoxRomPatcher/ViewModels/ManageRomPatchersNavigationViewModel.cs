using LaunchBoxRomPatcher.DataAccess;
using LaunchBoxRomPatcher.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Events;
using LaunchBoxRomPatcher.Helpers;
using LaunchBoxRomPatcher.UserInterface.Events;

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
            RomPatchers = new ObservableCollection<RomPatcher>();
        }

        public async Task LoadAsync()
        {
            List<RomPatcher> lookup = await RomPatcherRepository.Instance.GetAllAsync();

            RomPatchers.Clear();
            foreach (RomPatcher item in lookup)
            {
                RomPatchers.Add(item);
            }

            if(RomPatchers.Count != 0)
            {
                SelectedRomPatcher = RomPatchers[0];
            }
        }

        public ObservableCollection<RomPatcher> RomPatchers { get; }

        private RomPatcher selectedRomPatcher;
        public RomPatcher SelectedRomPatcher
        {
            get { return selectedRomPatcher; }
            set 
            {
                if (selectedRomPatcher != value)
                {
                    selectedRomPatcher = value;

                    OnPropertyChanged();

                    // publish event when selected rom patcher changes
                    if(selectedRomPatcher != null)
                    {
                        EventAggregatorHelper.Instance.EventAggregator
                            .GetEvent<OpenRomPatcherEvent>()
                            .Publish(selectedRomPatcher.RomPatcherId);
                    }
                }
            }
        }

        public string ErrorMessage 
        { 
            get
            {
                return RomPatcherRepository.Instance.ErrorMessage;                
            }
        }
    }
}
