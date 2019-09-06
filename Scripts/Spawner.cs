using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    //public DummyEnemy[] dumbEnemies;
    private float[] spawnCooldownTime;
    private float RATE_SPWN = 20.0f;
    
    // Use this for initialization
    void Start () {
        Debug.Log(GameManager.instance.GetDummySize());
        spawnCooldownTime = new float[GameManager.instance.GetDummySize()];
        for(int i = 0; i < GameManager.instance.GetDummySize(); i++){
            spawnCooldownTime[i] = RATE_SPWN;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for(int i=0; i<GameManager.instance.GetDummySize(); i++) {
            if (GameManager.instance.GetDummy(i) != null && !GameManager.instance.GetDummy(i).gameObject.activeInHierarchy) {
                //Debug.Log("CHECKING TO REACTIVATE" + spawnCooldownTime[i]);
                if (spawnCooldownTime[i] <= 0f) {
                    //Debug.Log("REACTIVATING");
                    GameManager.instance.ActivateDummy(i);
                    spawnCooldownTime[i] = RATE_SPWN;
                    if(RATE_SPWN >= 2) {
                        RATE_SPWN -= 1;
                    }
                    
                }
                else {
                    spawnCooldownTime[i] -= Time.deltaTime;
                    //Debug.Log("CANT REACTIVATE" + spawnCooldownTime[i]);
                }
            }
            
        }
		
	}
}
