using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupMenu : MonoBehaviour {

    public List<int> scenes = new List<int>();
    public GameObject panel;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(transform.parent);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnLevelWasLoaded(int level)
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        scenes.Add(level);
    }

    public void AsyncLevelLoaded(int level)
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        scenes.Add(level);
    }

    public void Back()
    {
        ShowPanel(false);
        int next = scenes[scenes.Count - 2];
        scenes.RemoveRange(scenes.Count - 2, 2);
        SceneManager.LoadScene(next);
    }

    public void Next()
    {
        ShowPanel(false);
        GameObject.Find("SceneManager").SendMessage("OnButtonClick");
        GameObject.Find("SceneManager").SendMessage("ButtonPressed");
    }

    public void ShowPanel(bool show = true)
    {
        panel.SetActive(show);
    }

    public void MainMenu()
    {
        ShowPanel(false);
        SceneManager.LoadScene("mainmenu");
    }

    public void ShowAR()
    {
        ShowPanel(false);
        SceneManager.LoadScene("Farm_AR");
        var fadeScript = GameObject.Find("SceneManager").GetComponent<SceneManager_fadeFX>();
        if (fadeScript) DataHolder.Instance.nextScene = fadeScript.NextPage;
        var nonFade = GameObject.Find("SceneManager").GetComponent<SceneManager_nonfade>();
        if (nonFade) DataHolder.Instance.nextScene = nonFade.NextPage;
    }

    public void ShowGarden()
    {
        ShowPanel(false);
        SceneManager.LoadScene("Tumbuhan_AR");
        var fadeScript = GameObject.Find("SceneManager").GetComponent<SceneManager_fadeFX>();
        if (fadeScript) DataHolder.Instance.nextScene = fadeScript.NextPage;
        var nonFade = GameObject.Find("SceneManager").GetComponent<SceneManager_nonfade>();
        if (nonFade) DataHolder.Instance.nextScene = nonFade.NextPage;
    }

    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject optionPanel;
    public GameObject tutorialPanel;

    public void ShowResultWin()
    {
        winPanel.SetActive(true);
        UpdateScoresReport();
    }

    public void ShowResultLose()
    {
        losePanel.SetActive(true);
        UpdateScoresReport();
    }

    public void ShowOptions()
    {
        optionPanel.SetActive(true);
    }

    public void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void HideAllPanel()
    {
        Invoke("DoHidePanel", 0.5F);
    }

    void DoHidePanel()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        optionPanel.SetActive(false);
        tutorialPanel.SetActive(false);

        ResetScorePanel();
    }

    public void UpdateInGameScorePanel(float newScore)
    {
        GameObject.Find("ScoreText").GetComponent<Text>().text = Mathf.CeilToInt(newScore).ToString();
    }

    void ResetScorePanel()
    {
        GameObject.Find("ScoreText").GetComponent<Text>().text = "";
    }

    void UpdateScoresReport()
    {
        // consult the data holder for the current scores to show
        float currentScore = GlobalGameManager.Instance.lastScore;
        float targetScore = DataHolder.Instance.curGameData.star1;
        float highScore = DataHolder.Instance.curGameData.timeRecord;

        GameObject.Find("ScoreTexts/TextScore").GetComponent<Text>().text = "SCORE " + currentScore.ToString();
        GameObject.Find("ScoreTexts/TextTargetScore").GetComponent<Text>().text = "GOAL " + targetScore.ToString();
        GameObject.Find("ScoreTexts/TextHighScore").GetComponent<Text>().text = "HIGH SCORE " + highScore.ToString();
    }
}
