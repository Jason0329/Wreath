using Autofac;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Wreath.MergeOrder.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_product_view_Merge_Order";

      

        public int Order
        {
            get { return 1; }
        }


        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, Core.Configuration.NopConfig config)
        {
            ////builder.RegisterType<ViewTrackingService>().As<IViewTrackingService>().InstancePerLifetimeScope();

            ////data context
            //this.RegisterPluginDataContext<SMS_SenderObjectContext>(builder, CONTEXT_NAME);

            ////override required repository with our custom context
            //builder.RegisterType<EfRepository<SMS_Sender>>()
            //    .As<IRepository<SMS_Sender>>()
            //    .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
            //    .InstancePerLifetimeScope();


        }


      
    }
}
