using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float timer, endTime;
    private bool stop = false;
    public Text text;
	// Use this for initialization
	void Start () {
        timer = 0;
        GameManager.instance.SetTimer(this);
	}

    public void StopTimer() {
        endTime = timer;
        GameManager.instance.endTime = endTime;
        stop = true;
    }

    public float GetEndTime() {
        return endTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (!stop) {
            timer += Time.deltaTime;
            text.text = ""+System.Math.Round(timer, 1);// timer.ToString("F2");
        }
        
	}
}
