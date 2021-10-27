using Autofac;
using WorkTimeControl.BLL.Infrastructure.Interfaces;
using WorkTimeControl.BLL.Interfaces;
using WorkTimeControl.DATA.Repositories;
using WorkTimeControl.DATA.Repositories.Abstract;

namespace WorkTimeControl.BLL.Infrastructure
{
   public class ServiceDTO
    {
        public IUserService UserService()
        {
            var userBuilder = new ContainerBuilder();
            userBuilder.RegisterType<UserService>().As<IUserService>();
            userBuilder.RegisterType<UserRepository>().As<IUserRepository>();
            return userBuilder.Build().Resolve<IUserService>();

        }
        public IUserTimeService UserTimeService()
        {
            var userTimeBuilder = new ContainerBuilder();
            userTimeBuilder.RegisterType<UserTimeService>().As<IUserTimeService>();
            userTimeBuilder.RegisterType<UserTimeRepository>().As<IUserTimeRepository>();
            return userTimeBuilder.Build().Resolve<IUserTimeService>();
        }
    }
}
