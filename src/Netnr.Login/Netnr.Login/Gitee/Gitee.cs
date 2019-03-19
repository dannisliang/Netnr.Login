using System.Collections.Generic;

namespace Netnr.Login
{
    /// <summary>
    /// 
    /// </summary>
    public class Gitee
    {
        /// <summary>
        /// 请求授权地址
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string AuthorizeHref(Gitee_Authorize_RequestEntity entity)
        {
            if (!LoginBase.IsValid(entity))
            {
                return null;
            }

            return string.Concat(new string[] {
                GiteeConfig.API_Authorize,
                "?client_id=",
                entity.client_id,
                "&response_type=",
                entity.response_type,
                "&redirect_uri=",
                LoginBase.EncodeUri(entity.redirect_uri)});
        }

        /// <summary>
        /// 获取 access token
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Gitee_AccessToken_ResultEntity AccessToken(Gitee_AccessToken_RequestEntity entity)
        {
            if (!LoginBase.IsValid(entity))
            {
                return null;
            }

            string pars = LoginBase.EntityToPars(entity);

            var hwr = LoginBase.HttpTo.HWRequest(GiteeConfig.API_AccessToken, "POST", pars);
            hwr.Accept = "application/json";//application/xml
            string result = LoginBase.HttpTo.Url(hwr);

            var outmo = LoginBase.ResultOutput<Gitee_AccessToken_ResultEntity>(result);

            return outmo;
        }

        /// <summary>
        /// 获取 用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Gitee_User_ResultEntity User(Gitee_User_RequestEntity entity)
        {
            if (!LoginBase.IsValid(entity))
            {
                return null;
            }

            string pars = LoginBase.EntityToPars(entity);

            var hwr = LoginBase.HttpTo.HWRequest(GiteeConfig.API_User + "?" + pars);
            hwr.UserAgent = entity.ApplicationName;
            string result = LoginBase.HttpTo.Url(hwr);

            var outmo = LoginBase.ResultOutput<Gitee_User_ResultEntity>(result, new List<string> { "plan" });

            return outmo;
        }
    }
}