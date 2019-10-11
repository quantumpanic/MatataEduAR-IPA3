var numberFish : int;
function Update () {
//if the object is on, and the object of the standard color
if(gameObject.activeSelf == true && gameObject.GetComponent(SpriteRenderer).color == Color(1,1,1,1)){
//save numberFish
PlayerPrefs.SetInt("NumberFish", numberFish);
}
}