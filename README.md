# Overview
Provides a RunInDifferentAppDomain attribute that can be placed on a method. Before calling the method, the
AppDomain.Aspects.DefaultAppDomainProvder.AppDomain must be set.

# Example

```csharp
using System;
using AppDomainAspects;

public static class RunInDifferentAppDomainExample
{
  public static void Main(string[] args)
  {
    AppDomain domain = AppDomain.CreateDomain("newDomain");
    DefaultAppDomainProvider.AppDomain = domain;
    LogCurrentDomain();
  }
  
  [RunInDifferentAppDomain]
  private static void LogCurrentDomain()
  {
    Console.WriteLine("Domain: {0}", AppDomain.CurrentDomain.FriendlyName);
  }
}
```
