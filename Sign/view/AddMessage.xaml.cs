using Sign.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
        private SqliteService ss = new SqliteService();
        private int currentId = -1;
        public AddMessage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 中文输入框失去焦点时，自动翻译为拼音
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void input_bw_LostFocus(object sender, RoutedEventArgs e)
        {
            string str = input_bw.Text;
            Translate trans = new Translate();
            string sstr = trans.ChineseToChar(str);
            input_pinyin.Text = sstr;
            input_qianming.Text = trans.getString(3);
        }
        /// <summary>
        /// 查询单条报文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_query_Click(object sender, RoutedEventArgs e)
        {
            if (input_id.Text != string.Empty)
            {
                int id = Convert.ToInt32(input_id.Text);
                Baowen baowen = this.ss.getBaowen(id);
                input_bw.Text = baowen.Bw;
                input_pinyin.Text = baowen.Pinyin;
                input_qianming.Text = baowen.Qianming;

                this.currentId = id;
                btn_update.IsEnabled = true;
            }
            else
            {
                btn_update.IsEnabled = false;
            }
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            string id = input_id.Text;
            string bw = input_bw.Text;
            string pinyin = input_pinyin.Text;
            string qianming = input_qianming.Text;
            Baowen baowen = new Baowen(bw, pinyin, qianming);
            int newid = this.ss.InsertBaowen(baowen);
            if (newid != -1)
            {
                input_id.Text = Convert.ToString(newid);
            }
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            string bw = input_bw.Text;
            string pinyin = input_pinyin.Text;
            string qianming = input_qianming.Text;
            Baowen baowen = new Baowen(bw, pinyin, qianming);
            baowen.Id = this.currentId;
            this.ss.UpdateBaowen(baowen);
            update_info.Text = "id " + this.currentId + " updated!";
        }

        private void btn_pre_Click(object sender, RoutedEventArgs e)
        {
            Baowen baowen = this.ss.getBaowen(this.currentId - 1);
            if (baowen != null)
            {
                this.currentId = baowen.Id;
                input_id.Text = Convert.ToString(baowen.Id);
                input_bw.Text = baowen.Bw;
                input_pinyin.Text = baowen.Pinyin;
                input_qianming.Text = baowen.Qianming;
            }
        }

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            Baowen baowen = this.ss.getBaowen(this.currentId + 1);
            if (baowen != null)
            {
                this.currentId = baowen.Id;
                input_id.Text = Convert.ToString(baowen.Id);
                input_bw.Text = baowen.Bw;
                input_pinyin.Text = baowen.Pinyin;
                input_qianming.Text = baowen.Qianming;
            }
        }

        private void btn_ouput_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFile1 = new System.Windows.Forms.SaveFileDialog();
            saveFile1.Filter = "文本文件(.txt)|*.txt";
            saveFile1.FilterIndex = 1;
            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFile1.FileName.Length > 0)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFile1.FileName, false);
                try
                {
                    sw.WriteLine("所有报底："); //只要这里改一下要输出的内容就可以了s
                    var sql = "select * from baowen order by id;";
                    SqliteHelper sh = new SqliteHelper();
                    DataTable dt = sh.Select(sql);
                    foreach (DataRow dr in dt.Rows)
                    {
                        //Situation s = new Situation();
                        //Baowen b = new Baowen();
                        //b.Id = int.Parse(dr["id"].ToString());
                        //b.Bw = dr["bw"].ToString();
                        //b.Pinyin = dr["pinyin"].ToString();
                        //b.Qianming = dr["qianming"].ToString();
                        string bw = dr["bw"].ToString();
                        bw = bw.Replace(" ", "");
                        sw.WriteLine(int.Parse(dr["id"].ToString()) + "."+bw);
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
