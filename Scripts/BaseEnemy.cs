using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Entity {

    protected bool wander = false;
    protected float startTimer, xDir, yDir, shootRange, followRange, shootAngle;
    protected State state;
    protected Vector3 shootVector;
    protected int burstCount = 0;
    protected Sprite standSprite;
    protected Vector3 diff;
    private const float RATE_REG = .3f;
    private const float MOVE_SPD = 1f;
    private const float DMG = 15.0f;
    private const float MAX_HLTH = 100.0f;
    private const float PRJTL_SPD = 5.0f;
    private const float RNG_SHOOT = 4.0f;
    private const float RNG_FOLLOW = 20.0f;
    private const float RNG_ACTIV = 7.0f;
    private const int BRST_CNT = 3;
    private const float BRST_DLY = 3.0f;
    

    protected enum State {
        Shoot,
        Follow,
        Stand,
        Null
    }

    public void resetHealth() {
        health = MAX_HLTH;
    }

    protected void OnEnable() {
        CancelInvoke();
        
    }

    // Use this for initialization
    void Start() {
        //Debug.Log("MADE ENEMY");
        standSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        GameManager.instance.SetEnemy(this);
        shootMag = gameObject.GetComponent<CircleCollider2D>().radius * transform.localScale.x+.5f;
        shootRange = RNG_SHOOT;
        followRange = RNG_FOLLOW;
        speed = MOVE_SPD;
        health = MAX_HLTH;
        damage = DMG;
        projectileSpeed = PRJTL_SPD;
        rateOfFire = RATE_REG;
        startTimer = Time.time;
    }

    protected void RunAI() {
        //Debug.Log("Start Timer " + startTimer);
        //Debug.Log("Delta Time " + Time.deltaTime);
        switch (FindState()) {
            case State.Shoot:
                //determine shoot angle and vector to shoot in
                diff = (GameManager.instance.player.transform.position - transform.position);
                shootAngle = Mathf.Atan2(diff.y, diff.x);
                shootVector = (GameManager.instance.player.transform.position - transform.position).normalized;
                Shoot(shootVector, Quaternion.Euler(0, 0, shootAngle * Mathf.Rad2Deg));
                //Debug.Log("State: Shoot");
                break;
            case State.Follow:
                //determine shoot angle and vector to shoot in
                diff = (GameManager.instance.player.transform.position - transform.position);
                shootAngle = Mathf.Atan2(diff.y, diff.x);
                shootVector = (GameManager.instance.player.transform.position - transform.position).normalized;
                Shoot(shootVector, Quaternion.Euler(0, 0, shootAngle * Mathf.Rad2Deg));
                Follow();
                //Debug.Log("State: Follow");
                break;
            case State.Stand:
                //Debug.Log("State: Stand");
                break;
            case State.Null:
                //Debug.Log("State: Null");
                break;
        }
    }

    // Update is called once per frame
    void Update() {
        RunAI();
    }

    public void DamageEnemy(float dmg) {
        TakeDamage(dmg);
        CheckDeath();
    }

    protected virtual void Shoot(Vector3 shootVector, Quaternion shootAngle) {
        //Debug.Log("USING BASE SHOOT");
        //if shoot is off cooldown, then create projectile and start cooldown
        StopAnim();
        if(burstCount == BRST_CNT) {
            shootCooldownTime = BRST_DLY;
            burstCount = 0;
        }

        if (shootCooldownTime <= 0f) {
            GameManager.instance.CreateProjectile(damage, shootAngle, (shootVector * shootMag) + transform.position, shootVector * projectileSpeed * Time.deltaTime, "Player");
            shootCooldownTime = RATE_REG;
            burstCount++;
        }
        else {
            shootCooldownTime -= Time.deltaTime;
        }
    }

    protected void Follow() {
        transform.position = Vector2.MoveTowards(transform.position, GameManager.instance.player.transform.position, speed*Time.deltaTime);
        PlayAnim(transform.position-GameManager.instance.player.transform.position);
    }

    protected void WanderAround() {
        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x+xDir, transform.position.y+yDir), speed*Time.deltaTime);
    }

    protected State FindState() {
        Vector2 playerDist = CheckPlayerRange();
        if (playerDist.magnitude < shootRange) {
            return State.Shoot;
        }
        else if (playerDist.magnitude < followRange) {
            return State.Follow;
        }
        return State.Null;

    }

    protected Vector2 CheckPlayerRange() {
        return transform.position - GameManager.instance.player.transform.position;
    }

    public virtual void StopAnim() {
        gameObject.GetComponent<SpriteRenderer>().sprite = standSprite;
        gameObject.GetComponent<Animator>().enabled = false;
    }

    public virtual void PlayAnim(Vector3 moveVector) {
        //Debug.Log("MOVE VECTOR ENEMY" + moveVector.x);
            if (moveVector.x < 0f) {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            gameObject.GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<Animator>().Play("Anim_EnemyRun");
    }
}
