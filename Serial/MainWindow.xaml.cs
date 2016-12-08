using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Serial
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Baowen a = new Baowen("a","a","a");
            Baowen b = new Baowen("b", "b", "a");
            Baowen c = new Baowen("c", "c", "a");
            List<Baowen> listB = new List<Baowen>();
            listB.Add(a);
            listB.Add(b);
            listB.Add(c);
            listBox.DataContext = listB;
        }
    }

    class Baowen
    {
        private int id;
        private string bw;
        private string pinyin;
        private string qianming;

        public Baowen()
        {
            this.bw = null;
            this.pinyin = null;
            this.qianming = null;
        }

        public Baowen(string bw, string pinyin, string qianming)
        {
            this.bw = bw;
            this.pinyin = pinyin;
            this.qianming = qianming;
        }


        public string Sign
        {
            get
            {
                string temp = pinyin.Replace('\'', '@');
                temp = temp.Replace('_', '[');
                temp = temp + " @ " + qianming;
                return temp;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }


        public string Pinyin
        {
            get
            {
                return pinyin;
            }

            set
            {
                string temp = value.Replace("/", "__");
                string pattern = "\\W+";
                Regex rgx = new Regex(pattern);
                temp = rgx.Replace(value, " ");
                pinyin = temp.Replace("__", "/");
            }
        }

        public string Qianming
        {
            get
            {
                return qianming;
            }

            set
            {
                qianming = value;
            }
        }

        public string Bw
        {
            get
            {
                return bw;
            }

            set
            {
                bw = value;
            }
        }
    }
}
