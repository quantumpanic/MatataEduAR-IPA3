using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StretchScaleTool : MonoBehaviour
{
    public bool FitScreen;
    public bool FitWidth;

    // Use this for initialization
    void Start () {
		// get our rect
		RectTransform thisRect = GetComponent<RectTransform> ();

        // fit or not
        float widthRef;
        float heightRef;

        if (FitScreen)
        {
            widthRef = (float)Screen.width;
            heightRef = (float)Screen.height;
        }
        else
        {
            widthRef = thisRect.rect.width;
            heightRef = thisRect.rect.height;
        }

        // first get the wide side scale to fit
        float canvasWidth = 800;
        float canvasHeight = 600;
        float rectWidth = thisRect.rect.width;
        float rectHeight = thisRect.rect.height;

        float toScaleX = canvasWidth / rectWidth;
        float toScaleY = canvasHeight / rectHeight;

        // now match the height
        float actualScreenRatio;
		float rectRatio;

        // invert if fit width
        if (FitWidth)
        {
            actualScreenRatio = widthRef / heightRef;
            rectRatio = thisRect.rect.width / thisRect.rect.height;
        }

        else
        {
            actualScreenRatio = heightRef / widthRef;
            rectRatio = thisRect.rect.height / thisRect.rect.width;
        }

        float scaleDown = rectRatio / actualScreenRatio;

        // fit width or height
        if (FitWidth) toScaleY = toScaleX * scaleDown;
        else toScaleX = toScaleY * scaleDown;


		// scale it
		thisRect.localScale = new Vector3(toScaleX, toScaleY, 1);

		// bring objects to front anchor
		foreach (ScrollRect trns in gameObject.GetComponentsInChildren<ScrollRect>()) {
			if (trns.gameObject == gameObject)
				continue;
			trns.transform.parent = GameObject.Find ("AnchorFront").transform;
		}
	}

    // Update is called once per frame
    void Update () {
		
	}
}
