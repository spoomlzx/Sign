using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Sign
{
    /// <summary>
    /// Lilun.xaml 的交互逻辑
    /// </summary>
    public partial class Lilun : UserControl
    {
        public Lilun()
        {
            InitializeComponent();
        }
        void img_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject(typeof(Image), (Image)sender);
                DragDrop.DoDragDrop((Image)sender, data, DragDropEffects.Copy);
            }
        }
        private void source_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DataObject data = new DataObject(typeof(Image), (Image)sender);
            DragDrop.DoDragDrop((Image)sender, data, DragDropEffects.Copy);
        }

        void canvas1_Drop(object sender, DragEventArgs e)
        {
            IDataObject data = new DataObject();
            data = e.Data;
            if (data.GetDataPresent(typeof(Image)))
            {
                Image img = new Image() {Width=100,Height=60,Margin=new Thickness(10,10,10,10) };
                Image temp = data.GetData(typeof(Image)) as Image;
                img.Source = temp.Source;
                ((WrapPanel)sender).Children.Add(img);
            }
        }


        void canvas1_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Image)))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }

        }
    }
}
