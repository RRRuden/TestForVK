using API.Profiles;
using AutoMapper;
using DAL;

namespace APITests.Common
{
    public class TestControllerBase : IDisposable
    {
        protected readonly ApplicationDbContext Context;
        protected readonly IMapper Mapper;


        public TestControllerBase()
        {
            Context = ApplicationDbContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfile());
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(Context);
        }
    }
}
