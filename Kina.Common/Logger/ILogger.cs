using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kina.Common.Logger
{
    /// <summary>
    /// 日志接口(调试，一般，错误，警告，严重故障）
    /// </summary>
    public interface ILogger
    {
        void Debug(object message);
        void DebugFormat(string format, params object[] args);
        void Debug(object message, Exception exception);

        void Info(object message);
        void InfoFormat(string format, params object[] args);
        void Info(object message, Exception exception);

        void Error(object message);
        void ErrorFormat(string format, params object[] args);
        void Error(object message, Exception exception);

        void Warn(object message);
        void WarnFormat(string format, params object[] args);
        void Warn(object message, Exception exception);

        void Fatal(object message);
        void FatalFormat(string format, params object[] args);
        void Fatal(object message, Exception exception);
    }
}
