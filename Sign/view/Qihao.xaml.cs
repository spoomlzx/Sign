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
    /// Qihao.xaml 的交互逻辑
    /// </summary>
    public partial class Qihao : UserControl
    {
        public Qihao()
        {
            InitializeComponent();
            //BitmapImage bimg = new BitmapImage(new Uri("../image/qizhi/C.png", UriKind.Relative));


            string charlist = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for(int i = 0; i < 26; i++)
            {
                BitmapImage bimg = new BitmapImage(new Uri("../image/qizhi/"+charlist[i]+".png", UriKind.Relative));
                Image img = new Image() { Width = 100, Height = 50, Margin = new Thickness(0, 8, 0, 8) };
                img.Name =Convert.ToString(charlist[i]);
                img.Source = bimg;
                img.MouseEnter += new MouseEventHandler(img_MouseEnter);
                img.MouseLeave += new MouseEventHandler(img_MouseLeave);
                img.MouseDown += new MouseButtonEventHandler(source_MouseDown);
                g.Children.Add(img);
            }
            
        }

        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            joinPopup.IsOpen = false;
        }

        private void img_MouseEnter(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            BitmapImage bimg = new BitmapImage(new Uri("../image/qizhi/" +img.Name+ ".png", UriKind.Relative));
            joinPopup.PlacementTarget = img;
            popupImg.Source = bimg;
            joinPopupTextBlock.Text = img.Name + "    啊呀，表示jsdkfljsfdksfd";
            joinPopup.IsOpen = true;
        }

        private void WrapPanel_Drop(object sender, DragEventArgs e)
        {
            IDataObject data = new DataObject();
            data = e.Data;
            if (data.GetDataPresent(typeof(Image)))
            {
                Image img = new Image() { Width = 100, Height = 60, Margin = new Thickness(10, 10, 10, 10) };
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
    }
}
