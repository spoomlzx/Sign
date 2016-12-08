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
using System.Windows.Shapes;

namespace Sign
{
    /// <summary>
    /// EditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditWindow : Window
    {
        public ListBoxItem lbi = new ListBoxItem();
        private SqliteService ss = new SqliteService();
        private int id = 0;

        public EditWindow(ListBoxItem lbi)
        {
            InitializeComponent();
            Baowen bw = (Baowen)lbi.Content;
            this.lbi = lbi;
            this.id = bw.Id;
            Baowen bw_query = this.ss.getBaowen(this.id);
            input_bw.Text = bw_query.Bw;
            input_pinyin.Text = bw_query.Pinyin;
            input_qianming.Text = bw_query.Qianming;
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            string bw = input_bw.Text;
            string pinyin = input_pinyin.Text;
            string qianming = input_qianming.Text;
            Baowen baowen = new Baowen(bw, pinyin, qianming);
            baowen.Id = this.id;
            this.ss.UpdateBaowen(baowen);
            update_info.Text = "id " + this.id + " updated!";
        }
    }
}
