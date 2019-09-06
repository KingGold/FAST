using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    protected float health, speed, damage, rateOfFire, projectileSpeed, shootCooldownTime;
    protected float shootMag;

    void Start() {

    }

    protected void CheckDeath() {
        if (health <= 0) {
            Debug.Log("DEAD MAN");
            gameObject.SetActive(false);

        }
    }

    protected void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }

    protected bool Heal()
    {
        return false;
    }

    protected void ChangeRoF(float newRate)
    {
        rateOfFire = newRate;
    }
}
