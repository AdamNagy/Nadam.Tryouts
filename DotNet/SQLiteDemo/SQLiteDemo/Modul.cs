using Microsoft.Extensions.DependencyInjection;
using SQLiteDemo.Config;

namespace SQLiteDemo
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
