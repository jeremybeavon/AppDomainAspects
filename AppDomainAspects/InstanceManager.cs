using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainAspects
{
    internal static class InstanceManager
    {
        private static readonly ConcurrentDictionary<AppDomain, AppDomainInstanceManager> instanceManagers =
            new ConcurrentDictionary<AppDomain, AppDomainInstanceManager>();

        public static void CreateInstance(AppDomain domain, object instance, object[] parameters)
        {
            GetInstanceManager(domain).CreateInstance(instance, parameters);
        }

        public static void DestroyInstance(AppDomain domain, object instance)
        {
            GetInstanceManager(domain).DestroyInstance(instance);
        }

        public static object CallMethod(AppDomain domain, object instance, MethodBase method, object[] parameters)
        {
            return GetInstanceManager(domain).CallMethod(instance, method, parameters);
        }

        private static AppDomainInstanceManager GetInstanceManager(AppDomain domain)
        {
            return instanceManagers.GetOrAdd(domain, CreateInstanceManager);
        }

        private static AppDomainInstanceManager CreateInstanceManager(AppDomain domain)
        {
            /*domain.DomainUnload += (sender, args) =>
            {
                AppDomainInstanceManager manager;
                instanceManagers.TryRemove(domain, out manager);
            };*/
            return new AppDomainInstanceManager(domain);
        }
    }
}
