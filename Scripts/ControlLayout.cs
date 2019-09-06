using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLayout : MonoBehaviour {
    public GameObject chargeHealthLabel, playButton, moveStickLabel, abilityStickLabel,
        shootO, shootS, moveO, moveS, abilitySelectorLabel, controlLayoutLabel, shootLabel,
        backButton, forteMove, forteShoot, forteShield, moveLabel, stickLabel, stickLabel2,
        trackShoot, trackShield, trackHeal, h2pLabel, chargeLabel, healthLabel, healthBorder, health,
        nextHealth, nextCharge, nextMove, nextShoot, nextAbilityS, nextAbilitySS, nextLast, charge, divider,
        track, trackMove, sticksInfo;

    // Use this for initialization
    void Start () {
        //parentObj = GameObject.Find("Tutorial GameCanvas");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadControl(string name) {
        if (name == "controlLayout") {
            sticksInfo.SetActive(true);
            trackMove.SetActive(true);
            trackShoot.SetActive(false);
            divider.SetActive(true);
            trackShield.SetActive(false);
            trackHeal.SetActive(false);
            trackShoot.SetActive(false);
            nextLast.SetActive(false);
            
            abilityStickLabel.SetActive(true);
            shootO.SetActive(true);
            shootS.SetActive(true);
            moveStickLabel.SetActive(true);
            moveO.SetActive(true);
            moveS.SetActive(true);
            playButton.SetActive(true);
        }
        else if(name == "moveStick") {
            
            moveLabel.SetActive(true);
            chargeLabel.SetActive(false);
            nextMove.SetActive(false);
            forteMove.SetActive(true);
            moveStickLabel.SetActive(true);
            moveO.SetActive(true);
            moveS.SetActive(true);
            nextShoot.SetActive(true);
        }
        else if (name == "abilityStick") {
            //sticksInfo.SetActive(false);
            nextShoot.SetActive(false);
            forteMove.SetActive(false);
            moveStickLabel.SetActive(false);
            moveO.SetActive(false);
            moveS.SetActive(false);
            moveLabel.SetActive(false);

            abilityStickLabel.SetActive(true);
            shootLabel.SetActive(true);
            forteShoot.SetActive(true);
            shootO.SetActive(true);
            shootS.SetActive(true);
            nextAbilityS.SetActive(true);

            trackMove.SetActive(false);
            track.SetActive(true);
        }
        else if (name == "abilitySelector") {
            trackMove.SetActive(true);
            track.SetActive(false);
            abilityStickLabel.SetActive(false);
            shootLabel.SetActive(false);
            forteShoot.SetActive(false);
            shootO.SetActive(false);
            shootS.SetActive(false);
            nextAbilityS.SetActive(false);
            track.SetActive(false);

            abilitySelectorLabel.SetActive(true);
            stickLabel.SetActive(true);
            stickLabel2.SetActive(true);
            nextAbilitySS.SetActive(true);
            //trackMove.SetActive(true);
        }
        else if (name == "abilitySelectorS") {
            nextAbilitySS.SetActive(false);
            stickLabel.SetActive(false);
            stickLabel2.SetActive(false);
            
            trackShield.SetActive(true);
            trackHeal.SetActive(true);
            trackShoot.SetActive(true);
            nextLast.SetActive(true);
        }
        else if (name == "charge") {
            healthLabel.SetActive(false);
            nextCharge.SetActive(false);
            charge.SetActive(true);
            chargeLabel.SetActive(true);
            nextMove.SetActive(true);
        }
        else if (name == "health") {
            h2pLabel.SetActive(false);
            nextHealth.SetActive(false);
            healthBorder.SetActive(true);
            healthLabel.SetActive(true);
            health.SetActive(true);
            chargeHealthLabel.SetActive(true);
            nextCharge.SetActive(true);
        }
    }
}
