using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizIntro : MonoBehaviour {

    public Text title;
    public Image button;
    public Image button2;
    public Image bg;

    public GameObject intro;

	// Use this for initialization
	void Start ()
    {
        title.CrossFadeAlpha(0, 1, false);
        button.CrossFadeAlpha(0, 1, false);
        bg.CrossFadeAlpha(0, 1, false);

        Invoke("SlidePaneLeft", 1);
        Invoke("SlideRoosterRight", 1);
        Invoke("ShowIntroText", 2);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public RectTransform pane;
    public RectTransform rooster;

    void ShowIntroText()
    {
        intro.SetActive(true);
        intro.GetComponent<TextTypeAnim>().enabled = true;
        button2.CrossFadeAlpha(0, 0, false);
        Invoke("ShowButton", 3);
    }

    void ShowButton()
    {
        button2.CrossFadeAlpha(1, 1, false);
    }

    void SlidePaneLeft()
    {
        StartCoroutine(DoSlidePane());
    }

    IEnumerator DoSlidePane()
    {
        while (pane.anchoredPosition.x > -700)
        {
            pane.anchoredPosition = new Vector2(Mathf.Lerp(pane.anchoredPosition.x, -750, Time.deltaTime * 2), pane.anchoredPosition.y);
            yield return null;
        }
    }

    void SlideRoosterRight()
    {
        StartCoroutine(DoSlideRooster());
    }

    IEnumerator DoSlideRooster()
    {
        while (rooster.anchoredPosition.x < 200)
        {
            rooster.anchoredPosition = new Vector2(Mathf.Lerp(rooster.anchoredPosition.x, 250, Time.deltaTime * 2), rooster.anchoredPosition.y);
            yield return null;
        }
    }
}
