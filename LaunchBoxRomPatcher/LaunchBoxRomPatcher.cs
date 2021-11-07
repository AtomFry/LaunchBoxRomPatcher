using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace LaunchBoxRomPatcher
{
    public class LaunchBoxRomPatcher : IGameMenuItemPlugin
    {
        public bool SupportsMultipleGames => false;

        public string Caption => "Add ROM patch";

        public System.Drawing.Image IconImage => Properties.Resources.RomHackingIcon;

        public bool ShowInLaunchBox => true;

        public bool ShowInBigBox => false;

        public bool GetIsValidForGame(IGame selectedGame)
        {
            return true;
        }

        public bool GetIsValidForGames(IGame[] selectedGames)
        {
            return false;
        }

        public void OnSelected(IGame selectedGame)
        {
            // todo: get handle on selectedGame file 
            // todo: resolve file directory
            // todo: copy selectedGame file to temp directory
            // todo: prompt for patch file 
            // todo: file prompt
            // todo: default title from file - allow overwrite
            // todo: option for copying images and video files
            // todo: resolve patcher 
            // todo: apply patch to temp file
            // todo: copy to destination directory
            // todo: create new iGame

            /*
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                MessageBox.Show(openFileDialog.FileName);
            }
            */
            /*
            IGame newGame = PluginHelper.DataManager.AddNewGame("Final Fantasy Tactics Test");
            newGame.AggressiveWindowHiding = selectedGame.AggressiveWindowHiding;
            newGame.ApplicationPath = selectedGame.ApplicationPath;
            newGame.EmulatorId = selectedGame.EmulatorId;
            newGame.Platform = selectedGame.Platform;
            newGame.GenresString = selectedGame.GenresString;
            newGame.CommandLine = selectedGame.CommandLine;
            newGame.StarRating = selectedGame.StarRating;
            newGame.Source = selectedGame.Source;
            newGame.ConfigurationCommandLine = selectedGame.ConfigurationCommandLine;
            newGame.ConfigurationPath = selectedGame.ConfigurationPath;
            newGame.Developer = selectedGame.Developer;
            newGame.DisableShutdownScreen = selectedGame.DisableShutdownScreen;
            newGame.DosBoxConfigurationPath = selectedGame.DosBoxConfigurationPath;
            newGame.GenresString = selectedGame.GenresString;
            newGame.HideAllNonExclusiveFullscreenWindows = selectedGame.HideAllNonExclusiveFullscreenWindows;
            newGame.HideMouseCursorInGame = selectedGame.HideMouseCursorInGame;
            newGame.MaxPlayers = selectedGame.MaxPlayers;
            newGame.OverrideDefaultStartupScreenSettings = selectedGame.OverrideDefaultStartupScreenSettings;
            newGame.PlayMode = selectedGame.PlayMode;
            newGame.Portable = selectedGame.Portable;
            newGame.Publisher = selectedGame.Publisher;
            newGame.Region = selectedGame.Region;
            newGame.ReleaseDate = selectedGame.ReleaseDate;
            newGame.ReleaseType = selectedGame.ReleaseType;
            newGame.ReleaseYear = selectedGame.ReleaseYear;
            newGame.RootFolder = selectedGame.RootFolder;
            newGame.Series = selectedGame.Series;
            newGame.StartupLoadDelay = selectedGame.StartupLoadDelay;
            newGame.UseStartupScreen = selectedGame.UseStartupScreen;

            string cParams = @"a C:\Users\Adam\Documents\Final Fantasy Tactics - The War of the Lions (USA)1.iso C:\Users\Adam\Documents\wolt usa fast fix.ppf";
            string filename = Path.Combine(@"C:\Users\Adam\Downloads\pdx-ppf3\ppfbin\applyppf\w32\ApplyPPF3.exe");
            var proc = System.Diagnostics.Process.Start(filename, cParams);
            
            PluginHelper.DataManager.Save(true);
            PluginHelper.DataManager.ReloadIfNeeded();
            PluginHelper.LaunchBoxMainViewModel.RefreshData();
             */
          
            // PluginHelper.DataManager.ReloadIfNeeded();
            // PluginHelper.DataManager.ForceReload();            
            PluginHelper.LaunchBoxMainViewModel.RefreshData();
        }

        public void OnSelected(IGame[] selectedGames)
        {
            throw new NotImplementedException();
        }

        public string ResourceFolder
        {
            get
            {
                return "pack://application:,,,/LaunchBoxRomPatcher;component";
            }
        }

    }
}
