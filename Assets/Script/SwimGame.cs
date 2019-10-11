using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwimGame : GameManagerClass {

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

        UpdateUI();
        StartCoroutine(OpeningSequence());
    }

    public Image black;
    public CanvasGroup HUDgroup;

    IEnumerator OpeningSequence()
    {
        CameraZoomTool camZoom = Camera.main.GetComponent<CameraZoomTool>();

        // hide HUD
        HUDgroup.alpha = 0;

        // pan camera close up
        camZoom.SnapTo(3);

        // hide chicken and pause animations
        SendMessage("PlayerDie");
        SendMessage("TurnOff");
        GetComponent<SpriteRenderer>().enabled = false;

        // make egg
        Instantiate(eggCrackPrefab, transform);

        // First fade in
        black.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1);

        // Animate egg shake
        EggShakeAnim();
        yield return new WaitForSeconds(3);

        // Then animate egg crack
        EggCrackAnim();
        Destroy(GameObject.Find("EggCrack(Clone)"), 1);
        SendMessage("StartBubbles");
        // after crack, play swimming anim
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(3);

        // zoom out and show HUD
        SendMessage("StartSwimming");
        GetComponent<Rigidbody2D>().simulated = true;
        camZoom.PanTo(5);
        while (HUDgroup.alpha < 0.9f)
        {
            HUDgroup.alpha = Mathf.Lerp(HUDgroup.alpha, 1, Time.deltaTime * 3);
            yield return null;
        }

        HUDgroup.alpha = 1;

        // start countdown immediately after
        CountDownAnim();
        yield return new WaitForSeconds(3);

        // begin spawning things
        //StartGame();
    }
    
    public GameObject eggCrackPrefab;
    public Text countDownTxt;

    void EggShakeAnim()
    {
        GameObject.Find("EggCrack(Clone)").GetComponent<Animator>().Play("EggIdle");
    }

    public GameObject legs;

    void EggCrackAnim()
    {
        GameObject.Find("EggCrack(Clone)").GetComponent<Animator>().Play("EggCrack");
        legs.SetActive(true);

        // sound
        GameObject.Find("SFX").GetComponent<SwimSFX>().CrackSFX();
    }

    void CountDownAnim()
    {
        countDownTxt.transform.parent.gameObject.SetActive(true);
        StartCoroutine(DoCountDown());

        // sound
        GameObject.Find("SFX").GetComponent<SwimSFX>().CountSFX();
    }

    IEnumerator DoCountDown()
    {
        int count = 3;
        while (count > 0)
        {
            countDownTxt.text = count.ToString();
            count--;
            yield return new WaitForSeconds(1);
        }

        countDownTxt.transform.parent.gameObject.SetActive(false);
        tutorial.ShowMoveHint();
    }

    public Tutorial tutorial;
	
	// Update is called once per frame
	void Update () {
        if (isPlaying)
        {
            // reduce the time remaining for knowledge star
            GetObjective("KnowStar").SetIncrement(-Time.deltaTime);
            IncrementObjective("KnowStar");

            // reduce energy
            //ReduceEnergy();
            tutorial.CheckFirstFood(this);

            // check is swimming
            isSwimming = (transform.position.y <= 0) ? true : false;

            if (isSwimming)
            {
                ReduceOxygen();
            }

            else
            {
                AddOxygen();
            }

            if (energy >= energyThreshold && oxygen >= oxygenThreshold)
            {
                if (!tutorial.hasFirstGrowth) tutorial.CheckFirstGrowth();
                AddGrowth();
            }
        }

        else
        {
            tutorial.CheckHintAxis(this);
            tutorial.CheckFirstOxygen(this);
        }

        // update the gauges
        UpdateUI();
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
            if (foodScore >= gameData.star1)
            {
                return true;
            }
        }

        if (obj.name == "TwoStar")
        {
            if (foodScore >= gameData.star2)
            {
                return true;
            }
        }

        if (obj.name == "ThreeStar")
        {
            if (foodScore >= gameData.star3)
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
    public float foodSpawned;
    public float foodEaten;
    float foodScore;
    public float energy;
    public float energyThreshold;
    public float oxygen;
    public float oxygenThreshold;
    public bool isSwimming;
    public int growthStage = 1;
    public int growthGoal;
    public float growth;
    public float growthThreshold;

    public Image energyGauge;
    public Image oxygenGauge;
    public Image growthGauge;

    void UpdateUI()
    {
        energyGauge.fillAmount = energy;
        oxygenGauge.fillAmount = oxygen;
        growthGauge.fillAmount = growth/growthThreshold;
    }

    public void SpawnFood()
    {
        foodSpawned++;
    }

    public void EatFood()
    {
        tutorial.GetFirstFood();
        foodEaten++;
        energy += 0.20f;
        if (energy >= 1)
            energy = 1;
    }

    public void ReduceEnergy()
    {
        energy -= Time.deltaTime / 35;
        if (energy <= 0)
            Die();
    }

    public void AddOxygen()
    {
        oxygen += Time.deltaTime / 15 * 2;
        if (oxygen >= 1)
            oxygen = 1;
    }

    void ReduceOxygen()
    {
        oxygen -= Time.deltaTime / 15;
        if (oxygen <= 0)
            Die();
    }

    void AddGrowth()
    {
        growth += Time.deltaTime * 5;
        oxygen -= Time.deltaTime / 15 * 2;
        energy -= Time.deltaTime / 5;
        if (growth >= growthThreshold)
            Grow();
    }

    void Grow()
    {
        growth = 0;
        energy = energyThreshold / 2;
        growthStage++;
        if (growthStage >= growthGoal)
            Finish();

        // change the sprite
        ChangeChicken();

        // play anim
        PlayGrowAnim();

        // play sound
        GameObject.Find("SFX").GetComponent<SwimSFX>().GrowSFX();

        // add score
        GlobalGameManager.Instance.ScoreAdd(1000);
    }

    void PlayGrowAnim()
    {
        transform.Find("PlayerParticle").GetComponent<ParticleSystem>().Play();
    }

    public List<Sprite> chickenSprites = new List<Sprite>();

    void ChangeChicken()
    {
        GetComponent<SpriteRenderer>().sprite = chickenSprites[Mathf.Min(growthStage, chickenSprites.Count - 1)];
        ChangeLegs();
    }

    void ChangeLegs()
    {
        switch (growthStage)
        {
            case 1:
                legs.SetActive(false);
                //legs.transform.localPosition = new Vector2(-0.12f, -0.03f);
                break;
            case 2:
                legs.SetActive(false);
                break;
        }
    }

    void Finish()
    {
        //GameOver();
        StopSpawning();
        CalculateReward();
    }

    public Sprite deadChick;
    bool isDead;

    void Die()
    {
        isDead = true;
        //GameOver();

        // stop collision and particles
        GetComponent<CircleCollider2D>().enabled = false;
        transform.Find("Bubles").gameObject.SetActive(false);
        legs.SetActive(false);
        transform.rotation = Quaternion.identity;

        // focus on corpse
        CameraZoomTool camZoom = Camera.main.GetComponent<CameraZoomTool>();
        camZoom.PanTo(3);
        //camZoom.FocusTo(transform);

        // call Die() function
        SendMessage("PlayerDie");

        // make kinematic
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<SpriteRenderer>().sprite = deadChick;

        // always give zero stars if die
        foodEaten = 0;

        CalculateReward();
    }

    float CalculateFood()
    {
        return (foodEaten / foodSpawned);
    }

    public void CalculateReward()
    {
        foodScore = CalculateFood();
        CheckAllObjectives();
        Invoke("ReportScore", 2f);
    }

    public void StopSpawning()
    {
        SendMessage("TurnOff");
        //GetComponent<CircleCollider2D>().enabled = false;
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

        int stars = 0;
        bool knowledge = false;

        if (GetObjective("OneStar").isDone) stars = 1;
        if (GetObjective("TwoStar").isDone) stars = 2;
        if (GetObjective("ThreeStar").isDone) stars = 3;
        if (GetObjective("KnowStar").isDone) knowledge = true;

        // override transit to outro
        DataHolder.Instance.starsHolder = stars;
        DataHolder.Instance.knowHolder = knowledge;

        // if finished play outro
        if (!isDead)
        {
            GameObject.Find("SceneManager").GetComponent<SceneManager_nonfade>().ButtonPressed();
            UpdateMilestones(stars, knowledge, timeElapsed);
            return;
        }

        GameOver();
        return;

        // if not, show score
        anim.transform.parent.gameObject.SetActive(true);

        anim.SetInteger("Stars", stars);
        anim.SetBool("Knowledge", knowledge);

        PlayStarsAnime();

        // play music
        GameObject.Find("MusicManager").GetComponent<MusicManager>().PlayMusic("in game_music2_win");

        UpdateMilestones(stars, knowledge, timeElapsed);
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
