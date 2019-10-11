using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObjectGame : GameManagerClass {

	public int oneStarRequiredPoint;
	public int twoStarRequiredPoint;
	public int threeStarRequiredPoint;

	// Use this for initialization
	new void Start ()
    {
        //NewObjective("OneStar").AddToList(objectives);
        //NewObjective("TwoStar").AddToList(objectives);
        //NewObjective("ThreeStar").AddToList(objectives);
        //NewObjective("KnowStar").SetGoal(0).SetProgress(15).AddToList(objectives);

        Transform holder = GameObject.Find("Objectives").transform;

		GetMilestones();

        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("OneStar")
            .SetProgress(gameData.star1)
            .SetGoal(0)
            .SetIncrement(0)
            .SetActive()
            .AddToList(objectives);
        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("TwoStar")
            .SetProgress(gameData.star2)
            .SetGoal(0)
            .SetIncrement(0)
            .SetActive()
            .AddToList(objectives);
        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("ThreeStar")
            .SetProgress(gameData.star3)
            .SetGoal(0)
            .SetIncrement(0)
            .SetActive()
            .AddToList(objectives);
        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("KnowStar")
            .SetProgress(gameData.knowledgeTime)
            .SetGoal(0)
            .SetIncrement(0)
            .SetActive()
            .AddToList(objectives);

        StartGame();
    }
	
	// Update is called once per frame
	void Update () {
        if (isPlaying)
        {
            // reduce the time remaining for knowledge star
            GetObjective("KnowStar").SetIncrement(-Time.deltaTime);
            IncrementObjective("KnowStar");
            GetObjective("OneStar").SetIncrement(-Time.deltaTime);
            IncrementObjective("OneStar");
            GetObjective("TwoStar").SetIncrement(-Time.deltaTime);
            IncrementObjective("TwoStar");
            GetObjective("ThreeStar").SetIncrement(-Time.deltaTime);
            IncrementObjective("ThreeStar");
        }
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
    }

    bool CheckGoal(Objective obj)
    {
        if (obj.name == "OneStar")
        {
            return true;
        }

        if (obj.name == "TwoStar")
        {
            return true;
        }

        if (obj.name == "ThreeStar")
        {
            return true;
        }

        if (obj.name == "KnowStar")
        {
            return true;
        }

        return false;
    }

    void Increment(Objective obj)
    {
        obj.CheckProgress();
    }

    // The Game

    public int itemFound = 0;

    public void ItemFound()
    {
		itemFound++;
        CheckAllObjectives();
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

        PlayStarsAnime();
        Invoke("StarReminder", 2);

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
