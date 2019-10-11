function OnTriggerEnter2D(collider : Collider2D) {
if(collider.gameObject.tag == "Player"){
//include the object animation
gameObject.GetComponent.<Animation>().Play();
if(PlayerPrefs.GetInt("SoundBoolean") == 0){
//play sounds when it is switched on.
gameObject.GetComponent.<AudioSource>().Play();
}
}
}

	function Start()
	{
		Destroy(gameObject, 20f);
	}