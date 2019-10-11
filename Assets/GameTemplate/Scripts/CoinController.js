var time : float = 3;
var coin : GameObject;
private var coordinateY : float;
private var coordinateX : float;
function Update () {
//reduce the time
time -= 1 * Time.deltaTime;
//if the time is less than or equal to 0
if(time <= 0){
//select at random coordinateY
coordinateY = Random.Range(-4.6, 2.6);
//select at random coordinateX
coordinateX = Random.Range(15, 15);
//create a coin
Instantiate(coin, Vector2(transform.position.x + coordinateX,coordinateY), Quaternion.identity);
// time becomes equal to 2
time = 3;
SendMessage("SpawnFood");
}
}

function TurnOff ()
{
	this.enabled = false;
}

function TurnOn()
{
	this.enabled = true;
}