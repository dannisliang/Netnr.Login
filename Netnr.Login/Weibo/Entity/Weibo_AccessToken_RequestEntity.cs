using System.ComponentModel.DataAnnotations;

namespace Netnr.Login
{
    /// <summary>
    /// Step2：Oauth2/access token
    /// 
    /// Url：http://open.weibo.com/wiki/Oauth2/access_token
    /// </summary>
    public class Weibo_AccessToken_RequestEntity
    {
        public Weibo_AccessToken_RequestEntity()
        {
            client_id = WeiboConfig.AppKey;
            client_secret = WeiboConfig.AppSecret;
            redirect_uri = WeiboConfig.Redirect_Uri;
        }

        /// <summary>
        /// 申请应用时分配的AppKey。
        /// </summary>
        [Required]
        public string client_id { get; set; }

        /// <summary>
        /// 申请应用时分配的AppSecret。
        /// </summary>
        [Required]
        public string client_secret { get; set; }

        private string _grant_type = "authorization_code";
        /// <summary>
        /// 请求的类型，填写authorization_code
        /// </summary>
        [Required]
        public string grant_type { get => _grant_type; set => _grant_type = value; }

        /// <summary>
        /// grant_type为authorization_code时
        /// 调用authorize获得的code值。
        [Required]
        public string code { get; set; }

        /// <summary>
        /// grant_type为authorization_code时
        /// 回调地址，需需与注册应用里的回调地址一致。
        /// </summary>
        [Required]
        public string redirect_uri { get; set; }

    }
}
