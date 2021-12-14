using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Cyclone.MciWrapper
{
    public static class IOHelper
    {

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint GetShortPathName(
            string lpszLongPath, 
            string lpszShortPath, 
            int cchBuffer);


        public static string GetShortPathName(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException("filename");

            string s = new string('\0', filename.Length *2 ); //可能长度要乘2
            GetShortPathName(filename, s, s.Length);

            return s.Substring(0 , s.IndexOf('\0'));
        }

    }


}
