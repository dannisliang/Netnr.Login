using System.ComponentModel.DataAnnotations;

namespace Netnr.Login
{
    /// <summary>
    /// access token 请求参数
    /// </summary>
    public class GitHub_AccessToken_RequestEntity
    {
        public GitHub_AccessToken_RequestEntity()
        {
            client_id = GitHubConfig.ClientID;
            client_secret = GitHubConfig.ClientSecret;
            redirect_uri = GitHubConfig.Redirect_Uri;
        }

        /// <summary>
        /// 注册应用时的获取的client_id
        /// </summary>
        [Required]
        public string client_id { get; set; }

        /// <summary>
        /// 注册应用时的获取的client_secret。
        /// </summary>
        [Required]
        public string client_secret { get; set; }

        /// <summary>
        /// 调用authorize获得的code值。
        [Required]
        public string code { get; set; }

        /// <summary>
        /// 回调地址，需需与注册应用里的回调地址一致。
        /// </summary>
        [Required]
        public string redirect_uri { get; set; }

        /// <summary>
        /// Step1 回传的值
        /// </summary>
        public string state { get; set; }
    }
}
