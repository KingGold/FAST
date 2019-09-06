using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity {

    private Vector2 touchPos, startTouchMove, startTouchShoot, startTouchTrack;
    private Ability currAbility;
    private const float RATE_REG = .25f;
    private const float RATE_SPD = .13f;
    
    private const float RATE_HLTH = .4f;
    private const float MOVE_SPD = 4.0f;
    private const float DMG = 15.0f;
    private Sprite forteStop;
    public Sprite forteStandShootUp, forteStandShootDown, forteStandShootLeft, forteStandShootRight;
    private const float PRJTL_SPD = 15.0f;
    //private float charge;
    public GameObject shieldPrefab;
    private GameObject shield;
    public GameObject healAuraPrefab;
    public GameObject healAura;
    private bool canMove = true;
    //public GameObject projectilePrefab;


    public enum Ability {
        Speed,
        Heal,
        ShieldCharge
    }

    //public float GetMAX_HLTH() {
    //    return MAX_HLTH;
    //}

    //public float GetMAX_CHG() {
    //    return MAX_CHG;
    //}

    private void Awake() {
        //healAura = GameObject.FindGameObjectWithTag("HealAura");
        healAura = (GameObject)Instantiate(healAuraPrefab);
    }

    //public void SetCharge(float val) {
    //    charge = val;
    //}

    // Use this for initialization
    void Start () {
        GameManager.instance.SetPlayer(this);
        shield = (GameObject)Instantiate(shieldPrefab);
        shield.SetActive(false);
        //healAura = (GameObject)Instantiate(healAuraPrefab);
        //healAura.SetActive(false);
        forteStop = gameObject.GetComponent<SpriteRenderer>().sprite;
        shootMag = gameObject.GetComponent<CircleCollider2D>().radius*transform.localScale.x+.1f;
        speed = MOVE_SPD;
        damage = DMG;
        rateOfFire = RATE_REG;
        projectileSpeed = PRJTL_SPD;

        //charge = GameManager.instance.GetMaxCharge();
        //Debug.Log("PLAYERCHARGE" + charge);
        //GameManager.instance.chargeBar.UpdateChargeUI(charge);

        health = GameManager.instance.GetMaxHealth();
        //GameManager.instance.healthBar.UpdateHealthUI(health);

        //Debug.Log("SETTING HEALTH");
        //Debug.Log("HEALTH BAR PLAYER: " + gm.healthBar == null);
        //gm.SetHealthBar(health);
        //gm.SetChargeBar(charge);
        //ChangeAbility(1);
        
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log("SHOOT MAG " + shootMag);
        if (currAbility == Ability.ShieldCharge) {
            UpdateCharge();
        }
        if (shootCooldownTime >= 0f) {
            shootCooldownTime -= Time.deltaTime;
        }
        if (!canMove) {
            canMove = true;
        }
        Debug.Log("AURA STATUS" + healAura.activeInHierarchy);
        if (currAbility == Ability.Heal) {
            healAura.transform.position = transform.position;


            if (GameManager.instance.chargeBar.GetCharge() > 0f) {
                if (Heal()) {
                    if (!healAura.activeInHierarchy) {
                        healAura.SetActive(true);
                    }
                    
                    UpdateCharge();
                }
                
            }
            else {
                healAura.SetActive(false);
            }

        }
        else if (healAura.activeInHierarchy) {
            healAura.SetActive(false);
        }
    }

    private new void CheckDeath() {
        if (health <= 0) {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void UpdateCharge() {
        if(currAbility == Ability.ShieldCharge) {
            //Debug.Log("CHARGING" +charge);
            GameManager.instance.chargeBar.AddCharge();
        }
        else {
            //Debug.Log("LOSING CHARGE" +charge);
            GameManager.instance.chargeBar.LoseCharge();
        }
    }

    public Ability GetCurrAbility() {
        //Debug.Log("CHECKING CURRABILITY: "+currAbility);
        return currAbility;
    }

    public new bool Heal() {
        if (health < GameManager.instance.GetMaxHealth()) {
            health += RATE_HLTH;
            GameManager.instance.healthBar.UpdateHealthUI(health);
            //healAura.SetActive(true);
            return true;
        }
        return false;
    }

    public void DamagePlayer(float dmg) {
        TakeDamage(dmg);
        GameManager.instance.healthBar.UpdateHealthUI(health);
        //Debug.Log("Health" + health);
        if (health <= 0) {
            if (GameManager.instance.timer) {
                GameManager.instance.timer.StopTimer();
                //Debug.Log("ENDTIME FROM PLAYER" + GameManager.instance.endTime);
            }

            SceneManager.LoadScene("GameOver");
        }
    }

    //public float GetCharge() {
    //    return charge;
    //}

    public void ChangeAbility(int trackPos) {
        switch (trackPos) {
            case 0:
                //Debug.Log("PLAYER CHANGED TO SPEED");
                currAbility = Ability.Speed;
                rateOfFire = RATE_SPD;
                healAura.SetActive(false);
                break;
                
            case 1:
                //Debug.Log("PLAYER CHANGED TO SHIELDCHARGE");
                currAbility = Ability.ShieldCharge;
                rateOfFire = RATE_REG;
                healAura.SetActive(false);
                break;
                
            case 2:
                //Debug.Log("PLAYER CHANGED TO HEALTH");
                currAbility = Ability.Heal;
                rateOfFire = RATE_REG;
                
                break;
            case 3:
                //Debug.Log("PLAYER CHANGED TO SHIELDCHARGE");
                currAbility = Ability.ShieldCharge;
                rateOfFire = RATE_REG;
                healAura.SetActive(false);
                break;
            default:
                //Debug.Log("PLAYER CHANGED TO SHIELDCHARGE");
                currAbility = Ability.ShieldCharge;
                rateOfFire = RATE_REG;
                healAura.SetActive(false);
                break;
        }
            
    }

    public void Shield(Vector3 shootVector, Quaternion shootAngle) {
        //activate shield and point in correct direction
        shield.SetActive(true);
        shield.transform.rotation = shootAngle;
        shield.transform.position = (shootVector * shootMag) + transform.position;
    }

    public void DeactivateShield() {
        shield.SetActive(false);
    }

    private void UpdateShootOrigin(Quaternion shootAngle) {
        transform.rotation = shootAngle;
    }

    public void SpawnProjectile(Vector3 shootVector, Quaternion shootAngle) {
        //if shoot is off cooldown, then create projectile and start cooldown
        //Debug.Log("Shooting");
        
        if (shootCooldownTime <= 0f && shootVector != new Vector3(0, 0, 0)) {
            //Debug.Log(shootAngle);
            GameManager.instance.CreateProjectile(damage, shootAngle, (shootVector*shootMag)+transform.position, shootVector * projectileSpeed * Time.deltaTime, "Enemy");
            if (currAbility == Ability.ShieldCharge) {
                shootCooldownTime = RATE_REG;
            }
            else if(currAbility == Ability.Heal) {
                shootCooldownTime = RATE_HLTH;
            }
            else if (currAbility == Ability.Speed) {
                shootCooldownTime = RATE_SPD;

            }
        }
        
    }

    private void StopPlayerAnim() {
        gameObject.GetComponent<SpriteRenderer>().sprite = forteStop;
        gameObject.GetComponent<Animator>().enabled = false;
    }

    public void MovePlayer(Vector3 moveVector) {
        //track direction from initial touch on screen
        if (canMove) {
            if ((moveVector).magnitude > 1)
                moveVector = (moveVector).normalized;
            transform.position += (moveVector * speed * Time.deltaTime);
            canMove = false;
        }
        
    }

    public void PlayPlayerAnim(Vector3 moveVector, Vector3 shootVector, bool requestingShoot) {
        if(moveVector == new Vector3(0,0,0) && !requestingShoot) {
            StopPlayerAnim();
        }
        else if (!requestingShoot) {
            if (moveVector.x > 0) {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            gameObject.GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<Animator>().Play("Anim_ForteRun");
        }
        else if (moveVector == new Vector3(0, 0, 0)) {
            if (Mathf.Abs(shootVector.y) > Mathf.Abs(shootVector.x)) {
                if (shootVector.y > 0) {
                    gameObject.GetComponent<SpriteRenderer>().sprite = forteStandShootUp;
                    gameObject.GetComponent<Animator>().enabled = false;
                    //gameObject.GetComponent<Animator>().enabled = true;
                    //gameObject.GetComponent<Animator>().Play("Anim_ForteShootBack");
                }
                else {
                    gameObject.GetComponent<SpriteRenderer>().sprite = forteStandShootDown;
                    gameObject.GetComponent<Animator>().enabled = false;
                    //gameObject.GetComponent<Animator>().enabled = true;
                    //gameObject.GetComponent<Animator>().Play("Anim_ForteShootFront");
                }
            }
            else {
                if (shootVector.x > 0) {
                    gameObject.GetComponent<SpriteRenderer>().sprite = forteStandShootRight;
                    gameObject.GetComponent<Animator>().enabled = false;
                }
                else {
                    gameObject.GetComponent<SpriteRenderer>().sprite = forteStandShootLeft;
                    gameObject.GetComponent<Animator>().enabled = false;
                }
            }
        }
        else if (Mathf.Abs(shootVector.y) > Mathf.Abs(shootVector.x)) {
            if (shootVector.y > 0) {
                gameObject.GetComponent<Animator>().enabled = true;
                gameObject.GetComponent<Animator>().Play("Anim_ForteShootBack");
            }
            else {
                gameObject.GetComponent<Animator>().enabled = true;
                gameObject.GetComponent<Animator>().Play("Anim_ForteShootFront");
            }
        }
        else if((moveVector.x > 0 && shootVector.x > 0) || (moveVector.x < 0 && shootVector.x < 0)){
            if (moveVector.x > 0) {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else{
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            gameObject.GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<Animator>().Play("Anim_ForteShoot");
        }
        
        else if((moveVector.x > 0 && shootVector.x < 0) || (moveVector.x < 0 && shootVector.x > 0)) {
            if (moveVector.x > 0) {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            gameObject.GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<Animator>().Play("Anim_ForteShootOver");
        }
        
    }
}

