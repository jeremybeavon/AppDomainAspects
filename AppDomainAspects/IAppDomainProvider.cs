﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace AppDomainAspects
{
    public interface IAppDomainProvider
    {
        AppDomain GetDomain(MethodExecutionArgs args);
    }
}
