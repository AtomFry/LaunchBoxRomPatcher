namespace LaunchBoxRomPatcher.ViewModels
{
    public class ManageRomPatchersNavigationItemViewModel : ViewModelBase
    {
        private string _displayMember;

        public ManageRomPatchersNavigationItemViewModel(string id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
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
    }
}
