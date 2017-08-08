using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RmVcode.Providers
{
    /// <summary>
    /// 超人打码实现，官方网址：http://www.sz789.net
    /// </summary>
    public class ChaoRenVcode : VcodeProvider
    {
        public ChaoRenVcode(string user, string pwd, string softId = null, string softKey = null, object tag = null)
            : base(VcodePlatform.ChaoRen, user, pwd, softId, softKey, tag) 
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
    }
}
