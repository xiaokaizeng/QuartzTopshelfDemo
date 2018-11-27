using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SeanLibrary
{
    /// <summary>
    /// Description:通用类库
    /// Author:xkz
    /// Date:2018-09-05
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 地球半径，单位米
        /// </summary>
        private const double EARTH_RADIUS = 6378137;

        #region 加解密

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            var algorithm = System.Security.Cryptography.MD5.Create();
            var data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(str));

            var sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }

            return sb.ToString().ToLower();
        }

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string SHA1(string str)
        {
            var algorithm = System.Security.Cryptography.SHA1.Create();
            var data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(str));

            var sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }

            return sb.ToString().ToLower();
        }

        /// <summary>
        /// HMACSHA1加密并返回ToBase64String
        /// </summary>
        /// <param name="key">密钥参数</param>
        /// <param name="str">返回一个签名值(即哈希值)</param>
        /// <returns>返回一个签名值(即哈希值)</returns>
        public static string HmacSha1Sign(string key, string str)
        {
            try
            {
                var hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(key));
                var hashBytes = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(str));
                return Convert.ToBase64String(hashBytes);
            }
            catch
            {
                return str;
            }
        }

        #endregion

        #region Web相关

        /// <summary>
        /// 获取web.xml下的appSettings配置
        /// </summary>
        /// <param name="vname">key</param>
        /// <returns>value</returns>
        public static string GetAppSettings(string vname)
        {
            try
            {
                return ConfigurationManager.AppSettings[vname];
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取Post传值
        /// </summary>
        /// <param name="vname">变量名称</param>
        /// <returns>Post值</returns>
        public static string GetPost(string vname)
        {
            try
            {
                return HttpContext.Current.Request.Form[vname].ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取Get传值
        /// </summary>
        /// <param name="vname">变量名称</param>
        /// <returns>Get值</returns>
        public static string GetQueryString(string vname)
        {
            try
            {
                return HttpContext.Current.Request.QueryString[vname].ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取Session变量的值
        /// </summary>
        /// <param name="session_name">Session变量名</param>
        /// <returns>Session值</returns>
        public static string GetSession(string session_name)
        {
            try
            {
                return HttpContext.Current.Session[session_name].ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 读取Cookies
        /// </summary>
        /// <param name="varname">Cookie变量名</param>
        /// <returns></returns>
        public static string CookiesRead(string varname)
        {
            string result = string.Empty;

            if (HttpContext.Current.Request.Cookies.Count > 0)
            {
                if (HttpContext.Current.Request.Cookies[varname] != null)
                {
                    result = HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[varname].Value);
                }
            }

            return result;
        }

        /// <summary>
        /// 创建Cookies
        /// </summary>
        /// <param name="varname">Cookie变量名</param>
        /// <param name="cvalue">Cookie值</param>
        public static void CookiesWrite(string varname, string cvalue)
        {
            HttpCookie mycookie = new HttpCookie(varname)
            {
                Value = HttpUtility.UrlEncode(cvalue),
                Path = "/"
            };

            HttpContext.Current.Response.Cookies.Add(mycookie);
        }

        /// <summary>
        /// 创建Cookies
        /// </summary>
        /// <param name="varname">Cookie变量名</param>
        /// <param name="cvalue">Cookie值</param>
        /// <param name="domain">对应的域名</param>
        /// <param name="path">对应的路径</param>
        /// <param name="time">过期时间</param>
        /// <param name="tt">过期的时间单位(默认：秒)</param>
        public static void CookiesWrite(string varname, string cvalue, string domain, string path, double time, TimeType tt)
        {
            if (path == string.Empty) path = "/";

            HttpCookie mycookie = new HttpCookie(varname)
            {
                Value = HttpUtility.UrlEncode(cvalue),
                Path = path,
                Domain = domain,
                Secure = false
            };
            if (time != 0)
            {
                switch (tt)
                {
                    case TimeType.Second:
                        mycookie.Expires.AddSeconds(time);
                        break;
                    case TimeType.Minute:
                        mycookie.Expires.AddMinutes(time);
                        break;
                    case TimeType.Hour:
                        mycookie.Expires.AddHours(time);
                        break;
                    case TimeType.Day:
                        mycookie.Expires.AddDays(time);
                        break;
                    default:
                        mycookie.Expires.AddSeconds(time);
                        break;
                }
            }

            HttpContext.Current.Response.Cookies.Add(mycookie);
        }

        /// <summary>
        /// 提取URL域名
        /// </summary>
        public static string GetDomain(string url)
        {
            try
            {
                var temp = url;
                var domain = string.Empty;
                domain = Regex.Match(temp, @"(?<=http://)[\w\.\-]+[^/]").Value.ToString();

                return domain;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 提取当前请求URL的域名
        /// </summary>
        /// <returns></returns>
        public static string GetDomain()
        {
            try
            {
                var url = HttpContext.Current.Request.Url.ToString();

                return GetDomain(url);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取请求的IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            try
            {
                string realip = string.Empty;

                //realip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString().Trim();

                //if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                //{
                //    string ips = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Trim();
                //    string[] iparr = ips.Split(new char[] { ',' });
                //    if (iparr != null && iparr.Length > 0)
                //    {
                //        realip = iparr[iparr.Length - 1];
                //    }
                //}


                string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (ip == null || ip.Length == 0 || ip.ToLower().IndexOf("unknown") > -1)
                {

                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                }

                else
                {

                    if (ip.IndexOf(',') > -1)
                    {

                        ip = ip.Substring(0, ip.IndexOf(','));

                    }

                    if (ip.IndexOf(';') > -1)
                    {

                        ip = ip.Substring(0, ip.IndexOf(';'));

                    }

                }



                Regex regex = new Regex("[^0-9.]");

                if (ip == null || ip.Length == 0 || regex.IsMatch(ip))
                {

                    ip = HttpContext.Current.Request.UserHostAddress;

                    if (ip == null || ip.Length == 0 || regex.IsMatch(ip))
                    {

                        ip = "0.0.0.0";

                    }

                }

                realip = ip;

                return realip;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 时间类型值枚举
        /// </summary>
        public enum TimeType
        {
            /// <summary>
            /// 秒钟
            /// </summary>
            Second,
            /// <summary>
            /// 分钟
            /// </summary>
            Minute,
            /// <summary>
            /// 小时
            /// </summary>
            Hour,
            /// <summary>
            /// 天
            /// </summary>
            Day
        }


        /// <summary>
        /// Post数据接口
        /// </summary>
        /// <param name="postUrl">接口地址</param>
        /// <param name="paramData">提交json数据</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        public static string Post(string postUrl, string paramData, Encoding dataEncode)
        {
            try
            {
                string ret = string.Empty;
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                //req.Host = GetAppSettings("wx_url");
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), dataEncode);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
                return ret;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Post数据接口
        /// </summary>
        /// <param name="url">接口地址</param>
        /// <param name="dic">提交Dictionary数据(?x=a&y=b)</param>
        public static string Post(string url, Dictionary<string, string> dic)
        {
            try
            {
                string result = string.Empty;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                //req.Host = GetAppSettings("wx_url");
                req.ContentType = "application/x-www-form-urlencoded";

                #region 添加Post 参数
                StringBuilder builder = new StringBuilder();
                int i = 0;

                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }

                byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }

                #endregion

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容  
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region 腾讯地图API进行IP定位

        /// <summary>
        /// 腾讯地图API进行IP定位
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        public static string IPToAddress(string ip)
        {
            try
            {
                var result = string.Empty;
                //腾讯地图API进行IP定位URL
                var url = "https://apis.map.qq.com/ws/location/v1/ip?ip=" + ip + "&key=3W2BZ-43333-WUJ3C-YAK7P-BRQWF-BNFUK";

                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.ContentType = "text/html";

                WebResponse response = request.GetResponse();
                Encoding enc = Encoding.UTF8;
                StreamReader sr = new StreamReader(response.GetResponseStream(), enc);
                var get_str = sr.ReadToEnd();
                var obj = JsonHelper.DeserializeJsonToObject<Ip2Address>(get_str);

                if (string.IsNullOrEmpty(get_str))
                {
                    return string.Empty;
                }

                if (null != obj)
                {
                    if (obj.Status != 0)
                    {
                        return obj.Message;
                    }
                    else
                    {
                        return $"{obj.Result.Ad_info.Province},{obj.Result.Ad_info.City}";
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// IP定位返回模型
        /// </summary>
        public class Ip2Address
        {
            /// <summary>
            /// 返回状态（成功：0）
            /// </summary>
            public int Status { get; set; }
            /// <summary>
            /// 返回消息
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// 返回结果
            /// </summary>
            public Ip2AddressResult Result { get; set; }
        }
        /// <summary>
        /// IP定位返回结果模型
        /// </summary>
        public class Ip2AddressResult
        {
            /// <summary>
            /// IP
            /// </summary>
            public string Ip { get; set; }
            /// <summary>
            /// 定位
            /// </summary>
            public Location Location { get; set; }
            /// <summary>
            /// 地址
            /// </summary>
            public Ad_info Ad_info { get; set; }
        }
        /// <summary>
        /// IP定位返回结果定位模型
        /// </summary>
        public class Location
        {
            /// <summary>
            /// 纬度
            /// </summary>
            public double Lat { get; set; }
            /// <summary>
            /// 经度
            /// </summary>
            public double Lng { get; set; }
        }
        /// <summary>
        /// IP定位返回结果地址模型
        /// </summary>
        public class Ad_info
        {
            /// <summary>
            /// 国家
            /// </summary>
            public string Nation { get; set; }
            /// <summary>
            /// 省
            /// </summary>
            public string Province { get; set; }
            /// <summary>
            /// 市
            /// </summary>
            public string City { get; set; }
            /// <summary>
            /// 县/区
            /// </summary>
            public string District { get; set; }
            /// <summary>
            /// 行政区划码
            /// </summary>
            public int Adcode { get; set; }
        }

        #endregion

        #region 地图相关

        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位：米；
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d">经纬度值</param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }

        #endregion
    }
}
