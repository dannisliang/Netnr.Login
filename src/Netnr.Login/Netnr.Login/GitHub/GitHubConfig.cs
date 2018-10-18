namespace Netnr.Login
{
    public class GitHubConfig
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
        public static string API_Authorize = "https://github.com/login/oauth/authorize";

        /// <summary>
        /// POST
        /// </summary>
        public static string API_AccessToken = "https://github.com/login/oauth/access_token";

        /// <summary>
        /// GET
        /// </summary>
        public static string API_User = "https://api.github.com/user";

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
        public static string Redirect_Uri = "https://www.netnr.com/account/githublogin";

        /// <summary>
        /// github 申请的应用名称
        /// </summary>
        public static string ApplicationName = "";
    }
}
