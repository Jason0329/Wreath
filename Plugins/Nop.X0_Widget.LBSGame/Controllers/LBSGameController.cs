using Nop.Core.Data;
using Nop.Web.Framework.Controllers;
using Nop.X0_Widget.LBSGame.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Nop.X0_Widget.LBSGame.Controllers
{
    public class LBSGameController : BasePluginController
    {
        private IRepository<Game> _GameRepository;
        private IRepository<Story> _StoryRepository;
        private IRepository<GameTask> _GameTaskRepository;
        private IRepository<Location> _LocationRepositroy;
        private IRepository<TaskLocationMapping> _TaskLocationMappingRepository;
        private IRepository<GameRecord> _GameRecordRepositroy;

        public LBSGameController(IRepository<Game> GameRepository , IRepository<Story> StoryRepository ,
            IRepository<GameTask> GameTaskRepository , IRepository<Location> LocationRepositroy,
            IRepository<TaskLocationMapping> TaskLocationMappingRepository,IRepository<GameRecord> GameRecordRepositroy)
        {
            _GameRepository = GameRepository;
            _StoryRepository = StoryRepository;
            _GameTaskRepository = GameTaskRepository;
            _LocationRepositroy = LocationRepositroy;
            _TaskLocationMappingRepository = TaskLocationMappingRepository;
            _GameRecordRepositroy = GameRecordRepositroy;
        }

        //public ActionResult StartGame(int _GameID,string _TeamName)
        //{
        //    //if (Check_IsGameStarted(_GameID, _TeamName))
        //    //    return View("~/test.chtml");

        //    return View();
        //}

        public ActionResult StartGame()
        {
            //if (Check_IsGameStarted(_GameID, _TeamName))
            //    return View("~/test.chtml");
            var model = new GameRecord();
            model.GameName = "test";
            
            return View(model);
        }

        public ActionResult Game()
        {
            return View();
        }

        [HttpPost, ActionName("StartGame")]
        [FormValueRequired("InitialGame")]
        public ActionResult InitialGame(string gameName, string teamName)
        {
            CurrentlyGameList CurrentlyGame = new CurrentlyGameList();

            var GetGameData = _GameRepository.Table.Where(m => m.GameID == 1).FirstOrDefault();

            CurrentlyGame.TeamName = teamName;
            CurrentlyGame.LevelCurrently = 1;
            CurrentlyGame.GameID = GetGameData.GameID;
            CurrentlyGame.GameName = GetGameData.GameName;
            CurrentlyGame.GameStartMusic = GetGameData.GameStartMusic;
            CurrentlyGame.GmaeEndMusic = GetGameData.GmaeEndMusic;
            CurrentlyGame.GameStartStory = GetGameData.GameStartStory;
            CurrentlyGame.GameEndStory = GetGameData.GameEndStory;

            var GetStoryData = _StoryRepository.Table.Where(m => m.GameID==1).OrderBy(m => m.StoryOrder).ToList();
            var GetGameTaskData = _GameTaskRepository.Table.Where(m => m.GameID == 1).ToList();

            

            for (int i = 0; i < GetStoryData.Count; i++)
            {
                CurrentlyGameData GameData = new CurrentlyGameData();

                GameData.StoryTitle = GetStoryData[i].StoryTitle;
                GameData.StoryContent = GetStoryData[i].StoryContent;
                GameData.StoryMusic = GetStoryData[i].StoryMusic;

                GameData.TaskType = GetGameTaskData[i].TaskType;
                GameData.TaskTitle = GetGameTaskData[i].TaskTitle;
                GameData.TaskContent = GetGameTaskData[i].TaskContent;
                GameData.TaskAnswer = GetGameTaskData[i].TaskAnswer;
                GameData.TaskEndStory = GetGameTaskData[i].TaskEndStory;

                var LocationMap = _TaskLocationMappingRepository.Table.Where(m => m.TaskID == GetGameTaskData[i].TaskID).FirstOrDefault();
                var GetLocationData = _LocationRepositroy.Table.Where(m => m.LocationID == LocationMap.LocationID).FirstOrDefault();

                GameData.LocationName = GetLocationData.LocationName;
                GameData.LocationDescription = GetLocationData.LocationDescription;
                GameData.LocationX = GetLocationData.LocationX;
                GameData.LocationY = GetLocationData.LocationY;

                CurrentlyGame._CurrentlyGameData.Add(GameData);
            }

            return null;
           
        }

        bool Check_IsGameStarted(int _GameID, string _TeamName)
        {
            List<CurrentlyGameList> GameList = (List<CurrentlyGameList>)System.Web.HttpContext.Current.Application["CurrentlyGame"];
            var CurrentlyGame = GameList.Where(m => m.GameID == _GameID && m.TeamName.CompareTo(_TeamName) == 0);

            if (CurrentlyGame == null)
                return false;

            return true;
        }
    }
}
