using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterfallSegment : MonoBehaviour {

    public GameObject water1;
    public GameObject water2;
    public GameObject water3;
    int waterStage = 5; // 5 is fully opaque, 0 is transparent

    public List<GameObject> turbines = new List<GameObject>();
    public List<GameObject> walls = new List<GameObject>();

    public GameObject current;
    public GameObject dam;

    // Use this for initialization
    void Start () {
        //InvokeRepeating("FadeWaterOneStageDown", 2, 2);
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void FadeWaterOneStageDown()
    {
        FadeWaterOneStage(true);
    }

    public void FadeWaterOneStageUp()
    {
        FadeWaterOneStage(false);
    }

    public void FadeWaterOneStage(bool fadeOut = true)
    {
        waterStage += fadeOut ? -1 : 1;
        if (waterStage > 5) waterStage = 5;
        if (waterStage < 0) waterStage = 0;
        FadeWater();
    }

    public void FadeWaterToStage(int stage, bool fadeOut = true)
    {
        waterStage = stage;
        FadeWater();
    }

    public void FadeWaterOut()
    {
        FadeWaterToStage(0);
    }

    public void FadeWaterIn()
    {
        FadeWaterToStage(5);
    }

    void FadeWater()
    {
        StartCoroutine(DoFadeWater());
    }

    public float currentOpacity = 1F;

    IEnumerator DoFadeWater()
    {
        float targetOpacity = waterStage * 0.2F;
        
        while (!Mathf.Approximately(currentOpacity,targetOpacity))
        {
            currentOpacity = Mathf.Lerp(currentOpacity, targetOpacity, Time.deltaTime * 30);
            water1.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, currentOpacity);
            water2.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, currentOpacity);
            water3.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, currentOpacity);
            yield return null;
        }

        water1.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, targetOpacity);
        water2.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, targetOpacity);
        water3.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, targetOpacity);
    }

    public int sacksCount = 0;
    public bool hasMaxSacks;
    public GameObject[] sackObjects;

    public void DropSack()
    {
        hasMaxSacks = false;

        sacksCount++;
        if (sacksCount >= 5)
        {
            sacksCount = 5;
            hasMaxSacks = true;
        }

        sackObjects[sacksCount - 1].GetComponent<Animator>().Play("SackDrop");
        FadeWaterOneStageDown();
        
        if (hasMaxSacks)
        {

        }
    }

    public Sprite turbine;
    public Sprite wall;

    public void BuildDam(Button button)
    {
        callBackButton = button;
        var image = button.transform.GetChild(0).GetComponent<Image>().sprite;
        if (image == turbine) BuildTurbine();
        if (image == wall) BuildWall();
    }

    Button callBackButton;

    void CannotBuildCallback()
    {
        callBackButton.SendMessage("MakeDebris");
    }

    public bool damFinished;

    void IsDamFinished()
    {
        if (allTurbinesBuilt && allWallsBuilt)
        {
            // move to next segment
            damFinished = true;
        }
    }

    bool allTurbinesBuilt;
    bool allWallsBuilt;

    public List<GameObject> clones = new List<GameObject>();
    public List<GameObject> cloneList = new List<GameObject>();

    void BuildTurbine()
    {
        int count = 0;

        foreach (GameObject turbine in turbines)
        {
            count++;
            if (!cloneList.Contains(turbine))
            {
                // clone the object and set active;
                var clone = Instantiate(turbine);
                clone.transform.position = turbine.transform.position;
                clones.Add(clone);
                cloneList.Add(turbine);
                clone.SetActive(true);

                // callback the button
                callBackButton.SendMessage("Hide");
                if (count >= turbines.Count) break;
                return;
            }
        }

        // reach here if all turbines built
        if (!allTurbinesBuilt) allTurbinesBuilt = true;
        else CannotBuildCallback();

        // check dam finished
        IsDamFinished();
    }

    void BuildWall()
    {
        int count = 0;

        foreach (GameObject wall in walls)
        {
            count++;
            if (!cloneList.Contains(wall))
            {
                // clone the object and set active;
                var clone = Instantiate(wall);
                clone.transform.position = wall.transform.position;
                clones.Add(clone);
                cloneList.Add(wall);
                clone.SetActive(true);

                // callback
                callBackButton.SendMessage("Hide");
                if (count >= turbines.Count) break;
                return;
            }
        }

        // all walls built
        if (!allWallsBuilt) allWallsBuilt = true;
        else CannotBuildCallback();

        // check dam finished
        IsDamFinished();
    }

    public void DestroyDam()
    {
        // set dam for impact with current
        foreach (GameObject c in clones)
        {
            c.GetComponent<Rigidbody2D>().simulated = true;
            c.GetComponent<Animator>().Play("PairCorrect");
        }

        //foreach (GameObject t in turbines)
        //{
        //    t.GetComponent<Rigidbody2D>().simulated = true;
        //    t.GetComponent<Animator>().Play("PairCorrect");
        //}

        //foreach (GameObject w in walls)
        //{
        //    w.GetComponent<Rigidbody2D>().simulated = true;
        //    w.GetComponent<Animator>().Play("PairCorrect");
        //}
    }

    public void DestroySacks()
    {
        // set sacks for impact with current
        foreach (GameObject s in sackObjects)
        {
            s.GetComponent<Animator>().enabled = false;
        }

        EnableCurrent();
    }

    public void ResetSacks()
    {
        // set sacks for impact with current
        foreach (GameObject s in sackObjects)
        {
            s.GetComponent<Animator>().enabled = true;
            s.GetComponent<Animator>().Play("SackIdle");
        }

        EnableCurrent();
    }

    void EnableCurrent()
    {
        current.SetActive(true);
    }

    void ResetCurrent()
    {
        current.transform.GetChild(0).transform.position = Vector3.zero;
        current.SetActive(false);
    }
}
