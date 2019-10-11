private var blackBackground : GameObject;
private var empty : boolean = false;
function Update(){
//assign a variable object called BlackBackground
blackBackground = GameObject.Find("BlackBackground");
//if the object has become completely opaque
if(blackBackground.GetComponent(SpriteRenderer).color.a >= 1 && empty == true){
//load GameScene
Application.LoadLevel("GameScene");
}
}
function OnMouseDown () {
if(PlayerPrefs.GetInt("SoundBoolean") == 0){
//play sounds when it is switched on.
gameObject.GetComponent.<AudioSource>().Play();
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
//include the blackBackground animation
blackBackground.GetComponent.<Animation>().Play("BlackBackgroundFinish");
empty = true;
}