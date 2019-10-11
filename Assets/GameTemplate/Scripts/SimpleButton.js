var menuObject : GameObject;
var shopObject : GameObject;
private var objectAudio : GameObject;
function Start(){
//assign a variable object called objectAudio
objectAudio = GameObject.Find("Main Camera");
}
function OnMouseDown () {
if(PlayerPrefs.GetInt("SoundBoolean") == 0){
//play sounds when it is switched on.
objectAudio.GetComponent.<AudioSource>().Play();
}
//button color becomes darkened
gameObject.GetComponent(SpriteRenderer).color = Color(0.8,0.8,0.8,1);
//button is slightly reduced
gameObject.transform.localScale = Vector3(0.9,0.9,0.9);
}
function OnMouseUp () {
//button becomes a standard color
gameObject.GetComponent(SpriteRenderer).color = Color(1,1,1,1);
//button is the standard size
gameObject.transform.localScale = Vector3(1,1,1);
//turn on shopObject
shopObject.SetActive(true);
//turn off menuObject
menuObject.SetActive(false);
}