private var numberOfTouch : int = 0;
private var a : float = 1;
function Start(){
if(PlayerPrefs.GetInt("SoundBoolean") == 0){
//check whether the sound is included, if turned on, then play sound.
numberOfTouch = 0;
gameObject.GetComponent(SpriteRenderer).color = Color(1,1,1,1);
}
if(PlayerPrefs.GetInt("SoundBoolean") == 1){
//check whether the sound is included, if turned off, then turn off the sound.
numberOfTouch = 1;
gameObject.GetComponent(SpriteRenderer).color = Color(0.7,0.7,0.7,1);
}
}
function OnMouseDown () {
if(a <= 0){
if(numberOfTouch == 0){
//completely turn off the music.
a = 1;
numberOfTouch = 1;
gameObject.GetComponent.<AudioSource>().Play();
gameObject.GetComponent(SpriteRenderer).color = Color(0.7,0.7,0.7,1);
PlayerPrefs.SetInt("SoundBoolean", 1);
PlayerPrefs.Save();
}
}
if(a <= 0){
if(numberOfTouch == 1){
//a fully turn on the music.
a = 1;
numberOfTouch = 0;
gameObject.GetComponent(SpriteRenderer).color = Color(1,1,1,1);
PlayerPrefs.SetInt("SoundBoolean", 0);
PlayerPrefs.Save();
}
}
}
function Update(){
if(a >= 0){
a -= 0.1;
}
}