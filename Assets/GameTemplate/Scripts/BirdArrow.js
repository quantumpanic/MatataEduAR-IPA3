private var player : GameObject;
function Start(){
//assign a variable object called Player
player = GameObject.Find("Player");
}
function Update () {
//object position becomes equal to the player's position + 4
gameObject.transform.position.x = player.transform.position.x + 4;
}
function OnTriggerEnter2D(collider : Collider2D) {
if(collider.gameObject.tag == "Bird"){
//destroy the object when the object hit him in the tag bird
Destroy(gameObject);
}
}