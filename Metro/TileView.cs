﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Metro
{
    class TileView : ViewBase
    {
        private DataTemplate itemTemplate;

        public DataTemplate ItemTemplate
        {
            get
            {
                return itemTemplate;
            }

            set
            {
                itemTemplate = value;
            }
        }
        protected override object DefaultStyleKey
        {
            get { return new ComponentResourceKey(GetType(), "TileView"); }
        }

        protected override object ItemContainerDefaultStyleKey
        {
            get { return new ComponentResourceKey(GetType(), "TileViewItem"); }
        }
    }
}
