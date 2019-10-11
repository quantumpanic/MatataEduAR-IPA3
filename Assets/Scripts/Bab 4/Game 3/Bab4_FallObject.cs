using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab4_FallObject : MonoBehaviour {

	[Header("Status")]
	public bool positive;
	public float speed;
	public float lifetime;

	[Header("Reference To Another Object")]
	private GameObject player;

	[Header("Reference To Another Script")]
	public Global_Explosion pop;

	void Start(){

		player = GameObject.FindGameObjectWithTag ("Player");
		speed = Random.Range (2f, 5f);

	}

	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.down * Time.deltaTime * speed);
		lifetime -= Time.deltaTime;
		if(lifetime <= 0f){
			Destroy (gameObject);
		}

	}

	void PopTheProj(){

		Global_Explosion newExplosion = Instantiate (pop, transform.position, transform.rotation) as Global_Explosion;
		Destroy (gameObject);

	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Player"){
			if(positive){
				GlobalGameManager.Instance.ScoreAdd(100);
				player.GetComponent<Bab4_Player> ().AddScore (1f);
				Destroy (gameObject);
			}else if(!positive){
				GlobalGameManager.Instance.ScoreAdd(-500);
				if(GlobalGameManager.Instance.lastScore <= 0){
					GlobalGameManager.Instance.lastScore = 0;
				}
				PopTheProj ();
			}
		}

		if(other.gameObject.tag == "ground" && !positive){
			PopTheProj ();
		}

		if(other.gameObject.tag == "ground" && positive){
			Destroy (gameObject);
		}

	}

}
