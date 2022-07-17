using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Service.AppStart
{
    public static class NetCoreDiSetupExtensions 
    {
        public static void RegisterServiceLayerDi 
            (this IServiceCollection services)    
        {
            services.RegisterAssemblyPublicNonGenericClasses() 
                .AsPublicImplementedInterfaces(); 

        }
    }
}
