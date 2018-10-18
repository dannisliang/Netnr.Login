namespace Netnr.Login
{
    public class Taobao
    {
        /// <summary>
        /// 
        /// Step1：请求用户授权Token
        /// 
        /// A标签href="https://oauth.taobao.com/authorize?response_type=code&client_id=xxx&redirect_uri=回调地址&state=xxx&view=web"
        ///    
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string AuthorizeHref(Taobao_Authorize_RequestEntity entity)
        {
            if (!LoginBase.IsValid(entity))
            {
                return null;
            }

            return string.Concat(new string[] {
                TaobaoConfig.API_Authorize,
                "?response_type=",
                entity.response_type,
                "&client_id=",
                entity.client_id,
                "&redirect_uri=",
                LoginBase.EncodeUri(entity.redirect_uri),
                "&state=",
                entity.state,
                "&view=",
                entity.view});
        }

        /// <summary>
        /// 
        /// Step2：获取授权过的Access Token
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Taobao_AccessToken_ResultEntity AccessToken(Taobao_AccessToken_RequestEntity entity)
        {
            if (!LoginBase.IsValid(entity))
            {
                return null;
            }

            string pars = LoginBase.EntityToPars(entity);
            
            string result = LoginBase.RequestTo.Url(TaobaoConfig.API_AccessToken, pars);
            
            var outmo = LoginBase.ResultOutput<Taobao_AccessToken_ResultEntity>(result);

            return outmo;
        }        
    }
}
