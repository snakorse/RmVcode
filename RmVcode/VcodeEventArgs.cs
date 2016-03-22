using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RmVcode
{
    [Serializable]
    public class VcodeEventArgs : EventArgs
    {
        public VcodeImgType ImgType { get; set; }
        public byte[] Bytes { get; set; }

        public bool Handled { get; set; }
        public VcodePlatform Platform { get; set; }
        public string Result { get; set; }
        public string VcodeId { get; set; }
        public string Msg { get; set; }

        public VcodeEventArgs(VcodeImgType type, byte[] bytes)
        { 
            this.Bytes = bytes;
            this.ImgType = type;
        }

    }
}
