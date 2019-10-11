private var player : GameObject;
function Start(){
//assign a variable object called Player
player = GameObject.Find("Player");
}
function Update () {
//arrow position on the x-axis becomes equal to a position player + 4
gameObject.transform.position.x = player.transform.position.x + 4;
}
function OnTriggerEnter2D(collider : Collider2D) {
if(collider.gameObject.tag == "Shark"){
//remove the arrow if it hit an object tagged as shark
Destroy(gameObject);
}
}