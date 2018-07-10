using System;
using System.Collections.Generic;
using System.Text;

namespace Netnr.Login
{
    public class MicroSoftConfig
    {
        /// <summary>
        /// 请根据步骤操作：authorize => access_token => user
        /// </summary>
        public enum Step
        {
            Step1_Authorize,
            Step2_AccessToken,
            Step3_User
        }

        #region API请求接口

        /// <summary>
        /// GET
        /// </summary>
        public static string API_Authorize = "https://login.live.com/oauth20_authorize.srf";

        /// <summary>
        /// POST
        /// </summary>
        public static string API_AccessToken = "https://login.live.com/oauth20_token.srf";

        /// <summary>
        /// GET
        /// </summary>
        public static string API_User = "https://apis.live.net/v5.0/me";

        #endregion

        /// <summary>
        /// Client ID
        /// </summary>
        public static string ClientID = "a96e90ce-57c8-496b-b943-b31e4f1184d2";

        /// <summary>
        /// Client Secret
        /// </summary>
        public static string ClientSecret = "ofyOVZG052};diiaAEF13_!";

        /// <summary>
        /// 回调
        /// </summary>
        public static string Redirect_Uri = "https://www.netnr.com/account/microsoftlogin";
    }
}
