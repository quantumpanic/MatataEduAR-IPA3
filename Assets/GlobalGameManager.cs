using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameManager : MonoBehaviour {

    public static GlobalGameManager Instance;

    public DataHolder dataHolder;
    public PopupMenu popupHolder;

    public string currentArea;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!showScorePanel) return;

        // Make the score count up to its current value
        if (!Mathf.Approximately(scoreCount, lastScore))
        {
            // Count up to the courrent value
            scoreCount = Mathf.Lerp(scoreCount, lastScore, Time.deltaTime * 10);

            // Update the score text
            popupHolder.UpdateInGameScorePanel(scoreCount);
        }
    }

    public void ScoreChange(float score)
    {
        lastScore = score;
    }

    public void ScoreAdd(float score)
    {
        lastScore += score;
    }

    public void GameFinished(GameUnlockedData gameData, float finalScore = 0)
    {
        // notification that a game is finished, data will be checked and saved
        // first get the game name
        string gameName = SceneManager.GetActiveScene().name;

        // compare it to the database
        foreach (GameUnlockedData gd in dataHolder.gameList)
        {
            if (gd.name == gameName)
            {
                if (finalScore >= lastScore) lastScore = finalScore;
                UpdateMilestones(gameData, gameData.stars, gameData.knowledgeStar, lastScore);
            }
        }
    }

    public float lastScore;
    float scoreCount;
    bool showScorePanel;

    public void ShowScore(bool win)
    {
        if (win) popupHolder.ShowResultWin();
        else popupHolder.ShowResultLose();
    }

    public void ShowScorePanel()
    {
        showScorePanel = true;
        scoreCount = -1;
    }

    public void HidePanels()
    {
        popupHolder.HideAllPanel();
        showScorePanel = false;
    }

    void UpdateMilestones(GameUnlockedData gameData, int stars, bool knowledge, float time)
    {
        print("updating scores");
        GameUnlockedData gd = DataHolder.Instance.gameList.Find(o => o == gameData);

        if (time >= gd.star1) stars = 1;
        if (time >= gd.star2) stars = 2;
        if (time >= gd.star3) stars = 3;

        gd.stars = stars;
        gd.knowledgeStar = knowledge;
        gd.timeRecord = time;

        DataHolder.Instance.SaveData();
    }

	private void OnLevelWasLoaded(int level)
	{
		// check if this is a game scene for showing stars gauge
		bool isGame = false;
		var gameName = SceneManager.GetSceneByBuildIndex(level).name;
		GameUnlockedData gd = DataHolder.Instance.gameList.Find(o => o.name == gameName);

		if (gd != null) isGame = true;

		if (isGame)
		{
			lastScore = 0;
		}
	
	}

}
