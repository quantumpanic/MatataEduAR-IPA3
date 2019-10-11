var fish1 : GameObject;
var fish2 : GameObject;
var fish3 : GameObject;
private var empty : boolean;
function OnMouseDown () {
if(PlayerPrefs.GetInt("SoundBoolean") == 0){
//play sounds when it is switched on.
gameObject.GetComponent.<AudioSource>().Play();
}
//button color becomes darkened
gameObject.GetComponent(SpriteRenderer).color = Color(0.8,0.8,0.8,1);
empty = true;
}
function OnMouseUp () {
//button becomes a standard color
gameObject.GetComponent(SpriteRenderer).color = Color(1,1,1,1);
//if the object name LeftSlider
if(gameObject.name == "LeftSlider"){
//if the fish1 included
if(fish1.activeSelf == true && empty == true){
//turn off fish1
fish1.SetActive(false);
//turn on fish3
fish3.SetActive(true);
empty = false;
}
//if the fish2 included
if(fish2.activeSelf == true && empty == true){
//turn off fish2
fish2.SetActive(false);
//turn on fish1
fish1.SetActive(true);
empty = false;
}
//if the fish3 included
if(fish3.activeSelf == true && empty == true){
//turn off fish3
fish3.SetActive(false);
//turn on fish2
fish2.SetActive(true);
empty = false;
}


}
//if the object name LeftSlider
if(gameObject.name == "RightSlider"){
//if the fish1 included
if(fish1.activeSelf == true && empty == true){
//turn off fish1
fish1.SetActive(false);
//turn on fish2
fish2.SetActive(true);
empty = false;
}
//if the fish2 included
if(fish2.activeSelf == true && empty == true){
//turn off fish2
fish2.SetActive(false);
//turn on fish3
fish3.SetActive(true);
empty = false;
}
//if the fish3 included
if(fish3.activeSelf == true && empty == true){
//turn off fish3
fish3.SetActive(false);
//turn on fish1
fish1.SetActive(true);
empty = false;
}



}
}