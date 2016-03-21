using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sign
{
    class Translate
    {
        private string[] chars = { "10111","111010101","11101011101","1110101","1","101011101","111011101",
                           "1010101","101","1011101110111","111010111","101110101","1110111","11101",
                           "11101110111","10111011101","1110111010111","1011101","10101","111","101011",
                           "101010111","101110111","11101010111","1110101110111","11101110101"};
        private string[] nums = { "111", "10111", "1010111", "1010101110111", "10101010111", "101010101",
                          "11101010101", "1110111010101", "1110101", "11101"};
        private string[] fullnums = { "10111011101110111","101011101110111","1010101110111","10101010111","101010101",
                            "11101010101","1110111010101","111011101110101","11101110111011101","1110111011101110111"};

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
    }
}
