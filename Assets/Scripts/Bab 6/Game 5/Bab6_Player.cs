using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Bab6_Player : MonoBehaviour {

	[Header("Status")]
	public int hitPoint;
	public float flySpeed;
	public float timeBetweenShot;

	[Header("Additional Status")]
	public float dirY, dirX;
	public bool invincible = false;

	[Header("Properties")]
	private Rigidbody2D rb;

	[Header("Reference To Another Object")]
	public Transform gunPosition;
	public GameObject explosion;
	public GameObject toBlack;

	[Header("Reference To Another Script")]
	public Global_GameManager gM;
	public Global_ObjPooler pool;

	[Header("Others")]
	private float timeStamp;

	// Use this for initialization
	void Start () {

		gM = GameObject.Find ("Game_Manager").GetComponentInChildren<Global_GameManager>();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		timeBetweenShot = 0.25f;
		flySpeed = 7f;

	}

	void FixedUpdate(){
	
		#if UNITY_EDITOR || UNITY_STANDALONE
		if(Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right * flySpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.position += Vector3.left * flySpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.UpArrow)){
			transform.position += Vector3.up * flySpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			transform.position += Vector3.down * flySpeed * Time.deltaTime;
		}
		if(gM.gameStart){
			if(Input.GetKey(KeyCode.Space)){
				IShoot();
			}
		}
		#endif

	}
	
	// Update is called once per frame
	void Update () {

		#if UNITY_ANDROID || UNITY_IOS
		dirY = CrossPlatformInputManager.GetAxis ("Vertical");
		dirX = CrossPlatformInputManager.GetAxis ("Horizontal");
		IFly ();
		if(gM.gameStart){
			if(CrossPlatformInputManager.GetButton("Shoot")){
				IShoot ();
			}
		}
		#endif

		if(hitPoint <= 0){
			gM.failed = true;
			gameObject.SetActive (false);
		}

	}

	void OnDisable(){
		Instantiate (explosion,transform.position,transform.rotation);
	}

	void IFly(){
		
		if (rb != null) {
			rb.velocity = new Vector2 (rb.velocity.x, dirY * flySpeed);
			rb.velocity = new Vector2 (dirX * flySpeed, rb.velocity.y);
		}

	}

	void IShoot(){
		
		if(Time.time >= timeStamp){
			timeStamp = Time.time + timeBetweenShot;

			GameObject newBullet = pool.GetBullet ();
			newBullet.transform.position = gunPosition.position;
			newBullet.transform.rotation = gunPosition.rotation;
			newBullet.SetActive (true);
		}

	}

	public void TakeDamage(int dmg){

		if(!invincible){
			hitPoint -= dmg;
			if(hitPoint > 0){
				StartCoroutine (Invincible());
			}
		}

	}

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