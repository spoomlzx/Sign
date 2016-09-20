using System;
using System.Collections.Generic;
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

namespace Sign
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : BaseWindow
    {
        private Qihao qihao;
        private Shouqi shouqi;
        private Dengguang denggguang;
        private Lilun lilun;
        private Morse shoubao;
        private Biandui biandui;
        private AddMessage addMessage;


        public MainWindow()
        {
            InitializeComponent();
            dengguangxunlian.IsSelected = true;
        }

        private void qihaoxunlian_Selected(object sender, RoutedEventArgs e)
        {
            root.Children.Clear();
            if (qihao == null)
            {
                qihao = new Qihao();
            }            
            root.Children.Add(qihao);
        }

        private void shouqixunlian_Selected(object sender, RoutedEventArgs e)
        {
            root.Children.Clear();
            if (shouqi == null)
            {
                shouqi = new Shouqi();
            }            
            root.Children.Add(shouqi);
        }

        private void dengguangxunlian_Selected(object sender, RoutedEventArgs e)
        {
            root.Children.Clear();
            if (denggguang==null)
            {
                denggguang = new Dengguang();
            }
            
            root.Children.Add(denggguang);
        }

        private void lilunzhishi_Selected(object sender, RoutedEventArgs e)
        {
            root.Children.Clear();
            if (lilun == null)
            {
                lilun = new Lilun();
            }            
            root.Children.Add(lilun);
        }

        private void shumashoubao_Selected(object sender, RoutedEventArgs e)
        {
            root.Children.Clear();
            if (shoubao == null)
            {
                shoubao = new Morse();
            }            
            root.Children.Add(shoubao);
        }

        private void bianduiyundong_Selected(object sender, RoutedEventArgs e)
        {
            root.Children.Clear();
            if (biandui == null)
            {
                biandui = new Biandui();
            }            
            root.Children.Add(biandui);
        }

        private void addmessage_Selected(object sender, RoutedEventArgs e)
        {
            root.Children.Clear();
            if (addMessage == null)
            {
                addMessage = new AddMessage();
            }
            root.Children.Add(addMessage);
        }
    }


}
