using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet4.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            AddImgType(VcodeImgType.Alpha4, "2040");
            AddImgType(VcodeImgType.Alpha5, "2050");
            AddImgType(VcodeImgType.Alpha6, "2060");
            AddImgType(VcodeImgType.AnyAlpha, "2000");

            AddImgType(VcodeImgType.AlphaOrNum4, "3040");
            AddImgType(VcodeImgType.AlphaOrNum5, "3050");
            AddImgType(VcodeImgType.AlphaOrNum6, "3060");
            AddImgType(VcodeImgType.AnyAlphaOrNum, "3000");

            AddImgType(VcodeImgType.Chinese2, "4020");
            AddImgType(VcodeImgType.Chinese4, "4040");
            AddImgType(VcodeImgType.AnyChinese, "4000");

            AddImgType(VcodeImgType.Num4, "1040");
            AddImgType(VcodeImgType.Num5, "1050");
            AddImgType(VcodeImgType.Num6, "1060");
            AddImgType(VcodeImgType.AnyNum, "1000");

            AddImgType(VcodeImgType.Any, "5000");
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
            result = vcodeId = extraMsg = null;

            #region 提交信息模板
            const string postTpl = @"---------------itsfunny
Content-Disposition: form-data; name=""username""

{username}
---------------itsfunny
Content-Disposition: form-data; name=""password""

{password}
---------------itsfunny
Content-Disposition: form-data; name=""typeid""

{typeid}
---------------itsfunny
Content-Disposition: form-data; name=""timeout""

{timeout}
---------------itsfunny
Content-Disposition: form-data; name=""softid""

{softid}
---------------itsfunny
Content-Disposition: form-data; name=""softkey""

{softkey}
---------------itsfunny
Content-Disposition: form-data; name=""image""; filename=""1.png""
Content-Type: application/octet-stream
Content-Transfer-Encoding: base64

{base64img}
---------------itsfunny--";
            #endregion

            var postdata = postTpl
                            .Replace("{username}", Username)
                            .Replace("{password}", Password)
                            .Replace("{typeid}", typeCode)
                            .Replace("{timeout}", "60")
                            .Replace("{softid}", SoftId)
                            .Replace("{softkey}", SoftKey)
                            .Replace("{base64img}", Convert.ToBase64String(img));

            var item = new HttpItem
            {
                URL = "http://api.ruokuai.com/create.json",
                Method = "POST",
                Accept = "*/*",
                ContentType = "multipart/form-data; boundary=-------------itsfunny",
                Postdata = postdata,
            };

            var resp = new HttpHelper().GetHtml(item);
            if (resp.StatusCode != System.Net.HttpStatusCode.OK)
            {
                extraMsg = "请求结果错误";
                return false;
            }

            var json = JsonConvert.DeserializeObject(resp.Html) as JObject;
            result = json.Value<string>("Result");
            vcodeId = json.Value<string>("Id");
            extraMsg = json.Value<string>("Error");
            
            return !string.IsNullOrEmpty(result);
        }

        protected override bool InternalReportErr(string vcodeId)
        {
            #region 提交信息模板
            const string postTpl = @"---------------itsfunny
Content-Disposition: form-data; name=""username""

{username}
---------------itsfunny
Content-Disposition: form-data; name=""password""

{password}
---------------itsfunny
Content-Disposition: form-data; name=""softid""

{softid}
---------------itsfunny
Content-Disposition: form-data; name=""softkey""

{softkey}
---------------itsfunny
Content-Disposition: form-data; name=""id""; 

{id}
---------------itsfunny--";
            #endregion
            var postdata = postTpl
                            .Replace("{username}", Username)
                            .Replace("{password}", Password)
                            .Replace("{softid}", SoftId)
                            .Replace("{softkey}", SoftKey)
                            .Replace("{id}", vcodeId);
            var item = new HttpItem
            {
                URL = "http://api.ruokuai.com/reporterror.json",
                Method = "POST",
                Accept = "*/*",
                ContentType = "multipart/form-data; boundary=-------------itsfunny",
                Postdata = postdata,
            };

            var resp = new HttpHelper().GetHtml(item);
            var json = JsonConvert.DeserializeObject(resp.Html) as JObject;

            return json.Value<string>("Result") != null;
        }
    }
}
