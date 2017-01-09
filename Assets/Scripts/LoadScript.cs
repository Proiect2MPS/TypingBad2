using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour {

    public void NewGame(string level)
    {
        SceneManager.LoadScene(level);
    }
}
