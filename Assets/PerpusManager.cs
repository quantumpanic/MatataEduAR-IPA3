using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerpusManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DataHolder.Instance.LoadData();
		if (isMap) {
			UpdateStarSprite ();
		}
		else
			PopulatePanel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<Sprite> starImgs = new List<Sprite>();

    int page;

    void PopulatePanel()
    {
        // get the first page per 3 games
        for (int x = 0; x < 3; x++)
        {
            // get the child
            Transform t = transform.GetChild(x);
            string gameName = "";
            string timeText = "";
            
            if (page + x >= DataHolder.Instance.gameList.Count)
            {
                return;
            }

            gameName = DataHolder.Instance.gameList[page + x].labelName;
            t.GetChild(0).GetComponent<Text>().text = gameName;

            float time = DataHolder.Instance.gameList[page + x].timeRecord;
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

            string bestScore = Mathf.CeilToInt(DataHolder.Instance.gameList[page + x].timeRecord).ToString();

            timeText = "High Score " + bestScore;
            t.GetChild(1).GetComponent<Text>().text = timeText;

            t.GetComponent<Image>().sprite = starImgs[DataHolder.Instance.gameList[page + x].stars];
            t.GetChild(2).GetComponent<Image>().CrossFadeAlpha(DataHolder.Instance.gameList[page + x].knowledgeStar ? 1 : 0, 0, false);
        }
    }


	void UpdateStarSprite()
	{
		for (int x = 0; x < 5; x++)
		{
			// get the child
			Transform t = transform.GetChild(x);

			if (page + x >= DataHolder.Instance.gameList.Count)
			{
				return;
			}

            var objs = GameObject.FindGameObjectsWithTag("Player");
            //var img = t.GetComponentsInChildren<Image>()[x].GetComponent<Image>();
            //if (img.name != "1")
            //    t.GetComponentsInChildren<Image>()[x].GetComponent<Image> ().sprite = starImgs [DataHolder.Instance.gameList [((bab - 1) * 5) + x].stars];

            foreach (GameObject g in objs)
            {
                if (int.Parse(g.transform.parent.name) == x + 1)
                    g.GetComponent<Image>().sprite = starImgs[DataHolder.Instance.gameList[((bab - 1) * 5) + x].stars];
            }
            //objs[x].GetComponent<Image>().sprite = starImgs[DataHolder.Instance.gameList[((bab - 1) * 5) + x].stars];
        }
	}

    public void NextPage()
    {
        //if ((page + 1) * 3 > DataHolder.Instance.gameList.Count - 1) return;
        if (page + 3  > DataHolder.Instance.gameList.Count - 1) return;
        page++;
        PopulatePanel();
    }

    public void PrevPage()
    {
        if (page <= 0) return;
        page--;
        PopulatePanel();
    }

    public void OpenAR()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Farm_AR");
    }

	public bool isMap;
	public int bab;
}
