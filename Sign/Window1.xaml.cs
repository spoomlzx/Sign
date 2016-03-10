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
using System.Windows.Shapes;

namespace Sign
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : BaseWindow
    {
        private bool isFirstRender=true;

        public Window1()
        {
            InitializeComponent();
            tabControl1.ItemsSource = Enumerable.Range(1, 3);
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (isFirstRender)
            {
                TabItem tbi = new TabItem();
                tbi.Header = "sdjf";
                Dengguang dg = new Dengguang();
                tbi.Content = dg;
                TabItem tbi2 = new TabItem();
                tbi2.Header = "s32";
                Dengguang dg2 = new Dengguang();
                tbi2.Content = dg2;
                tabControl.Items.Add(tbi);
                tabControl.Items.Add(tbi2);
                isFirstRender = false;
            }
            base.OnRender(drawingContext);
        }
    }
}
