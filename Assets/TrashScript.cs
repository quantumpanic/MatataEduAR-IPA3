using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour {

    private Rigidbody2D rb;
    public GameObject target;
    Vector2 startPos;
    public float forceMultiplier = 5;

    public int trashType;
    public List<Sprite> trashSprites = new List<Sprite>();

    public int containerType;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        if (!isGhost) ResetTrash();
        else ResetGhost();
    }

    // Update is called once per frame
    void Update ()
    {
        CheckTouchInput();

        // if ball is below starting point
        if (transform.position.y < startPos.y - 5)
        {
            if (!isGhost)
            {
                Score(false);
                ResetTrash();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            {
                return;
                // else shoot
                if (!rb.simulated) Shoot();
            }
        }
    }

    public bool isGhost;
    bool hasShot;

    public void Shoot()
    {
        if (hasShot) return;
        hasShot = true;
        rb.simulated = true;
        Vector2 force = target.transform.position - transform.position;
        rb.AddForce(force * rb.mass * forceMultiplier * 100);
        game.trashLeft--;

        Invoke("ResetTrash", 5F); // if trash is stuck
    }

    void SimulateShoot()
    {
        rb.simulated = true;
        Vector2 force = target.transform.position - transform.position;
        rb.AddForce(force * rb.mass * forceMultiplier * 100);
    }

    public GarbageTossGame game;

    void Score(bool score = true)
    {
        if (!score) print("oops");
        if (score) game.AddScore(); else game.ComboBreak();
    }

    bool hasContainer;

    public void EnterContainer(TrashObstacle container)
    {
        containerType = container.containerType;
        hasContainer = true;

        ResetContainers(container);
    }

    void ResetTrash()
    {
        hasShot = false;
        hasContainer = false;
        RandomTrash();

        transform.position = startPos;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.simulated = false;

        transform.SetAsLastSibling();
        transform.SetSiblingIndex(transform.GetSiblingIndex() - 2);

        game.ResetTrash();
        ResetContainers();

        CancelInvoke("ResetTrash"); // for trash stuck
    }

    void ResetGhost()
    {
        GetComponent<TrailRenderer>().Clear();

        transform.position = startPos;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.simulated = false;

        transform.SetAsLastSibling();
        transform.SetSiblingIndex(transform.GetSiblingIndex() - 2);
    }

    void ResetContainers(TrashObstacle except = null)
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("TrashCollider"))
        {
            if (except != null && except.gameObject == g) continue;
            g.SendMessage("ResetCollider");
        }
    }

    void ChangeTrash(int type)
    {
        trashType = type;
        GetComponent<UnityEngine.UI.Image>().sprite = trashSprites[type];

        // update the game values
        game.trashType = type;
    }

    void RandomTrash()
    {
        int rnd = Random.Range(0, trashSprites.Count);
        ChangeTrash(rnd);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // check if inside the bin
        if (collision.collider is BoxCollider2D)
        {
            // if ball is stuck above the starting point
            if (rb.velocity.magnitude < .5f)// && hasContainer)
            {
                if (!isGhost)
                {
                    // score
                    if (trashType == containerType)
                        Score();

                    else
                        Score(false);

                    ResetTrash();
                    GameObject.Find("GhostTrash").SendMessage("ResetGhost");
                }

                else
                {
                    rb.simulated = false;
                }
            }
        }
    }

    public LayerMask touchInputMask;
    private List<GameObject> touchList = new List<GameObject>();
    public GameObject[] touchesOld;
    public RaycastHit hit;
    public RaycastHit oldHit;

    public bool isDraggingPhase;
    public Transform hitTrans;
    public Transform oldHitTrans;
    RaycastHit firstHit;

    void CheckTouchInput()
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
        Vector3 shootMouse = Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition);

        // if mouse is over shoot button, stop here
        if (RectTransformUtility.RectangleContainsScreenPoint(GameObject.Find("SHOOT").GetComponent<RectTransform>(), shootMouse)) return;

        if (Input.GetMouseButtonDown(0))// || Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            // shoot at position of ray hit
            target.transform.position = shootMouse - (Vector3.forward * shootMouse.z);
            if (isGhost)
            {
                ResetGhost();
                SimulateShoot();
            }
        }
#endif
#if UNITY_ANDROID
        Vector3 shootTouch = Camera.allCameras[0].ScreenToWorldPoint(Input.touches[0].position);

        // if mouse is over shoot button, stop here
        if (RectTransformUtility.RectangleContainsScreenPoint(GameObject.Find("SHOOT").GetComponent<RectTransform>(), shootTouch)) return;

        if (Input.touches[0].phase == TouchPhase.Began)
        {
            // shoot at position of ray hit
            target.transform.position = shootTouch - (Vector3.forward * shootTouch.z);
            if (isGhost)
            {
                ResetGhost();
                SimulateShoot();
            }
        }
#endif
    }
}
