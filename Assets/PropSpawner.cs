using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour {

    public Transform islandPos;
    public Transform coralPos;
    public List<Sprite> coralSprites = new List<Sprite>();

    public GameObject island;
    public GameObject coral;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("SpawnIsland", 1, 10);
        InvokeRepeating("SpawnCoral", 0, 6);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnIsland()
    {
        island.transform.position = islandPos.position;
    }

    void SpawnCoral()
    {
        coral.GetComponent<SpriteRenderer>().sprite = coralSprites[Random.Range(0, 3)];
        coral.transform.position = coralPos.position;
    }
}
