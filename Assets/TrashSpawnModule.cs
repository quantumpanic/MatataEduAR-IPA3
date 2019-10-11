using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawnModule : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SpawnTrash();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // trash prefab
    public GameObject trashPrefab;

    // the positions to spawn trash
    public List<Transform> trashPos = new List<Transform>();

    void SpawnTrash()
    {
        foreach(Transform t in trashPos)
        {
            Instantiate(trashPrefab, t.position, Quaternion.identity);
        }
    }
}
