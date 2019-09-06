using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float attackDamage;
    public Vector3 velocity;
    public string targetTag;
    private const float LIFE_TIME = 5f;
    private float lifeTime = 5f;

    

    private void OnEnable() {
        //Invoke("Disable", 5f);
        lifeTime = 5f;
    }

    private void Disable() {

        if (gameObject.activeInHierarchy) {
            Debug.Log("Projectile Lived too Long" + transform.position + "    "+ lifeTime);
            gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy" && targetTag == "Enemy") {
            Debug.Log("Hit Enemy" + transform.position);
            collision.gameObject.GetComponent<BaseEnemy>().DamageEnemy(attackDamage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Hench" && targetTag == "Enemy") {
            Debug.Log("Hit Enemy" + transform.position);
            collision.gameObject.GetComponent<Henchman>().DamageEnemy(attackDamage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Player" && targetTag == "Player") {
            Debug.Log("Hit Player" + transform.position);
            GameObject.Find("Main Camera").GetComponent<ScreenShake>().ShakeCamera();
            collision.gameObject.GetComponent<Player>().DamagePlayer(attackDamage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Razer" && targetTag == "Enemy") {
            Debug.Log("Hit Enemy" + transform.position);
            collision.gameObject.GetComponent<Razer>().DamageEnemy(attackDamage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Shield" || 
            ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Hench" || collision.gameObject.tag == "Razer") && targetTag == "Enemy")) {
            Debug.Log("Hit Shield" + transform.position);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy" && targetTag == "Enemy") {
            Debug.Log("Hit Enemy" + transform.position);
            collision.gameObject.GetComponent<BaseEnemy>().DamageEnemy(attackDamage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Hench" && targetTag == "Enemy") {
            Debug.Log("Hit Enemy" + transform.position);
            collision.gameObject.GetComponent<Henchman>().DamageEnemy(attackDamage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Player" && targetTag == "Player") {
            Debug.Log("Hit Player" + transform.position);
            GameManager.instance.player.GetComponent<ScreenShake>().ShakeCamera();
            collision.gameObject.GetComponent<Player>().DamagePlayer(attackDamage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Razer" && targetTag == "Enemy") {
            Debug.Log("Hit Enemy" + transform.position);
            collision.gameObject.GetComponent<Razer>().DamageEnemy(attackDamage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Shield" ||
            ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Hench" || collision.gameObject.tag == "Razer") && targetTag == "Player")) {
            Debug.Log("Hit Shield" + transform.position);
            gameObject.SetActive(false);
        }

    }

    // Use this for initialization
    void Start() {
        if(targetTag == "Player") {
            GameManager.instance.SetEnemyProjectile(this);
        }
        else {
            GameManager.instance.SetPlayerProjectile(this);
        }
    }
   

    // Update is called once per frame
    void Update () {

        if (lifeTime <= 0f) {
            Disable();
            lifeTime = LIFE_TIME;
        }
        else {
            lifeTime -= Time.deltaTime;
        }
    }
}
