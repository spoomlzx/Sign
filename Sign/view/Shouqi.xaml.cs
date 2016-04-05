using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using Sign.util;

namespace Sign
{
    /// <summary>
    /// Shouqi.xaml 的交互逻辑
    /// </summary>
    public partial class Shouqi : UserControl
    {
        private SqliteHelper sh = null;
        
        public Shouqi()
        {
            InitializeComponent();
            sh = new SqliteHelper();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=db.db"))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        SQLiteHelper sh = new SQLiteHelper(cmd);
                        sh.Execute(textBox.Text);
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=db.db"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;

                    SQLiteHelper sh = new SQLiteHelper(cmd);

                    try
                    {
                        DataTable dt = sh.Select(textBox.Text);
                        DataSet ds = sh.SelectSet(textBox.Text);
                        dataGrid.ItemsSource = ds.Tables[0].DefaultView;
                    }
                    catch (Exception ex)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Error");
                        dt.Rows.Add(ex.ToString());
                        dataGrid.ItemsSource = dt.DefaultView;
                    }

                    conn.Close();
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string name = "B";
            int id = 2;
            var cmdparams = new List<SQLiteParameter>()
            {
                new SQLiteParameter("name",name),
                new SQLiteParameter("id",id)
            };
            string sql = "select * from flag where name=@name and id=@id;";
            DataTable dt = sh.Select(sql,cmdparams);
            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = sh.GetSchema();
            dataGrid.ItemsSource = dt.DefaultView;
        }
    }
}
