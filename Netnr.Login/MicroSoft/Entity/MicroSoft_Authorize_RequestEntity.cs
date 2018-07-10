using System;
using System.ComponentModel.DataAnnotations;

namespace Netnr.Login
{
    /// <summary>
    /// 
    /// Step1：获取authorize Code
    /// 
    /// </summary>
    public class MicroSoft_Authorize_RequestEntity
    {
        public MicroSoft_Authorize_RequestEntity()
        {
            client_id = MicroSoftConfig.ClientID;
            redirect_uri = MicroSoftConfig.Redirect_Uri;
            state = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 注册应用时的获取的client_id
        /// </summary>
        [Required]
        public string client_id { get; set; }

        /// <summary>
        /// 必须包括授权代码流的 code
        /// </summary>
        [Required]
        public string response_type { get; set; } = "code";

        /// <summary>
        /// 作用域
        /// </summary>
        [Required]
        public string scope { get; set; } = "wl.signin";


        /// <summary>
        /// 鉴权成功之后，重定向到网站
        /// </summary>
        [Required]
        public string redirect_uri { get; set; }

        
        /// <summary>
        /// 自己设定，用于防止跨站请求伪造攻击
        /// </summary>
        [Required]
        public string state { get; set; }
    }
}
