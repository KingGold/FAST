using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour {

    private const float JOY_LNGTH = 80f;
    //private const int STRT_SHT_TMR = 10;
    private Vector2 touchPos, startTouchMove, startTouchShoot, startTouchTrack;
    private Touch[] myTouches;
    private Rect moveZone, shootZone, trackZone;
    private float shootAngle;
    private Vector3 moveVector, shootVector, trackVector;
    private Image moveStick, shootStick, moveStickO, shootStickO;
    bool requestingShoot = false;
    bool inShootZone = false;
    //private const float DEAD_ZONE_LNGTH = 80f;

    void Start () {
        GameManager.instance.SetTouchInput(this);
        //create touch zones
        moveZone = new Rect(0, 0, Screen.width * .5f, Screen.height);
        shootZone = new Rect(Screen.width * .5f, 0, Screen.width, Screen.height);
        trackZone = new Rect(Screen.width * .85f, 0, Screen.width, Screen.height * .25f);
        moveStick = GameObject.FindGameObjectWithTag("MoveStick").GetComponent<Image>();
        moveStickO = GameObject.FindGameObjectWithTag("MoveStickO").GetComponent<Image>();
        shootStick = GameObject.FindGameObjectWithTag("ShootStick").GetComponent<Image>();
        shootStickO = GameObject.FindGameObjectWithTag("ShootStickO").GetComponent<Image>();
        moveStick.gameObject.SetActive(false);
        shootStick.gameObject.SetActive(false);
        moveStickO.gameObject.SetActive(false);
        shootStickO.gameObject.SetActive(false);
        
        
    }
	
    public void UpdateTouches() {
        //Debug.Log("IN SHOOT ZOME" +inShootZone);
        if (Input.touchCount > 0) {
            myTouches = Input.touches;
            //get touch
            //touch = Input.GetTouch(0);
            //set initial touch once first pressed for stick
            for (int i = 0; i < Input.touchCount; i++) {

                if (moveZone.Contains(myTouches[i].position)) {
                    //touch in move zone
                    if (myTouches[i].phase == TouchPhase.Began) {
                        startTouchMove = myTouches[i].position;
                        //Debug.Log(startTouchMove);
                    }
                    if (myTouches[i].phase == TouchPhase.Moved || myTouches[i].phase == TouchPhase.Stationary) {
                        moveVector = myTouches[i].position - startTouchMove;
                        GameManager.instance.player.MovePlayer(moveVector);
                        //control stick UI
                        moveStick.gameObject.SetActive(true);
                        moveStickO.gameObject.SetActive(true);
                        moveStickO.rectTransform.position = startTouchMove;
                        if (moveVector.magnitude <= JOY_LNGTH) {
                            
                            moveStick.rectTransform.position = myTouches[i].position;
                        }
                        else {
                            moveStick.rectTransform.position = startTouchMove + (Vector2)moveVector.normalized * JOY_LNGTH;
                        }

                        
                    }
                    if(myTouches[i].phase == TouchPhase.Ended||myTouches[i].phase == TouchPhase.Canceled) {
                        moveVector = new Vector3(0, 0, 0);
                        moveStick.gameObject.SetActive(false);
                        moveStickO.gameObject.SetActive(false);
                        //GameManager.instance.player.StopPlayerAnim();
                    }
                }
                if (trackZone.Contains(myTouches[i].position) && !inShootZone) {
                    //touch in track zone
                    if (myTouches[i].phase == TouchPhase.Began) {
                        startTouchTrack = myTouches[i].position;
                    }
                    else if (myTouches[i].phase == TouchPhase.Ended || myTouches[i].phase == TouchPhase.Canceled) {
                        trackVector = myTouches[i].position - startTouchTrack;
                        GameManager.instance.trackSelector.SwitchTrack(trackVector, GameManager.instance.player);
                    }
                }
                else if ((shootZone.Contains(myTouches[i].position) && GameManager.instance.player.GetCurrAbility() == Player.Ability.Speed)) {
                    Debug.Log("SHOOT ABILITY ACTIVE");
                    //int timer = 0;
                    //touch in shoot zone with speed ability equipped
                    if (myTouches[i].phase == TouchPhase.Began) {
                        requestingShoot = true;
                        inShootZone = true;
                        startTouchShoot = myTouches[i].position;
                        //Debug.Log("STARTTOUCHSHOOT:  " + startTouchShoot);
                        //shooting = true;
                        //timer = STRT_SHT_TMR;
                    }
                    if (myTouches[i].phase == TouchPhase.Moved || myTouches[i].phase == TouchPhase.Stationary) {
                        //if(timer != 0) {
                            //timer--;
                        //}
                        if ((GameManager.instance.player.GetCurrAbility() == Player.Ability.Speed && GameManager.instance.chargeBar.GetCharge() >= 0f)) {
                            //get angle and vector
                            shootVector = myTouches[i].position - startTouchShoot;
                            //Debug.Log("SHOOTVECTORMAG:  " + shootVector.magnitude);
                            shootAngle = Mathf.Atan2(shootVector.y, shootVector.x);
                            //Debug.Log(shootAngle);

                            
                            //control stick UI

                            shootStick.gameObject.SetActive(true);
                            shootStickO.gameObject.SetActive(true);
                            shootStickO.rectTransform.position = startTouchShoot;

                            if (shootVector.magnitude <= JOY_LNGTH) {
                                //Debug.Log("MAG < JOYLENGTH");
                                shootStick.rectTransform.position = myTouches[i].position;
                            }
                            else {
                                //Debug.Log("MAG > JOYLENGTH");
                                shootStick.rectTransform.position = startTouchShoot + (Vector2)shootVector.normalized * JOY_LNGTH;
                                GameManager.instance.player.SpawnProjectile(shootVector.normalized, Quaternion.Euler(0, 0, shootAngle * Mathf.Rad2Deg));
                                GameManager.instance.player.UpdateCharge();
                            }

                        }
                    }
                    else if (myTouches[i].phase == TouchPhase.Ended || myTouches[i].phase == TouchPhase.Canceled) {
                        shootStick.gameObject.SetActive(false);
                        shootStickO.gameObject.SetActive(false);
                        requestingShoot = false;
                        inShootZone = false;
                    }
                }
                else if (shootZone.Contains(myTouches[i].position) && GameManager.instance.player.GetCurrAbility() == Player.Ability.ShieldCharge) {
                    //Debug.Log("SHIELD ACTIVE");
                    //touch in shield zone with shield ability equipped
                    if (myTouches[i].phase == TouchPhase.Began) {
                        //Debug.Log("New Stick Loc");
                        startTouchShoot = myTouches[i].position;
                        inShootZone = true;


                    }
                    if (myTouches[i].phase == TouchPhase.Moved || myTouches[i].phase == TouchPhase.Stationary) {
                        if(GameManager.instance.chargeBar.GetCharge() >= 0f) {
                            //get angle and vector
                            shootVector = myTouches[i].position - startTouchShoot;
                            shootAngle = Mathf.Atan2(shootVector.y, shootVector.x);
                            //Debug.Log(shootAngle);
                            //do shield stuff
                            GameManager.instance.player.Shield(shootVector.normalized, Quaternion.Euler(0, 0, shootAngle * Mathf.Rad2Deg));
                            GameManager.instance.player.UpdateCharge();
                            //control stick UI
                            shootStick.gameObject.SetActive(true);
                            shootStickO.gameObject.SetActive(true);
                            shootStickO.rectTransform.position = startTouchShoot;
                            if (shootVector.magnitude <= JOY_LNGTH) {
                                //Debug.Log("SHOOT VECTOR MAG:   " + shootVector.magnitude);
                                shootStick.rectTransform.position = myTouches[i].position;
                            }
                            else {
                                shootStick.rectTransform.position = startTouchShoot + (Vector2)shootVector.normalized * JOY_LNGTH;
                            }

                        }
                        else {
                            GameManager.instance.player.DeactivateShield();
                        }

                    }
                    else if (myTouches[i].phase == TouchPhase.Ended || myTouches[i].phase == TouchPhase.Canceled) {
                        GameManager.instance.player.DeactivateShield();
                        shootStick.gameObject.SetActive(false);
                        shootStickO.gameObject.SetActive(false);
                        inShootZone = false;
                    }
                }
                else if (shootZone.Contains(myTouches[i].position) && GameManager.instance.player.GetCurrAbility() == Player.Ability.Heal) {
                    //touch in shoot zone with Heal ability equipped
                    if (GameManager.instance.chargeBar.GetCharge() > 0f) {
                        if (GameManager.instance.player.Heal()) {
                            //GameManager.instance.player.healAuraa.SetActive(true);
                            GameManager.instance.player.UpdateCharge();


                            //GameManager.instance.player.healAura.transform.position = GameManager.instance.player.transform.position;
                            //if (!GameManager.instance.player.healAura.activeInHierarchy) {
                            //    Debug.Log("AURA ACTIVE");
                            //    GameManager.instance.player.healAura.SetActive(true);
                            //}
                        }
                    }
                    
                    //if ((myTouches[i].phase == TouchPhase.Ended && GameManager.instance.player.healAura.activeInHierarchy )
                    //    || (myTouches[i].phase == TouchPhase.Canceled && GameManager.instance.player.healAura.activeInHierarchy)) {
                    //    GameManager.instance.player.healAura.SetActive(false);
                    //}
                }
            }
        }
        GameManager.instance.player.PlayPlayerAnim(moveVector, shootVector, requestingShoot);
    }

	// Update is called once per frame
	public void Update () {
        //Debug.Log("Touch Updating");
        if (!GameManager.instance.pauseMenu.isPaused) {
            UpdateTouches();
        }
        
    }
}
