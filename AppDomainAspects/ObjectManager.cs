using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainAspects
{
    [Serializable]
    internal sealed class ObjectManager : IObjectManager
    {
        private readonly ConcurrentDictionary<Guid, object> objects;

        public ObjectManager()
        {
            objects = new ConcurrentDictionary<Guid, object>();
        }

        public Guid CreateObject(Type type, params object[] parameters)
        {
            Guid objectId = Guid.NewGuid();
            objects.TryAdd(objectId, Activator.CreateInstance(type, parameters));
            return objectId;
        }

        public void DestroyObject(Guid objectId)
        {
            object objectToDestroy;
            if (!objects.TryRemove(objectId, out objectToDestroy))
                return;

            IDisposable disposable = objectToDestroy as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }

        public object CallMethod(Guid objectId, MethodBase method, object[] parameters)
        {
            return method.Invoke(objects[objectId], parameters);
        }
    }
}
