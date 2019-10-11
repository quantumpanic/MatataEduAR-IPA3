using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenObjectGame2 : GameManagerClass {

	public float elapsedTime;
	public float timeLimit;
	float finishTime;
	public Text timeText;

	void ResetTimer()
	{
		finishTime = timeElapsed + timeLimit;
	}

	public void DisableBoolAnimator (Animator anim)
	{
		anim.SetBool ("IsDisplayed", false);
	}

	public void EnableBoolAnimator (Animator anim)
	{
		anim.SetBool ("IsDisplayed", true);
	}

	public int oneStarRequiredPoint;
	public int twoStarRequiredPoint;
	public int threeStarRequiredPoint;

	// Use this for initialization
	void Start ()
    {
        //NewObjective("OneStar").AddToList(objectives);
        //NewObjective("TwoStar").AddToList(objectives);
        //NewObjective("ThreeStar").AddToList(objectives);
        //NewObjective("KnowStar").SetGoal(0).SetProgress(15).AddToList(objectives);

        Transform holder = GameObject.Find("Objectives").transform;

        GetMilestones();

        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("OneStar").SetActive().AddToList(objectives);
        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("TwoStar").SetActive().AddToList(objectives);
        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("ThreeStar").SetActive().AddToList(objectives);
        Instantiate(objectivePrefab, holder).GetComponent<Objective>().SetName("KnowStar")
            .SetProgress(5)
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

			timeElapsed += Time.deltaTime;
			float remainingTime = timeLimit - timeElapsed;

			// update timer
			if (remainingTime <= 0) {
				ReportScore ();
			} else {
				

				int minutes = Mathf.FloorToInt(remainingTime / 60F);
				int seconds = Mathf.FloorToInt(remainingTime - minutes * 60);
				string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
				string niceTime2 = string.Format ("{0:0}:{1:00}", minutes, seconds);

				timeText.text = "Waktu " + niceTime;
			}

        }
	}

    // triggers for individual goals

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
			if (itemFound == oneStarRequiredPoint)
            {
                return true;
            }
        }

        if (obj.name == "TwoStar")
        {
			if (itemFound == twoStarRequiredPoint)
            {
                return true;
            }
        }

        if (obj.name == "ThreeStar")
        {
			if (itemFound == threeStarRequiredPoint)
            {
                return true;
            }
        }

        if (obj.name == "KnowStar")
        {
			if (itemFound == 1)
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

        if (GetObjective("ThreeStar").isDone)
        {
            isPlaying = false;
            ReportScore();
        }
    }

    // The Game

    public int itemFound = 0;

    public void ItemFound()
    {
		GlobalGameManager.Instance.ScoreAdd (100);
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

        anim.SetInteger("Stars", stars);
        anim.SetBool("Knowledge", knowledge);

        anim.transform.parent.gameObject.SetActive(true);
        PlayStarsAnime();

        // play music
        //GameObject.Find("MusicManager").GetComponent<MusicManager>().PlayMusic("in game_music1_win");
    }
}
