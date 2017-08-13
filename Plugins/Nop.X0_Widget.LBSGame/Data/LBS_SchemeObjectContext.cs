using Nop.Core;
using Nop.Data;
using Nop.X0_Widget.LBSStory.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.X0_Widget.LBSGame.Data
{
    public class LBS_SchemeObjectContext : DbContext, IDbContext
    {
        public LBS_SchemeObjectContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public string CreateDatabaseInstallationScript()
        {
            string command = ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
            return command;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Configurations.Add(new StoryMap());
            modelBuilder.Configurations.Add(new TaskMap());
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new TaskLocationMappingMap());
            modelBuilder.Configurations.Add(new GameRecordMap());

            base.OnModelCreating(modelBuilder);
        }

        public void Install()
        {
            //It's required to set initializer to null (for SQL Server Compact).
            //otherwise, you'll get something like "The model backing the 'your context name' context has changed since the database was created. Consider using Code First Migrations to update the database"
            Database.SetInitializer<LBS_SchemeObjectContext>(null);

            Database.ExecuteSqlCommand(CreateDatabaseInstallationScript());
            SaveChanges();
        }

        public void Uninstall()
        {
            try
            {
                var dbScript = "DROP TABLE LBSGame_Task";

                Database.ExecuteSqlCommand(dbScript);
                SaveChanges();

                dbScript = "DROP TABLE LBSGame_Story";

                Database.ExecuteSqlCommand(dbScript);
                SaveChanges();
                dbScript = "DROP TABLE LBSGame_Location";

                Database.ExecuteSqlCommand(dbScript);
                SaveChanges();

                dbScript = "DROP TABLE LBSGame_TaskLocationMapping";

                Database.ExecuteSqlCommand(dbScript);
                SaveChanges();

                dbScript = "DROP TABLE LBSGame_Game";

                Database.ExecuteSqlCommand(dbScript);
                SaveChanges();

                dbScript = "DROP TABLE LBSGame_GameRecord";
                Database.ExecuteSqlCommand(dbScript);
                SaveChanges();
            }
            catch (Exception e)
            { }

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
