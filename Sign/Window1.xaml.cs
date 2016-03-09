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
        public Window1()
        {
            InitializeComponent();
            List<Unit> units = new List<Unit>();
            Unit unit1 = new Unit() { Year = "2001", Price = 100 };
            Unit unit2 = new Unit() { Year = "2002", Price = 120 };
            Unit unit3 = new Unit() { Year = "2003", Price = 140 };
            Unit unit4 = new Unit() { Year = "2004", Price = 160 };
            Unit unit5 = new Unit() { Year = "2005", Price = 180 };
            units.Add(unit1);
            units.Add(unit2);
            units.Add(unit3);
            units.Add(unit4);
            units.Add(unit5);
            listBox.ItemsSource = units;
        }
    }
    class Unit
    {
        public string Year { get; set; }
        public int Price { get; set; }
    }
}
