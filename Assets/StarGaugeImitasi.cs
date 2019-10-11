using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StarGaugeImitasi : MonoBehaviour {
	
	public Image gauge;
	public Text scoreTxt;
	public List<Sprite> starSprites = new List<Sprite>();
	public GameObject knowStar;
	public float star3Vault;
	public float star2Vault;
	public float star1Vault;

	void ShowGauge(bool show = true)
	{
		gauge.enabled = show;
		scoreTxt.enabled = show;
		gauge.color = show ? Color.white : new Color(1, 1, 1, 0);
//		knowStar.SetActive(show);
	}

	void Update(){

		if(star1Vault != 0 && star2Vault != 0 && star3Vault != 0){
			if (GlobalGameManager.Instance.lastScore >= star3Vault) {
				gauge.sprite = starSprites[3];
			} else if (GlobalGameManager.Instance.lastScore >= star2Vault) {
				gauge.sprite = starSprites[2];
			} else if (GlobalGameManager.Instance.lastScore >= star1Vault) {
				gauge.sprite = starSprites[1];
			} else {
				gauge.sprite = starSprites[0];
			}
		}
	
	}

//	void UpdateGauge(GameUnlockedData gd)
//	{


//		int curStars = gd.stars;
//
//		switch (curStars)
//		{
//		case 0:
//			gauge.sprite = starSprites[0];
//			break;
//		case 1:
//			gauge.sprite = starSprites[1];
//			break;
//		case 2:
//			gauge.sprite = starSprites[2];
//			break;
//		case 3:
//			gauge.sprite = starSprites[3];
//			break;
//		}
//
//		knowStar.SetActive(gd.knowledgeStar ? true : false);
//	}

	private void OnLevelWasLoaded(int level)
	{
		// check if this is a game scene for showing stars gauge
		bool isGame = false;
		var gameName = SceneManager.GetSceneByBuildIndex(level).name;
		GameUnlockedData gd = DataHolder.Instance.gameList.Find(o => o.name == gameName);
		print (gameName);

		if (gd != null) isGame = true;

		if (isGame)
		{
			print (isGame);
			ShowGauge(true);
			star3Vault = gd.star3;
			star2Vault = gd.star2;
			star1Vault = gd.star1;
			// add the values
//			UpdateGauge(gd);
		}

		else
		{
			print (isGame);
			// if this is not a game scene, hide the gauge
			ShowGauge(false);
		}
	}

}
