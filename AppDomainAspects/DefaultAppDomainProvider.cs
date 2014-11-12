using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace AppDomainAspects
{
    public sealed class DefaultAppDomainProvider : IAppDomainProvider
    {
        internal static readonly IAppDomainProvider Instance = new DefaultAppDomainProvider();

        public static AppDomain AppDomain { get; set; }

        public AppDomain GetDomain(MethodExecutionArgs args)
        {
            return AppDomain;
        }
    }
}
