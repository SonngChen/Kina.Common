using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kina.Common.Logger
{
    public class EmptyLogger : ILogger
    {
        public bool IsDebugEnabled
        {
            get { return false; }
        }

        public void Debug(object message)   
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void DebugFormat(string format, params object[] args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Debug(object message, Exception exception)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Info(object message)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void InfoFormat(string format, params object[] args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Info(object message, Exception exception)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Error(object message)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void ErrorFormat(string format, params object[] args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Error(object message, Exception exception)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Warn(object message)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void WarnFormat(string format, params object[] args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Warn(object message, Exception exception)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Fatal(object message)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void FatalFormat(string format, params object[] args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Fatal(object message, Exception exception)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
