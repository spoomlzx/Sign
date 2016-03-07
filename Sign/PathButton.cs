﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Sign
{
    class PathButton:Button
    {
        #region PathData Property

        public string PathData
        {
            get { return (string)GetValue(PathDataProperty); }
            set { SetValue(PathDataProperty, value); }
        }
        public static readonly DependencyProperty PathDataProperty = DependencyProperty.Register("PathData",typeof(string),typeof(PathButton),
            new FrameworkPropertyMetadata(OnPathDataChanged));

        private static void OnPathDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }
        #endregion
    }
}
