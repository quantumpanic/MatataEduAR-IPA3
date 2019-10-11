var velosityObject : Vector2  = Vector2.zero;
var cam : GameObject;
var bubles : GameObject;
var fishSprite1 : Sprite;
var fishSprite2 : Sprite;
var fishSprite3 : Sprite;

var isAlive : boolean = false;
var isSwimming : boolean = false;

private var empty : boolean = true;
function Start(){
//if the number is 1
if(PlayerPrefs.GetInt("NumberFish") == 1){
//fish sprite changes to the fishSprite1
gameObject.GetComponent(SpriteRenderer).sprite = fishSprite1;
}
//if the number is 2
if(PlayerPrefs.GetInt("NumberFish") == 2){
//fish sprite changes to the fishSprite2
gameObject.GetComponent(SpriteRenderer).sprite = fishSprite2;
}
//if the number is 3
if(PlayerPrefs.GetInt("NumberFish") == 3){
//fish sprite changes to the fishSprite3
gameObject.GetComponent(SpriteRenderer).sprite = fishSprite3;
}


}
function FixedUpdate () {
//if the player is at the bottom of the stage
if(gameObject.transform.position.y < 0 && empty == true && isAlive){
//turn on ParticleSystem
bubles.GetComponent.<ParticleSystem>().Play();
if(PlayerPrefs.GetInt("SoundBoolean") == 0){
//play sounds when it is switched on
gameObject.GetComponent.<AudioSource>().Play();
}
empty = false;
}
//if the player is in the upper part of the scene
if(gameObject.transform.position.y > 0 && empty == false){
//stop particle bubles
bubles.GetComponent.<ParticleSystem>().Stop();
//stop sound
gameObject.GetComponent.<AudioSource>().Stop();
empty = true;
}
//turn the fish so that it is not overturned
if(gameObject.transform.rotation.z < 0.7 && gameObject.transform.rotation.z > -0.7){
gameObject.transform.rotation.z = velosityObject.y / 10;
}
if(gameObject.transform.rotation.z > 0.7){
gameObject.transform.rotation.z -= 0.1;
}
if(gameObject.transform.rotation.z < -0.7){
gameObject.transform.rotation.z += 0.1;
}
//we get fish speed
velosityObject = gameObject.GetComponent.<Rigidbody2D>().velocity;
//camera monitors the fish
cam.transform.position.x = gameObject.transform.position.x + 1.5;
//asking fish movement along the x-axis
gameObject.transform.position.x += 0.08;
//the lower the fish the greater the buoyancy force
if(gameObject.transform.position.y < 0 && isSwimming){
gameObject.GetComponent(Rigidbody2D).AddForce(new Vector2(0,3 * -gameObject.transform.position.y));
}
//if you pressed the up arrow and the fish is at the top of the screen
if(Input.GetKey(KeyCode.UpArrow) && gameObject.transform.position.y < 0 && isSwimming){
//if the speed of the object is less than 10
if(velosityObject.y < 10){
//add force y-axis
gameObject.GetComponent(Rigidbody2D).AddForce(new Vector2(0,25));
}
}
//if you pressed the down arrow
if(Input.GetKey(KeyCode.DownArrow) && isAlive){
//if the speed of the object is less than 10
if(velosityObject.y > -10){
//add force y-axis
gameObject.GetComponent(Rigidbody2D).AddForce(new Vector2(0,-25));
}
}




for (var touch : Touch in Input.touches) {
//if touch is at the top of the screen and the subject is at the bottom of the screen
if(touch.position.x > cam.GetComponent(Camera).pixelHeight / 2 && gameObject.transform.position.y < 0){
//if the speed of the object is less than 10
if(velosityObject.x < 10){
//add force y-axis
gameObject.GetComponent(Rigidbody2D).AddForce(new Vector2(0,25));
}
}
//If touch is at the bottom of the screen
if(touch.position.y < cam.GetComponent(Camera).pixelHeight / 2){
//if the speed of the object is less than 10
if(velosityObject.y > -10){
//add force y-axis
gameObject.GetComponent(Rigidbody2D).AddForce(new Vector2(0,-25));
}
}
}

// if dead
if(!isAlive && isSwimming)
{
gameObject.GetComponent(Rigidbody2D).AddForce(new Vector2(0,-25));
}

}

function StartBubbles()
{
	isAlive = true;
}

function StartSwimming()
{	
	velosityObject.y = 0;
	isSwimming = true;
}

function PlayerDie()
{
	isAlive = false;
//stop particle bubles
bubles.GetComponent.<ParticleSystem>().Stop();
//stop sound
gameObject.GetComponent.<AudioSource>().Stop();
}