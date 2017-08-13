using Nop.Core.Data;
using Nop.Core.Plugins;
using Nop.Web.Framework.Menu;
using Nop.X0_Widget.LBSGame.Data;
using Nop.X0_Widget.LBSGame.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Nop.X0_Widget.LBSGame
{
    public class LBSGame_Plugin : BasePlugin, IAdminMenuPlugin
    {
        private LBS_SchemeObjectContext _LBSSchemeObjectContext;
        //private GameObjectContext _gameObjectContext;
        private IRepository<Game> _GameRepository;
        private IRepository<Story> _StoryRepository;
        private IRepository<GameTask> _GameTaskRepository;
        private IRepository<Location> _LocationRepositroy;
        private IRepository<TaskLocationMapping> _TaskLocationMappingRepository;
        private IRepository<GameRecord> _GameRecordRepositroy;
        private GameObjectContext _gameObjectContext;

        public LBSGame_Plugin(GameObjectContext gameObjectContext,
            LBS_SchemeObjectContext LBSSchemeObjectContext, 
            IRepository<Game> GameRepository, IRepository<Story> StoryRepository,
            IRepository<GameTask> GameTaskRepository, IRepository<Location> LocationRepositroy,
            IRepository<TaskLocationMapping> TaskLocationMappingRepository, IRepository<GameRecord> GameRecordRepositroy)
        {
            _LBSSchemeObjectContext = LBSSchemeObjectContext;
            _gameObjectContext = gameObjectContext;

            _GameRepository = GameRepository;
            _StoryRepository = StoryRepository;
            _GameTaskRepository = GameTaskRepository;
            _LocationRepositroy = LocationRepositroy;
            _TaskLocationMappingRepository = TaskLocationMappingRepository;
            _GameRecordRepositroy = GameRecordRepositroy;
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            
            var menuItem = new SiteMapNode()
            {
                SystemName = "LBSGame",
                Title = "LBSGame List",
                ControllerName = "UpdateProduct",
                ActionName = "ProductList",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };

          

           

           
            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            if (pluginNode != null)
            {
                pluginNode.ChildNodes.Add(menuItem);
               
            }
            else
            {
                rootNode.ChildNodes.Add(menuItem);
               
            }
        }

        public override void Install()
        {

            try
            {
                _gameObjectContext.Install();
                _LBSSchemeObjectContext.Install();
            }
            catch (Exception ee)
            {
            }
            base.Install();


            int GameCount = 6;
            int StoryCount = 30;
            int TaskCount = 30;
            int LocationCount = 30;

            for (int i = 1; i < GameCount; i++)
            {
                Game game = new Game();
                game.GameID = i;
                game.GameName = "Name" + i;
                game.GameStartMusic = "Music" + i;
                game.GameStartStory = "StartStory" + i;
                game.GameEndStory = "EndStory" + i;

                _GameRepository.Insert(game);

                for (int j = 1; j < StoryCount; j++)
                {
                    Story story = new Story();
                    story.GameID = i;
                    story.StoryID = i * GameCount + j;
                    story.StoryMusic = "StoryMusic" + (i * GameCount + j);
                    story.StoryContent = "StoryContent" + (i * GameCount + j);
                    story.StoryOrder = j;
                    story.StoryTitle = "StoryTitle" + (i * GameCount + j);

                    _StoryRepository.Insert(story);
                }

                for (int k = 1; k < TaskCount; k++)
                {
                    GameTask task = new GameTask();
                    task.GameID = i;
                    task.TaskID = i * GameCount + k;
                    task.TaskAnswer = "TaskAnswer" + (i * GameCount + k);
                    task.TaskContent = "TaskContent" + (i * GameCount + k);
                    task.TaskTitle = "TaskTitle" + (i * GameCount + k);
                    task.TaskType = "TaskType" + k;

                    _GameTaskRepository.Insert(task);
                  

                    for (int l = 0; l < LocationCount; l++)
                    {
                        TaskLocationMapping taskLocationMap = new TaskLocationMapping();
                        taskLocationMap.TaskID = k;
                        taskLocationMap.LocationID = l;
                        _TaskLocationMappingRepository.Insert(taskLocationMap);

                        Location location = new Location();
                        location.LocationX = 123.223M;
                        location.LocationY = 246.452M;
                        location.LocationType = "type" + l;
                        location.LocationName = "LocationName" + l;
                        location.LocationDescription = "Description" + l;
                        location.LocationID = l;
                    }
                }
            }

        }

        public override void Uninstall()
        {
            _gameObjectContext.Uninstall();
            _LBSSchemeObjectContext.Uninstall();
            base.Uninstall();
        }
    }
}
