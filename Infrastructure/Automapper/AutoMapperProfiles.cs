using AutoMapper;
using Infrastructure.Automapper.Profiles;

namespace Infrastructure.Automapper
{
    public static class AutoMapperProfiles
    {
        public static void AddAutoMapperProfiles(this IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<RoleProfile>();
            cfg.AddProfile<PaymentProfile>();
            cfg.AddProfile<ObjectiveTypeProfile>();
            cfg.AddProfile<CategoryProfile>();
            cfg.AddProfile<ObjectiveProfile>();
        }
    }
}
