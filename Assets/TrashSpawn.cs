using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour {

    public List<Sprite> sprites = new List<Sprite>();

	// Use this for initialization
	void Start () {
        // choose a random sprite
        int rnd = Random.Range(0, sprites.Count);
        GetComponent<SpriteRenderer>().sprite = sprites[rnd];

        Destroy(gameObject, 6);
	}
	
	// Update is called once per frame
	void Update () {
        // keep turning the sprite
        transform.Rotate(Vector3.forward * 8);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "SACK")
        {
            GameObject.Find("Player").SendMessage("GetTrash");
            GetComponent<SpriteRenderer>().sprite = null;
            GetComponent<AudioSource>().Play();
            Destroy(gameObject,1);
        }
    }
}
