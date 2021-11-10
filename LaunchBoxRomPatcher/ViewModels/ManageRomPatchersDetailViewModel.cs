using LaunchBoxRomPatcher.DataAccess;
using LaunchBoxRomPatcher.Helpers;
using LaunchBoxRomPatcher.Models;
using LaunchBoxRomPatcher.Models.ModelWrappers;
using LaunchBoxRomPatcher.UserInterface.Events;
using Microsoft.Win32;
using Prism.Commands;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LaunchBoxRomPatcher.ViewModels
{
    public sealed class ManageRomPatchersDetailViewModel : ViewModelBase
    {
        private static readonly ManageRomPatchersDetailViewModel instance = new ManageRomPatchersDetailViewModel();

        static ManageRomPatchersDetailViewModel()
        {
        }

        private ManageRomPatchersDetailViewModel()
        {
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            SelectFileCommand = new DelegateCommand(OnSelectFile, OnSelectFileCanExecute);
        }

        private void OnSelectFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                RomPatcher.RomPatcherFilePath = openFileDialog.FileName;

                // trip the property changed to update the UI
                OnPropertyChanged("RomPatcher");
            }
        }

        // check if rom patcher is valid for picking file
        private bool OnSelectFileCanExecute()
        {
            if (RomPatcher == null) return false;

            return true;
        }

        private async void OnSaveExecute()
        {
            await RomPatcherRepository.Instance.SaveRomPatcher(RomPatcher.Model);

            HasChanges = await RomPatcherRepository.Instance.RomPatcherHasChanges(RomPatcher.Model);

            EventAggregatorHelper.Instance.EventAggregator
                .GetEvent<RomPatcherSaved>()
                .Publish(RomPatcher.Model);
        }

        // check if rom patcher is valid for saving
        private bool OnSaveCanExecute()
        {
            if (RomPatcher == null) return false;
            if (RomPatcher.HasErrors) return false;
            if (!HasChanges) return false;

            return true;
        }

        public static ManageRomPatchersDetailViewModel Instance => instance;

        public async Task LoadAsync(string romPatcherId)
        {
            RomPatcher romPatcher = await RomPatcherRepository.Instance.GetByIdAsync(romPatcherId);

            RomPatcher = new RomPatcherWrapper(romPatcher);

            HasChanges = false;

            RomPatcher.PropertyChanged += async (s, e) =>
            {
                if(!HasChanges)
                {
                    HasChanges = await RomPatcherRepository.Instance.RomPatcherHasChanges(RomPatcher.Model);
                }

                if (e.PropertyName == nameof(RomPatcher.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)SelectFileCommand).RaiseCanExecuteChanged();
        }
        
        private RomPatcherWrapper _romPatcher;
        public RomPatcherWrapper RomPatcher
        {
            get { return _romPatcher; }
            private set
            {
                _romPatcher = value;
                OnPropertyChanged();
            }
        }

        private bool _hasChanges;
        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand SelectFileCommand { get; }
    }
}
