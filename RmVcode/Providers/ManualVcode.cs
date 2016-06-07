using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RmVcode.Providers
{
    /// <summary>
    /// 手动打码实现，弹出窗口
    /// </summary>
    public class ManualVcode : VcodeProvider
    {
        public ManualVcode()
            : base(VcodePlatform.Manual, null, null, null, null, null)
        {
            
        }

        protected override string GetImageTypeCode(VcodeImgType type)
        {
            if (type == VcodeImgType.Any) return "0";
            return null;
        }

        protected override bool InternalInitialize(out string errMsg)
        {
            errMsg = null;
            return true;
        }

        protected override bool InternalGetVcode(string typeCode, byte[] img, out string result, out string vcodeId, out string extraMsg)
        {
            var frm = new ManualForm(img);
            var dret = frm.ShowDialog();
            frm.Dispose();

            result = frm.Result;
            vcodeId = extraMsg = null;

            return dret == System.Windows.Forms.DialogResult.OK;
        }

        protected override bool InternalReportErr(string vcodeId)
        {
            return true;
        }

        
    }
}
