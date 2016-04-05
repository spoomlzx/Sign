using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Sign
{
    class Tick
    {
        private DispatcherTimer timer = new DispatcherTimer();
        //private int position = 0;
        private int interval = 500;
        private string signIn=string.Empty;

        private delegate void OneFuncDelegate();
        private delegate void ZeroFuncDelegate();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="str">输入的数字信号</param>
        public Tick(string str)
        {
            signIn = str;
            timer.Interval = TimeSpan.FromMilliseconds(interval);
            timer.Tick += new EventHandler(Timer_Tick);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
