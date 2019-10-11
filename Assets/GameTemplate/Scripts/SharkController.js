var score : GameObject;
var shark : GameObject;
var sharkArrow : GameObject;
var player : GameObject;
var coordinateY : float;
private var time : float = 1;
function Update () {
//If you type a value of 100 points higher than
if(score.GetComponent(Score).score >= 100){
//reduce the time
time -= 0.1 * Time.deltaTime;
//if the time is less than or equal to 0
if(time <= 0){
//select at random coordinateY
coordinateY = Random.Range(-0.9, -4.2);
//create a shark
Instantiate(shark,Vector2(player.transform.position.x + 20,coordinateY), Quaternion.identity);
//create a sharkArrow
Instantiate(sharkArrow,Vector2(0,coordinateY), Quaternion.identity);
// time becomes equal to 1
time = 1;
}
}
}

function TurnOff ()
{
	this.enabled = false;
}