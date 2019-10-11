using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarbageTossGame : GameManagerClass
{

    // Use this for initialization
    new void Start()
    {
        Transform holder = GameObject.Find("Objectives").transform;
        UpdateUI();

        StartGame();
        CalculateSpawn();
        SpawnContainers();

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

    // Update is called once per frame
    void Update()
    {

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
    new public int score;
    public int combo = 0;
    public int trashLeft;
    public int trashType;

    public Text scoreCounter;
    public Text comboCounter;
    public Text trashCounter;

    void UpdateUI()
    {
        scoreCounter.text = "Score: " + score.ToString();
        comboCounter.text = "x" + combo.ToString();
        trashCounter.text = MakeTrashString();

        CheckTrash();
    }

    string MakeTrashString()
    {
        string bullet = "•";
        string trashString = "";

        int rowLen = 15;
        int leftOver = trashLeft % rowLen;
        for (int x = 0; x < leftOver; x++)
        {
            trashString += bullet;
        }

        trashString += '\n';

        int remainder = trashLeft - leftOver;
        int rows = remainder / rowLen;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < rowLen; x++)
            {
                trashString += bullet;
            }

            trashString += '\n';
        }

        trashString += "Trash Left";

        return trashString;
    }

    void CheckTrash()
    {
        if (trashLeft <= 0)
        {
            GameOver();
        }
    }

    public void AddScore()
    {
        score += (combo + 1);
        combo++;
        UpdateUI();

        // add score
        GlobalGameManager.Instance.ScoreAdd(100 * combo);
    }

    public void ComboBreak()
    {
        combo = 0;
        UpdateUI();
    }

    public GameObject binPrefab;
    int binsToSpawn;
    List<GameObject> bins = new List<GameObject>();
    GameObject movingBin1;
    GameObject movingBin2;
    List<GameObject> movingBins = new List<GameObject>();
    public List<Transform> binPos = new List<Transform>();
    List<int> binTypes = new List<int>() { 0, 1, 2 };
    public List<string> binTypeLbls = new List<string>();

    public void SpawnContainers()
    {
        // spawn some bins

        // destroy current bins
        ClearBins();
        
        // first get the iterator
        for (int x = 0; x < binsToSpawn; x++)
        {
            var g = Instantiate(binPrefab, transform);
            g.transform.SetAsFirstSibling();
            bins.Add(g);
        }

        // get the random positions in binPos
        List<Transform> tempPos = new List<Transform>(binPos);
        List<int> tempTypes = new List<int>(binTypes);

        bool hasCorrectType = false;

        foreach (GameObject bin in bins)
        {
            // position
            int rndmPos = Random.Range(0, tempPos.Count - 1);
            Vector2 basePos = tempPos[rndmPos].position;

            // randomize the position
            basePos.Set(basePos.x + Random.Range(-.5f, .5f), basePos.y + Random.Range(-.5f, .5f));

            bin.transform.position = basePos;
            tempPos.RemoveAt(rndmPos);

            // type
            // get the correct type first, then randomize for the rest
            int rndmType;
            if (!hasCorrectType)
            {
                rndmType = trashType;
                hasCorrectType = true;
            }
            else
            {
                rndmType = Random.Range(0, tempTypes.Count - 1);
            }

            // get the trash type
            int typeIndex = tempTypes[rndmType];

            bin.GetComponent<TrashObstacle>().containerType = typeIndex;
            tempTypes.RemoveAt(rndmType);

            // update the text
            if (bins.Count > 1) bin.GetComponentInChildren<Text>().text = binTypeLbls[typeIndex];
        }
    }

    void ClearBins()
    {
        foreach (GameObject bin in bins)
        {
            Destroy(bin);
        }
        bins.Clear();
    }

    void CalculateSpawn()
    {
        // based on combo, calculate how to spawn bins

        // first determine how many bins to spawn
        // usually up to 5x combo spawns 1 bin
        // up to x10 spawns 2 bins
        // x11 and beyond 3 bins
        if (combo < 5) binsToSpawn = 1;
        else if (combo < 10) binsToSpawn = 2;
        else binsToSpawn = 3;

        // now determine the moving bin(s)
        // for easy levels, no moving bins
        // then after 2 static bins, add 1 moving bin
        // lastly when there are 3 bins, have 2 moving bins
        movingBins.Clear();
        if (binsToSpawn == 2) movingBins.Add(movingBin1);
        if (binsToSpawn == 3)
        {
            movingBins.Add(movingBin1);
            movingBins.Add(movingBin2);
        }
    }

    public void ResetTrash()
    {
        if (!isPlaying)
        {
            ClearBins();
            return;
        }

        CalculateSpawn();
        SpawnContainers();
    }
}
