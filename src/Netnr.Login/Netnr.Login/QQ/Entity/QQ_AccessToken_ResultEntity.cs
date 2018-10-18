namespace Netnr.Login
{
    /// <summary>
    /// 返回说明：
    /// 如果成功返回，即可在返回包中获取到Access Token。 
    /// 如：access_token=FE04************************CCE2&expires_in=7776000&refresh_token=88E4************************BE14
    /// </summary>
    public class QQ_AccessToken_ResultEntity
    {
        /// <summary>
        /// 授权令牌，Access_Token。
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 该access token的有效期，单位为秒。
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// 在授权自动续期步骤中，获取新的Access_Token时需要提供的参数。
        /// </summary>
        public string refresh_token { get; set; }
    }
}
