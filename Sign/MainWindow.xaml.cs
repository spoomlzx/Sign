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
        private bool isFirstRender=true;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 绘制tabitem，一次性渲染所有模块，会有性能损失，后期如有需要，将进行使用时渲染
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (isFirstRender)
            {
                Dengguang dg = new Dengguang();
                Shouqi sq = new Shouqi();
                Qihao qh = new Qihao();
                Lilun ll = new Lilun();

                Morse morse = new Morse();
                tabItem_kaohe.Content = morse;

                tabItem_qihao.Content = qh;
                tabItem_shouqi.Content = sq;
                tabItem_dengguang.Content = dg;
                tabItem_lilun.Content = ll;
                isFirstRender = false;
            }
            base.OnRender(drawingContext);
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Window1 win = new Window1();
            win.ShowDialog();
        }
    }


}
