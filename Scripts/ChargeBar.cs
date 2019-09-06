using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour {
    private const int SPRT_CNT = 8;
    public Sprite[] meter = new Sprite[SPRT_CNT];
    private const float RATE_CHG = .05f;
    private const float PLY_MAX_CHG = 16f;
    private float multiplier = PLY_MAX_CHG/SPRT_CNT;
    private float charge = PLY_MAX_CHG;
    // Use this for initialization
    void Start() {
        GameManager.instance.SetChargeBar(this);
        UpdateChargeUI(PLY_MAX_CHG);
    }

    public float GetCharge() {
        return charge;
    }

    public float GetMax() {
        return PLY_MAX_CHG;
    }

    public void AddCharge() {
        if (charge < GameManager.instance.GetMaxCharge()) {
            charge += RATE_CHG;
            GameManager.instance.chargeBar.UpdateChargeUI(charge);
        }
    }

    public void LoseCharge() {
        if (charge >= 0f) {
            charge -= RATE_CHG;
            GameManager.instance.chargeBar.UpdateChargeUI(charge);
        }
    }

    private void UpdateChargeUI(float val) {
        //Debug.Log("CM: " + multiplier);
        //Debug.Log("CHARGE VALUE" + (int)val);
        gameObject.GetComponent<Image>().sprite = meter[Mathf.Max((int)(val/multiplier)-1,0)];
    }
}
