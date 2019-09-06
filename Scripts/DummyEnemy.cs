using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : MonoBehaviour {

    private const float SPWN_DIST = 10.0f;
    public bool spawnEnemy = false;
    public bool isHenchmen = false;

	// Use this for initialization
	void Start () {
        GameManager.instance.SetDummy(this);
    }

    private Vector2 CheckPlayerRange() {
        return transform.position - GameManager.instance.player.transform.position;
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("Searching for Player");
        if (CheckPlayerRange().magnitude <= SPWN_DIST) {
            spawnEnemy = true;
        }
	}
}
