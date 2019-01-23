using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Netnr.Login.WeChat
{
    public class WeChat
    {
        /// <summary>
        /// Step1：获取Authorization Code
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string AuthorizationHref(WeChat_Authorization_RequestEntity entity)
        {
            if (!LoginBase.IsValid(entity))
            {
                return null;
            }

            return string.Concat(new string[] {
                WeChatConfig.API_Authorization,
                "?appid=",
                entity.appid,
                "&response_type=",
                entity.response_type,
                "&scope=",
                entity.scope,
                "&state=",
                entity.state,
                "&redirect_uri=",
                LoginBase.EncodeUri(entity.redirect_uri)});
        }

        /// <summary>
        /// Step2：通过Authorization Code获取Access Token、openid
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static WeChat_AccessToken_ResultEntity AccessToken(WeChat_AccessToken_RequestEntity entity)
        {
            if (!LoginBase.IsValid(entity))
            {
                return null;
            }

            string pars = LoginBase.EntityToPars(entity);

            string result = LoginBase.HttpTo.Url(WeChatConfig.API_AccessToken + "?" + pars);

            List<string> listPars = result.Split('&').ToList();
            var jo = new JObject();
            foreach (string item in listPars)
            {
                var items = item.Split('=').ToList();
                jo[items.FirstOrDefault()] = items.LastOrDefault();
            }

            var outmo = LoginBase.ResultOutput<WeChat_AccessToken_ResultEntity>(Newtonsoft.Json.JsonConvert.SerializeObject(jo));

            return outmo;
        }

        /// <summary>
        /// Step3：获取用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static WeChat_OpenId_get_user_info_ResultEntity Get_User_Info(WeChat_OpenAPI_RequestEntity entity)
        {
            if (!LoginBase.IsValid(entity))
            {
                return null;
            }

            string pars = LoginBase.EntityToPars(entity);
            string result = LoginBase.HttpTo.Url(WeChatConfig.API_UserInfo + "?" + pars);

            var outmo = LoginBase.ResultOutput<WeChat_OpenId_get_user_info_ResultEntity>(result.Replace("\r\n", ""));

            return outmo;
        }
    }
}
