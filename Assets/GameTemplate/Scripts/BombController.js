var time1 : float;
var time2 : float = 2;
var bomb : GameObject;
var score : GameObject;
var boost : int = 50;
private var coordinateY : float;
function Update () {
//reduce the time
time1 -= 1.5 * Time.deltaTime;
//if the time is less than or equal to 0
if(time1 <= 0){
//select at random coordinateY
coordinateY = Random.Range(-0.3, -4.2);
//create a bomb
Instantiate(bomb, Vector2(transform.position.x + 10,coordinateY), Quaternion.identity);
// time becomes equal to time2
time1 = time2;
}
//if the Score is less than or equal to boost
if(score.GetComponent(Score).score >= boost){
//the time2 gets smaller
time2 -= 0.2;
//the boost gets bigger
boost += 50;
} 
}

function TurnOff ()
{
	this.enabled = false;
}