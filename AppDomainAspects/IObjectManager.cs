using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainAspects
{
    internal interface IObjectManager
    {
        Guid CreateObject(Type type, params object[] parameters);

        void DestroyObject(Guid objectId);

        object CallMethod(Guid objectId, MethodBase method, object[] parameters);
    }
}
