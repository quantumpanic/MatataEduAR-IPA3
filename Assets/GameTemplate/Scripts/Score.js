var score : int;
var player : GameObject;
var highscore : int; 
function Start(){
//load best result
highscore = PlayerPrefs.GetInt("Highscore");
}
function FixedUpdate () {
//points are equal to the player's position on the x-axis
score = player.transform.position.x;
//deduce points earned on the screen
gameObject.GetComponent(GUIText).text = "score " + score;
//if the points earned more than the Highscore
if(score > highscore){
//save Highscore
PlayerPrefs.SetInt("Highscore", score);
}
}