using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : GameManagerClass {

	// Use this for initialization
	new void Start () {
        Transform holder = GameObject.Find("Objectives").transform;

        GetMilestones();

        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("OneStar").SetActive().AddToList(objectives);
        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("TwoStar").SetActive().AddToList(objectives);
        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("ThreeStar").SetActive().AddToList(objectives);
        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("KnowStar")
            .SetProgress(gameData.knowledgeTime)
            .SetGoal(0)
            .SetIncrement(0)
            .SetActive()
            .AddToList(objectives);

        //StartSession(sessions[1]);
    }

    public GameObject tutorial;

    public void EndTutorial()
    {
        tutorial.SetActive(false);
        timeText.gameObject.SetActive(true);
        hintText.gameObject.SetActive(true);
        StartGame();
        PlayAllSessions();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isPlaying)
        {
            // reduce the time remaining for knowledge star
            GetObjective("KnowStar").SetIncrement(-Time.deltaTime);
            IncrementObjective("KnowStar");

            // update timer
            timeElapsed += Time.deltaTime;
            float remainingTime = finishTime - timeElapsed;

            if (remainingTime <= 0) activeSession.EndSession(this);
            else
            {
                int minutes = Mathf.FloorToInt(remainingTime / 60F);
                int seconds = Mathf.FloorToInt(remainingTime - minutes * 60);
                string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

                timeText.text = "Waktu " + niceTime;
            }

            // check mouse down
            if (Input.GetMouseButtonDown(0))
            {
                //play sfx
            }

            // check mouse up
            if (Input.GetMouseButtonUp(0))
            {
                CheckMouseUp();
            }
        }
    }

    public float timeLimit = 10;
    float finishTime;

    void ResetTimer()
    {
        finishTime = timeElapsed + timeLimit;
    }

    //GameUnlockedData gameData;

    new void GetMilestones()
    {
        gameData = DataHolder.Instance.GetCurrentSceneData();
    }

    // screen all goals and increment if condition met
    new public void CheckAllObjectives()
    {
        foreach (Objective obj in objectives)
        {
            obj.CheckThenDo(CheckGoal, Increment);
        }

        CalculateScore();
        ReportScore();
    }

    bool CheckGoal(Objective obj)
    {
        if (obj.name == "OneStar")
        {
            if (score >= gameData.star1)
            {
                return true;
            }
        }

        if (obj.name == "TwoStar")
        {
            if (score >= gameData.star2)
            {
                return true;
            }
        }

        if (obj.name == "ThreeStar")
        {
            if (score >= gameData.star3)
            {
                return true;
            }
        }

        if (obj.name == "KnowStar")
        {
            if (GetObjective("ThreeStar").isDone)
            {
                return true;
            }
        }

        return false;
    }

    void Increment(Objective obj)
    {
        if (obj.name == "KnowStar")
        {
            obj.CheckProgress();
        }

        else
        {
            obj.Increment();
            obj.CheckProgress();
        }
    }

    // The Game
    public float animalsMatched;
    public float totalAnimals;
    new public float score;

    public int correctAns;

    public Text hintText;
    public Text timeText;

    public List<Session> sessions;
    public Session activeSession;

    public void PlayAllSessions()
    {
        StartCoroutine(IterateSessions());
    }

    IEnumerator IterateSessions()
    {
        foreach (Session s in sessions)
        {
            StartSession(s);
            yield return new WaitUntil(() => s.isDone == true);

            //s.EndSession(this);
        }

        print("sessions finished");
        CheckAllObjectives();
    }

    void StartSession(Session session)
    {
        ResetTimer();
        oldCorrect = 0;
        
        session.isDone = false;
        activeSession = session;
        hintText.text = session.hint;

        // play sound and animate text
        GetComponent<AudioSource>().Play();
        hintText.SendMessage("Restart");

        totalAnimals += session.targets.Count;
        session.goalArea.SetActive(true);

        foreach(Transform t in session.goalArea.transform.GetComponentInChildren<Transform>())
        {
            t.gameObject.SetActive(false);
        }

        int hintNum = 0;
        foreach (string s in session.hints)
        {
            session.goalArea.transform.GetChild(hintNum).gameObject.SetActive(true);
            hintNum++;
        }

        foreach (GameObject g in session.targets)
        {
            g.SetActive(true);
        }

        foreach (string hint in session.hints)
        {
            int index = session.hints.IndexOf(hint);
            session.goalArea.transform.GetChild(index).GetComponentInChildren<Text>().text = hint;
        }
    }

    public AudioSource correctSfx;
    public AudioSource wrongSfx;
    int oldCorrect;

    public void CheckMouseUp()
    {
        correctAns = 0;
        bool inBox = false;

        foreach (GameObject animal in activeSession.targets)
        {
            int animalIndex = activeSession.targets.IndexOf(animal);
            int goalIndex = activeSession.targetsGoals[animalIndex];
            
            if(RectTransformUtility.RectangleContainsScreenPoint(activeSession.goalArea.transform.GetChild(goalIndex).GetComponent<RectTransform>(), animal.transform.GetChild(0).GetComponent<RectTransform>().position))
            {
                correctAns++;
                inBox = true;

                animal.SetActive(false);

            }
        }

        if (correctAns > oldCorrect)
        {
            correctSfx.Play();

            // add score
            GlobalGameManager.Instance.ScoreAdd(100);
        }
        if (correctAns <= oldCorrect && inBox) wrongSfx.Play();
        oldCorrect = correctAns;

        if (correctAns == activeSession.targets.Count)
        {
            animalsMatched += correctAns;
            activeSession.EndSession(this);
        }
    }

    public void CalculateScore()
    {
        score = animalsMatched / totalAnimals;
    }

    public Animator anim;

    void SetAnimStars(int stars)
    {
        anim.SetInteger(0, stars);
    }

    void PlayStarsAnime()
    {
        anim.Play("Star0");
    }

    public override void ReportScore()
    {
        base.ReportScore();
        Victory();
        return;

        int stars = 0;
        bool knowledge = false;

        if (GetObjective("OneStar").isDone) stars = 1;
        if (GetObjective("TwoStar").isDone) stars = 2;
        if (GetObjective("ThreeStar").isDone) stars = 3;
        if (GetObjective("KnowStar").isDone) knowledge = true;

        anim.transform.parent.gameObject.SetActive(true);

        // add accumulated stars
        if (knowledge == true)
        {
            DataHolder.Instance.totalKnowStars++;
        }
        else
        {
            GameObject.Find("ButtonAR").SetActive(false);
        }

        anim.SetInteger("Stars", stars);
        anim.SetBool("Knowledge", knowledge);

        this.stars = stars + (knowledge ? 1 : 0);
        Invoke("StarReminder", 2);

        PlayStarsAnime();

        // play music
        GameObject.Find("MusicManager").GetComponent<MusicManager>().PlayMusic("in game_music1_win");

        UpdateMilestones(stars, knowledge, timeElapsed);
    }

    int stars;

    void StarReminder()
    {
        DataHolder.Instance.SetStarReminder(stars);
        DataHolder.Instance.ShowStarReminder();
    }

    void UpdateMilestones(int stars, bool knowledge, float time)
    {
        GameUnlockedData gd = DataHolder.Instance.gameList.Find(o => o == gameData);

        gd.stars = stars;
        gd.knowledgeStar = knowledge;
        gd.timeRecord = time;

        DataHolder.Instance.SaveData();
    }
}

[System.Serializable]
public class Session
{
    public string sessionName;
    public string hint;
    public GameObject goalArea;
    public bool isDone;
    public List<GameObject> targets;
    public List<int> targetsGoals;
    public List<string> hints;

    public void EndSession(DragDrop game)
    {
        goalArea.SetActive(false);
        foreach (GameObject g in targets)
        {
            g.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            g.SetActive(false);
        }

        game.CalculateScore();
        isDone = true;
        Debug.Log("Session ended");
    }
}