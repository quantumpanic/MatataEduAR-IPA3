var fish : GameObject;
var price : int;
var allCoins : GameObject;
var textPrice : GameObject;
function Start(){
//if the fish already purchased
if(PlayerPrefs.GetInt(fish.name) == 1){
//turn off textPrice
textPrice.SetActive(false);
//turn off gameObject
gameObject.SetActive(false);
//fish becomes a standard color
fish.GetComponent(SpriteRenderer).color = Color(1,1,1,1);
}
}
function OnMouseDown () {
//if the value of the fish is less than or equal to all the coins collected
if(price <= allCoins.GetComponent(AllCoin).allCoins){
if(PlayerPrefs.GetInt("SoundBoolean") == 0){
//play sounds when it is switched on.
gameObject.GetComponent.<AudioSource>().Play();
}
//button color becomes darkened
gameObject.GetComponent(SpriteRenderer).color = Color(0.8,0.8,0.8,1);
}
}
function OnMouseUp () {
//if the value of the fish is less than or equal to all the coins collected
if(price <= allCoins.GetComponent(AllCoin).allCoins){
//button becomes a standard color
gameObject.GetComponent(SpriteRenderer).color = Color(1,1,1,1);
//fish becomes a standard color
fish.GetComponent(SpriteRenderer).color = Color(1,1,1,1);
//turn off textPrice
textPrice.SetActive(false);
//save the value, remember to purchase
PlayerPrefs.SetInt(fish.name, 1);
//reduce the number of collected coins in the value of the price of fish and save the number
PlayerPrefs.SetInt("AllCoins",allCoins.GetComponent(AllCoin).allCoins - price);
//turn off button
gameObject.SetActive(false);
}
}