using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Bab4_Player : MonoBehaviour {

	[Header("Status")]
	public float moveSpeed;
	public float jumpPower;

	[Header("Additional Status")]
	public bool canJump;
	public float dirX;
	public float score;
	public float curScore;

	[Header("Properties")]
	private Rigidbody2D rb;

	[Header("Reference To Another Class")]
	public Global_GameManager gM;

	[Header("Others")]
	private bool ganjel;

	void Start(){

		gM = GameObject.Find ("Game_Manager").GetComponentInChildren<Global_GameManager>();
		rb = GetComponent<Rigidbody2D> ();
		rb.mass = 1f;

	}

	void FixedUpdate(){
		
		#if UNITY_EDITOR || UNITY_STANDALONE
		if(Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right * moveSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.position += Vector3.left * moveSpeed * Time.deltaTime;
		}
		if(Input.GetKeyDown(KeyCode.UpArrow) && canJump){
			IJump ();
		}
		#endif

	}

	void Update(){

		#if UNITY_ANDROID || UNITY_IOS
		dirX = CrossPlatformInputManager.GetAxis ("Horizontal");
		IMove ();
		if(CrossPlatformInputManager.GetButtonDown("Jump") && canJump){
			IJump ();
		}
		#endif

		if(curScore >= score){
			gM.clear = true;
		}

	}

	void IMove(){
		
		rb.velocity = new Vector2 (dirX * moveSpeed, rb.velocity.y);

	}

	void IJump(){

		rb.velocity = new Vector2 (0, jumpPower);
//		rb.AddForce (new Vector2(0, jumpPower), ForceMode2D.Force);

	}

	//pake trigger supaya playernya bisa detect ground
	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag != "MainCamera" && other.gameObject.tag != "wall") {
			rb.gravityScale = 3f;
			canJump = true;
		}

	}

	void OnTriggerStay2D(Collider2D other){

		if (other.gameObject.tag != "MainCamera" && other.gameObject.tag != "wall") {
			rb.gravityScale = 3f;
			canJump = true;
		}

	}

	void OnTriggerExit2D(Collider2D other){

		rb.gravityScale = 7f;
		canJump = false;

	}

	public void AddScore(float scr){

		curScore += scr;

	}

}
