using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public Transform pause;

    public void NewGame(string level) {

        SceneManager.LoadScene(level);
    }

    public void ExitGame() {

        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }
    }

    public void Pause() {

        if (pause.gameObject.activeInHierarchy == false)
        {
            pause.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pause.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

}
