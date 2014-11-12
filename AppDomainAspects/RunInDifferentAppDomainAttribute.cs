using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace AppDomainAspects
{
    [AttributeUsage(AttributeTargets.Method)]
    [Serializable]
    public sealed class RunInDifferentAppDomainAttribute : OnMethodBoundaryAspect
    {
        public static IAppDomainProvider AppDomainProvider { get; set; }

        public override void OnEntry(MethodExecutionArgs args)
        {
            AppDomain appDomain = (AppDomainProvider ?? DefaultAppDomainProvider.Instance).GetDomain(args);
            if (appDomain == null)
            {
                base.OnEntry(args);
                return;
            }

            args.ReturnValue = InstanceManager.CallMethod(
                appDomain,
                args.Instance,
                args.Method,
                args.Arguments.ToArray());
            args.FlowBehavior = FlowBehavior.Return;
        }
    }
}
