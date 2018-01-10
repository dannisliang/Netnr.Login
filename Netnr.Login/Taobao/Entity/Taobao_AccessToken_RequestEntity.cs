using System.ComponentModel.DataAnnotations;

namespace Netnr.Login
{
    /// <summary>
    /// Step2：Oauth2/access token
    /// </summary>
    public class Taobao_AccessToken_RequestEntity
    {
        public Taobao_AccessToken_RequestEntity()
        {
            client_id = TaobaoConfig.AppKey;
            client_secret = TaobaoConfig.AppSecret;
            redirect_uri = TaobaoConfig.Redirect_Uri;
            state = System.Guid.NewGuid().ToString("N");
            view = "web";
        }

        /// <summary>
        /// 等同于AppKey，创建应用时获得。
        /// </summary>
        [Required]
        public string client_id { get; set; }

        /// <summary>
        /// 等同于AppSecret，创建应用时获得。
        /// </summary>
        [Required]
        public string client_secret { get; set; }


        private string _grant_type = "authorization_code";
        /// <summary>
        /// authorization_code	授权类型 ，值为authorization_code
        /// </summary>
        [Required]
        public string grant_type { get => _grant_type; set => _grant_type = value; }

        /// <summary>
        /// 上一步获取code得到
        /// </summary>
        [Required]
        public string code { get; set; }

        /// <summary>
        /// 可填写应用注册时回调地址域名。
        /// redirect_uri指的是应用发起请求时，所传的回调地址参数，在用户授权后应用会跳转至redirect_uri。
        /// 要求与应用注册时填写的回调地址域名一致或顶级域名一致 。
        /// </summary>
        [Required]
        public string redirect_uri { get; set; }

        /// <summary>
        /// 可自定义，如1212等；维持应用的状态，传入值与返回值保持一致。
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 可选web、tmall或wap其中一种，
        /// Web对应PC端（淘宝logo）浏览器页面样式；
        /// Tmall对应天猫的浏览器页面样式；
        /// Wap对应无线端的浏览器页面样式。
        /// </summary>
        public string view { get; set; }

    }
}
