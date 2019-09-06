using Boo.Lang;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private const float PLY_MAX_HLTH = 130.0f;
    
    private const int PLYR_PRJCTL_CNT = 20;
    private const int ENMY_PRJCTL_CNT = 20;
    private const int ENMY_CNT = 20;
    private const int SPWN_ENMY_CNT = 20;
    private bool[] unlockedLevels;
    //private Spawner spawner;
    public Player player;
    private Projectile[] playerProjectiles = new Projectile[PLYR_PRJCTL_CNT];
    private Projectile[] enemyProjectiles = new Projectile[ENMY_PRJCTL_CNT];
    private BaseEnemy[] enemies = new BaseEnemy[ENMY_CNT];
    private Henchman hench;
    public GameObject projectilePrefab, enemyPrefab, henchPrefab;
    public static GameManager instance = null;
    public HealthBar healthBar;
    public TrackSelector trackSelector;
    private DummyEnemy[] dumbEnemies = new DummyEnemy[SPWN_ENMY_CNT];
    public ChargeBar chargeBar;
    private TouchInput tI;
    public int trackPos = 1;
    public Timer timer;
    public float endTime;
    public PauseMenu pauseMenu;
    private bool updatePlayerTrackAbility = false;
    public float GetMaxHealth() {
        return PLY_MAX_HLTH;
    }

    

    public float GetMaxCharge() {

        return chargeBar.GetMax();
    }

    private void Awake() {
        //Check if instance already exists
        if (instance == null) {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this) {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        //Instantiate gameManager prefab

        playerProjectiles = new Projectile[PLYR_PRJCTL_CNT];
        enemyProjectiles = new Projectile[ENMY_PRJCTL_CNT];
        //create player projectiles pool

        //for (int i = 0; i < PLYR_PRJCTL_CNT; i++) {
        //    GameObject newObj = (GameObject)Instantiate(projectilePrefab);
        //    newObj.SetActive(false);
        //    playerProjectiles[i] = newObj;
        //}
        ////create enemy projectiles pool

        //for (int i = 0; i < ENMY_PRJCTL_CNT; i++) {
        //    GameObject newObj = (GameObject)Instantiate(projectilePrefab);
        //    newObj.SetActive(false);
        //    enemyProjectiles[i] = newObj;
        //}
    }

    public void SetPause(PauseMenu p) {
        pauseMenu = p;
    }

    public void ActivateDummy(int i) {
        dumbEnemies[i].gameObject.SetActive(true);
    }

    public int GetDummySize() {
        return dumbEnemies.Length;
    }

    public DummyEnemy GetDummy(int i) {
        return dumbEnemies[i];
    }

    public void SetTrackSelector(TrackSelector t) {
        trackSelector = t;
        if (!trackSelector.SetTrackPostion(trackPos)) {
            updatePlayerTrackAbility = true;
        }
        
    }

    public void SetTimer(Timer t) {
        timer = t;
        endTime = 0f;
    }

    public void SetPlayer(Player p) {
        player = p;
        if (updatePlayerTrackAbility) {
            player.ChangeAbility(trackPos);
        }
    }

    public void SetEnemy(BaseEnemy e) {
        for (int i = 0; i < ENMY_CNT; i++) {
            if (enemies[i] == null) {
                enemies[i] = e;
                enemies[i].gameObject.SetActive(false);
                //Debug.Log("FOUND ENEMY");
                return;
            }
        }
    }

    public void SetRazer(Henchman r) {
        hench = r;
    }

    public void SetHenchman(Henchman h) {
        hench = h;
        hench.gameObject.SetActive(false);
    }

    public void SetDummy(DummyEnemy d) {
        for(int i=0; i<SPWN_ENMY_CNT; i++) {
            if(dumbEnemies[i] == null) {
                dumbEnemies[i] = d;
                //Debug.Log("FOUND DUMB ENEMY");
                return;
            }
        }

    }

    public void SetPlayerProjectile(Projectile p) {
        for (int i = 0; i < PLYR_PRJCTL_CNT; i++) {
            if (playerProjectiles[i] == null) {
                playerProjectiles[i] = p;
                playerProjectiles[i].gameObject.SetActive(false);
                return;
            }
        }
        //if(SceneManager.GetActiveScene().name == "BossLevel") {
            //spawner = new Spawner();
            //spawner.dumbEnemies = dumbEnemies;
        //}
    }

    public void SetEnemyProjectile(Projectile p) {
        for (int i = 0; i < ENMY_PRJCTL_CNT; i++) {
            if (enemyProjectiles[i] == null) {
                enemyProjectiles[i] = p;
                enemyProjectiles[i].gameObject.SetActive(false);
                return;
            }
        }
    }

    public void SetTouchInput(TouchInput t) {
        tI = t;
    }

    public void SetChargeBar(ChargeBar c) {
        chargeBar = c;
        //player.SetCharge(chargeBar.GetMax());
    }

    public void SetHealthBar(HealthBar h) {
        healthBar = h;
        healthBar.UpdateHealthUI(PLY_MAX_HLTH);

    }

    // Use this for initialization
    void Start() {
        


        //dumbEnemies = GameObject.FindGameObjectsWithTag("DummyEnemy");
        //for (int i = 0; i < dumbEnemies.Length; i++) {
        //    dumbEnemies[i].GetComponent<DummyEnemy>().SetGameManager(this);
        //}

        //trackSelector = GameObject.FindGameObjectWithTag("TrackSelector").GetComponent<TrackSelector>();

        //healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        //SetHealthBar(player.MAX_HLTH);

        //chargeBar = GameObject.FindGameObjectWithTag("ChargeBar").GetComponent<ChargeBar>();
        //SetChargeBar(player.MAX_CHG);

        //create player projectiles pool
        
        //for (int i = 0; i < PLYR_PRJCTL_CNT; i++) {
        //    GameObject newObj = (GameObject)Instantiate(projectilePrefab);
        //    newObj.SetActive(false);
        //    playerProjectiles[i] = newObj;
        //}
        //create enemy projectiles pool
        
        //for (int i = 0; i < ENMY_PRJCTL_CNT; i++) {
        //    GameObject newObj = (GameObject)Instantiate(projectilePrefab);
        //    newObj.SetActive(false);
        //    enemyProjectiles[i] = newObj;
        //}
        ////create enemy pool
        //enemies = new GameObject[ENMY_CNT];
        //for (int i = 0; i < ENMY_CNT; i++) {
        //    GameObject newObj = (GameObject)Instantiate(enemyPrefab);
        //    newObj.SetActive(false);
        //    newObj.GetComponent<BaseEnemy>().SetGameManager(this);
        //    enemies[i] = newObj;
        //}
        ////create henchman pool
        //{
        //    GameObject newObj = (GameObject)Instantiate(henchPrefab);
        //    newObj.SetActive(false);
        //    newObj.GetComponent<Henchman>().SetGameManager(this);
        //    //hench = newObj;
        //}

    }

    public void ActivateEnemiesInDummyRange() {
        for (int i = 0; i < dumbEnemies.Length; i++) {
            //call activate enemy if dummy is close enough to player
            if (dumbEnemies[i] != null && dumbEnemies[i].spawnEnemy && dumbEnemies[i].gameObject.activeInHierarchy) {
                //Debug.Log("SPAWN ENEMY");
                if(ActivateEnemy(dumbEnemies[i].gameObject.transform.position, dumbEnemies[i].isHenchmen)) {
                    dumbEnemies[i].gameObject.SetActive(false);
                }
                
            }
        }
    }

    private bool ActivateEnemy(Vector3 pos, bool isHench) {
        if (!isHench) {
            //activate regular enemy
            for (int i = 0; i < ENMY_CNT; i++) {
                if (!enemies[i].gameObject.activeInHierarchy) {
                    enemies[i].transform.position = pos;
                    enemies[i].resetHealth();
                    enemies[i].gameObject.SetActive(true);
                    return true;
                }
            }
        }
        else {
            //activate henchman
            hench.transform.position = pos;
            hench.gameObject.SetActive(true);
            return true;
        }
        return false;

    }

    public void CreateProjectile(float dmg, Quaternion rotation, Vector3 forwardRelLoc, Vector3 vel, string targetTag) {
        //cycle player projectiles
        if (targetTag == "Enemy") {
            for (int i = 0; i < PLYR_PRJCTL_CNT; i++) {
                if (!playerProjectiles[i].gameObject.activeInHierarchy) {
                    playerProjectiles[i].GetComponent<Projectile>().velocity = vel;
                    playerProjectiles[i].GetComponent<Projectile>().attackDamage = dmg;
                    playerProjectiles[i].GetComponent<Projectile>().transform.rotation = rotation;
                    playerProjectiles[i].GetComponent<Projectile>().transform.position = forwardRelLoc;
                    //playerProjectiles[i].GetComponent<Projectile>().targetTag = targetTag;
                    playerProjectiles[i].gameObject.SetActive(true);

                    return;
                }
            }
        }
        else {
            //cycle enemy projectiles
            for (int i = 0; i < ENMY_PRJCTL_CNT; i++) {
                if (!enemyProjectiles[i].gameObject.activeInHierarchy) {
                    enemyProjectiles[i].GetComponent<Projectile>().velocity = vel;
                    enemyProjectiles[i].GetComponent<Projectile>().attackDamage = dmg;
                    enemyProjectiles[i].GetComponent<Projectile>().transform.rotation = rotation;
                    enemyProjectiles[i].GetComponent<Projectile>().transform.position = forwardRelLoc;
                    //enemyProjectiles[i].GetComponent<Projectile>().targetTag = targetTag;
                    enemyProjectiles[i].gameObject.SetActive(true);

                    return;
                }
            }
        }

    }

    void UpdateProjectileLoc() {
        //update player projectiles
        for (int i = 0; i < PLYR_PRJCTL_CNT; i++) {
            if (playerProjectiles[i] && playerProjectiles[i].gameObject.activeInHierarchy) {
                playerProjectiles[i].transform.position += playerProjectiles[i].GetComponent<Projectile>().velocity;
            }
        }
        //update enemy projectiles
        for (int i = 0; i < ENMY_PRJCTL_CNT; i++) {
            if (enemyProjectiles[i] && enemyProjectiles[i].gameObject.activeInHierarchy) {
                enemyProjectiles[i].transform.position += enemyProjectiles[i].GetComponent<Projectile>().velocity;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log(playerProjectiles[0]);
        if (pauseMenu && !pauseMenu.isPaused) {
            UpdateProjectileLoc();
            ActivateEnemiesInDummyRange();
        }
        
    }
}
