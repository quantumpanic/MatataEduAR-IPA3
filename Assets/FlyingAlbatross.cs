using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingAlbatross : MonoBehaviour
{

    public Camera cam;
    public Transform body;
    private Rigidbody2D _rb;

    public int lives;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = (Vector3.right * 8f);

        ResetBird();
    }

    bool readyMode;
    public GameObject readyPanel;

    void ReadyMode()
    {
        InvokeRepeating("SimulateTap", 0, 0.8F);
        readyMode = true;

        SendMessage("TurnOff");

        readyPanel.SetActive(true);
    }

    void ExitReadyMode()
    {
        if (!readyMode) return;

        nextSpawn = transform.position.x;
        readyMode = false;
        SendMessage("TurnOff");

        readyPanel.SetActive(false);
    }

    void SimulateTap()
    {
        if (readyMode == false)
        {
            CancelInvoke("SimulateTap");
            return;
        }
        transform.position = Vector3.right * transform.position.x + Vector3.up * 0.5F;
        _rb.velocity = (Vector3.right * 8f);
        _rb.AddForce(Vector2.up * 200);
    }

    bool hasTapped;
    float tapCooldown;
    float tapMaxTime = 0.3F ; // 300 ms

    // Update is called once per frame
    void Update()
    {
        //camera monitors the fish
        cam.transform.position = Vector3.right * (4 + transform.position.x) + Vector3.back;
        //asking fish movement along the x-axis

        // check if below or above line
        if (transform.position.y <= -5) Crashed();

        if (Input.GetKey(KeyCode.UpArrow))
        {
            ExitReadyMode();

            if (!crashed && !hasTapped)
            {
                _rb.velocity = (Vector3.right * 8f);
                _rb.AddForce(Vector2.up * 200);
                tapCooldown = 0;
                hasTapped = true;
            }
        }

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            ExitReadyMode();

            if (!crashed && !hasTapped)
            {
                _rb.velocity = (Vector3.right * 8f);
                _rb.AddForce(Vector2.up * 200);
                tapCooldown = 0;
                hasTapped = true;
            }
        }
#endif

        if (hasTapped)
        {
            tapCooldown += Time.deltaTime;
            if (tapCooldown >= tapMaxTime) hasTapped = false;
        }

        if (readyMode) return;

        xPos = transform.position.x;
        if (xPos >= nextSpawn)
        {
            CreateNextSection();
            nextSpawn += 25;

            doubleSpawnCounter++;
            if (doubleSpawnCounter >= doubleSpawnLimit)
            {
                isDoubleSpawn = true;
            }
        }
    }

    int doubleSpawnCounter;
    int doubleSpawnLimit = 5;

    public List<GameObject> prefabs = new List<GameObject>();
    private GameObject sectionBuffer;
    private float xPos;
    private float nextSpawn;

    void CreateNextSection()
    {
        // use the building blocks to make the next section
        var section = CreateObstacles();
        section.transform.position = Vector2.right * (nextSpawn + 25);
        Destroy(section, 6);
    }

    void CreateBackground()
    {

    }

    GameObject CreateObstacles()
    {
        sectionBuffer = new GameObject("Section");

        // make a copy of the prefab list
        List<GameObject> tempList = new List<GameObject>(prefabs);

        // there are X columns in each section
        // fill each randomly with the prefabs
        // at least one column must be empty so the player can pass through

        bool hasOpening = false;
        bool hasRemoved = false;
        GameObject col;

        for (int x = 0; x < 3; x++)
        {
            if (!hasOpening)
            {
                bool isOpening = (Random.Range(0, 2) > 0) ? true : false;
                // if 5th column, force true
                if (x == 4) isOpening = true;

                if (isOpening)
                {
                    hasOpening = true;
                    col = null;
                    continue;
                }
            }

            int rnd = Random.Range(0, tempList.Count);
            // pick a random column to spawn
            col = Instantiate(tempList[rnd], sectionBuffer.transform);

            // get the random y position
            int rndy = Random.Range(0, 2);
            float yPos = -5 + (10 * rndy);
            col.transform.localPosition = new Vector2(x * 6, yPos);

            // has a chance of spawning double columns
            if (isDoubleSpawn)
            {
                bool doSpawn = false;
                float chance = Random.Range(0f, 1f);
                if (chance > .33f) doSpawn = true;

                if (doSpawn)
                {
                    isDoubleSpawn = false;
                    doubleSpawnCounter = 0;

                    var col2 = Instantiate(tempList[rnd], sectionBuffer.transform);

                    // flip the y position
                    yPos = -yPos;
                    col2.transform.localPosition = new Vector2(x * 6, yPos);
                }
            }

            tempList.RemoveAt(rnd);

            // remove conflicting columns
            if (!hasRemoved)
            {
                if (rnd == 0)
                    tempList.RemoveAt(1);
                if (rnd == 1)
                    tempList.RemoveAt(0);
                if (rnd == 2)
                    tempList.RemoveAt(0);

                hasRemoved = true;
            }
        }

        return sectionBuffer;
    }

    bool isDoubleSpawn;

    bool crashed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Crashed();
    }

    void Crashed()
    {
        if (crashed) return;

        crashed = true;
        lives--;
        Invoke("ResetBird", 2);

        GameObject.Find("SFX").GetComponent<AudioSource>().Play();
        GameObject.Find("PlayerParticle").GetComponent<ParticleSystem>().Play();
    }

    public int trashCollected;
    public int trashNeeded;
    bool goalMet;
    public Text livesLabel;

    void GetTrash()
    {
        trashCollected++;
        GameObject.Find("TRASH").GetComponent<Text>().text = trashCollected.ToString() + "/" + trashNeeded.ToString();
        GlobalGameManager.Instance.ScoreAdd(100);

        if (trashCollected >= trashNeeded)
        {
            _rb.isKinematic = true;
            GetComponent<Collider2D>().enabled = false;
            GameObject.Find("Objectives").SendMessage("Victory");
        }
    }

    void UpdateLivesUI()
    {
        string hearts = "";
        for (int count = 0; count < lives; count++)
        {
            hearts += "❤";
        }
        livesLabel.text = "Lives " + hearts;
    }

    void ResetBird()
    {
        if (lives <= 0)
        {
            GameObject.Find("Objectives").SendMessage("GameOver");
            return;
        }

        UpdateLivesUI();

        crashed = false;
        transform.position = Vector3.zero;
        _rb.velocity = (Vector3.right * 8f);
        xPos = 0;
        nextSpawn = 0;

        ReadyMode();

        GameObject.Find("SACK").transform.position = Vector3.zero;

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            Destroy(g);
        }

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Coin"))
        {
            Destroy(g);
        }

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Trash"))
        {
            Destroy(g);
        }
    }
}
