using LaunchBoxRomPatcher.Helpers;
using LaunchBoxRomPatcher.UserInterface.Events;
using Prism.Commands;
using System;
using System.Windows.Input;

namespace LaunchBoxRomPatcher.ViewModels
{
    public class ManageRomPatchersNavigationItemViewModel : ViewModelBase
    {
        private string _displayMember;

        public ManageRomPatchersNavigationItemViewModel(string id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;

            OpenRomPatcherDetailViewCommand = new DelegateCommand(OnOpenRomPatcherDetailView);
        }

        public string Id { get; }
        public string DisplayMember
        {
            get { return _displayMember; } 
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenRomPatcherDetailViewCommand { get; }

        private void OnOpenRomPatcherDetailView()
        {
            EventAggregatorHelper.Instance.EventAggregator
                .GetEvent<OpenRomPatcherEvent>()
                .Publish(Id);
        }
    }
}
