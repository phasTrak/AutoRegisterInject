using Xunit;

namespace AutoRegisterInject.Tests;

public partial class GenerationTests
{
    [Fact]
    public async Task ShouldRegisterTryTransient()
    {
        const string INPUT = @"[TryRegisterTransient]
public class Foo { }";

        const string EXPECTED = @"// <auto-generated>
//     Automatically generated by AutoRegisterInject.
//     Changes made to this file may be lost and may cause undesirable behaviour.
// </auto-generated>
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
public static class AutoRegisterInjectServiceCollectionExtension
{
    public static Microsoft.Extensions.DependencyInjection.IServiceCollection AutoRegisterFromTestProject(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
    {
        return AutoRegister(serviceCollection);
    }

    internal static Microsoft.Extensions.DependencyInjection.IServiceCollection AutoRegister(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddTransient<Foo>();
        return serviceCollection;
    }
}";

        await RunGenerator(INPUT, EXPECTED);
    }

    [Fact]
    public async Task ShouldRegisterTryTransientFromInterface()
    {
        const string INPUT = @"[TryRegisterTransient]
public class Foo : IFoo { }
public interface IFoo { }
";

        const string EXPECTED = @"// <auto-generated>
//     Automatically generated by AutoRegisterInject.
//     Changes made to this file may be lost and may cause undesirable behaviour.
// </auto-generated>
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
public static class AutoRegisterInjectServiceCollectionExtension
{
    public static Microsoft.Extensions.DependencyInjection.IServiceCollection AutoRegisterFromTestProject(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
    {
        return AutoRegister(serviceCollection);
    }

    internal static Microsoft.Extensions.DependencyInjection.IServiceCollection AutoRegister(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddTransient<IFoo, Foo>();
        return serviceCollection;
    }
}";

        await RunGenerator(INPUT, EXPECTED);
    }
}