using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace Netnr.Login
{
    public class LoginBase
    {
        public class RequestTo
        {
            #region 服务器 发送请求

            /// <summary>
            /// 发送请求参数设置
            /// </summary>
            /// <param name="url">地址</param>
            /// <param name="type">请求类型 默认GET </param>
            /// <param name="postData">POST发送内容 默认空</param>
            /// <returns></returns>
            public static HttpWebRequest HWRequest(string url, string type = "GET", string postData = "")
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = type;
                request.KeepAlive = true;
                request.AllowAutoRedirect = true;
                request.MaximumAutomaticRedirections = 4;
                request.Timeout = short.MaxValue * 3;//MS
                request.ContentType = "application/x-www-form-urlencoded";

                if (type != "GET")
                {
                    //发送内容
                    byte[] bytes = Encoding.UTF8.GetBytes(postData);
                    request.ContentLength = postData.Length;
                    System.IO.Stream outputStream = request.GetRequestStream();
                    outputStream.Write(bytes, 0, bytes.Length);
                    outputStream.Close();
                }
                return request;
            }

            /// <summary>
            /// 发送请求 得到请求结果
            /// </summary>
            /// <param name="request">HttpWebRequest对象 可通过HWRequest方法创建</param>
            /// <param name="e">返回类容编码 默认UTF-8</param>
            /// <returns></returns>
            public static string Url(HttpWebRequest request, Encoding e)
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                if (string.Compare(response.ContentEncoding, "gzip", true) >= 0)
                    responseStream = new System.IO.Compression.GZipStream(responseStream, System.IO.Compression.CompressionMode.Decompress);
                StreamReader reader = new StreamReader(responseStream, e);
                string result = "", strData = "";
                //result = reader.ReadToEnd();
                while ((strData = reader.ReadLine()) != null)
                {
                    result += strData + "\r\n";
                }
                reader.Close();
                responseStream.Close();
                return result;
            }

            /// <summary>
            /// 发送请求 得到请求结果
            /// </summary>
            /// <param name="request">HttpWebRequest对象 可通过HWRequest方法创建</param>
            /// <returns></returns>
            public static string Url(HttpWebRequest request)
            {
                return Url(request, Encoding.UTF8);
            }

            /// <summary>
            /// 发送 GET 请求 得到请求结果
            /// </summary>
            /// <param name="url">URL</param>
            /// <returns></returns>
            public static string Url(string url)
            {
                return Url(HWRequest(url));
            }

            /// <summary>
            /// 发送 POST 请求
            /// </summary>
            /// <param name="url">URL</param>
            /// <param name="postData">发送内容</param>
            /// <returns></returns>
            public static string Url(string url, string postData)
            {
                return Url(HWRequest(url, "POST", postData));
            }

            #endregion
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
        /// <typeparam name="T">输出的实体</typeparam>
        /// <param name="result">请求的结果</param>
        /// <param name="rtype">处理的类型，默认JObject</param>
        /// <param name="resultNeedJObject">子节点需要JObject转化</param>
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
        /// <param name="obj">要验证的对象</param>
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

        /// <summary>
        /// 流写入
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="path">物理目录</param>
        /// <param name="fileName">文件名</param>
        /// <param name="type">写入类型 默认 追加 cover 则覆盖</param>
        public static void WriteText(string content, string path, string fileName, string type = "add")
        {
            FileStream fs;

            //检测目录
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                fs = new FileStream(path + fileName, FileMode.Create);
            }
            else
            {
                //文件是否存在 创建 OR 追加
                if (!File.Exists(path + fileName))
                {
                    fs = new FileStream(path + fileName, FileMode.Create);
                }
                else
                {
                    FileMode fm = type == "add" ? FileMode.Append : FileMode.Truncate;
                    fs = new FileStream(path + fileName, fm);
                }
            }

            //流写入
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.WriteLine(content);
            sw.Close();
            fs.Close();
        }
    }
}
