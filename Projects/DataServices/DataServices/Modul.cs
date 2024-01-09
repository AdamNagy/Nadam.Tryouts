using Microsoft.Extensions.DependencyInjection;
using Nadam.DataServices.Config;

namespace Nadam.DataServices
{
    public static class Modul
    {
        public static IServiceCollection AddData(this IServiceCollection services, DbConfig config)
            => services;

        //public static IServiceCollection AddData(this IServiceCollection services, IEnumerable<DbConfig> config)
        //{

        //}
    }
}
