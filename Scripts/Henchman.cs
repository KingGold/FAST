using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Henchman : BaseEnemy {
    
    private const float RATE_REG = 1f;
    private const float MOVE_SPD = 1.2f;
    private const float DMG = 15.0f;
    private const float MAX_HLTH = 200.0f;
    private const float PRJTL_SPD = 5.5f;
    private const float RNG_SHOOT = 4.0f;
    private const float RNG_FOLLOW = 20.0f;
    private const float RNG_ACTIV = 7.0f;
    private const float ANG_OFFSET1 = Mathf.PI/ 4.0f;
    private const float ANG_OFFSET2 = Mathf.PI / 2.0f;
    private const float ANG_OFFSET3 = 3*Mathf.PI / 4.0f;

    protected override void Shoot(Vector3 shootVector, Quaternion shootAngle) {
        Debug.Log("USING HENCHMAN SHOOT");
        //if shoot is off cooldown, then create projectile and start cooldown
        StopAnim();
        if (shootCooldownTime <= 0f) {
            GameManager.instance.CreateProjectile(damage, shootAngle, (shootVector * shootMag)
                + transform.position, shootVector * projectileSpeed * Time.deltaTime, "Player");
            GameManager.instance.CreateProjectile(damage, Quaternion.Euler(0, 0, shootAngle.z + ANG_OFFSET1 * Mathf.Rad2Deg),  (shootVector * shootMag)
                + transform.position, Quaternion.Euler(0, 0, ANG_OFFSET1 * Mathf.Rad2Deg) * shootVector * projectileSpeed * Time.deltaTime, "Player");
            GameManager.instance.CreateProjectile(damage, Quaternion.Euler(0, 0, shootAngle.z - ANG_OFFSET1 * Mathf.Rad2Deg), (shootVector * shootMag)
                + transform.position, Quaternion.Euler(0, 0, -ANG_OFFSET1 * Mathf.Rad2Deg) * shootVector * projectileSpeed * Time.deltaTime, "Player");

            GameManager.instance.CreateProjectile(damage, Quaternion.Euler(0, 0, shootAngle.z - ANG_OFFSET2 * Mathf.Rad2Deg), (shootVector * shootMag)
                + transform.position, Quaternion.Euler(0, 0, -ANG_OFFSET2 * Mathf.Rad2Deg) * shootVector * projectileSpeed * Time.deltaTime, "Player");
            GameManager.instance.CreateProjectile(damage, Quaternion.Euler(0, 0, shootAngle.z - ANG_OFFSET2 * Mathf.Rad2Deg), (shootVector * shootMag)
                + transform.position, Quaternion.Euler(0, 0, -ANG_OFFSET2 * Mathf.Rad2Deg) * shootVector * projectileSpeed * Time.deltaTime, "Player");

            GameManager.instance.CreateProjectile(damage, Quaternion.Euler(0, 0, shootAngle.z - ANG_OFFSET3 * Mathf.Rad2Deg), (shootVector * shootMag)
                + transform.position, Quaternion.Euler(0, 0, -ANG_OFFSET3 * Mathf.Rad2Deg) * shootVector * projectileSpeed * Time.deltaTime, "Player");
            GameManager.instance.CreateProjectile(damage, Quaternion.Euler(0, 0, shootAngle.z - ANG_OFFSET3 * Mathf.Rad2Deg), (shootVector * shootMag)
                + transform.position, Quaternion.Euler(0, 0, -ANG_OFFSET3 * Mathf.Rad2Deg) * shootVector * projectileSpeed * Time.deltaTime, "Player");

            shootCooldownTime = RATE_REG;
        }
        else {
            shootCooldownTime -= Time.deltaTime;
        }
    }

    public void Init() {
        
        //Debug.Log("MADE ENEMY");
        standSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        shootMag = gameObject.GetComponent<CircleCollider2D>().radius * transform.localScale.x + .6f;
        shootRange = RNG_SHOOT;
        followRange = RNG_FOLLOW;
        speed = MOVE_SPD;
        health = MAX_HLTH;
        damage = DMG;
        projectileSpeed = PRJTL_SPD;
        rateOfFire = RATE_REG;
        startTimer = Time.time;

    }

    // Use this for initialization
    void Start() {
        GameManager.instance.SetHenchman(this);
        Init();
    }
    // Update is called once per frame
    void Update() {
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
        gameObject.GetComponent<Animator>().Play("Anim_HenchRun");
    }

}
