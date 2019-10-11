using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Tutorial : MonoBehaviour {

	[Header("Additional Status")]
	public bool usingCD;

	[Header("Reference To Another Object")]
	[SerializeField]
	List<GameObject> tutorials;
	public GameObject countDown;
	public GameObject content;

	[Header("Reference To Another Script")]
	public Global_GameManager gM;

	[Header("Others")]
	private int j;

	void Awake(){

		ClearTut ();
		content.SetActive (true);
		countDown.SetActive (false);
		gM = GameObject.FindGameObjectWithTag ("Game_Manager").GetComponent<Global_GameManager>();
		j = 0;
		tutorials[j].SetActive(true);

	}

	// Update is called once per frame
	void Update () {

		if(content.activeInHierarchy){
			gM.gameStart = false;
		}

		if(!content.activeInHierarchy){
			if (usingCD) {
				if(countDown != null){
					countDown.SetActive (true);
					if (countDown.activeInHierarchy) {
						StartCoroutine (waitCountdown ());
					}
				}
			} else if(!usingCD){
				gM.gameStart = true;
			}
		}

		#if UNITY_EDITOR || UNITY_STANDALONE
		if(Input.GetKeyDown(KeyCode.Space)){
			if(j == tutorials.Count - 1){
				content.SetActive(false);
			}
			tutorials[j].SetActive(false);
			j++;
			tutorials[j].SetActive(true);
		}
		#endif

		#if UNITY_ANDROID || UNITY_IOS
		if (Input.GetMouseButtonDown(0)) {
			if(j == tutorials.Count - 1){
				content.SetActive(false);
			}
			tutorials[j].SetActive(false);
			j++;
			tutorials[j].SetActive(true);
		}
		#endif

	}

	public void ClearTut(){

		for (int i = 0; i < tutorials.Count; i++){
			tutorials[i].SetActive (false);
		}

	}

	IEnumerator waitCountdown(){

		yield return new WaitForSeconds (3.5f);
		gM.gameStart = true;
		Destroy (countDown);
//		countDown.SetActive (false);

	}

}
