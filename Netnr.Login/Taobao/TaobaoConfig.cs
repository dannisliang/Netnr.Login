using System;
using System.Collections.Generic;
using System.Text;

namespace Netnr.Login
{
    public class TaobaoConfig
    {
        /// <summary>
        /// 请根据步骤操作：authorize => access_token => get_token_info => users/show
        /// </summary>
        public enum Step
        {
            Step1_Authorize,
            Step2_AccessToken,
            Step3_GetTokenInfo,
            Step4_UserShow
        }

        #region API请求接口

        /// <summary>
        /// GET
        /// </summary>
        public static string API_Authorize = "https://oauth.taobao.com/authorize";

        /// <summary>
        /// POST
        /// </summary>
        public static string API_AccessToken = "https://oauth.taobao.com/token";

        /// <summary>
        /// POST
        /// </summary>
        public static string API_GetTokenInfo = "https://api.weibo.com/oauth2/get_token_info";

        /// <summary>
        /// GET
        /// </summary>
        public static string API_UserShow = "https://api.weibo.com/2/users/show.json";

        #endregion

        /// <summary>
        /// App Key
        /// </summary>
        public static string AppKey = "24745571";

        /// <summary>
        /// App Secret
        /// </summary>
        public static string AppSecret = "afa1dac2c8b9438fccafdd56e0852750";

        /// <summary>
        /// 回调
        /// </summary>
        public static string Redirect_Uri = "https://www.netnr.com/account/taobaologin";
    }
}
