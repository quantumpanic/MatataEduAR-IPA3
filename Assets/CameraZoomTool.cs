using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomTool : MonoBehaviour {

    public Camera cam;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SnapTo(float size)
    {
        cam.orthographicSize = size;
    }

    public void PanTo(float size)
    {
        StartCoroutine(DoPan(size));
    }

    public void FocusTo(GameObject obj)
    {
        StartCoroutine(DoFocus(obj.transform.position));
    }

    public void FocusTo(Transform trn)
    {
        StartCoroutine(DoFocus(trn.position));
    }

    public void FocusTo(Vector2 point)
    {
        StartCoroutine(DoFocus(point));
    }

    IEnumerator DoPan(float newSize)
    {
        while (cam.orthographicSize != newSize)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newSize, Time.deltaTime * 3);
            yield return null;
        }

        cam.orthographicSize = newSize;
    }

    IEnumerator DoFocus(Vector2 point)
    {
        while (Vector2.Distance(cam.transform.position, point) > 0)
        {
            cam.transform.position = new Vector3(cam.transform.position.x,Mathf.Lerp(cam.transform.position.y, point.y, Time.deltaTime * 3), cam.transform.position.z);
            yield return null;
        }

        cam.transform.position = point;
    }
}
