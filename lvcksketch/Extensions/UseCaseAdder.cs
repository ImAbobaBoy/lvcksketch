using lvcksketch.UseCases.GetMapNames;
using lvcksketch.UseCases.GetMapNames.Interfaces;

namespace lvcksketch.Extensions;

public static class UseCaseAdder
{
    public static void ConfigureUseCases(this IServiceCollection services)
    {
        services.AddSingleton<IGetMapNames, GetMapNames>();
    }
}