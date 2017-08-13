using Autofac;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Web.Framework.Mvc;
using Nop.X0_Widget.Widget.StatisticPriceEachMonth.Data;
using Nop.X0_Widget.Widget.StatisticPriceEachMonth.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.Widget.StatisticPriceEachMonth.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_product_view_Widget.StatisticPriceEachMonth";

      

        public int Order
        {
            get { return 1; }
        }


        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, Core.Configuration.NopConfig config)
        {
            ////builder.RegisterType<ViewTrackingService>().As<IViewTrackingService>().InstancePerLifetimeScope();

            //data context
            this.RegisterPluginDataContext<StatisticPriceMonthlyObjectContext>(builder, CONTEXT_NAME);

            //override required repository with our custom context
            builder.RegisterType<EfRepository<StatisticPriceMonthly>>()
                .As<IRepository<StatisticPriceMonthly>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();


        }


      
    }
}
