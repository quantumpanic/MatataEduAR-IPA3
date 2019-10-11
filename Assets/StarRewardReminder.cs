using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRewardReminder : MonoBehaviour {
    
    public Image starsImg;
    public List<Sprite> starSprites = new List<Sprite>();
    public Text textLbl;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetStarSprite(int num, bool knowledge = false)
    {
        starsImg.sprite = starSprites[num];
        starsImg.transform.GetChild(0).gameObject.SetActive(num >= 4 ? true : false);

        if (num >= 4)
            textLbl.text = string.Format("Selamat, kamu dapat bintang emas!" + '\n' + "Kamu mendapatkan hadiah binatang AR! Buka AR Farm untuk melihat binatang barumu!", num);
        else if (num > 0)
            textLbl.text = string.Format("Selamat, kamu dapat {0} bintang!" + '\n' + "Main terus untuk kumpulkan bintang! Bintang emas bisa membuka hadiah baru binatang AR!", num);
        else
            textLbl.text = "Silahkan coba lagi!" + '\n' + "Main terus untuk kumpulkan bintang! Bintang bisa membuka hadiah baru binatang AR!";
    }
    
}
