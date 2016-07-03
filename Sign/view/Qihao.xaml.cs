using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Sign.util;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace Sign
{
    /// <summary>
    /// Qihao.xaml 的交互逻辑
    /// </summary>
    public partial class Qihao : UserControl
    {
        private DispatcherTimer dispt = new DispatcherTimer();
        private int situationPosition = 0;
        private List<Situation> listS = null;
        public Qihao()
        {
            InitializeComponent();
            //BitmapImage bimg = new BitmapImage(new Uri("../image/qizhi/C.png", UriKind.Relative));

            //初始化弹出popup的计时器
            dispt.Interval = TimeSpan.FromMilliseconds(1000);
            dispt.Tick += new EventHandler(Timer_Tick);
            dispt.Stop();

            InitFlag();
            InitSituation();
        }

        private void InitSituation()
        {
            situationPosition = 0;
            SqliteService ss = new SqliteService();
            listS = ss.getListSituationByCategory("aa");
            grid_situation.DataContext = listS[situationPosition];
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            joinPopup.IsOpen = true;
            dispt.Stop();
        }

        /// <summary>
        /// 初始化popup中的旗子
        /// </summary>
        private void InitFlag()
        {
            for (int i = 1; i < 27; i++)
            {
                BitmapImage bimg = new BitmapImage(new Uri("../image/flag/char/" + i + ".png", UriKind.Relative));
                Image img = new Image() { Height = 40, Margin = new Thickness(10, 5, 5, 5) };
                img.Name = "id"+ Convert.ToString(i);
                img.Source = bimg;
                img.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.HighQuality);
                img.MouseEnter += new MouseEventHandler(img_MouseEnter);
                img.MouseLeave += new MouseEventHandler(img_MouseLeave);
                img.MouseDown += new MouseButtonEventHandler(source_MouseDown);
                wrapChar.Children.Add(img);
            }
            
            //初始化数字旗
            for (int i = 27; i < 37; i++)
            {
                BitmapImage bimg = new BitmapImage(new Uri("../image/flag/num/" + i + ".png", UriKind.Relative));
                Image img = new Image() {Height=40, Margin = new Thickness(10, 5, 5, 5) };
                img.Name = "id" + Convert.ToString(i);
                img.Source = bimg;
                img.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.HighQuality);
                img.MouseEnter += new MouseEventHandler(img_MouseEnter);
                img.MouseLeave += new MouseEventHandler(img_MouseLeave);
                img.MouseDown += new MouseButtonEventHandler(source_MouseDown);
                wrapNum.Children.Add(img);
            }
            //初始化其他旗
            for (int i = 37; i < 47; i++)
            {
                BitmapImage bimg = new BitmapImage(new Uri("../image/flag/other/" + i + ".png", UriKind.Relative));
                Image img = new Image() { Height = 40, Margin = new Thickness(10, 5, 5, 5) };
                img.Name = "id" + Convert.ToString(i);
                img.Source = bimg;
                img.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.HighQuality);
                img.MouseEnter += new MouseEventHandler(img_MouseEnter);
                img.MouseLeave += new MouseEventHandler(img_MouseLeave);
                img.MouseDown += new MouseButtonEventHandler(source_MouseDown);
                wrapOther.Children.Add(img);
            }

        }

        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            joinPopup.IsOpen = false;
            //离开后重置计时器
            dispt.Stop();
        }

        private void img_MouseEnter(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            SqliteService ss = new SqliteService();
            int id = Convert.ToInt16(img.Name.Substring(2));
            Flag flag = ss.GetFlagById(id);
            joinPopup.PlacementTarget = img;
            joinPopup.PopupAnimation = System.Windows.Controls.Primitives.PopupAnimation.Slide;
            popupTbName.Text = flag.name + "  " + flag.substitute;
            popupTbMeaning.Text = flag.meaning;
            dispt.Start();
        }



        private void WrapPanel_Drop(object sender, DragEventArgs e)
        {
            IDataObject data = new DataObject();
            data = e.Data;
            if (data.GetDataPresent(typeof(Image)))
            {
                Image img = new Image() { Width = 100, Height = 60, Margin = new Thickness(0, 10, 0, 10) };
                Image temp = data.GetData(typeof(Image)) as Image;
                img.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.HighQuality);
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

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            popupChar.IsOpen = false;
            popupNum.IsOpen = false;
            popupOther.IsOpen = false;
        }

        private void btnPre_Click(object sender, RoutedEventArgs e)
        {
            if (situationPosition > 0)
            {
                situationPosition--;
                grid_situation.DataContext = listS[situationPosition];
            }
            wrapPanel_left.Children.Clear();
            wrapPanel_right.Children.Clear();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (situationPosition+1 < listS.Count)
            {
                situationPosition++;
                grid_situation.DataContext = listS[situationPosition];
            }
            wrapPanel_left.Children.Clear();
            wrapPanel_right.Children.Clear();
        }
    }
}
