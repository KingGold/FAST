using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Razer : Henchman {

    private const float RATE_REG = 1f;
    private const float ANG_OFFSET = Mathf.PI / 4.0f;

    // Use this for initialization
    void Start () {
        GameManager.instance.SetRazer(this);
        Init();
        health = 1000f;
	}

    protected override void Shoot(Vector3 shootVector, Quaternion shootAngle) {
        Debug.Log("USING HENCHMAN SHOOT");
        //if shoot is off cooldown, then create projectile and start cooldown
        if (shootCooldownTime <= 0f) {
            GameManager.instance.CreateProjectile(damage, shootAngle, (shootVector * shootMag)
                + transform.position, shootVector * projectileSpeed * Time.deltaTime, "Player");
            GameManager.instance.CreateProjectile(damage, Quaternion.Euler(0, 0, shootAngle.z + ANG_OFFSET * Mathf.Rad2Deg), (shootVector * shootMag)
                + transform.position, Quaternion.Euler(0, 0, ANG_OFFSET * Mathf.Rad2Deg) * shootVector * projectileSpeed * Time.deltaTime, "Player");
            GameManager.instance.CreateProjectile(damage, Quaternion.Euler(0, 0, shootAngle.z - ANG_OFFSET * Mathf.Rad2Deg), (shootVector * shootMag)
                + transform.position, Quaternion.Euler(0, 0, -ANG_OFFSET * Mathf.Rad2Deg) * shootVector * projectileSpeed * Time.deltaTime, "Player");

            shootCooldownTime = RATE_REG;
        }
        else {
            shootCooldownTime -= Time.deltaTime;
        }
    }

    public new void DamageEnemy(float dmg) {
        TakeDamage(dmg);
        CheckDeath();
    }

    private new void CheckDeath() {
        if (health <= 0) {
            SceneManager.LoadScene("Win");
        }
    }

    // Update is called once per frame
    void Update () {
        //gameObject.SetActive(true);
        RunAI();
	}

    public override void PlayAnim(Vector3 moveVector) {
        //Debug.Log("MOVE VECTOR ENEMY" + moveVector.x);
        if (moveVector.x < 0f) {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<Animator>().Play("Anim_RazerRun");
    }
}
