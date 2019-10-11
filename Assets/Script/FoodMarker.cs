using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMarker : MonoBehaviour {

    public GameObject foodObject;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        // always attach to a food object
        if (!foodObject) return;

        transform.position = new Vector2(foodObject.transform.position.x + 3, foodObject.transform.position.y);
        if (transform.position.x < Camera.main.transform.position.x -7)
        {
            FindNewFood();
        }
	}

    public void FindNewFood()
    {
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject food in foods)
        {
            if (food.transform.position.x > 0)
            {
                foodObject = food;
            }
        }
    }
}
