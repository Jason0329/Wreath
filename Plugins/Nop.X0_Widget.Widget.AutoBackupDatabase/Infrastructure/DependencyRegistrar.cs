using Autofac;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Core.Fakes;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Widgets.AutoBackupDatabase;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Nop.Plugin.Widgets.AutoBackupDatabase.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_product_view_Widget.AutoBackup";

      

        public int Order
        {
            get { return 1; }
        }


        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, Core.Configuration.NopConfig config)
        {
            ////builder.RegisterType<ViewTrackingService>().As<IViewTrackingService>().InstancePerLifetimeScope();

            ////data context
            //this.RegisterPluginDataContext<UpdateProductObjectContext>(builder, CONTEXT_NAME);

            ////override required repository with our custom context
            //builder.RegisterType<EfRepository<UpdateProduct>>()
            //    .As<IRepository<UpdateProduct>>()
            //    .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
            //    .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
               .As<HttpRequestBase>()
               .InstancePerLifetimeScope();

            builder.Register(c =>
                //register FakeHttpContext when HttpContext is not available
                  HttpContext.Current != null ?
                  (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                  (new FakeHttpContext("~/") as HttpContextBase))
                  .As<HttpContextBase>()
                  .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
               .As<HttpResponseBase>()
               .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();


        }


      
    }
}
