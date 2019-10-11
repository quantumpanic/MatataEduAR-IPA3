using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bab3_Kamper : MonoBehaviour {

	[Header("Status")]
	public float hitPoint;
	public float maxHP;
	public float rotSpeed;
	public float timeBetweenShot;

	[Header("Additional Status")]
	public bool buffed;
	public float bufftime;
	public bool invincible = false;
	public float score;
	public float curScore;

	[Header("Reference To Another Object")]
	public Text hitPointText;
	public GameObject aura;
	public Transform hand;
	public Transform gunPosition;
	public Transform gunPosition2;
	public Transform gunPosition3;
	public GameObject toBlack;

	[Header("Reference To Another Class")]
	public Global_GameManager gM;
	public Global_ObjPooler pool;

	[Header("Others")]
	private float timeStamp;

	void Start () {

		maxHP = hitPoint;
		gM = GameObject.Find ("Game_Manager").GetComponentInChildren<Global_GameManager>();
		aura.SetActive (false);
		curScore = 0f;
		timeBetweenShot = .1f;

	}

	// Update is called once per frame
	void Update () {

		if(buffed){
			aura.SetActive (true);
			if(bufftime >= 0f){
				bufftime -= Time.deltaTime;
			}
				
			if(bufftime < 0f){
				buffed = false;
			}
		}

		if(!buffed){
			aura.SetActive (false);
			bufftime = 0f;
		}

		if(!gM.clear){
			if(curScore >= score){
				gM.clear = true;
			}
		}

		if(hitPoint <= 0){
			gM.failed = true;
			gameObject.SetActive (false);
//			Instantiate (toBlack);
		}

		hitPointText.text = hitPoint + "%";

		IShoot ();

		#if UNITY_EDITOR || UNITY_STANDALONE
//		works on 3 dimentional
//		Vector3 mousePos = Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition);
//		Vector3 targetDir = mousePos - transform.position;
//		float step = rotSpeed * Time.deltaTime;
//		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, .0f);
//		Debug.DrawRay (transform.position, newDir, Color.red);
//		hand.transform.rotation = Quaternion.LookRotation (newDir);

//		works on 2 dimentional
		Vector3 mousePos = Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition);
		Vector3 targetPos = mousePos - transform.position;
		float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		hand.transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotSpeed);
		#endif

		#if UNITY_ANDROID || UNITY_IOS
//		works on 2 dimentional
		if(Input.touchCount >= 0){
			Vector3 touchPos = Camera.allCameras[0].ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector3 targetPos2 = touchPos - transform.position;
			float angle2 = Mathf.Atan2(targetPos2.y, targetPos2.x) * Mathf.Rad2Deg;
			Quaternion q2 = Quaternion.AngleAxis(angle2, Vector3.forward);
			hand.transform.rotation = Quaternion.Slerp(transform.rotation, q2, Time.deltaTime * rotSpeed);
		}
		#endif

	}

	void IShoot(){
		
		if(gM.gameStart){
			if(Time.time >= timeStamp){
				timeStamp = Time.time + timeBetweenShot;

                if (!pool) pool = GameObject.Find("Player_Pool_Manager").GetComponent<Global_ObjPooler>();

				GameObject newBullet = pool.GetBullet ();
				newBullet.transform.position = gunPosition.position;
				newBullet.transform.rotation = gunPosition.rotation;
				newBullet.SetActive (true);

				if(buffed){
					GameObject newBullet2 = pool.GetBullet ();
					newBullet2.transform.position = gunPosition2.position;
					newBullet2.transform.rotation = gunPosition2.rotation;
					newBullet2.SetActive (true);

					GameObject newBullet3 = pool.GetBullet ();
					newBullet3.transform.position = gunPosition3.position;
					newBullet3.transform.rotation = gunPosition3.rotation;
					newBullet3.SetActive (true);
				}
			}
		}

	}

	public void AddScore(float scr){
	
		curScore += scr;
		GlobalGameManager.Instance.ScoreAdd(100);

	}

	public void TakeDamage(int dmg){

		if(!invincible){
			hitPoint -= dmg;
			if(hitPoint > 0){
				StartCoroutine (Invincible());
			}
		}

	}

	public void AddBuffTime(float time){

		bufftime += time;

	}

//	IEnumerator BuffTime(){
//
//		yield return new WaitForSeconds (bufftime);
//		buffed = false;
//
//	}

	IEnumerator Invincible(){

		float curTime = 0f;
		bool showSprite = false;
		invincible = true;
		var temp = GetComponentInChildren<SpriteRenderer> ();
		while(curTime < 3f){
			temp.enabled = showSprite;
			yield return new WaitForSeconds (0.1f);
			showSprite = !showSprite;
			curTime = curTime + 0.1f;
		}
		temp.enabled = true;
		invincible = false;

	}

}
