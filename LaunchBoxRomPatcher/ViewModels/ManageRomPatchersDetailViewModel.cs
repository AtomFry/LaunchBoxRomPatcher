using LaunchBoxRomPatcher.DataAccess;
using LaunchBoxRomPatcher.Helpers;
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

            EventAggregatorHelper.Instance.EventAggregator
                .GetEvent<RomPatcherSaved>()
                .Publish(RomPatcher.Model);
        }

        // check if rom patcher is valid for saving
        private bool OnSaveCanExecute()
        {
            // todo: check if RomPatcher has any changes - no need to save if nothing changed

            if (RomPatcher == null) return false;
            if (RomPatcher.HasErrors) return false;

            return true;
        }

        public static ManageRomPatchersDetailViewModel Instance => instance;

        public async Task LoadAsync(string romPatcherId)
        {
            var romPatcher = await RomPatcherRepository.Instance.GetByIdAsync(romPatcherId);

            RomPatcher = new RomPatcherWrapper(romPatcher);

            RomPatcher.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(RomPatcher.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
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

        public ICommand SaveCommand { get; }
        public ICommand SelectFileCommand { get; }
    }
}
