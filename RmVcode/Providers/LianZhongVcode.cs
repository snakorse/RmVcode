﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RmVcode.Providers
{
    /// <summary>
    /// 联众打码实现，官方网址：http://www.jsdati.com
    /// </summary>
    public class LianZhongVcode : VcodeProvider
    {
        public LianZhongVcode(string user, string pwd, string softId = null, string softKey = null, object tag = null)
            : base(VcodePlatform.LianZhong, user, pwd, softId, softKey, tag)
        {
            if (this.softId == null || this.softKey == null)
            {
                this.softId = "";
                this.softKey = "12987d18269fffd9ad9e0a0d33dd96e9";
            }

            AddImgType(VcodeImgType.Any, "0");
        }

        protected override bool InternalInitialize(out string errMsg)
        {
            errMsg = null;
            if (string.IsNullOrEmpty(this.user) || string.IsNullOrEmpty(this.pwd))
            {
                errMsg = "The Username or Password prop is empty.";
                return false;
            }
            return true;
        }

        protected override bool InternalGetVcode(string typeCode, byte[] img, out string result, out string vcodeId, out string extraMsg)
        {
            result = vcodeId = extraMsg = null;
            var resp = FastVerCode.VerCode.RecByte_A(img, img.Length, this.user, this.pwd, this.softKey);
            if(string.IsNullOrEmpty(resp) || !resp.Contains("|!|"))
            {
                extraMsg = resp;
                return false;
            }

            var arr = resp.Split(new[] { "|!|" }, StringSplitOptions.None);
            result = arr[0];
            vcodeId = arr[1];
            extraMsg = resp;
            return true;
        }

        protected override bool InternalReportErr(string vcodeId)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(vcodeId))
                return false;
            FastVerCode.VerCode.ReportError(this.user, vcodeId);
            return true;
        }
    }
}
