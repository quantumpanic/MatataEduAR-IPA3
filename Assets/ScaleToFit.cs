using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleToFit : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        RectTransform rectTrans = GetComponent<RectTransform>();

        // get the image ratio
        float imgRatio = (float)rectTrans.rect.width / (float)rectTrans.rect.height;
        float screenRatio = (float)Screen.width / (float)Screen.height;

        //print(imgRatio);
        //print(screenRatio);

        bool screenIsWider = screenRatio > imgRatio;

        RectTransform canvasRect = GetComponent<Image>().canvas.GetComponent<RectTransform>();

        if (!screenIsWider)
        {
            // scale image height to match screen height so width is clipped
            rectTrans.sizeDelta = new Vector2((float)canvasRect.rect.height * imgRatio, canvasRect.rect.height);
        }

        else
        {
            // scale up image width instead
            rectTrans.sizeDelta = new Vector2(canvasRect.rect.width, (float)canvasRect.rect.width / imgRatio);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
