using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using Unbroken.LaunchBox.Plugins;
using LaunchBoxRomPatcher.UserInterface;
using LaunchBoxRomPatcher.DataAccess;

namespace LaunchBoxRomPatcher
{
    class LaunchBoxRomPatcherConfig : ISystemMenuItemPlugin
    {
        public string Caption => "Manage ROM patchers";

        public Image IconImage => Properties.Resources.RomHackingIcon;

        public bool ShowInLaunchBox => true;

        public bool ShowInBigBox => false;

        public bool AllowInBigBoxWhenLocked => false;

        public void OnSelected()
        {
            // open the Manage ROM patchers window
            ManageRomPatchers manageRomPatchers = new ManageRomPatchers();
            manageRomPatchers.Show();
        }
    }
}
