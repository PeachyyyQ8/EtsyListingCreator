using System;
using System.Diagnostics;
using log4net;
using log4net.Core;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using StructureMap;

namespace EtsyListingCreator
{
    internal class DependencyRegistry : Registry
    {

        public DependencyRegistry()
        {
            For<ILogger>().Use(c => new Log4netLogger(c.ParentType)).AlwaysUnique();
        }
    }

    public class Log4netLogger : ILogger
    {
        private readonly ILog logger;

        public Log4netLogger(Type type)
        {
            this.logger = log4net.LogManager.GetLogger(type);
        }

        public void Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Log(LoggingEvent logEvent)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabledFor(Level level)
        {
            throw new NotImplementedException();
        }

        public string Name { get; }
        public ILoggerRepository Repository { get; }
    }
}
