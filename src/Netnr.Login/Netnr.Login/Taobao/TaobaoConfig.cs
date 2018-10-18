namespace Netnr.Login
{
    public class TaobaoConfig
    {
        /// <summary>
        /// 请根据步骤操作：authorize => access_token
        /// </summary>
        public enum Step
        {
            Step1_Authorize,
            Step2_AccessToken
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
        
        #endregion

        /// <summary>
        /// App Key
        /// </summary>
        public static string AppKey = "";

        /// <summary>
        /// App Secret
        /// </summary>
        public static string AppSecret = "";

        /// <summary>
        /// 回调
        /// </summary>
        public static string Redirect_Uri = "";
    }
}
