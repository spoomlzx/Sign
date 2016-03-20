using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Morse.xaml 的交互逻辑
    /// </summary>
    public partial class Morse : UserControl
    {
        string[] morse = { "111", "10111", "1010111", "1010101110111", "10101010111", "101010101", "11101010101", "1110111010101", "1110101", "11101" };
        private string m = string.Empty;
        private MediaPlayer player = new MediaPlayer();
        private int position = 0;
        private DispatcherTimer timer = new DispatcherTimer();
        public Morse()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(Timer_Tick);
        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            //textBlock.Text =Convert.ToString( m.Length);
            if (timer.IsEnabled)
            {
                timer.Stop();
                player.Stop();
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

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            m = string.Empty;
            m += "111010101011100011101010101110001110101010111";//开始报文
            position = 0;

            //将报文位置重置到报头，并重置播放器状态
            btnStart.Content = "开始收报";
            btnStart.IsEnabled = true;
            //初始化播放器
            if (player.HasAudio==false)
            {
                player.Open(new Uri("112.wav", UriKind.Relative));
                player.Volume = 0.5;
            }
            player.Stop();
            //重置计时器
            int ml = Convert.ToInt16(textBoxMl.Text);
            timer.Interval = TimeSpan.FromMilliseconds(5000 / ml);
            timer.Stop();
            //生成报文
            ObservableCollection<Baodi> baodiData = new ObservableCollection<Baodi>();
            for (int i = 0; i < 10; i++)
            {
                Baodi temp = new Baodi();
                temp.S1 = randomString(System.DateTime.Now.Second * (i + 1));
                temp.S2 = randomString(Convert.ToInt16(temp.S1));
                temp.S3 = randomString(Convert.ToInt16(temp.S2));
                temp.S4 = randomString(Convert.ToInt16(temp.S3));
                temp.S5 = randomString(Convert.ToInt16(temp.S4));
                temp.S6 = randomString(Convert.ToInt16(temp.S5));
                temp.S7 = randomString(Convert.ToInt16(temp.S6));
                temp.S8 = randomString(Convert.ToInt16(temp.S7));
                temp.S9 = randomString(Convert.ToInt16(temp.S8));
                temp.S10 = randomString(Convert.ToInt16(temp.S9));
                baodiData.Add(temp);
            }
            m += "0000011101011101011100000";//结束报文
            m += "2";//结束符号
            dataGrid.DataContext = baodiData;
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (timer.IsEnabled)
            {
                timer.Stop();
                player.Stop();
                btn.Content = "继续收报";
            }
            else
            {
                timer.Start();
                btn.Content = "暂停收报";
            }
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            player.Volume = (double)slider.Value / 100;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //逐个读取信号，转译成电键信号
            if (m[position].Equals('1'))
            {
                player.Play();
            }
            else if (m[position].Equals('2'))
            {
                btnStart.Content = "开始收报";
                timer.Stop();
                player.Stop();
            }
            else
            {
                player.Stop();
            }
            position++;
        }

        //生成随机4位数字，并添加时间间隔
        private string randomString(int seed)
        {
            Random rd = new Random((int)System.DateTime.Now.Second* (int)System.DateTime.Now.Millisecond * seed);
            string result = string.Empty;

            m += "0000";
            int pos = -1;
            int prepos = -1;
            for (int i = 0; i < 4; i++)
            {
                pos = rd.Next(0, 9);
                if (pos == prepos)//避免连码报文
                {
                    pos = rd.Next(0, 9);
                }
                if (pos == prepos)//避免连码报文
                {
                    pos = rd.Next(0, 9);
                }
                m += "000";
                m += morse[pos];
                result += Convert.ToString(pos);
                prepos = pos;
            }
            return result;
        }
    }
    public class Baodi
    {
        public string S1 { get; set; }
        public string S2 { get; set; }
        public string S3 { get; set; }
        public string S4 { get; set; }
        public string S5 { get; set; }
        public string S6 { get; set; }
        public string S7 { get; set; }
        public string S8 { get; set; }
        public string S9 { get; set; }
        public string S10 { get; set; }
    }
}
