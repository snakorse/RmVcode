using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RmVcode.Providers
{
    public class UUWiseVcode : VcodeProvider
    {
        public UUWiseVcode(string user, string pwd, string softId = null, string softKey = null, object tag = null)
            : base(VcodePlatform.UUWise, user, pwd, softId, softKey, tag) 
        {
            
        }

        protected override bool InternalInitialize(out string errMsg)
        {
            throw new NotImplementedException();
        }

        protected override bool InternalGetVcode(string typeCode, byte[] img, out string result, out string vcodeId, out string extraMsg)
        {
            throw new NotImplementedException();
        }

        protected override bool InternalReportErr(string vcodeId)
        {
            throw new NotImplementedException();
        }

        protected override string GetImageTypeCode(VcodeImgType type)
        {
            throw new NotImplementedException();
        }
    }
}
