using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RmVcode;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new VcodeManager();
            manager.GetVcode(new VcodeEventArgs(VcodeImgType.Any, null));
            Console.Read();
        }
    }
}
