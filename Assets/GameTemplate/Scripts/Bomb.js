private var playerParticle : GameObject;
private var bubles : GameObject;
private var timeToDownloadTheMenu : float = 1;
private var empty : boolean = false;
var bomb : GameObject;
var timeToDestroy : float = 1;
var bombParticle : GameObject;
private var blackBackground : GameObject;
function Update(){
//assign a variable object called BlackBackground
blackBackground = GameObject.Find("BlackBackground");
//assign a variable object called PlayerParticle
playerParticle = GameObject.Find("PlayerParticle");
//assign a variable object called Bubles
bubles = GameObject.Find("Bubles");
//reduce the time to destruction
timeToDestroy -= 0.2 * Time.deltaTime;
if(empty == true){
//reduce the time to load menu
timeToDownloadTheMenu -= 0.03;
if(timeToDownloadTheMenu <= 0){
//include the blackBackground animation
blackBackground.GetComponent.<Animation>().Play("BlackBackgroundFinish");
}
//if the object has become completely opaque
if(blackBackground.GetComponent(SpriteRenderer).color.a >= 1){
empty = false;
//load menu scene
Application.LoadLevel("Menu");
}
}
if(timeToDestroy <= 0){
//destroy the object
Destroy(bomb);
}
}
function OnTriggerEnter2D(collider : Collider2D) {
if(collider.gameObject.tag == "Player"){
if(PlayerPrefs.GetInt("SoundBoolean") == 0){
//play sounds when it is switched on.
gameObject.GetComponent(AudioSource).Play();
}
//turn off the SpriteRenderer object to which is attached a script
gameObject.GetComponent(SpriteRenderer).enabled = false;
//turn off bubles
bubles.SetActive(false);
//turn on bombParticle
bombParticle.SetActive(true);
//turn off the AudioSource object that hit the trigger
collider.GetComponent(AudioSource).enabled = false;
//turn off the CircleCollider2D object that hit the trigger
collider.GetComponent(CircleCollider2D).enabled = false;
//turn off the PlayerController object that hit the trigger
collider.GetComponent(PlayerController).enabled = false;
//turn off the SpriteRenderer object that hit the trigger
collider.GetComponent(SpriteRenderer).enabled = false;
//turn off the BombController object that hit the trigger
collider.GetComponent(BombController).enabled = false;
//turn off the CoinController object that hit the trigger
collider.GetComponent(CoinController).enabled = false;
//turn on ParticleSystem
playerParticle.GetComponent.<ParticleSystem>().Play();
empty = true;
}
}