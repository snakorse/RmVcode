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
            manager.InitializePlatform(VcodePlatform.RuoKuai, "kcottys", "hsh1984112");
            manager.CurrentPlatform = VcodePlatform.RuoKuai;
            var image = System.IO.File.ReadAllBytes(@"D:\Project\Code\C#\MSMailRegister\MSMailRegister\AppTest\bin\Debug\captcha.png");
            var imgArgs = new VcodeEventArgs(VcodeImgType.AnyAlphaOrNum, image.ToArray());
            manager.GetVcode(imgArgs);
            Console.Read();
        }
    }
}
