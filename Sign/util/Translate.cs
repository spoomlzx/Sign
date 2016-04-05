using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sign
{
    class Translate
    {
        private string[] chars = {
            "10111",//A
            "111010101",//B
            "11101011101",//C
            "1110101",//D
            "1",//E
            "101011101",//F
            "111011101",//G
            "1010101",//H
            "101",//I
            "1011101110111",//J
            "111010111",//K
            "101110101",//L
            "1110111",//M
            "11101",//N
            "11101110111",//O
            "10111011101",//P
            "1110111010111",//Q
            "1011101",//R
            "10101",//S
            "111",//T
            "101011",//U
            "101010111",//V
            "101110111",//W
            "11101010111",//X
            "1110101110111",//Y
            "11101110101"//Z
        };
        private string[] nums = {
            "111",//0
            "10111",//1
            "1010111",//2
            "1010101110111",//3
            "10101010111",//4
            "101010101",//5
            "11101010101",//6
            "1110111010101",//7
            "1110101",//8
            "11101"//9
        };
        private string[] fullnums = { "10111011101110111","101011101110111","1010101110111","10101010111","101010101",
                            "11101010101","1110111010101","111011101110101","11101110111011101","1110111011101110111"};
        //短间隔，通常为3个单位
        private string shortInterval = "000";
        //长间隔，通常为5个单位,但是除掉一个短间隔
        private string longInterval = "00";
        /// <summary>
        /// 转译带字符的报，数字用全码
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        public string CharToS(char chr)
        {
            if (chr >= 'a' && chr <= 'z')
            {
                return chars[Convert.ToInt16(chr)-97];
            }
            else if(chr >= 'A' && chr <= 'Z')
            {
                return chars[Convert.ToInt16(chr) - 65];
            }
            else if (chr >= '0' && chr <= '9')
            {
                return fullnums[Convert.ToInt16(chr) - 48];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 中文转数字信号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ChineseToS(string str)
        {
            string r = string.Empty;
            foreach(char c in ChineseToChar(str))
            {
                if (c == '&')
                {
                    r += longInterval;
                }
                else
                {
                    r += CharToS(c);
                    r += shortInterval;
                }
            }
            return r;
        }

        /// <summary>
        /// 转译数字报文，数字用短码
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string NumToS(int i)
        {
            if (i <= 9 && i >= 0)
            {
                return nums[i];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 将汉字string str转换为string类型的拼音
        /// </summary>
        /// <param name="str">输入汉字</param>
        /// <returns></returns>
        public string ChineseToChar(string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, t.Length - 1);
                    r += "&";
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }


        
    }
}
