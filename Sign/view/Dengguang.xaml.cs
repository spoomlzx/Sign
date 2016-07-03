using System;
using System.Collections.Generic;
using System.IO.Ports;
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
        private bool output = false;

        //串口通信接口
        private SerialPort sp = new SerialPort();
        public Dengguang()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(Timer_Tick);
            double ml = sliderMl.Value;
            timer.Interval = TimeSpan.FromMilliseconds(5000/ml);
            //InitPort();
        }

        private void InitPort()
        {
            sp.Close();
            sp.PortName = "COM12";
            sp.BaudRate = 9600;
            sp.DataBits = 8;
            sp.StopBits = StopBits.One;
            sp.Parity = Parity.None;
            if (!sp.IsOpen)
            {
                try
                {
                    sp.Open();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("错误：" + ex.Message);
                }
            }
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            byte[] s1 = new byte[5];
            s1[0] = 0xAF;
            s1[1] = 0xFF;
            s1[2] = 0x01;
            s1[3] = 0x01;
            s1[4] = 0xDF;
            byte[] s0 = new byte[5];
            s0[0] = 0xAF;
            s0[1] = 0xFF;
            s0[2] = 0x02;
            s0[3] = 0x02;
            s0[4] = 0xDF;
            //逐个读取信号，转译成电键信号
            if (signIn[position].Equals('1'))
            {
                Img_dg.Source = new BitmapImage(new Uri("../image/shine.png", UriKind.Relative));
                if (output)
                {
                    sp.Write(s1, 0, 5);
                }
                
            }
            else if (signIn[position].Equals('2'))
            {
                btnStart.Content = "开始收报";
                timer.Stop();
                Img_dg.Source = new BitmapImage(new Uri("../image/slake.png", UriKind.Relative));
                if (output)
                {
                    sp.Write(s0, 0, 5);
                }
            }
            else
            {
                Img_dg.Source = new BitmapImage(new Uri("../image/slake.png", UriKind.Relative));
                if (output)
                {
                    sp.Write(s0, 0, 5);
                }
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

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            InitPort();
            output = true;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            output = false;
        }
    }
}
