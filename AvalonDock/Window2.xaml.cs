using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AvalonDock
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {

        private DispatcherTimer timer = new DispatcherTimer();

        private double bombInterval = 1;
        private double bombDurationTime = 5;
        private int numBomb = 0;

        public Window2()
        {
            InitializeComponent();
        }

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RectangleGeometry rect = new RectangleGeometry();
            rect.Rect = new Rect(0, 0, canvas.ActualWidth, canvas.ActualHeight);
            canvas.Clip = rect;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(ticker);
            timer.Interval = TimeSpan.FromSeconds(bombInterval);
            timer.Start();
        }

        private void ticker(object sender, EventArgs e)
        {
            Bomb bomb = new Bomb();
            bomb.isFalling = true;
            Random rand = new Random();
            bomb.SetValue(Canvas.LeftProperty, (double)(rand.Next(0, (int)(canvas.ActualWidth - 30))));
            bomb.SetValue(Canvas.TopProperty, 0.0);
            canvas.Children.Add(bomb);

            bomb.MouseLeftButtonDown += Bomb_MouseLeftButtonDown;

            Storyboard storyboard = new Storyboard();
            DoubleAnimation downAnim = new DoubleAnimation();
            downAnim.To = canvas.ActualHeight;
            downAnim.Duration = TimeSpan.FromSeconds(bombDurationTime);
            Storyboard.SetTarget(downAnim, bomb);
            Storyboard.SetTargetProperty(downAnim, new PropertyPath("(Canvas.Top)"));
            storyboard.Children.Add(downAnim);

            storyboard.Duration = downAnim.Duration;
            storyboard.Begin();

        }

        private void Bomb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Bomb bomb=(Bomb)sender;
            bomb.isFalling = false;
            canvas.Children.Remove(bomb);
            textBlock.Text = String.Format("Destroyed {0} bombs!", ++numBomb);
        }
    }
}
