using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindGenerator : MonoBehaviour {

    private float _turnSpeed = 12f;
    private float _maxTurnSpeed = 20f;
    private float _turnAccelerate = 5f;
    private float _turnDecelerate = 1f;
    private Image img;
    public WindManager windManager;

    // Use this for initialization
    void Start () {
        //windManager = GameObject.Find("WindManager").GetComponent<WindManager>();
        img = GetComponent<Image>();
        InvokeRepeating("Breeze", 0, .5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        _turnSpeed -= Time.deltaTime * _turnDecelerate;
        _turnSpeed = Mathf.Clamp(_turnSpeed, 0, _maxTurnSpeed);
        transform.Rotate(0, 0, _turnSpeed);
        img.color = _turnSpeed > _maxTurnSpeed / 2 ? Color.white : Color.grey;
    }

    private void OnMouseDrag()
    {
        _turnSpeed += Time.deltaTime * (_turnAccelerate + _turnDecelerate);
    }

    void Breeze()
    {
        _turnSpeed += Random.Range(0f, windManager.windStrength);
    }
}
