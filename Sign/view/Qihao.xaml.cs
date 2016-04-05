using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Sign.util;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;

namespace Sign
{
    /// <summary>
    /// Qihao.xaml 的交互逻辑
    /// </summary>
    public partial class Qihao : UserControl
    {
        public Qihao()
        {
            InitializeComponent();
            //BitmapImage bimg = new BitmapImage(new Uri("../image/qizhi/C.png", UriKind.Relative));

            InitFlag();
        }

        /// <summary>
        /// 初始化popup中的旗子
        /// </summary>
        private void InitFlag()
        {
            //初始化字母旗
            string charlist = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 1; i < 27; i++)
            {
                BitmapImage bimg = new BitmapImage(new Uri("../image/flag/char/" + charlist[i] + ".png", UriKind.Relative));
                Image img = new Image() { Height = 40, Margin = new Thickness(10, 5, 5, 5) };
                img.Name = Convert.ToString(charlist[i]);
                img.Source = bimg;
                img.MouseEnter += new MouseEventHandler(img_MouseEnter);
                img.MouseLeave += new MouseEventHandler(img_MouseLeave);
                img.MouseDown += new MouseButtonEventHandler(source_MouseDown);
                wrapChar.Children.Add(img);
            }
            //初始化数字旗
            for(int i = 1; i < 11; i++)
            {
                BitmapImage bimg = new BitmapImage(new Uri("../image/flag/num/" + i + ".png", UriKind.Relative));
                Image img = new Image() { Height = 40, Margin = new Thickness(10, 5, 5, 5) };
                img.Name = "n" + Convert.ToString(i);
                img.Source = bimg;
                img.MouseEnter += new MouseEventHandler(img_MouseEnter);
                img.MouseLeave += new MouseEventHandler(img_MouseLeave);
                img.MouseDown += new MouseButtonEventHandler(source_MouseDown);
                wrapNum.Children.Add(img);
            }
            //初始化其他旗
            for (int i = 0; i < 10; i++)
            {
                BitmapImage bimg = new BitmapImage(new Uri("../image/flag/other/" + i + ".png", UriKind.Relative));
                Image img = new Image() { Height = 40, Margin = new Thickness(10, 5, 5, 5) };
                img.Name = "num" + Convert.ToString(i);
                img.Source = bimg;
                img.MouseEnter += new MouseEventHandler(img_MouseEnter);
                img.MouseLeave += new MouseEventHandler(img_MouseLeave);
                img.MouseDown += new MouseButtonEventHandler(source_MouseDown);
                wrapOther.Children.Add(img);
            }

        }

        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            joinPopup.IsOpen = false;
        }

        private void img_MouseEnter(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            SqliteService ss = new SqliteService();
            Flag flag = ss.GetFlagByName(img.Name);
            joinPopup.PlacementTarget = img;
            joinPopup.PopupAnimation = System.Windows.Controls.Primitives.PopupAnimation.Slide;
            popupTbName.Text = img.Name + "  " + flag.substitute;
            popupTbMeaning.Text = flag.meaning;
            joinPopup.IsOpen = true;
        }

        private void WrapPanel_Drop(object sender, DragEventArgs e)
        {
            IDataObject data = new DataObject();
            data = e.Data;
            if (data.GetDataPresent(typeof(Image)))
            {
                Image img = new Image() { Width = 100, Height = 60, Margin = new Thickness(0, 10, 0, 10) };
                Image temp = data.GetData(typeof(Image)) as Image;
                img.Source = temp.Source;
                ((WrapPanel)sender).Children.Add(img);
            }
        }
        private void source_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DataObject data = new DataObject(typeof(Image), (Image)sender);
            DragDrop.DoDragDrop((Image)sender, data, DragDropEffects.Copy);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border bd = (Border)sender;
            if (bd.Name == "borderChar")
            {
                if (popupChar.IsOpen)
                {
                    popupChar.IsOpen = false;
                }
                else
                {
                    popupChar.IsOpen = true;
                    popupNum.IsOpen = false;
                    popupOther.IsOpen = false;
                }
            }
            else if(bd.Name == "borderNum")
            {
                if (popupNum.IsOpen)
                {
                    popupNum.IsOpen = false;
                }
                else
                {
                    popupChar.IsOpen = false;
                    popupNum.IsOpen = true;
                    popupOther.IsOpen = false;
                }
            }
            else
            {
                if (popupOther.IsOpen)
                {
                    popupOther.IsOpen = false;
                }
                else
                {
                    popupChar.IsOpen = false;
                    popupNum.IsOpen = false;
                    popupOther.IsOpen = true;
                }
            }
            
            
        }
    }
}
