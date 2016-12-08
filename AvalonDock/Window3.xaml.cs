using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AvalonDock
{
    /// <summary>
    /// Window3.xaml 的交互逻辑
    /// </summary>
    public partial class Window3 : Window
    {

        private ObservableCollection<Warship> warships = new ObservableCollection<Warship>();
        public Window3()
        {
            InitializeComponent();
        }

        private void button_turnRight_Click(object sender, RoutedEventArgs e)
        {
            Storyboard _storyboard = new Storyboard();
            Storyboard _xstoryboard = new Storyboard();
            Storyboard _ystoryboard = new Storyboard();

            TimeSpan span = TimeSpan.FromSeconds(0.1);
            for(int i = 0; i < 10; i++)
            {
                DoubleAnimation _animation = new DoubleAnimation();
                Storyboard.SetTarget(_animation, ws);
                Storyboard.SetTargetProperty(_animation, new PropertyPath("Course"));
                _animation.Duration = span;
                _animation.To = ws.Course - 9.0*(i+1);
                _animation.BeginTime = TimeSpan.FromSeconds(0.1 * i);
                _storyboard.Children.Add(_animation);

                double angle = Math.PI * (i+1)*9 / 180.0;
                double x = Math.Sin(angle) * 10.0;
                double y = Math.Cos(angle) * 10.0;

                DoubleAnimation _xanimation = new DoubleAnimation();
                Storyboard.SetTarget(_xanimation, ws);
                Storyboard.SetTargetProperty(_xanimation, new PropertyPath("(Canvas.Left)"));
                _xanimation.Duration = span;

                DoubleAnimation _yanimation = new DoubleAnimation();
                Storyboard.SetTarget(_yanimation, ws);
                Storyboard.SetTargetProperty(_yanimation, new PropertyPath("(Canvas.Bottom)"));
                _yanimation.Duration = span;

                _xanimation.To = Canvas.GetLeft(ws) + x;
                _yanimation.To = Canvas.GetBottom(ws) + y;
                _xanimation.BeginTime = TimeSpan.FromSeconds(0.1 * i);
                _yanimation.BeginTime = TimeSpan.FromSeconds(0.1 * i);

                _xstoryboard.Children.Add(_xanimation);
                _ystoryboard.Children.Add(_yanimation);
            }
            _storyboard.Begin();
            _xstoryboard.Begin();
            _ystoryboard.Begin();
        }
    }
}
