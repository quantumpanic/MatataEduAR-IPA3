using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    bool hasOxygenRise;
    public bool hasFirstOxygen;
    public bool hasFirstFood;
    public bool hasFirstGrowth;
    public bool hasMovedUp;
    public bool hasMovedDown;
    bool isCheckMove;
    bool canMove;

    public Text oxygenHintText;
    public Text foodHintText;
    public Text growthHintText;
    public Image movementHintImg;
    public Image movementHintImgUp;
    public Image movementHintImgDwn;
    public Image foodHintMarker;

	// Use this for initialization
	void Start ()
    {
        FadeGraphic(foodHintText.GetComponentInParent<Image>(), 0, 0);
        FadeGraphic(oxygenHintText.GetComponentInParent<Image>(), 0, 0);
        FadeGraphic(growthHintText.GetComponentInParent<Image>(), 0, 0);
        FadeGraphic(movementHintImg, 0, 0);
        FadeGraphic(movementHintImgDwn, 0, 0);
        FadeGraphic(movementHintImgUp, 0, 0);
        canMove = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isCheckMove && canMove)
        {
            if (Input.GetKey(KeyCode.UpArrow)) CheckFirstMove(1);
            if (Input.GetKey(KeyCode.DownArrow)) CheckFirstMove(-1);
        }

        if(Input.touches[0].phase == TouchPhase.Began)
        {
            if (Input.touches[0].position.y > Screen.height / 2) CheckFirstMove(1);
            if (Input.touches[0].position.y < Screen.height / 2) CheckFirstMove(-1);
        }
    }

    public void CheckHintAxis(SwimGame game)
    {
        if (!isCheckMove) return;

        if (game.transform.position.y > 0)
        {
            FadeGraphic(movementHintImgDwn, 1, .2f);
            FadeGraphic(movementHintImgUp, 0, .2f);
        }

        else
        {
            FadeGraphic(movementHintImgDwn, 0, .2f);
            FadeGraphic(movementHintImgUp, 1, .2f);
        }
    }

    void FadeGraphic(Graphic graphic, float amt, float time)
    {
        graphic.CrossFadeAlpha(amt, time, false);
        foreach (Graphic g in graphic.GetComponentsInChildren<Graphic>())
        {
            g.CrossFadeAlpha(amt, time, false);
        }
    }

    public void CheckFirstMove(int direction)
    {
        if (direction > 0) hasMovedUp = true;
        else hasMovedDown = true;

        if (hasMovedDown && hasMovedUp)
        {
            // stop showing movement
            FadeGraphic(movementHintImg, 0, 1);
            isCheckMove = false;
        }
    }

    public void ShowMoveHint()
    {
        FadeGraphic(movementHintImg, 1, 1);
        isCheckMove = true;
        canMove = true;
    }

    public void CheckFirstOxygen(SwimGame game)
    {
        // stop decrease oxygen and
        // stop spawning food until
        // player knows how to get oxygen
        
        if (isCheckMove || !canMove) return;

        game.isSwimming = (game.transform.position.y <= 0) ? true : false;

        if (!game.isSwimming)
        {
            game.AddOxygen();
            // show oxygen is rising
            if(!hasOxygenRise) OxygenRise();
        }

        if (game.oxygen > game.oxygenThreshold)
        {
            game.StartGame();
            game.SendMessage("TurnOn");
            GetFirstOxygen();
        }
    }

    void OxygenRise()
    {
        FadeGraphic(oxygenHintText.GetComponentInParent<Image>(), 1, 1);
        hasOxygenRise = true;
    }

    void FoodRise()
    {
        FadeGraphic(foodHintText.GetComponentInParent<Image>(), 1, 1);
        hasOxygenRise = true;
    }

    public void GetFirstOxygen()
    {
        hasFirstOxygen = true;
        FadeGraphic(oxygenHintText.GetComponentInParent<Image>(), 0, 1);
        Invoke("SpawnFirstFood", 3.5f);
    }

    void SpawnFirstFood()
    {
        GameObject.Find("FoodMarker").GetComponent<FoodMarker>().FindNewFood();
    }

    public void CheckFirstFood(SwimGame game)
    {
        if (hasFirstFood)
        {
            game.ReduceEnergy();
        }
    }

    public void GetFirstFood()
    {
        if (!hasFirstGrowth)
        {
            FadeGraphic(foodHintText.GetComponentInParent<Image>(), 1, 1);
            FadeGraphic(oxygenHintText.GetComponentInParent<Image>(), 0, 1);
            FadeGraphic(foodHintMarker.GetComponentInParent<Image>(), 0, 1);
            hasFirstFood = true;
        }
    }

    public void CheckFirstGrowth()
    {
        FadeGraphic(foodHintText.GetComponentInParent<Image>(), 0, 1);
        hasFirstGrowth = true;
        FadeGraphic(growthHintText.GetComponentInParent<Image>(), 1, 1);
        Invoke("GetFirstGrowth", 3);
    }

    void GetFirstGrowth()
    {
        FadeGraphic(growthHintText.GetComponentInParent<Image>(), 0, 1);
    }
}
