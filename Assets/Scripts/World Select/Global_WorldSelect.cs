using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global_WorldSelect : MonoBehaviour {

	public RectTransform panel; //scrollpanel
	public Button[] btns;
	public RectTransform center; //center2Compare

	private float[] distance; //btnDistance to center
	private bool dragging = false; //enable the snap when not drag
	private int btnDistance; //distance betweenButton
	private int minButtonNum;

	void Awake(){
	
		if(panel.anchoredPosition != SaveSnap.instance.rct.anchoredPosition){
			panel.anchoredPosition = SaveSnap.instance.rct.anchoredPosition;
		}
	
	}

	// Use this for initialization
	void Start () {

		int btnLength = btns.Length;
		distance = new float[btnLength];
		btnDistance = (int)Mathf.Abs (btns[1].GetComponent<RectTransform>().anchoredPosition.x - btns[0].GetComponent<RectTransform>().anchoredPosition.x);

	}
	
	// Update is called once per frame
	void Update () {

		for(int i=0; i<btns.Length; i++){
			distance [i] = Mathf.Abs (center.transform.position.x - btns[i].transform.position.x);
		}

		float minDistance = Mathf.Min (distance);
		for(int a=0; a<btns.Length; a++){
			if(minDistance == distance[a]){
				minButtonNum = a;
			}
		}

		if(!dragging){
			LerpToButton (minButtonNum * -btnDistance);
		}

	}

	void LerpToButton(int position){
		float newX = Mathf.Lerp (panel.anchoredPosition.x, position, Time.deltaTime * 10f);
		Vector2 newPos = new Vector2 (newX, panel.anchoredPosition.y);
		panel.anchoredPosition = newPos; //save ini dunks
	}

	public void StartDrag(){
		dragging = true;
	}

	public void EndDrag(){
		dragging = false;
	}

}
