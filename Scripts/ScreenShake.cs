using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {


    public float shakeTimer, shakeAmount;
    //private Vector3 initPos;
	// Use this for initialization
	void Start () {
        //initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (shakeTimer >= 0) {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmount;

            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);

            shakeTimer -= Time.deltaTime;
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 4;
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 5;
        }
        //if(shakeTimer < 0) {
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 5;
        //}
	}

    public void ShakeCamera() {
        shakeAmount = .05f;
        shakeTimer = .5f;
    }

    public void ShakeCamera(float pwr, float dur) {

        shakeAmount = pwr;
        shakeTimer = dur;

    }
}
