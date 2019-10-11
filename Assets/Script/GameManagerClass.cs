using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerClass : MonoBehaviour
{

    // This is base class for all game managers
    public GameManagerClass()
    {

    }

    // Use this for initialization
    public void Start()
    {
        Transform holder = GameObject.Find("Objectives").transform;

        GetMilestones();

        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("OneStar").SetActive().AddToList(objectives);
        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("TwoStar").SetActive().AddToList(objectives);
        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("ThreeStar").SetActive().AddToList(objectives);

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameUnlockedData gameData;

    public void GetMilestones()
    {
        gameData = DataHolder.Instance.GetCurrentSceneData();
        gameDataStar1 = gameData.star1;
        gameDataStar2 = gameData.star2;
        gameDataStar3 = gameData.star3;
    }

    void UpdateMilestones(int stars, bool knowledge, float time)
    {
        GameUnlockedData gd = DataHolder.Instance.gameList.Find(o => o == gameData);

        gd.stars = stars;
        gd.knowledgeStar = knowledge;
        gd.timeRecord = time;

        DataHolder.Instance.SaveData();
    }

    // screen all goals and increment if condition met
    public void CheckAllObjectives()
    {
        foreach (Objective obj in objectives)
        {
            obj.CheckThenDo(CheckGoal, Increment);
        }
    }

    public float score; // generic score, you can replace the condition checks below with other variables
    public float gameDataStar1;
    public float gameDataStar2;
    public float gameDataStar3;

    /// <summary>
    /// generic goal checking, 3 levels of completion. all visible in the inspector
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    bool CheckGoal(Objective obj)
    {

        if (obj.name == "OneStar")
        {
            if (score >= gameDataStar1)
            {
                return true;
            }
        }

        if (obj.name == "TwoStar")
        {
            if (score >= gameDataStar2)
            {
                return true;
            }
        }

        if (obj.name == "ThreeStar")
        {
            if (score >= gameDataStar3)
            {
                return true;
            }
        }

        return false;
    }

    void Increment(Objective obj)
    {
        obj.Increment();
        obj.CheckProgress();
    }

    // Main variables
    public bool isPlaying;
    public bool isGameOver;
    public int curScore;
    public float timeElapsed; // alwasys increment

    public GameObject objectivePrefab;

    public void StartGame()
    {
        isPlaying = true;
        isGameOver = false;

        GlobalGameManager.Instance.ShowScorePanel();
    }

    public void PauseGame(bool pause)
    {
        isPlaying = !pause;
    }

    public void AddScore(float score)
    {
        GlobalGameManager.Instance.ScoreAdd(100);
    }

    public void Victory()
    {
        isPlaying = false;
        isGameOver = true;

        GlobalGameManager.Instance.GameFinished(gameData);
        GlobalGameManager.Instance.ShowScore(true);
    }

    public void GameOver()
    {
        isPlaying = false;
        isGameOver = true;

        GlobalGameManager.Instance.GameFinished(gameData);
        GlobalGameManager.Instance.ShowScore(false);
    }

    public virtual void Tick()
    {
        if (isPlaying)
        {
            timeElapsed += Time.deltaTime;
        }
    }

    // Game variables
    public List<Objective> objectives = new List<Objective>();

    // Methods

    public Objective GetObjective(string name)
    {
        foreach (Objective obj in objectives)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }

        print("No Objective named " + name);
        return null;
    }

    public void IncrementObjective(string name, float val = 0)
    {
        foreach (Objective obj in objectives)
        {
            if (obj.name == name)
            {
                obj.Increment(val);
                return;
            }
        }

        print("No Objective named " + name);
    }

    public void CheckObjectiveProgress(string name)
    {
        foreach (Objective obj in objectives)
        {
            if (obj.name == name)
            {
                obj.CheckProgress();
                return;
            }
        }

        print("No Objective named " + name);
    }

    public void CheckObjectiveProgress(Objective obj)
    {
        foreach (Objective o in objectives)
        {
            if (o == obj)
            {
                o.CheckProgress();
                return;
            }
        }

        print("No Objective named " + name);
    }

    public virtual void ReportScore()
    {
        // add things here
        GlobalGameManager.Instance.GameFinished(gameData);
    }
}

//    // Gameplay variables
//    private Session curSession;
//    private List<Session> _sessions;

//    public Session CurrentSession
//    {
//        get
//        {
//            // returns current session
//            return curSession;
//        }

//        set
//        {
//            // sets the session
//            curSession = value;
//        }
//    }
//

//    public List<Session> SessionList
//    {
//        get
//        {
//            return _sessions;
//        }
//    }

//    public List<Session> AddSession(Session session)
//    {
//        // add a new session to the queue
//        _sessions.Add(session);
//        session.SessionEndedEvt += SessionEnded;
//        return _sessions;
//    }

//    public List<Session> RemoveSession(Session session)
//    {
//        // find & remove session the queue
//        if (_sessions.Contains(session))
//        {
//            _sessions.Remove(session);
//            print("removed " + session);
//        }
//        return _sessions;
//    }

//    public Session NewSession(Session session)
//    {
//        return session;
//    }

//    public void BeginSession(Session session)
//    {
//        // starts the session
//    }

//    public void BeginFromFirstSession()
//    {
//        // starts all sessions
//    }

//    public void SessionEnded()
//    {
//        // callback for session ended
//        NextSession();
//    }

//    public void NextSession()
//    {
//        int current = SessionList.IndexOf(CurrentSession);
//        int next = current + 1;
//        if (current < SessionList.Count)
//        {
//            CurrentSession = SessionList[next];
//        }
//        else
//        {
//            AllSessionsEnded();
//        }
//    }

//    public void AllSessionsEnded()
//    {
//        // callback for all sessions ended
//    }

//    // Session

//    public class Session
//    {
//        public Session(ISession stageLayout)
//        {
//            stageLayout.CreateStages(this);
//        }

//        public string name;
//        public List<Stage> stages = new List<Stage>();
//        public Stage curStage;

//        public delegate void SessionListener();
//        public event SessionListener SessionEndedEvt;

//        public class Stage
//        {
//            public Stage(string name)
//            {

//            }
//        }

//        public Stage NewStage(string name = "New Stage")
//        {
//            return new Stage(name);
//        }

//        public void AddStage(Stage stage)
//        {
//            stages.Add(stage);
//        }

//        public void NextStage()
//        {
//            int current = stages.IndexOf(curStage);
//            int next = current + 1;
//            if (next < stages.Count)
//            {
//                curStage = stages[next];
//            }
//            else
//            {
//                SessionEnded();
//            }
//        }

//        public void SessionEnded()
//        {
//            SessionEndedEvt();
//        }
//    }

//    // Interfaces

//    public interface ISession
//    {
//        // template stage layout
//        // for manually creating stages, use NewStage()

//        void CreateStages(Session session);
//    }

//    public class SingleSession : ISession
//    {
//        // 1 stage

//        public void CreateStages(Session session)
//        {
//            session.NewStage("Stage 1");
//        }
//    }

//    public class BasicSession : ISession
//    {
//        // 3 stages total

//        public void CreateStages(Session session)
//        {
//            session.NewStage("Stage 1");
//            session.NewStage("Stage 2");
//            session.NewStage("Bonus Stage");
//        }
//    }

//    public interface IReward
//    {
//        void ImburseReward(GameManagerClass gameManager);
//    }
//}