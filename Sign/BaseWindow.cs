using Microsoft.Windows.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Sign
{
    public class BaseWindow : Window
    {
        public BaseWindow()
        {
            SetupCommandBindings();
        }
        private void SetupCommandBindings()
        {
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnSystemCommandCloseWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnSystemCommandMinimizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnSystemCommandMaximizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnSystemCommandRestoreWindow));
        }

        private void OnSystemCommandCloseWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void OnSystemCommandMinimizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnSystemCommandMaximizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void OnSystemCommandRestoreWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }
    }
}
