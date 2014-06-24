using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kina.Common.Logger
{
    public class EmptyLoggerFactory : ILoggerFactory
    {
        private static readonly EmptyLogger Logger = new EmptyLogger();

        public ILogger Create(string name)
        {
            return Logger; 
        }
        
        public ILogger Create(Type type)
        {
            return Logger;
        }
    }
}
