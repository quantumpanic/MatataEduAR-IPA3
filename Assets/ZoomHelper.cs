using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomHelper : MonoBehaviour {

    public Camera camera;

	// Use this for initialization
	void Start () {

    }

    Vector2 oldFirstPos;
    Vector2 firstTouchPos;
    Vector2 oldSecondPos;
    Vector2 secondTouchPos;
    float distanceDelta;

    // Update is called once per frame
    void Update() {
#if UNITY_STANDALONE || UNITY_EDITOR
        // check mouse up
        if (Input.GetMouseButtonUp(0))
        {

        }

#endif

#if UNITY_ANDROID || UNITY_IOS

        bool hasTouch = false;

        // get the first touch
        if (Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Stationary)
        {
            hasTouch = true;
            oldFirstPos = firstTouchPos;
            firstTouchPos = Input.touches[0].position;
        }

        // now the second touch
        if (hasTouch)
        {
            if (Input.touches[1].phase == TouchPhase.Began || Input.touches[1].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Stationary)
            {
                oldSecondPos = secondTouchPos;
                secondTouchPos = Input.touches[1].position;
            }

            // if both touches registered, check their distance
            float oldDist = Vector2.Distance(oldFirstPos, oldSecondPos);
            float newDist = Vector2.Distance(firstTouchPos, secondTouchPos);
            if (oldDist != newDist)
            {
                Zoom(newDist - oldDist);
            }
        }
#endif
    }

    void Zoom(float amt)
    {
        camera.orthographicSize += amt * 0.1f;
        if (camera.orthographicSize > 8) camera.orthographicSize = 8;
        if (camera.orthographicSize < 5) camera.orthographicSize = 5;
    }
}
