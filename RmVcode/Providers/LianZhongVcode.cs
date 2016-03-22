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
                this.softKey = "3680a3fbfda740aa3e9e8968b72dfe4b";
            }
        }

        protected override string GetImageTypeCode(VcodeImgType type)
        {
            if (type == VcodeImgType.Any) return "0";
            return null;
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
            throw new NotImplementedException();
        }

        protected override bool InternalReportErr(string vcodeId)
        {
            throw new NotImplementedException();
        }
    }
}