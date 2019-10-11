var allCoinsText : GameObject;
var allCoins : int;

function OnTriggerEnter2D(collider : Collider2D) {
if(collider.gameObject.tag == "Coin"){
//add the one coin to the total number of coins
allCoins += 1;
SendMessage("EatFood");
//preserve the value of collected coins
PlayerPrefs.SetInt("AllCoins",allCoins);
}
}
function Update(){
//We load the value of all the collected coins, and assign it to the variable allCoins
allCoins = PlayerPrefs.GetInt("AllCoins");
//We derive the value of all the collected coins on the screen
allCoinsText.GetComponent(GUIText).text = "coins " + allCoins;
//reset all points press D on keyboard.
if(Input.GetKeyDown(KeyCode.D)){
PlayerPrefs.DeleteAll();
}
}