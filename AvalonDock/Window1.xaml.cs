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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AvalonDock
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        private List<EllipseInfo> ellipses = new List<EllipseInfo>();
        private double accelerationY = 0.1;
        private int minStartSpeed = 1;
        private int maxStartSpeed = 50;
        private double ratio = 0.1;
        private int minEllipse = 5;
        private int maxEllipse = 40;
        private int ellipseRadius = 10;
        private bool rendered = false;

        public Window1()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (!rendered)
            {
                ellipses.Clear();
                canvas.Children.Clear();
                CompositionTarget.Rendering += RenderFrame;
                rendered = true;
            }
        }

        private void RenderFrame(object sender, EventArgs e)
        {
            if (ellipses.Count == 0)
            {
                int halfCanvasWidth = (int)canvas.ActualWidth / 2;
                Random rand = new Random();
                int ellipseCount = rand.Next(minEllipse, maxEllipse + 1);
                for(int i = 0; i < ellipseCount; i++)
                {
                    Ellipse ellipse = new Ellipse();
                    ellipse.Fill = Brushes.GreenYellow;
                    ellipse.Width = ellipseRadius;
                    ellipse.Height = ellipseRadius;

                    Canvas.SetLeft(ellipse,halfCanvasWidth + rand.Next(-halfCanvasWidth, halfCanvasWidth));
                    Canvas.SetTop(ellipse, 2);
                    canvas.Children.Add(ellipse);

                    EllipseInfo info = new EllipseInfo(ellipse, ratio*rand.Next(minStartSpeed, maxStartSpeed));
                    ellipses.Add(info);
                }
            }
            else
            {
                for(int i = ellipses.Count - 1; i >= 0; i--)
                {
                    EllipseInfo info = ellipses[i];
                    double top = Canvas.GetTop(info.Ellipse);
                    Canvas.SetTop(info.Ellipse, top + 1 * info.VelocityY);
                    if (top >= canvas.ActualHeight - ellipseRadius * 2)
                    {
                        ellipses.Remove(info);
                    }
                    else
                    {
                        info.VelocityY += accelerationY;
                    }
                }
                if (ellipses.Count == 0)
                {
                    CompositionTarget.Rendering -= RenderFrame;
                    rendered = false;
                }
            }
        }
    }

    public class EllipseInfo
    {
        public Ellipse Ellipse { get; set; }
        public double VelocityY { get; set; }
        public EllipseInfo(Ellipse ellipse,double velocityY)
        {
            VelocityY = velocityY;
            Ellipse = ellipse;
        }
    }
}
