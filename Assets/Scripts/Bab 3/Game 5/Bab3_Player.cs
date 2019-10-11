using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bab3_Player : MonoBehaviour {

	[Header("Status")]
	public float moveSpeed = 10f;

	[Header("Additional Status")]
	public bool burning;
	public bool freezing;
	public bool solid;
	public bool liquid;
	public bool gas;

	[Header("Properties")]
	private Rigidbody2D rb;
	private SpriteRenderer solidC;
	private SpriteRenderer liquidC;
	private SpriteRenderer gasC;

	[Header("Reference To Another Object")]
	public GameObject liquidFalling;
	public GameObject liquidIdle;
	public GameObject solid_obj;
	public GameObject liquid_obj;
	public GameObject gas_obj;
	public GameObject textAlert;
	public Text txt;

	[Header("Reference To Another Script")]
	public Global_GameManager gM;

	[Header("Others")]
	public float waitTime;
	public float waitTemp;

	void Start(){

		gM = GameObject.Find ("Game_Manager").GetComponentInChildren<Global_GameManager>();
		txt = textAlert.GetComponentInChildren<Text> ();
		solidC = solid_obj.GetComponentInChildren<SpriteRenderer> ();
		liquidC = liquid_obj.GetComponentInChildren<SpriteRenderer> ();
		gasC = gas_obj.GetComponentInChildren<SpriteRenderer> ();
		waitTime = 5f;
		waitTemp = waitTime;
		solid = true;
		liquid = false;
		liquidFalling.SetActive (false);
		gas = false;
		burning = false;
		freezing = false;
		rb = GetComponent<Rigidbody2D> ();

	}

	void Update(){

		CheckMaterial ();
		ChangeMaterial ();

		#if UNITY_EDITOR || UNITY_STANDALONE
		if(Input.GetKey(KeyCode.RightArrow) && gM.gameStart){
			MoveRight ();
		}
		if(Input.GetKey(KeyCode.LeftArrow) && gM.gameStart){
			MoveLeft();
		}
		if(Input.GetKey(KeyCode.UpArrow) && gameObject.tag == "Gas" && gM.gameStart){
			MoveUp ();
		}
		if(Input.GetKey(KeyCode.DownArrow) && gameObject.tag == "Gas" && gM.gameStart){
			MoveDown ();
		}
		#endif

		#if UNITY_ANDROID || UNITY_IOS
		if(Input.touchCount > 0 && gM.gameStart){
			if(Input.GetTouch(0).position.x < Screen.width / 3 && Input.GetTouch(0).position.y > Screen.height / 3 && Input.GetTouch(0).position.y < Screen.height * 2 / 3)
			{
				MoveLeft();
			}
			if(Input.GetTouch(0).position.x > Screen.width * 2 / 3 && Input.GetTouch(0).position.y > Screen.height / 3 && Input.GetTouch(0).position.y < Screen.height * 2 / 3)
			{
				MoveRight ();
			}
		}
		if(Input.touchCount >= 0 && gameObject.tag == "Gas" && gM.gameStart){
			if(Input.GetTouch(0).position.y < Screen.height / 3)
			{
				MoveDown ();
			}
			if(Input.GetTouch(0).position.y > Screen.height * 2 / 3)
			{
				MoveUp ();
			}
		}
		#endif
			
	}

	void MoveLeft(){
		
		FlipToLeft ();
		rb.velocity = new Vector2 (-moveSpeed, rb.velocity.y);

	}

	void MoveRight(){
		
		FlipToRight ();
		rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);

	}

	void MoveUp(){
		
		rb.velocity = new Vector2 (rb.velocity.x, moveSpeed);

	}

	void MoveDown(){
		
		rb.velocity = new Vector2 (rb.velocity.x, -moveSpeed);

	}

	void FlipToLeft(){
		
		Vector2 temp = transform.localScale;
		temp.x = -1;
		transform.localScale = temp;

	}

	void FlipToRight(){

		Vector2 temp = transform.localScale;
		temp.x = 1;
		transform.localScale = temp;

	}

	void ChangeMaterial(){

		float perc = waitTemp / waitTime;

		if(burning){
			solidC.color = Color.Lerp (Color.red, solidC.color, perc);
			liquidC.color = Color.Lerp (Color.red, liquidC.color, perc);
		}
		if(freezing){
			liquidC.color = Color.Lerp (Color.blue, liquidC.color, perc);
			gasC.color = Color.Lerp (Color.blue, gasC.color, perc);
		}
		if(!burning && !freezing){
			solidC.color = Color.white;
			liquidC.color = Color.white;
			gasC.color = Color.white;
		}

		if(burning && solid){
//			solidC.color = Color.red;
			waitTemp -= Time.deltaTime;
			if(waitTemp <= 0f){
				txt.text = "Mencair";
				Instantiate (textAlert);
				solid = !solid;
				liquid = !liquid;
				waitTemp = waitTime;
			}
		}
		if(burning && liquid){
//			liquidC.color = Color.red;
			waitTemp -= Time.deltaTime;
			if(waitTemp <= 0f){
				txt.text = "Menguap";
				Instantiate (textAlert);
				liquid = !liquid;
				gas = !gas;
				waitTemp = waitTime;
			}
		}
		if(freezing && liquid){
//			liquidC.color = Color.blue;
			waitTemp -= Time.deltaTime;
			if(waitTemp <= 0f){
				txt.text = "Membeku";
				Instantiate (textAlert);
				liquid = !liquid;
				solid = !solid;
				waitTemp = waitTime;
			}
		}
		if(freezing && gas){
//			gasC.color = Color.blue;
			waitTemp -= Time.deltaTime;
			if(waitTemp <= 0f){
				txt.text = "Mengembun";
				Instantiate (textAlert);
				gas = !gas;
				liquid = !liquid;
				waitTemp = waitTime;
			}
		}
		if(!burning && !freezing){
//			solidC.color = Color.white;
//			liquidC.color = new Color (1f, 1f, 1f, 100f/255f);
//			gasC.color = new Color (1f, 1f, 1f, 100f/255f);
//			waitTemp += Time.deltaTime;
//			if(waitTemp >= waitTime){
				waitTemp = waitTime;
//			}
		}

	}

	void CheckMaterial(){
		
		if(solid){
			solid_obj.SetActive(true);
			transform.tag = "Solid";
			rb.drag = 5f;
			rb.gravityScale = 10f;
			rb.freezeRotation = false;
			liquid_obj.SetActive (false);
			gas_obj.SetActive (false);
		}
		if(liquid){
			liquid_obj.SetActive (true);
			transform.tag = "Liquid";
			rb.drag = 0f;
			rb.gravityScale = 5f;
			rb.freezeRotation = true;
			var temp = rb.transform.rotation;
			temp.z = 0f;
			rb.transform.rotation = temp;
			solid_obj.SetActive(false);
			gas_obj.SetActive (false);
			if (rb.velocity.y < -1f) 
			{
				WaterFalling ();
			}
			else
			{
				WaterIdle ();
			}
		}
		if(gas){
			gas_obj.SetActive (true);
			transform.tag = "Gas";
			rb.drag = 5f;
			rb.gravityScale = 0f;
			rb.freezeRotation = true;
			var temp = rb.transform.rotation;
			temp.z = 0f;
			rb.transform.rotation = temp;
			solid_obj.SetActive(false);
			liquid_obj.SetActive (false);
		}

	}

	void WaterFalling(){
		
		liquidFalling.SetActive (true);
		liquidIdle.SetActive (false);
	
	}

	void WaterIdle(){
		
		liquidIdle.SetActive (true);
		liquidFalling.SetActive (false);

	}

}