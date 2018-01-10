using System;
using System.Collections.Generic;
using System.Text;

namespace Netnr.Login
{
    public class QQConfig
    {
        /// <summary>
        /// 仅做说明用
        /// 
        /// 请根据步骤操作：Authorization => AccessToken => OpenId => OpenAPI（get_user_info）
        /// </summary>
        public enum Step
        {
            Step1_Authorization,
            Step2_AccessToken,
            Step3_OpenId,
            Step4_OpenAPI
        }

        #region API请求接口

        /// <summary>
        /// PC网站，GET
        /// </summary>
        public static string API_Authorization_PC = "https://graph.qq.com/oauth2.0/authorize";

        /// <summary>
        /// PC网站，GET
        /// </summary>
        public static string API_AccessToken_PC = "https://graph.qq.com/oauth2.0/token";
        /// <summary>
        /// WAP网站，GET
        /// </summary>
        public static string API_AccessToken_WAP = "https://graph.z.qq.com/moc2/token";

        /// <summary>
        /// PC GET
        /// </summary>
        public static string API_OpenID_PC = "https://graph.qq.com/oauth2.0/me";

        /// <summary>
        /// WAP GET
        /// </summary>
        public static string API_OpenID_WAP = "https://graph.z.qq.com/moc2/me";

        /// <summary>
        /// GET
        /// </summary>
        public static string API_Get_User_Info = "https://graph.qq.com/user/get_user_info";

        #endregion
        
        /// <summary>
        /// APP ID
        /// </summary>
        public static string APPID = "101219614";

        /// <summary>
        /// APP Key
        /// </summary>
        public static string APPKey = "798716476f7c189ca301c81c6e60024a";

        /// <summary>
        /// 回调
        /// </summary>
        public static string Redirect_Uri = "https://www.netnr.com/account/qqlogin";
    }
}
