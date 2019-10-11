using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool isDebris;
    int debrisHealth = 3;

    void BreakDebris()
    {
        if (isDebris)
        {
            GetComponentInChildren<Animator>().Play("PairWrong");
            debrisHealth--;
            if (debrisHealth <= 0) Hide();
        }
    }

    void Hide()
    {
        GetComponentInChildren<Animator>().Play("PairCorrect");
        GetComponent<Button>().interactable = false;
    }

    public Sprite debrisImage;

    void ResetButton()
    {
        isDebris = false;

        if (transform.GetChild(0).GetComponent<Image>().sprite == debrisImage)
        {
            isDebris = true;
            debrisHealth = 3;
        }

        GetComponentInChildren<Animator>().Play("PairSpawn");
        GetComponent<Button>().interactable = true;
    }

    public void MakeDebris()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = debrisImage;
        ResetButton();
    }
}
