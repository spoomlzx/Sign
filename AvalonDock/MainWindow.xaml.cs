using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AvalonDock
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.To = button.Width + 50;
            widthAnimation.Duration = TimeSpan.FromSeconds(3);
            button.BeginAnimation(Button.WidthProperty, widthAnimation);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation heightAnimation = new DoubleAnimation();
            heightAnimation.To = button.Height + 50;
            heightAnimation.Duration = TimeSpan.FromSeconds(3);
            button.BeginAnimation(Button.HeightProperty, heightAnimation);
        }

        private void Storyboard_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            Clock storyboardClock = (Clock)sender;
            if (storyboardClock.CurrentProgress == null)
            {
                txtTime.Text = "[[Stop]]";
                proTime.Value = 0;
            }
            else
            {
                txtTime.Text = storyboardClock.CurrentTime.ToString();
                proTime.Value = (double)storyboardClock.CurrentProgress;
            }
        }
    }
    
}
