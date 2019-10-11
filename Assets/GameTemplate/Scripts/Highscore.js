

function Start(){
//display the highscore
gameObject.GetComponent(GUIText).text = "highscore " + PlayerPrefs.GetInt("Highscore");;
}