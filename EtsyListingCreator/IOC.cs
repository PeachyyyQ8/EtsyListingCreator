using StructureMap;
using System;
using log4net;
using log4net.Core;

namespace EtsyListingCreator
{

    internal class IOC
    {
        private Container _container;

        public void Configure()
        {
            var _container = new Container(new DependencyRegistry());

        }

        public object GetInstance(Type type)
        {
            return _container.GetInstance(type);
        }
    }
}
