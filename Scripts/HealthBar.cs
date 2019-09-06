using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    

    public string[] animationName =
        {"Anim_H2",
        "Anim_H3",
        "Anim_H4",
        "Anim_H5",
        "Anim_H6",
        "Anim_H7",
        "Anim_H8",
        "Anim_H9",
        "Anim_H10",
        "Anim_H11",
        "Anim_H12",
        "Anim_H13",
        "Anim_H14",
        "Anim_H15"};
    //public Image img;


    // Use this for initialization
    void Start() {
        GameManager.instance.SetHealthBar(this);
        Vector3 pos = new Vector3(Screen.width * 3.5f / 15f, Screen.height * 9.1f / 10f, 0);
        //Vector3 pos = new Vector3(0, 0, 0);

        transform.position = Camera.main.ScreenToWorldPoint(pos);
    }

    private void Awake() {
        Vector3 pos = new Vector3(Screen.width * 3f / 15f, Screen.height * 9f/ 10f, 0);
        //Vector3 pos = new Vector3(0, 0, 0);

        transform.position = Camera.main.ScreenToWorldPoint(pos);
    }

    private void Update() {
        
        Debug.Log(transform.position);
    }

    public void UpdateHealthUI(float val) {
        
        gameObject.GetComponent<Animator>().Play(animationName[Mathf.Max((int)(val / 10f),0)]);
        //Debug.Log("PLAYING HEALTH");
    }
}
