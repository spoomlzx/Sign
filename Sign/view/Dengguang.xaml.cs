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
        private SqliteService ss = new SqliteService();

        private List<Baowen> listB = new List<Baowen>();

        //串口通信接口
        private SerialPort sp = new SerialPort();

        public Dengguang()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(Timer_Tick);
            double ml = sliderMl.Value;
            timer.Interval = TimeSpan.FromMilliseconds(5000 / ml);
            InitPort();
        }

        private void InitPort()
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                combox_com.Items.Add(port);
            }
            combox_com.SelectedIndex = ports.Length - 1;
        }

        private void btn_opencom_Click(object sender, RoutedEventArgs e)
        {
            if (combox_com.Items.Count == 0)
            {
                InitPort();
            }
            else
            {
                sp.Close();
                sp.PortName = combox_com.SelectedValue.ToString();
                sp.BaudRate = 9600;
                sp.DataBits = 8;
                sp.StopBits = StopBits.One;
                sp.Parity = Parity.None;
                if (!sp.IsOpen)
                {
                    try
                    {
                        sp.Open();
                        btn_opencom.Content = "灯光已打开";
                        output = true;
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("错误：" + ex.Message);
                    }
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
            this.listB = this.ss.getListBaowen(10);
            listBox_bw.DataContext = this.listB;
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


        private void Shezhibaowen_Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string tag = button.Tag.ToString();
            textBox_pinyin.Text = tag;
            Translate trans = new Translate();
            string sign = trans.PinyinToS(tag);
            signIn = sign + "2";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string baowen = input_baowen.Text;
            string qianming = input_qianming.Text;
            baowen = baowen.Replace("/", "__");
            string pattern = "\\W+";
            Regex rgx = new Regex(pattern);
            string result = rgx.Replace(baowen, " ");
            result = result.Replace("__", "/");
            Translate trans = new Translate();
            textBox_pinyin.Text = trans.ChineseToChar(result);

            signIn = trans.PinyinToS(trans.ChineseToChar(result) + " @" + qianming) + "2";
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox = e.Source as TextBox;
                //MessageBox.Show("enter");
                int id = Convert.ToInt32(textBox.Text);
                Baowen baowen = this.ss.getBaowen(id);
                baowen.Bw = baowen.Bw.Replace(" ", "");
                ContentPresenter cp = textBox.TemplatedParent as ContentPresenter;
                Border bd = (Border)cp.Parent;
                //只有修改listboxitem的content以后才能更新控件
                ListBoxItem item = (ListBoxItem)bd.TemplatedParent;
                //list.ItemContainerGenerator
                int index = listBox_bw.ItemContainerGenerator.IndexFromContainer(item);
                if (baowen.Id != 0)
                {
                    this.listB[index] = baowen;
                    item.Content = baowen;
                }
            }
        }

        private void ListBoxItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ListBoxItem lbi = (ListBoxItem)sender;
            EditWindow ew = new EditWindow(lbi);
            Baowen bw = (Baowen)lbi.Content;
            ew.Title = "修改报文： " + bw.Id;
            ew.Closed += new EventHandler(EditWindow_Closed);
            ew.Show();
        }

        private void EditWindow_Closed(object sender, EventArgs e)
        {
            EditWindow ew = (EditWindow)sender;
            ListBoxItem lbi = (ListBoxItem)ew.lbi;
            Baowen bw = (Baowen)lbi.Content;
            Baowen updateBaowen = this.ss.getBaowen(bw.Id);
            updateBaowen.Bw = updateBaowen.Bw.Replace(" ", "");
            lbi.Content = updateBaowen;
        }

        private void btnOutput_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFile1 = new System.Windows.Forms.SaveFileDialog();
            saveFile1.Filter = "文本文件(.txt)|*.txt";
            saveFile1.FilterIndex = 1;
            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFile1.FileName.Length > 0)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFile1.FileName, false);
                try
                {
                    sw.WriteLine("当前10条报文："); //只要这里改一下要输出的内容就可以了s
                    sw.WriteLine("");
                    foreach (Baowen bw in listB)
                    {
                        string temp_bw = bw.Bw.Replace(" ", "");
                        sw.WriteLine(bw.Id + "." + bw.Bw+"-------"+bw.Qianming);
                        sw.WriteLine("  " + bw.Pinyin);
                        sw.WriteLine("");
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    sw.Close();
                }
            }
        }

    }
}
