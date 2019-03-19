namespace Netnr.Login
{
    /// <summary>
    /// 配置
    /// </summary>
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
        public static string ClientID = "";

        /// <summary>
        /// Client Secret
        /// </summary>
        public static string ClientSecret = "";

        /// <summary>
        /// 回调
        /// </summary>
        public static string Redirect_Uri = "";
    }
}