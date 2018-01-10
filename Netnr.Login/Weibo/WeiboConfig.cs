using System;
using System.Collections.Generic;
using System.Text;

namespace Netnr.Login
{
    public class WeiboConfig
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
        public static string API_Authorize = "https://api.weibo.com/oauth2/authorize";

        /// <summary>
        /// POST
        /// </summary>
        public static string API_AccessToken = "https://api.weibo.com/oauth2/access_token";
        
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
        public static string AppKey = "717256243";

        /// <summary>
        /// App Secret
        /// </summary>
        public static string AppSecret = "dde115ded861189163bf9430a7825de2";

        /// <summary>
        /// 回调
        /// </summary>
        public static string Redirect_Uri = "https://www.netnr.com/account/weibologin";

    }
}
