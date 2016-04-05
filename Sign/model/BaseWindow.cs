using Microsoft.Windows.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sign
{
    public class BaseWindow : Window
    {
        public BaseWindow()
        {
            SetupCommandBindings();
            //InitCommand();
        }
        private void SetupCommandBindings()
        {
            CommandBindings.Add(new CommandBinding(Microsoft.Windows.Shell.SystemCommands.CloseWindowCommand, OnSystemCommandCloseWindow));
            CommandBindings.Add(new CommandBinding(Microsoft.Windows.Shell.SystemCommands.MinimizeWindowCommand, OnSystemCommandMinimizeWindow));
            CommandBindings.Add(new CommandBinding(Microsoft.Windows.Shell.SystemCommands.MaximizeWindowCommand, OnSystemCommandMaximizeWindow));
            CommandBindings.Add(new CommandBinding(Microsoft.Windows.Shell.SystemCommands.RestoreWindowCommand, OnSystemCommandRestoreWindow));
        }

        //private void OnWinMouseDoubleClick(object sender, ExecutedRoutedEventArgs e)
        //{
        //    try
        //    {
        //        //Grid ob = e.OriginalSource as Grid;
        //        //if (ob.Name == "CaptionGrid")
        //        //{
        //        //    BaseWindow bw = e.Source as BaseWindow;
        //        //    bw.WindowState = System.Windows.WindowState.Maximized;
        //        //}
        //        MessageBox.Show(Convert.ToString(e.OriginalSource.GetType()));
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        //public static RoutedCommand WinMaximizeCommand = new RoutedCommand("WinMaximizeCommand", typeof(BaseWindow));
        ////newCmdWinMove.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));
        //private void InitCommand()
        //{
        //    MouseGesture mg = new MouseGesture();
        //    mg.MouseAction = MouseAction.LeftClick;
        //    WinMaximizeCommand.InputGestures.Add(mg);
        //    CommandBindings.Add(new CommandBinding(WinMaximizeCommand, OnWinMouseDoubleClick));
        //}




        private void OnSystemCommandCloseWindow(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.CloseWindow(this);
        }

        private void OnSystemCommandMinimizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.MinimizeWindow(this);
        }

        private void OnSystemCommandMaximizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.MaximizeWindow(this);
        }

        private void OnSystemCommandRestoreWindow(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.RestoreWindow(this);
        }
    }
}
