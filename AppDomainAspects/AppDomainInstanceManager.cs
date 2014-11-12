using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using AppDomainCallbackExtensions;
using TextSerialization;

namespace AppDomainAspects
{
    internal sealed class AppDomainInstanceManager
    {
        private readonly IObjectManager objectManager;
        private readonly ConcurrentDictionary<object, Guid> instanceDictionary;

        public AppDomainInstanceManager(AppDomain domain)
        {
            objectManager = domain.CreateProxy<ObjectManager, IObjectManager, BinarySerialization>(
                WellKnownObjectMode.Singleton,
                new BinarySerialization());
            instanceDictionary = new ConcurrentDictionary<object, Guid>();
        }

        public void CreateInstance(object instance, object[] parameters)
        {
            instanceDictionary.GetOrAdd(
                instance,
                localInstance => objectManager.CreateObject(localInstance.GetType(), parameters));
        }

        public void DestroyInstance(object instance)
        {
            Guid objectId;
            if (instanceDictionary.TryRemove(instance, out objectId))
            {
                objectManager.DestroyObject(objectId);
            }
        }

        public object CallMethod(object instance, MethodBase method, object[] parameters)
        {
            Guid objectId = instanceDictionary.GetOrAdd(instance, CreateRemoteInstance);
            return objectManager.CallMethod(objectId, method, parameters);
        }

        private Guid CreateRemoteInstance(object localInstance)
        {
            return objectManager.CreateObject(localInstance.GetType());
        }
    }
}
