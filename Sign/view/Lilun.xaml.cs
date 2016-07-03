using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Animation;
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
        private ObservableCollection<Warship> warships = new ObservableCollection<Warship>();
        public Lilun()
        {
            InitializeComponent();
            InitWarships();
        }

        /// <summary>
        /// 初始化4个warship，并填入collection
        /// </summary>
        public void InitWarships()
        {
            warships.Clear();
            canvas.Children.Clear();
            for (int i = 1; i < 5; i++)
            {
                Warship warship = new Warship();
                warship.Name = "ws_0" + i;
                Canvas.SetLeft(warship, 36.0 + 100.0 * i);
                Canvas.SetBottom(warship, 20.0);
                warship.MouseLeftButtonDown += Warship_MouseLeftButtonDown;
                warship.Speed = 100.0;
                canvas.Children.Add(warship);
                warships.Add(warship);
            }
            warships[warships.Count - 1].IsLeader = true;
        }

        private void Warship_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Warship warship = (Warship)sender;
            if (warship.IsLeader == true)
            {
                warship.IsLeader = false;
            }
            else
            {
                warship.IsLeader = true;
            }
        }

        private void button_turnRight_Click(object sender, RoutedEventArgs e)
        {
            foreach (Warship warship in warships)
            {
                warship.Turn(Convert.ToDouble(sliderangle.Value));
            }

        }

        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            warships[0].Move(sliderdistance.Value);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            double width = canvas.ActualWidth;
            double height = canvas.ActualHeight;
            MessageBox.Show(width + ":" + height);
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach(Warship warship in warships)
            {
                warship.Move(100.0 + 70.0 * (i++));
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan span = TimeSpan.FromSeconds(0.1);
            warships[0].Turn(10.0,span);
            warships[0].Move(10.0);
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            InitWarships();
        }

        private void btn_11_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 3; i < 4; i++)
            {
                warships[i].Change();
                //warships[i].Move(i * 100.0);
            }
        }
    }
}
