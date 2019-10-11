using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataHolder : MonoBehaviour
{
    public static DataHolder Instance;

    public int starsHolder;
    public bool knowHolder;
    public string nextScene;

    public int totalKnowStars {
        get
        {
            int knowStars = 0;

            foreach (GameUnlockedData gd in gameList)
            {
                if (gd.knowledgeStar) knowStars++;
            }

            return knowStars;
        }

        set
        {
            return;
        }
    }

    public List<GameUnlockedData> gameList = new List<GameUnlockedData>();

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        totalKnowStars = 0;

        LoadData();

        SceneManager.LoadScene(nextScene);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public PopupMenu sceneManager;
    public GameUnlockedData curGameData;

    public GameUnlockedData GetCurrentSceneData()
    {
        GameUnlockedData temp = null;

        string curSceneName = "";

        int totalScenes = sceneManager.scenes.Count;

        if (totalScenes > 0)
        {
            curSceneName = SceneManager.GetSceneByBuildIndex(sceneManager.scenes[totalScenes - 1]).name;

            foreach (GameUnlockedData gd in gameList)
            {
                if (gd.name == curSceneName)
                {
                    temp = gd;
                    curGameData = gd;
                    return temp;
                }
            }

            // if we reach here, no game data found
            print("No game data found for: " + curSceneName);
        }

        return temp;
    }

    public void SaveData()
    {
        foreach (GameUnlockedData gd in gameList)
        {
            // check if score data is alraedy higher
            bool moreStars = PlayerPrefs.GetInt(gd.name + "_stars") > gd.stars? true: false;
            bool moreKnowledge = PlayerPrefs.GetInt(gd.name + "_knowledge") == 1 ? true : false;
            bool betterTime = PlayerPrefs.GetFloat(gd.name + "_time") > gd.timeRecord ? true : false;

            if (!moreStars) PlayerPrefs.SetInt(gd.name + "_stars", gd.stars);
            if (!moreKnowledge) PlayerPrefs.SetInt(gd.name + "_knowledge", gd.knowledgeStar ? 1 : 0);
            if (!betterTime) PlayerPrefs.SetFloat(gd.name + "_time", gd.timeRecord);

            // save unlocked levels
            PlayerPrefs.SetInt(gd.name + "_unlocked", gd.isUnlocked ? 1 : 0);
        }

        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        foreach (GameUnlockedData gd in gameList)
        {
            gd.stars = PlayerPrefs.GetInt(gd.name + "_stars");
            gd.knowledgeStar = PlayerPrefs.GetInt(gd.name + "_knowledge") == 1 ? true : false;
            gd.timeRecord = PlayerPrefs.GetFloat(gd.name + "_time");
            gd.isUnlocked = PlayerPrefs.GetInt(gd.name + "_unlocked") == 1 ? true : false;
        }
    }

    public StarRewardReminder starReminder;

    public void ShowStarReminder(bool show = true)
    {
        //starReminder.gameObject.SetActive(show);
    }

    public void SetStarReminder(int num, bool knowledge = false)
    {
        starReminder.SetStarSprite(num, knowledge);
    }

    public void UnlockAllLevels()
    {
        foreach (GameUnlockedData gd in gameList)
        {
            PlayerPrefs.SetInt(gd.name + "_unlocked", 1);
        }

        PlayerPrefs.Save();
    }

    public void ClearAllData()
    {
        foreach (GameUnlockedData gd in gameList)
        {
            PlayerPrefs.SetInt(gd.name + "_stars", 0);
            PlayerPrefs.SetInt(gd.name + "_knowledge", 0);
            PlayerPrefs.SetFloat(gd.name + "_time", 0);
            PlayerPrefs.SetInt(gd.name + "_unlocked", 0);
        }

        PlayerPrefs.Save();
    }
}

[System.Serializable]
public class GameUnlockedData
{

    public string name;
    public bool isUnlocked;
    public float star1;
    public float star2;
    public float star3;
    public int stars;
    public bool knowledgeStar;
    public float knowledgeTime;
    public float timeRecord;
    public string labelName;

}