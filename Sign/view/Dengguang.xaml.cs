using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Sign
{
    /// <summary>
    /// Dengguang.xaml 的交互逻辑
    /// </summary>
    public partial class Dengguang : UserControl
    {
        private int position = 0;
        private DispatcherTimer timer = new DispatcherTimer();
        private string signIn = string.Empty;
        public Dengguang()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(Timer_Tick);
            double ml = sliderMl.Value;
            timer.Interval = TimeSpan.FromMilliseconds(5000/ml);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //逐个读取信号，转译成电键信号
            if (signIn[position].Equals('1'))
            {
                Img_dg.Source = new BitmapImage(new Uri("../image/shine.png", UriKind.Relative));
            }
            else if (signIn[position].Equals('2'))
            {
                btnStart.Content = "开始收报";
                timer.Stop();
                Img_dg.Source = new BitmapImage(new Uri("../image/slake.png", UriKind.Relative));
            }
            else
            {
                Img_dg.Source = new BitmapImage(new Uri("../image/slake.png", UriKind.Relative));
            }
            position++;
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            string chinese = messageIn.Text;
            Translate trans = new Translate();
            textBox.Text = trans.ChineseToS(chinese);
            signIn = textBox.Text+"2";
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            //textBlock.Text =Convert.ToString( m.Length);
            if (timer.IsEnabled)
            {
                timer.Stop();
                Img_dg.Source = new BitmapImage(new Uri("../image/slake.png", UriKind.Relative));
                //将报文位置重置到报头
                btn.Content = "开始收报";
            }
            else
            {
                position = 0;
                timer.Start();
                btn.Content = "停止收报";
            }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            Img_dg.Source = new BitmapImage(new Uri("../image/shine.png", UriKind.Relative));
        }

        private void sliderMl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double ml = sliderMl.Value;
            timer.Interval = TimeSpan.FromMilliseconds(5000 / ml);
        }
    }
}
