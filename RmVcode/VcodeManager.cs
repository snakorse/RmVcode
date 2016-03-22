using RmVcode.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RmVcode
{
    /// <summary>
    /// 提供此类以便于更方便的使用和管理内置的VcodeProviders
    /// </summary>
    public class VcodeManager
    {
        private Dictionary<VcodePlatform, VcodeProvider> vcodeDict = new Dictionary<VcodePlatform, VcodeProvider>();
        private VcodeProvider currentProvider = null;

        public IEnumerable<VcodePlatform> SupportedPlatforms
        {
            get { return this.vcodeDict.Keys; }
        }

        public VcodePlatform CurrentPlatform
        {
            get
            {
                if (currentProvider != null)
                {
                    return currentProvider.Platform;
                }

                return null;
            }

            set
            {
                if (!vcodeDict.ContainsKey(value))
                {
                    throw new KeyNotFoundException("The platform is not found in SupportedPlatforms, you should register it first.");
                }

                currentProvider = vcodeDict[value];
            }
        }

        public VcodeManager()
        {
            AddDefaultPlatforms();
        }

        private void AddDefaultPlatforms()
        {
            RegisterPlatform(new ManualVcode());
            RegisterPlatform(new LianZhongVcode(null, null));
            RegisterPlatform(new UUWiseVcode(null, null));
            RegisterPlatform(new ChaoRenVcode(null, null));
            RegisterPlatform(new RuoKuaiVcode(null, null));
            RegisterPlatform(new ZhiMaVcode(null, null));
        }

        public void RegisterPlatform(VcodeProvider vcodeProvider)
        {
            if (vcodeProvider == null)
                throw new ArgumentNullException("vcodeProvider");

            vcodeDict[vcodeProvider.Platform] = vcodeProvider;
        }

        public bool InitializePlatform(VcodePlatform platform, string user, string pwd, string softId = null, string softKey = null, object tag = null)
        {
            if (platform == null)
                throw new ArgumentNullException("platform");

            if (!vcodeDict.ContainsKey(platform))
            {
                throw new KeyNotFoundException("the platform is not found in SupportedPlatforms, you should register it first.");
            }

            var vcode = vcodeDict[platform];
            return vcode.Initialize(vcode.Username != user || vcode.Password != pwd
                || vcode.SoftId != softId || vcode.SoftKey != softKey || vcode.Tag != tag);
        }

        public decimal GetCredit()
        {
            if (currentProvider == null)
                throw new NullReferenceException("CurrentPlatform should be set before excuting this method.");

            return currentProvider.GetCredit();
        }

        public bool GetVcode(VcodeEventArgs e)
        {
            if (currentProvider == null)
                throw new NullReferenceException("CurrentPlatform should be set before excuting this method.");

            return currentProvider.GetVcode(e);
        }

        public bool ReportErr(VcodeEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("e");
            if (currentProvider != null && e.Platform == currentProvider.Platform)
                return currentProvider.ReportErr(e);

            if (e.Platform == null || !vcodeDict.ContainsKey(e.Platform))
            {
                return false;
            }
            else
            {
                return vcodeDict[e.Platform].ReportErr(e);
            }
        }
    }
}
