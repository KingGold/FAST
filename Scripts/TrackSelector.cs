using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSelector : MonoBehaviour {

    private int trackPos = 1;

    public AudioClip shootAudio, chargeAudio, healAudio;

    // Use this for initialization
    void Start () {
        GameManager.instance.SetTrackSelector(this);
        //if (trackPos != 0) {
        //    trackPos %= 4;
        //    for(int i = 0; i < trackPos; i++) {
        //        transform.Rotate(Vector3.back, 90.000000000000000000000000f);
        //    }
        //}
    }


    public bool SetTrackPostion(int tP) {
        //sets trackpostion and rotation
        //returns true if it updates player
        trackPos = tP;
        GameManager.instance.trackPos = trackPos;
        if (trackPos != 0) {
            trackPos %= 4;
            for (int i = 0; i < trackPos; i++) {
                transform.Rotate(Vector3.back, 90.000000000000000000000000f);
            }
        }
        if (GameManager.instance.player) {
            GameManager.instance.player.ChangeAbility(trackPos);
            return true;
        }
        return false;
    }

    public int SwitchTrack(Vector2 swipeDir, Player player) {
        //swipe Up Right
        if (swipeDir.x > 0 && swipeDir.y > 0) {
            if (trackPos != 2) {
                trackPos++;
                transform.Rotate(Vector3.back, 90.000000000000000000000000f);
            }

            else {
                trackPos = 0;
                transform.Rotate(Vector3.back, 180.000000000000000000000000f);
            }
                
        }
        //swipe Down Left
        else if (swipeDir.x < 0 && swipeDir.y < 0) {
            if (trackPos != 0) {
                trackPos--;
                transform.Rotate(Vector3.forward, 90.000000000000000000000000f);
            }
                
            else{
                trackPos = 2;
                transform.Rotate(Vector3.forward, 180.000000000000000000000000f);
            }
                
            
            
        }
        player.ChangeAbility(trackPos);
        Debug.Log(trackPos);
        if(trackPos == 1) {
            gameObject.GetComponent<AudioSource>().clip = chargeAudio;
        }
        else if(trackPos == 2) {
            gameObject.GetComponent<AudioSource>().clip = healAudio;
        }
        else if(trackPos == 0){
            gameObject.GetComponent<AudioSource>().clip = shootAudio;
        }
        gameObject.GetComponent<AudioSource>().Play();
        GameManager.instance.trackPos = trackPos;
        return trackPos;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
