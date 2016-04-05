using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Metro
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ObservableCollection<Baodi> baodiData = new ObservableCollection<Baodi>();
            for (int i = 0; i < 15; i++)
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
            listView.DataContext = baodiData;
            
        }

        private string randomString(int seed)
        {
            Random rd = new Random((int)System.DateTime.Now.Second * (int)System.DateTime.Now.Millisecond * seed);
            string result = string.Empty;
            int pos = -1;
            int prepos = -1;
            for (int i = 0; i < 4; i++)
            {
                pos = rd.Next(0, 9);
                if (pos == prepos)//避免连码报文
                {
                    pos = rd.Next(0, 9);
                }
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

    public class ToggleButtonIcon : ToggleButton
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(ToggleButtonIcon));
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
    }
    public class ExpanderIcon : Expander
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(ExpanderIcon));
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
    }
}
