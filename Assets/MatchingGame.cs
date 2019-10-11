using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingGame : GameManagerClass {

	// Use this for initialization
	new void Start ()
    {
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
    }

    GameUnlockedData gameData;

    new void GetMilestones()
    {
        gameData = DataHolder.Instance.GetCurrentSceneData();
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
    new public void CheckAllObjectives()
    {
        foreach (Objective obj in objectives)
        {
            obj.CheckThenDo(CheckGoal, Increment);
        }
        
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

    // Update is called once per frame
    void Update () {
		
	}

    // The Game
    new public int score;
}
