using Nop.Core;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.Widget.StatisticPriceEachMonth.Data
{
     public class StatisticPriceMonthlyObjectContext: DbContext, IDbContext
    {
         public StatisticPriceMonthlyObjectContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        #region Implementation of IDbContext

        #endregion

        public string CreateDatabaseInstallationScript()
        {
            string command = ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
            return command;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StatisticPriceMonthlyMap());

            base.OnModelCreating(modelBuilder);
        }

        public void Install()
        {
            //It's required to set initializer to null (for SQL Server Compact).
            //otherwise, you'll get something like "The model backing the 'your context name' context has changed since the database was created. Consider using Code First Migrations to update the database"
            Database.SetInitializer<StatisticPriceMonthlyObjectContext>(null);
            try
            {
                Database.ExecuteSqlCommand(CreateDatabaseInstallationScript());
                SaveChanges();
            }
            catch (Exception ee)
            {
            }
        }

        public void Uninstall()
        {
            try
            {
                //var dbScript = "DROP TABLE StatisticPriceMonthly";
                //Database.ExecuteSqlCommand(dbScript);
                //SaveChanges();
            }
            catch (Exception ee)
            {
            }
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }



        public bool AutoDetectChangesEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Detach(object entity)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : Core.BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public bool ProxyCreationEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        } 

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
