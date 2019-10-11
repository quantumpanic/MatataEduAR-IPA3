using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chapter1 : MonoBehaviour {

    public float elapsedTime;
    public float timeLimit;
    float startTime;

	public void DisableBoolAnimator (Animator anim)
	{
		anim.SetBool ("IsDisplayed", false);
	}

	public void EnableBoolAnimator (Animator anim)
	{
		anim.SetBool ("IsDisplayed", true);
	}

    private void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        // check mouse down
        //if (Input.GetMouseButtonDown(0)) RevealObject();

        // check mouse up
        if (Input.GetMouseButtonUp(0)) if (game.isPlaying) CheckChoices();

#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touches[0].phase == TouchPhase.Ended)
        {
            if (game.isPlaying) CheckChoices();
        }
#endif
    }

    public List<GameObject> choicesAlive = new List<GameObject>();
    public List<GameObject> choicesDead = new List<GameObject>();
    public List<GameObject> alive = new List<GameObject>();
    public List<GameObject> dead = new List<GameObject>();
    public List<GameObject> found = new List<GameObject>();

    public RectTransform aliveRect;
    public RectTransform deadRect;

    public ParticleSystem particle;

    public void RevealObject(GameObject clicked)
    {
        foreach (GameObject obj in choicesAlive)
        {
            if (clicked == obj && !found.Contains(obj))
            {
                obj.GetComponentInChildren<Image>().color = Color.white;
                obj.GetComponentInChildren<Image>().rectTransform.sizeDelta = Vector2.one * 100;
                obj.GetComponentInChildren<Image>().rectTransform.rotation = Quaternion.identity;
                PlaySparkleAnim(obj.transform.position);

                found.Add(obj);

                clicked.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
        }

        foreach (GameObject obj in choicesDead)
        {
            if (clicked == obj && !found.Contains(obj))
            {
                obj.GetComponentInChildren<Image>().color = Color.white;
                obj.GetComponentInChildren<Image>().rectTransform.sizeDelta = Vector2.one * 100;
                obj.GetComponentInChildren<Image>().rectTransform.rotation = Quaternion.identity;
                PlaySparkleAnim(obj.transform.position);

                found.Add(obj);

                clicked.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    void PlaySparkleAnim()
    {
        particle.Play();
        GetComponent<AudioSource>().Play();
    }

    void PlaySparkleAnim(Vector3 pos)
    {
        particle.transform.position = pos;
        particle.Play();
        GetComponent<AudioSource>().Play();
    }

    public AudioSource wrongSfx;

    public void CheckChoices()
    {
        bool hasCorrect = false;

        foreach (GameObject obj in choicesAlive)
        {
            if (alive.Contains(obj)) alive.Remove(obj);

            if (RectTransformUtility.RectangleContainsScreenPoint(aliveRect, obj.transform.GetChild(0).GetComponent<RectTransform>().position))
            {
                alive.Add(obj);
                PlaySparkleAnim(aliveRect.GetChild(alive.Count).position);
                obj.SetActive(false);
                aliveRect.GetChild(alive.Count).GetChild(0).GetComponent<Image>().sprite = obj.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite;

                choicesAlive.Remove(obj);
                hasCorrect = true;
                GlobalGameManager.Instance.ScoreAdd(100);
                break;
            }

            if (RectTransformUtility.RectangleContainsScreenPoint(deadRect, obj.transform.GetChild(0).GetComponent<RectTransform>().position))
            {
                if (!hasCorrect)
                    wrongSfx.Play();
            }
        }

        foreach (GameObject obj in choicesDead)
        {
            if (dead.Contains(obj)) dead.Remove(obj);

            if (RectTransformUtility.RectangleContainsScreenPoint(deadRect, obj.transform.GetChild(0).GetComponent<RectTransform>().position))
            {
                dead.Add(obj);
                PlaySparkleAnim(deadRect.GetChild(dead.Count).position);
                obj.SetActive(false);
                deadRect.GetChild(dead.Count).GetChild(0).GetComponent<Image>().sprite = obj.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite;

                choicesDead.Remove(obj);
                hasCorrect = true;
                GlobalGameManager.Instance.ScoreAdd(100);
                break;
            }

            if (RectTransformUtility.RectangleContainsScreenPoint(aliveRect, obj.transform.GetChild(0).GetComponent<RectTransform>().position))
            {
                if (!hasCorrect)
                    wrongSfx.Play();
            }
        }

        if (alive.Count + dead.Count == 20)
        {
            //ShowPane();
            EndGame();
        }
    }

    public GameObject pane;

    public void ShowPane(bool show = true)
    {
        pane.SetActive(show);
        game.anim.transform.parent.gameObject.SetActive(false);
    }

    public HiddenObjectGame game;

    public void EndGame()
    {
        ShowPane(false);
        game.isPlaying = false;
        game.ItemFound();
        game.Victory();
        //game.ReportScore();

        //Invoke("StarReminder", 2);
    }

    int stars;

    void StarReminder()
    {
        DataHolder.Instance.SetStarReminder(stars);
        DataHolder.Instance.ShowStarReminder();
    }
}