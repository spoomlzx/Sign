using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sign
{
    /// <summary>
    /// Warship.xaml 的交互逻辑
    /// </summary>
    public partial class Warship : UserControl
    {

        //转向动画
        private Storyboard _storyboard;
        private DoubleAnimation _animation;

        //航行动画
        private Storyboard _moveStoryboard;
        private DoubleAnimation _xanimation;
        private DoubleAnimation _yanimation;

        public Warship()
        {
            InitializeComponent();

            //转向动画板
            _storyboard = new Storyboard();
            _animation = new DoubleAnimation();
            _animation.Duration = TimeSpan.FromMilliseconds(500);
            Storyboard.SetTarget(_animation, this);
            Storyboard.SetTargetProperty(_animation, new PropertyPath("Course"));
            _storyboard.Children.Add(_animation);

            //航行动画板
            _moveStoryboard = new Storyboard();
            _xanimation = new DoubleAnimation();
            _xanimation.Duration = TimeSpan.FromSeconds(2);
            Storyboard.SetTarget(_xanimation, this);
            Storyboard.SetTargetProperty(_xanimation, new PropertyPath("(Canvas.Left)"));
            _yanimation = new DoubleAnimation();
            _yanimation.Duration = TimeSpan.FromSeconds(2);
            Storyboard.SetTarget(_yanimation, this);
            Storyboard.SetTargetProperty(_yanimation, new PropertyPath("(Canvas.Bottom)"));
            _moveStoryboard.Children.Add(_xanimation);
            _moveStoryboard.Children.Add(_yanimation);
        }

        public void Change()
        {
            this.Turn(90.0);
            Storyboard _storyboard = new Storyboard();


            for (int j = 0; j < 1; j++)
            {
                DoubleAnimation _animation = new DoubleAnimation();
                _animation.Duration = TimeSpan.FromMilliseconds(500);
                Storyboard.SetTarget(_animation, this);
                Storyboard.SetTargetProperty(_animation, new PropertyPath("Course"));

                //warships[i].Turn(-10.0, span);
                //warships[i].Move(10.0);
                _animation.To = -10.0 + this.Course;
                _storyboard.Children.Add(_animation);

                DoubleAnimation _xanimation = new DoubleAnimation();
                _xanimation.Duration = TimeSpan.FromMilliseconds(500);
                Storyboard.SetTarget(_xanimation, this);
                Storyboard.SetTargetProperty(_xanimation, new PropertyPath("(Canvas.Left)"));
                DoubleAnimation _yanimation = new DoubleAnimation();
                _yanimation.Duration = TimeSpan.FromMilliseconds(500);
                Storyboard.SetTarget(_yanimation, this);
                Storyboard.SetTargetProperty(_yanimation, new PropertyPath("(Canvas.Bottom)"));
                _xanimation.To = Canvas.GetLeft(this) + 5.0;
                _yanimation.To = Canvas.GetBottom(this) + 5.0;
                _storyboard.Children.Add(_xanimation);
                _storyboard.Children.Add(_yanimation);
            }
            _storyboard.Begin();
        }

        /// <summary>
        /// 从初始航向course转向degree度
        /// </summary>
        /// <param name="degree">转向度数</param>
        public void Turn(double degree)
        {
            _animation.To = degree+this.Course;
            _storyboard.Begin();
        }
        /// <summary>
        /// 带时间参数的转向函数
        /// </summary>
        /// <param name="degree"></param>
        /// <param name="span"></param>
        public void Turn(double degree,TimeSpan span)
        {
            _animation.Duration = span;
            Turn(degree);
        }

        /// <summary>
        /// 移动(x,y)坐标
        /// </summary>
        /// <param name="x">x轴平移</param>
        /// <param name="y">y轴平移</param>
        private void Move(double x,double y)
        {
            _xanimation.To = Canvas.GetLeft(this) + x;
            _yanimation.To = Canvas.GetBottom(this) + y;
            _moveStoryboard.Begin();
        }

        /// <summary>
        /// 沿着航向，按航速this.speed前进distance距离
        /// </summary>
        /// <param name="distance">前进距离</param>
        public void Move(double distance)
        {
            double angle = Math.PI* this.Course / 180.0;
            double x = Math.Sin(angle) * distance;
            double y = Math.Cos(angle) * distance;
            //重新计算航行时间
            _xanimation.Duration = TimeSpan.FromSeconds(distance/this.Speed);
            _yanimation.Duration = TimeSpan.FromSeconds(distance/this.Speed);
            this.Move(x, y);
        }
        

        #region IsLeader Property

        public bool IsLeader
        {
            get { return (bool)GetValue(IsLeaderProperty); }
            set { SetValue(IsLeaderProperty, value); }
        }
        public static readonly DependencyProperty IsLeaderProperty = DependencyProperty.Register("IsLeader", typeof(bool), typeof(Warship),
            new FrameworkPropertyMetadata(OnIsLeaderChanged));

        private static void OnIsLeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Warship)d).OnIsLeaderChanged(e);
        }

        private void OnIsLeaderChanged(DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                leaderPath.Fill = new SolidColorBrush( Color.FromRgb(134,64,33));
            }
            else
            {
                leaderPath.Fill = new SolidColorBrush(Color.FromRgb(79, 154, 54));
            }
        }
        #endregion

        #region Course Property
        public double Course
        {
            get { return (double)GetValue(CourseProperty); }
            set { SetValue(CourseProperty, value); }
        }

        public static readonly DependencyProperty CourseProperty = DependencyProperty.Register("Course", typeof(double), typeof(Warship),
            new FrameworkPropertyMetadata(OnCourseChanged));

        private static void OnCourseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Warship)d).OnCourseChanged(e);
        }

        private void OnCourseChanged(DependencyPropertyChangedEventArgs e)
        {
            RotateTransform rotate = new RotateTransform(Course);
            RenderTransform = rotate;
        }

        #endregion

        #region Speed Property
        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register("Speed", typeof(double), typeof(Warship),
            new FrameworkPropertyMetadata(OnSpeedChanged));

        private static void OnSpeedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Warship)d).OnSpeedChanged(e);
        }

        private void OnSpeedChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

    }
}
