using Sign.util;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sign
{
    /// <summary>
    /// AddMessage.xaml 的交互逻辑
    /// </summary>
    public partial class AddMessage : UserControl
    {
        public AddMessage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string id = input_id.Text;
            string bw = input_bw.Text;
            string pinyin = input_pinyin.Text;
            string qianming = input_qianming.Text;
            Baowen baowen = new Baowen(bw,pinyin,qianming);
            SqliteService ss = new SqliteService();
            int newid=ss.InsertBaowen(baowen);
            if (newid != -1)
            {
                input_id.Text = Convert.ToString(newid);
            }
        }
    }
}
