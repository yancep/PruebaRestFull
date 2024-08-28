using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repository;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDb"));

            #region
            service.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
            #endregion
        }
    }
}
