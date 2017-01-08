using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public Transform pause;
    public Slider volumeSlider;
    public AudioSource volumeAudio;
    public Image instimg;
    public Image highimg;
    public TextAsset highfile;
    public Text hightext;

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
        hightext.text = highfile.ToString();
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

    public void VolumeControl(float volumecontrol) {


        volumeAudio.volume = volumeSlider.value;
    }

    public void instructions() {



        if (instimg.gameObject.activeInHierarchy == false)
        {
            instimg.gameObject.SetActive(true);
            
        }
        



    }

    public void highscore() {

        if (highimg.gameObject.activeInHierarchy == false)
        {
            hightext.text = highfile.ToString();
            highimg.gameObject.SetActive(true);

        }
    }


    public void closeinst()
    {

        if (instimg.gameObject.activeInHierarchy == true)
        {
            instimg.gameObject.SetActive(false);

        }
        

    }

    public void closehigh() {

        if (highimg.gameObject.activeInHierarchy == true)
        {
            highimg.gameObject.SetActive(false);

        }
    }

}
