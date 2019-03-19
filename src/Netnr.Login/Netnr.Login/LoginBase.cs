using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace Netnr.Login
{
    /// <summary>
    /// 辅助方法
    /// </summary>
    public class LoginBase
    {
        /// <summary>
        /// 登录类型枚举
        /// </summary>
        public enum LoginType
        {
            QQ,
            WeiBo,
            WeChat,
            GitHub,
            TaoBao,
            MicroSoft
        }

        /// <summary>
        /// HTTP请求
        /// </summary>
        public class HttpTo
        {
            /// <summary>
            /// HttpWebRequest对象
            /// </summary>
            /// <param name="url">地址</param>
            /// <param name="type">请求类型，默认GET</param>
            /// <param name="data">POST数据</param>
            /// <param name="charset">编码，默认utf-8</param>
            /// <returns></returns>
            public static HttpWebRequest HWRequest(string url, string type = "GET", string data = null, string charset = "utf-8")
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = type;
                request.KeepAlive = true;
                request.AllowAutoRedirect = true;
                request.MaximumAutomaticRedirections = 4;
                request.Timeout = short.MaxValue * 3;//MS
                request.ContentType = "application/x-www-form-urlencoded";

                if (type != "GET" && data != null)
                {
                    //发送内容
                    byte[] bytes = Encoding.GetEncoding(charset).GetBytes(data);
                    request.ContentLength = Encoding.GetEncoding(charset).GetBytes(data).Length;
                    Stream outputStream = request.GetRequestStream();
                    outputStream.Write(bytes, 0, bytes.Length);
                    outputStream.Close();
                }
                return request;
            }

            /// <summary>
            /// HTTP请求
            /// </summary>
            /// <param name="request">HttpWebRequest对象</param>
            /// <param name="charset">编码，默认utf-8</param>
            /// <returns></returns>
            public static string Url(HttpWebRequest request, string charset = "utf-8")
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                if (string.Compare(response.ContentEncoding, "gzip", true) >= 0)
                    responseStream = new System.IO.Compression.GZipStream(responseStream, System.IO.Compression.CompressionMode.Decompress);

                using (var sr = new StreamReader(responseStream, Encoding.GetEncoding(charset)))
                {
                    var result = sr.ReadToEnd();
                    return result;
                }
            }

            /// <summary>
            /// GET请求
            /// </summary>
            /// <param name="url">地址</param>
            /// <param name="charset">编码，默认utf-8</param>
            /// <returns></returns>
            public static string Get(string url, string charset = "utf-8")
            {
                var request = HWRequest(url, "GET", null, charset);
                return Url(request, charset);
            }

            /// <summary>
            /// POST请求
            /// </summary>
            /// <param name="url">地址</param>
            /// <param name="data">POST数据</param>
            /// <param name="charset">编码，默认utf-8</param>
            /// <returns></returns>
            public static string Post(string url, string data, string charset = "utf-8")
            {
                var request = HWRequest(url, "POST", data, charset);
                return Url(request, charset);
            }
        }

        /// <summary>
        /// 实体 转 Pars
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string EntityToPars<T>(T entity)
        {
            string result = string.Empty;
            var pis = entity.GetType().GetProperties();
            foreach (var pi in pis)
            {
                string value = pi.GetValue(entity, null)?.ToString();
                if (value != null)
                {
                    result += "&" + pi.Name + "=" +
                        LoginBase.EncodeUri(value);
                }
            }
            return result.TrimStart('&');
        }

        /// <summary>
        /// 处理结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">请求的结果</param>
        /// <param name="resultNeedJObject">处理的类型，默认JObject</param>
        /// <returns></returns>
        public static T ResultOutput<T>(string result, List<string> resultNeedJObject = null) where T : class, new()
        {
            var mo = new T();
            var pis = mo.GetType().GetProperties();
            var jo = JObject.Parse(result);
            foreach (var pi in pis)
            {
                object value;
                try
                {
                    Type type;
                    if (pi.PropertyType.FullName.Contains("System.Nullable"))
                    {
                        type = Type.GetType("System." + pi.PropertyType.FullName.Split(',')[0].Split('.')[2]);
                    }
                    else
                    {
                        type = pi.PropertyType;
                    }
                    value = Convert.ChangeType(jo[pi.Name].ToString(), type);
                }
                catch (Exception)
                {
                    value = null;
                }

                if (resultNeedJObject?.Count > 0)
                {
                    try
                    {
                        if (resultNeedJObject.Contains(pi.Name))
                        {
                            value = JObject.Parse(jo[pi.Name].ToString());
                        }
                    }
                    catch (Exception)
                    {
                        value = null;
                    }
                }
                pi.SetValue(mo, value, null);
            }

            return mo;
        }

        /// <summary>
        /// 验证对象是否有效
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsValid<T>(T entity) where T : new()
        {
            bool b = true;
            var reqName = typeof(Required).Name;
            var pis = entity.GetType().GetProperties();
            foreach (var pi in pis)
            {
                var isReq = false;
                object[] attrs = pi.GetCustomAttributes(true);
                foreach (var attr in attrs)
                {
                    var agt = attr.GetType();
                    if (agt.Name == reqName)
                    {
                        isReq = true;
                        break;
                    }
                }
                if (isReq)
                {
                    var value = pi.GetValue(entity, null);
                    if (value == null || value.ToString() == "")
                    {
                        b = false;
                        break;
                    }
                }
            }
            return b;
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string EncodeUri(string uri, string charset = "utf-8")
        {
            string URL_ALLOWED_CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

            if (string.IsNullOrEmpty(uri))
                return string.Empty;

            const string escapeFlag = "%";
            var encodedUri = new StringBuilder(uri.Length * 2);
            var bytes = Encoding.GetEncoding(charset).GetBytes(uri);
            foreach (var b in bytes)
            {
                char ch = (char)b;
                if (URL_ALLOWED_CHARS.IndexOf(ch) != -1)
                    encodedUri.Append(ch);
                else
                {
                    encodedUri.Append(escapeFlag).Append(string.Format(CultureInfo.InstalledUICulture, "{0:X2}", (int)b));
                }
            }
            return encodedUri.ToString();
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="uriToDecode"></param>
        /// <returns></returns>
        public static string DecodeUri(string uriToDecode)
        {
            if (!string.IsNullOrEmpty(uriToDecode))
            {
                uriToDecode = uriToDecode.Replace("+", " ");
                return Uri.UnescapeDataString(uriToDecode);
            }

            return string.Empty;
        }
    }
}