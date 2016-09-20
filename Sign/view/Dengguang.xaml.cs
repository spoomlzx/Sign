using Sign.util;
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
using System.Text.RegularExpressions;

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
            timer.Interval = TimeSpan.FromMilliseconds(5000 / ml);

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
            if (output)
            {
                sp.Write(s0, 0, 5);
            }
            if (signIn[position].Equals('1'))
            {
                Img_dg.Source = new BitmapImage(new Uri("../image/liang.png", UriKind.Relative));
                if (output)
                {
                    sp.Write(s1, 0, 5);
                }

            }
            else if (signIn[position].Equals('2'))
            {
                btnStart.Content = "开始收报";
                timer.Stop();
                Img_dg.Source = new BitmapImage(new Uri("../image/an.png", UriKind.Relative));
                if (output)
                {
                    sp.Write(s0, 0, 5);
                }
            }
            else
            {
                Img_dg.Source = new BitmapImage(new Uri("../image/an.png", UriKind.Relative));
                if (output)
                {
                    sp.Write(s0, 0, 5);
                }
            }
            position++;
        }
        /// <summary>
        /// 任选10句报文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            SqliteService ss = new SqliteService();
            List<Baowen> listB = ss.getListBaowen(10);
            listBox_bw.ItemsSource = listB;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            //textBlock.Text =Convert.ToString( m.Length);
            if (timer.IsEnabled)
            {
                timer.Stop();
                Img_dg.Source = new BitmapImage(new Uri("../image/an.png", UriKind.Relative));
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
        /// <summary>
        /// 调节发报速度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sliderMl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double ml = sliderMl.Value;
            timer.Interval = TimeSpan.FromMilliseconds(5000 / ml);
        }

        /// <summary>
        /// 选择是否外接灯泡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            InitPort();
            output = true;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            output = false;
        }

        private void Shezhibaowen_Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            string tag = button.Tag.ToString();
            textBox1.Text = tag;
            Translate trans = new Translate();
            string sign = trans.PinyinToS(tag);
            textBox.Text = sign;
            signIn = sign + "2";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string baowen = input_baowen.Text;
            string qianming = input_qianming.Text;

            string pattern = "\\W+";
            Regex rgx = new Regex(pattern);
            string result = rgx.Replace(baowen, " ");
            textBox1.Text = result;


            Translate trans = new Translate();
            textBox.Text = trans.ChineseToS(result + " @ " + qianming);
            signIn = textBox.Text + "2";
        }
    }
}
