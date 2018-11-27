using log4net;
using System;

namespace SeanLibrary
{
    public class Log4NetHelper
    {
        private static readonly ILog loginfo = LogManager.GetLogger("loginfo");

        private static readonly ILog logerror = LogManager.GetLogger("logerror");

        private static readonly ILog logdebug = LogManager.GetLogger("logdebug");

        private static readonly ILog logwarn = LogManager.GetLogger("logwarn");

        #region Info

        /// <summary>
        /// 写入Info日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="ex"></param>
        public static void Info(string msg, Exception ex)
        {
            loginfo.Info(msg, ex);
        }

        /// <summary>
        /// 写入Info日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Info(string msg)
        {
            loginfo.Info(msg);
        }

        /// <summary>
        /// InfoFormat
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void InfoFormat(string format, object[] args)
        {
            loginfo.InfoFormat(format, args);
        }

        #endregion

        #region Error

        /// <summary>
        /// 写入Error日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="ex"></param>
        public static void Error(string msg, Exception ex)
        {
            logerror.Error(msg, ex);
        }

        /// <summary>
        /// 写入Error日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Error(string msg)
        {
            logerror.Error(msg);
        }

        /// <summary>
        /// ErrorFormat
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void ErrorFormat(string format, object[] args)
        {
            logerror.ErrorFormat(format, args);
        }

        #endregion

        #region Debug

        /// <summary>
        /// 写入Debug日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="ex"></param>
        public static void Debug(string msg, Exception ex)
        {
            logdebug.Debug(msg, ex);
        }

        /// <summary>
        /// 写入Debug日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Debug(string msg)
        {
            logdebug.Debug(msg);
        }

        /// <summary>
        /// DebugFormat
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void DebugFormat(string format, object[] args)
        {
            logdebug.DebugFormat(format, args);
        }

        #endregion

        #region Warn

        /// <summary>
        /// 写入Warn日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="ex"></param>
        public static void Warn(string msg, Exception ex)
        {
            logwarn.Warn(msg, ex);
        }

        /// <summary>
        /// 写入Warn日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Warn(string msg)
        {
            logwarn.Warn(msg);
        }

        /// <summary>
        /// WarnFormat
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void WarnFormat(string format, object[] args)
        {
            logwarn.WarnFormat(format, args);
        }

        #endregion
    }
}
