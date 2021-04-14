using System;
using System.Collections.Generic;
using System.Text;

namespace UsefulExtensions
{
    public static class BoolExtension
    {
        public static bool IsTrue(this bool input, Action action)
        {
            if (input == true)
                action();
            return input;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsFalse(this bool input, Action action)
        {
            if (input == false)
                action();
            return input;
        }
    }
}
