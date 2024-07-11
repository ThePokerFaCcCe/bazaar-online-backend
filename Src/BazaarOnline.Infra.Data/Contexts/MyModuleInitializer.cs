using System.Runtime.CompilerServices;

namespace BazaarOnline.Infra.Data.Contexts;

public static class MyModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}