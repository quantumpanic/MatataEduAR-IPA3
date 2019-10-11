using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScalar : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    private void OnEnable()
    {
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(
            gameObject.GetComponent<RectTransform>().sizeDelta.x,
            gameObject.GetComponent<RectTransform>().sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {

    }
}