using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARDragDrop : MonoBehaviour
{

    public LayerMask touchInputMask;
    private List<GameObject> touchList = new List<GameObject>();
    public GameObject[] touchesOld;
    public RaycastHit hit;
    public RaycastHit oldHit;

    // Use this for initialization
    void Start() {

    }

    public bool isDraggingPhase;
    public Transform hitTrans;
    public Transform oldHitTrans;
    RaycastHit firstHit;

    public bool plantMode;
    public Button button;
    public GameObject plantHint;

    public void PlantMode()
    {
        if (isGrowing) return;

        if (plantMode)
        {
            plantMode = false;
            button.image.color = button.colors.normalColor;
            plantHint.SetActive(false);
        }

        else
        {
            plantMode = true;
            button.image.color = button.colors.pressedColor;
            plantHint.SetActive(true);
        }
    }

    bool isGrowing;

    public void PlantGrow()
    {
        plantMode = false;
        isGrowing = true;
        button.image.color = button.colors.pressedColor;
    }

    // Update is called once per frame
    void Update()
    {
        hitTrans = hit.transform;
        oldHitTrans = oldHit.transform;

        if (isDraggingPhase && hit.transform && oldHit.transform)
        {
        }
        else
        {
            isDraggingPhase = false;
        }

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
        Ray ray = Camera.allCameras[1].ScreenPointToRay(Input.mousePosition);
        // Debug.DrawRay(ray1.origin, ray1.direction, Color.yellow);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchInputMask))
        {
            GameObject clicked = hit.transform.gameObject;
            //if (clicked == null) return;
            if (clicked != null)
            {
                Debug.DrawLine(ray.origin, hit.point);
                // show active blocks here
            }
        }

        if (Input.GetMouseButtonUp(0))// || Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            // release seed at position of ray hit
            if (plantMode)
            {
                ReleaseSeed(hit.point);
            }
        }
#endif
#if UNITY_ANDROID || UNITY_IOS
        Ray rayMouse = Camera.allCameras[1].ScreenPointToRay(Input.touches[0].position);

        if (Physics.Raycast(rayMouse, out hit, Mathf.Infinity, touchInputMask))
        {
            GameObject clicked = hit.transform.gameObject;
            //if (clicked == null) return;
            if (clicked != null)
            {
                Debug.DrawLine(rayMouse.origin, hit.point);
                // show active blocks here
            }
        }

        if (Input.touches[0].phase == TouchPhase.Began)
        {
            // release seed at position of ray hit
            if (plantMode)
            {
                ReleaseSeed(hit.point);
            }
        }
#endif
    }

    public GameObject seedObject;
    public GameObject marker;

    void ReleaseSeed(Vector3 point)
    {
        seedObject.transform.position = point;
        marker.transform.position = point;
        PlantMode();
    }
}