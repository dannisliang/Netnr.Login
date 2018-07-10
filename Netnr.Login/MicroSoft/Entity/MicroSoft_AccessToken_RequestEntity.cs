using System.ComponentModel.DataAnnotations;

namespace Netnr.Login
{
    /// <summary>
    /// access token 请求参数
    /// </summary>
    public class MicroSoft_AccessToken_RequestEntity
    {
        public MicroSoft_AccessToken_RequestEntity()
        {
            client_id = MicroSoftConfig.ClientID;
            client_secret = MicroSoftConfig.ClientSecret;
            redirect_uri = MicroSoftConfig.Redirect_Uri;
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

        [Required]
        public string grant_type { get; set; } = "authorization_code";

        /// <summary>
        /// 调用authorize获得的code值。
        [Required]
        public string code { get; set; }

        /// <summary>
        /// 用于获取 authorization_code 的相同 redirect_uri 值
        /// </summary>
        [Required]
        public string redirect_uri { get; set; }
    }
}
