using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RmVcode
{
    [Serializable]
    public sealed class VcodePlatform
    {
        private Guid guid;
        private string name;

        private static VcodePlatform manual = new VcodePlatform(new Guid("{b96b7caa-0728-11d3-9d7b-0000f81ef32e}"), "手动打码");
        private static VcodePlatform lianzhong = new VcodePlatform(new Guid("{b96b7cab-0728-11d3-9d7b-0000f81ef32e}"), "联众打码");
        private static VcodePlatform uuwise = new VcodePlatform(new Guid("{b96b7cac-0728-11d3-9d7b-0000f81ef32e}"), "优优打码");
        private static VcodePlatform ruokuai = new VcodePlatform(new Guid("{b96b7cad-0728-11d3-9d7b-0000f81ef32e}"), "若快打码");
        private static VcodePlatform chaoren = new VcodePlatform(new Guid("{b96b7cae-0728-11d3-9d7b-0000f81ef32e}"), "超人打码");
        private static VcodePlatform zhima = new VcodePlatform(new Guid("{b96b7caf-0728-11d3-9d7b-0000f81ef32e}"), "知码打码");

        public static VcodePlatform Manual { get { return manual; } }
        public static VcodePlatform LianZhong { get { return lianzhong; } }
        public static VcodePlatform UUWise { get { return uuwise; } }
        public static VcodePlatform RuoKuai { get { return ruokuai; } }
        public static VcodePlatform ChaoRen { get { return chaoren; } }
        public static VcodePlatform ZhiMa { get { return zhima; } }

        public Guid Id { get { return guid; } }
        public string Name { get; set; }

        public VcodePlatform(Guid guid, string name = null)
        {
            this.guid = guid;
            this.name = name;
        }

        public override string ToString()
        {
            return Name ?? "[VcodePlatform: " + this.guid + "]";
        }

        public override bool Equals(object obj)
        {
            var vo = obj as VcodePlatform;
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
