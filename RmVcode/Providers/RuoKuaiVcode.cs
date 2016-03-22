using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RmVcode.Providers
{
    public class RuoKuaiVcode : VcodeProvider
    {
        public RuoKuaiVcode(string user, string pwd, string softId = null, string softKey = null, object tag = null)
            : base(VcodePlatform.RuoKuai, user, pwd, softId, softKey, tag) 
        {
            if (string.IsNullOrEmpty(softId) && string.IsNullOrEmpty(softKey))
            {
                this.softId = "2207";
                this.softKey = "76fd8d76008954aea8175cb364291db3";
            }
        }

        protected override string GetImageTypeCode(VcodeImgType type)
        {
            if(type == VcodeImgType.Any) return "3000";
            if (type == VcodeImgType.AnyNum) return "1000";
            if(type == VcodeImgType.AnyAlp) return "2000";
            if(type == VcodeImgType.AnyAan) return "3000";
            if(type == VcodeImgType.AnyChi) return "4000";
            if(type == VcodeImgType.Num4) return "1040";
            if(type == VcodeImgType.Num5) return "1050";
            if(type == VcodeImgType.Num6) return "1060";
            if(type == VcodeImgType.Alp4) return "2040";
            if(type == VcodeImgType.Alp5) return "2050";
            if(type == VcodeImgType.Alp6) return "2060";
            if(type == VcodeImgType.AaN4) return "3040";
            if(type == VcodeImgType.AaN5) return "3050";
            if(type == VcodeImgType.AaN6) return "3060";
            if(type == VcodeImgType.Chi2) return "4020";
            if(type == VcodeImgType.Chi4) return "4040";

            return null;
        }

        protected override bool InternalInitialize(out string errMsg)
        {
            errMsg = null;
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pwd))
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

        #region Inner method

        #endregion
    }
}
