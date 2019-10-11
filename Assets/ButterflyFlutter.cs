using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButterflyFlutter : MonoBehaviour {

    RectTransform rect;
    Image img;

    float width;
    float height;

	// Use this for initialization
	void Start ()
    {
        rect = GetComponent<RectTransform>();
        img = GetComponentInChildren<Image>();

        // get corners
        Vector3[] corners = new Vector3[4];
        img.canvas.GetComponent<RectTransform>().GetWorldCorners(corners);
        width = corners[2].x - corners[0].x;
        height = corners[1].y - corners[0].y;
    }

    // Update is called once per frame
    void Update()
    {
        // move the butterfly around

        // turn a random angle
        float angle = Random.Range((float)-15, (float)15);
        rect.Rotate(0, 0, angle);

        // keep the last position
        Vector2 lastPos = rect.position;

        // then move forward
        rect.Translate(0, 0.1f, 0);

        // always keep sprite upright
        img.rectTransform.rotation = Quaternion.identity;

        // face direction of movement unless vertical movement is significant
        if (rect.position.y - lastPos.y < 0.2f || rect.position.y - lastPos.y > -0.2f)
        {
            if (rect.position.x > lastPos.x) img.rectTransform.localScale = new Vector2(1, 1);
            else img.rectTransform.localScale = new Vector2(-1, 1);
        }

        // wrap around bounds
        bool outBounds = false;
        if (rect.position.x >= (width / 2) + 3) outBounds = true;
        if (rect.position.x < -(width / 2) - 3) outBounds = true;
        if (rect.position.y >= (height / 2) + 3) outBounds = true;
        if (rect.position.y < -(height / 2) - 3) outBounds = true;

        if (outBounds) rect.position = new Vector3(-rect.position.x, -rect.position.y, 0);
    }
}
