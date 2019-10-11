var score : GameObject;
var bird : GameObject;
var birdArrow : GameObject;
var player : GameObject;
var coordinateY : float;
private var time : float = 1;
function Update () {
//If you type a value of 50 points higher than
if(score.GetComponent(Score).score >= 50){
//reduce the time
time -= 0.15 * Time.deltaTime;
//if the time is less than or equal to 0
if(time <= 0){
//select at random coordinateY
coordinateY = Random.Range(0.6, 4.2);
//create a bird
Instantiate(bird,Vector2(player.transform.position.x + 20,coordinateY), Quaternion.identity);
//create a birdArrow
Instantiate(birdArrow,Vector2(0,coordinateY), Quaternion.identity);
// time becomes equal to 1
time = 1;
}
}
}

function TurnOff ()
{
	this.enabled = false;
}