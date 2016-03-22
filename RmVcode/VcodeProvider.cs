using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RmVcode
{
    public abstract class VcodeProvider
    {
        private static object sync = new object();
        private bool inited;
        private string initErrMsg;

        protected string user;
        protected string pwd;
        protected string softId;
        protected string softKey;
        protected VcodePlatform platform;
        protected Dictionary<VcodeImgType, string> imgTypeDict = new Dictionary<VcodeImgType, string>();
        protected object tag;

        #region Proterties
        public bool IsInitialized
        {
            get { return this.inited; }
        }

        public VcodePlatform Platform
        {
            get { return this.platform; }
        }

        public string InitErrMsg
        {
            get { return this.initErrMsg; }
        }

        public string Username
        {
            get { return this.user; }
            set { this.user = value; }
        }

        public string Password
        {
            get { return this.pwd; }
            set { this.pwd = value; }
        }

        public string SoftId
        {
            get { return this.softId; }
            set { this.softId = value; }
        }

        public string SoftKey
        {
            get { return this.softKey; }
            set { this.softKey = value; }
        }

        public object Tag
        {
            get { return this.tag; }
            set { this.tag = value; }
        }

        #endregion

        protected VcodeProvider(VcodePlatform platform, string user, string pwd, string softId, string softKey, object tag)
        {
            if (platform == null)
                throw new ArgumentNullException("platform");

            this.platform = platform;
            this.user = user;
            this.pwd = pwd;
            this.softId = softId;
            this.softKey = softKey;
            this.tag = tag;
        }

        public void AddImgType(VcodeImgType type, string code)
        {
            if (type == null || code == null)
                return;
            imgTypeDict[type] = code;
        }

        /// <summary>
        /// 执行一些必要的初始化工作，比如校验数据准备情况，加载字库，登录等等
        /// </summary>
        /// <param name="force">是否强制重新初始化</param>
        /// <returns></returns>
        public bool Initialize(bool force = false)
        {
            if (inited && !force)
                return true;
            lock (sync)
            {
                if (inited && !force)
                    return true;

                inited = InternalInitialize(out initErrMsg);
                return inited;
            }
        }

        protected abstract bool InternalInitialize(out string errMsg);

        /// <summary>
        /// 获取登录的账号的当前剩余积分点数，打码平台一般都提供了此接口
        /// </summary>
        /// <returns></returns>
        public decimal GetCredit()
        {
            Initialize();
            if (!inited)
                throw new InvalidOperationException("Initialization not excuted.");
            return InternalGetCredit();
        }

        protected virtual decimal InternalGetCredit()
        {
            return 0;
        }

        /// <summary>
        /// 识别验证码的主要操作
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool GetVcode(VcodeEventArgs e)
        {

            if (e == null)
                throw new ArgumentNullException("e");
            if (e.Bytes == null)
                throw new ArgumentNullException("e.Bytes");

            Initialize();
            if (!inited)
                throw new InvalidOperationException("Initialization not excuted.");

            string typeCode, result, vcodeId, msg;

            typeCode = GetImageTypeCode(e.ImgType);
            if (typeCode == null && e.ImgType != null)
            {
                this.imgTypeDict.TryGetValue(e.ImgType, out typeCode);
            }

            if (typeCode == null && e.ImgType != VcodeImgType.Any)
            {
                typeCode = GetImageTypeCode(VcodeImgType.Any);
            }

            if (typeCode == null && e.ImgType != VcodeImgType.Any)
            {
                this.imgTypeDict.TryGetValue(VcodeImgType.Any, out typeCode);
            }

            if (typeCode == null)
                throw new NotSupportedException("Not support the imgtype that e.Type stands for.");

            var vResult = InternalGetVcode(typeCode, e.Bytes, out result, out vcodeId, out msg);
            
            e.Handled = true;
            e.Platform = this.platform;
            e.Result = result;
            e.VcodeId = vcodeId;
            e.Msg = msg;

            return vResult;
        }

        protected abstract string GetImageTypeCode(VcodeImgType type);

        protected abstract bool InternalGetVcode(string typeCode, byte[] img, out string result, out string vcodeId, out string extraMsg);

        /// <summary>
        /// 上报错误验证码，打码平台一般都提供了此接口
        /// </summary>
        /// <param name="e">GetVcode返回的结果</param>
        /// <returns></returns>
        public bool ReportErr(VcodeEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            Initialize();
            if (!inited)
                throw new InvalidOperationException("Initialization not excuted.");
            return InternalReportErr(e.VcodeId);
        }

        protected abstract bool InternalReportErr(string vcodeId);
    }
}
