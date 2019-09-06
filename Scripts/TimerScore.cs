using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScore : MonoBehaviour {
    public Text text;
	// Use this for initialization
	void Start () {
		if(GameManager.instance.endTime != 0f) {
            text.text = "You Survived " + System.Math.Round(GameManager.instance.endTime,2) + " Seconds in Endless Mode!";
            GameManager.instance.endTime = 0f;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
