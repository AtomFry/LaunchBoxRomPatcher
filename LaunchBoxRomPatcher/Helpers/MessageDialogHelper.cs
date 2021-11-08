using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace LaunchBoxRomPatcher.Helpers
{
    public class MessageDialogHelper
    {
        public static MessageDialogResult ShowOKCancelDialog(string text, string title)
        {
            MessageBoxResult result = MessageBox.Show(text, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK
                ? MessageDialogResult.OK
                : MessageDialogResult.Cancel;
        }
    }

    public enum MessageDialogResult { OK, Cancel}
}
