using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public bool isPaused = false;
    public GameObject pauseMenuUI;
	
    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Awake() {
        Resume();
    }

    private void Start() {
        pauseMenuUI.SetActive(isPaused);
        GameManager.instance.SetPause(this);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
