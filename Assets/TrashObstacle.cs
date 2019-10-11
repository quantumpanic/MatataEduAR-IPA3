using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObstacle : MonoBehaviour {

    public int containerType;
    Vector2 startPos;
    public bool moving;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
        ResetCollider();
	}
	
	// Update is called once per frame
	void Update () {
        if (moving) MoveSideways();
	}

    public Collider2D col;
    public Collider2D col2;
    bool hasReset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if already triggered, don't trigger again
        if (hasReset) return;
        hasReset = true;

        col.enabled = true;
        col2.enabled = true;

        // check if trash order is lower than this image
        int deduct = 0;
        if (collision.transform.GetSiblingIndex() < transform.GetSiblingIndex()) deduct = 1;

        // put the trash order behind this image
        collision.transform.SetSiblingIndex(transform.GetSiblingIndex() - deduct);

        // set the container type
        collision.GetComponent<TrashScript>().EnterContainer(this);
    }

    void ResetCollider()
    {
        StartCoroutine(DoResetCollider());
    }

    IEnumerator DoResetCollider()
    {
        yield return new WaitForFixedUpdate();
        col.enabled = false;
        col2.enabled = false;

        hasReset = false;
    }

    void MoveSideways()
    {
        
    }
}
