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
    /// Shouqi.xaml 的交互逻辑
    /// </summary>
    public partial class Shouqi : UserControl
    {
        public Shouqi()
        {
            InitializeComponent();
        }
        

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            DragDrop.DoDragDrop(img, img, DragDropEffects.Copy);
        }
        

        private void OnDrop(object sender, DragEventArgs e)
        {
            IDataObject data = new DataObject();
            data = e.Data;
            if (data.GetDataPresent(typeof(Image)))
            {
                //Rectangle rect = new Rectangle();
                //rect = data.GetData(typeof(Rectangle)) as Rectangle;
                //canvas2.Children.Remove(rect);
                //canvas1.Children.Add(rect);
                //序列化Control,以深复制Control!!!!
                //string rectXaml = XamlWriter.Save(rect);
                //StringReader stringReader = new StringReader(rectXaml);
                //XmlReader xmlReader = XmlReader.Create(stringReader);
                //UIElement clonedChild = (UIElement)XamlReader.Load(xmlReader);
                //canvas1.Children.Add(clonedChild);

            }
        }
        private void OnDragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Image)))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }

        }
    }
}
