using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridMargins : MonoBehaviour {

    private GridLayoutGroup grid;

    private void OnEnable()
    {
        grid = GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(GetComponent<RectTransform>().rect.width/2, GetComponent<RectTransform>().rect.height / 2);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
