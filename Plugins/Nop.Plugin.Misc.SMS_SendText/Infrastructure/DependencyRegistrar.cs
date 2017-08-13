using Autofac;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Misc.SMS_SendText.Data;
using Nop.Plugin.Misc.SMS_SendText.Domain;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.SMS_SendText.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_product_view_SMS_Text";

      

        public int Order
        {
            get { return -2; }
        }


        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, Core.Configuration.NopConfig config)
        {
            //builder.RegisterType<ViewTrackingService>().As<IViewTrackingService>().InstancePerLifetimeScope();

            //data context
            this.RegisterPluginDataContext<SMS_MessageObjectContext>(builder, CONTEXT_NAME);

            //override required repository with our custom context
            builder.RegisterType<EfRepository<SMS_Message>>()
                .As<IRepository<SMS_Message>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();

        }


      
    }
}