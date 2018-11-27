using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeanLibrary
{
    /// <summary>
    /// desc:Json帮助类
    /// author:xkz
    /// date:2018-09-11
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 将json字符串序列化为对象
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns>对象</returns>
        public static object SerializeToObject(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject(json);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// 将对象序列化为JSON格式串
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            try
            {
                return JsonConvert.SerializeObject(o);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex.Message, ex);
                return null;
            }
        }


        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                StringReader sr = new StringReader(json);
                object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
                T t = o as T;
                return t;
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                StringReader sr = new StringReader(json);
                object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
                List<T> list = o as List<T>;
                return list;
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            try
            {

                return JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(ex.Message, ex);
                return default(T);
            }
        }
    }
}
