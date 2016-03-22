using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RmVcode
{
    [Serializable]
    public sealed class VcodeImgType
    {
        private Guid guid;
        private string name;

        private static VcodeImgType any = new VcodeImgType(new Guid("{b96b9c0a-0728-11d3-9d7b-0000f81ef32e}"), "任意类型");
        private static VcodeImgType anynum = new VcodeImgType(new Guid("{b96b9c0b-0728-11d3-9d7b-0000f81ef32e}"), "任意长度数字");
        private static VcodeImgType anyalp = new VcodeImgType(new Guid("{b96b9c0c-0728-11d3-9d7b-0000f81ef32e}"), "任意长度字母");
        private static VcodeImgType anyaan = new VcodeImgType(new Guid("{b96b9c0d-0728-11d3-9d7b-0000f81ef32e}"), "任意长度字母或数字");
        private static VcodeImgType anychi = new VcodeImgType(new Guid("{b96b9c0e-0728-11d3-9d7b-0000f81ef32e}"), "任意长度汉字");
        private static VcodeImgType num4 = new VcodeImgType(new Guid("{b96b9caa-0728-11d3-9d7b-0000f81ef32e}"), "4位数字");
        private static VcodeImgType num5 = new VcodeImgType(new Guid("{b96b9cab-0728-11d3-9d7b-0000f81ef32e}"), "5位数字");
        private static VcodeImgType num6 = new VcodeImgType(new Guid("{b96b9cac-0728-11d3-9d7b-0000f81ef32e}"), "6位数字");
        private static VcodeImgType alp4 = new VcodeImgType(new Guid("{b96b9cba-0728-11d3-9d7b-0000f81ef32e}"), "4位字母");
        private static VcodeImgType alp5 = new VcodeImgType(new Guid("{b96b9cbb-0728-11d3-9d7b-0000f81ef32e}"), "5位字母");
        private static VcodeImgType alp6 = new VcodeImgType(new Guid("{b96b9cbc-0728-11d3-9d7b-0000f81ef32e}"), "6位字母");
        private static VcodeImgType aan4 = new VcodeImgType(new Guid("{b96b9cca-0728-11d3-9d7b-0000f81ef32e}"), "4位字母或数字");
        private static VcodeImgType aan5 = new VcodeImgType(new Guid("{b96b9ccb-0728-11d3-9d7b-0000f81ef32e}"), "5位字母或数字");
        private static VcodeImgType aan6 = new VcodeImgType(new Guid("{b96b9ccc-0728-11d3-9d7b-0000f81ef32e}"), "6位字母或数字");
        private static VcodeImgType chi2 = new VcodeImgType(new Guid("{b96b9cda-0728-11d3-9d7b-0000f81ef32e}"), "2位汉字");
        private static VcodeImgType chi4 = new VcodeImgType(new Guid("{b96b9cdb-0728-11d3-9d7b-0000f81ef32e}"), "4位汉字");

        public static VcodeImgType Any { get { return any; } }
        public static VcodeImgType AnyNum { get { return anynum; } }
        public static VcodeImgType AnyAlp { get { return anyalp; } }
        public static VcodeImgType AnyAan { get { return anyaan; } }
        public static VcodeImgType AnyChi { get { return anychi; } }
        public static VcodeImgType Num4 { get { return num4; } }
        public static VcodeImgType Num5 { get { return num5; } }
        public static VcodeImgType Num6 { get { return num6; } }
        public static VcodeImgType Alp4 { get { return alp4; } }
        public static VcodeImgType Alp5 { get { return alp5; } }
        public static VcodeImgType Alp6 { get { return alp6; } }
        public static VcodeImgType AaN4 { get { return aan4; } }
        public static VcodeImgType AaN5 { get { return aan5; } }
        public static VcodeImgType AaN6 { get { return aan6; } }
        public static VcodeImgType Chi2 { get { return chi2; } }
        public static VcodeImgType Chi4 { get { return chi4; } }



        public Guid Id { get { return guid; } }
        public string Name { get; set; }

        public VcodeImgType(Guid guid, string name = null)
        {
            this.guid = guid;
            this.name = name;
        }

        public override string ToString()
        {
            return Name ?? "[VcodeImgType: " + this.guid + "]";
        }

        public override bool Equals(object obj)
        {
            var vo = obj as VcodeImgType;
            if (vo == null)
                return false;

            return this.Id == vo.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
