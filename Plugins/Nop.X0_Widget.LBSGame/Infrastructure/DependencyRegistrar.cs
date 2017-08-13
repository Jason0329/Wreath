using Autofac;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Web.Framework.Mvc;
using Nop.X0_Widget.LBSGame.Data;
using Nop.X0_Widget.LBSGame.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSGame.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_LBSgame";
        private const string CONTEXT_NAME_2 = "nop_object_context_LBSScheme";
        private const string CONTEXT_NAME_Game = "nop_object_context_LBSScheme2";
        private const string CONTEXT_NAME_Location = "nop_object_context_LBSScheme3";
        private const string CONTEXT_NAME_Mapping = "nop_object_context_LBSScheme4";

      

        public int Order
        {
            get { return 1; }
        }


        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, Core.Configuration.NopConfig config)
        {
            //builder.RegisterType<ViewTrackingService>().As<IViewTrackingService>().InstancePerLifetimeScope();

            ////data context
            this.RegisterPluginDataContext<GameObjectContext>(builder, CONTEXT_NAME_Game);

            //override required repository with our custom context
            builder.RegisterType<EfRepository<Game>>()
              .As<IRepository<Game>>()
              .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME_Game))
              .InstancePerLifetimeScope();

            ////data context
            this.RegisterPluginDataContext<LBS_SchemeObjectContext>(builder, CONTEXT_NAME_2);

            //override required repository with our custom context

            builder.RegisterType<EfRepository<Story>>()
               .As<IRepository<Story>>()
               .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME_2))
               .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<GameTask>>()
               .As<IRepository<GameTask>>()
               .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME_2))
               .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<Location>>()
               .As<IRepository<Location>>()
               .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME_2))
               .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<TaskLocationMapping>>()
              .As<IRepository<TaskLocationMapping>>()
              .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME_2))
              .InstancePerLifetimeScope();


        }


      
    }
}
