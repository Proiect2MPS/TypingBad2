﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerScript : MonoBehaviour {

    public Text doneText;
    public Text timerText;
    private float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;

		
	}
	
	// Update is called once per frame
	void Update () {
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f0");

        timerText.text = "Time: " + minutes + ":" + seconds;

        if (Equals(seconds, "59")) {
            Time.timeScale = 0;
            
            doneText.text = "GAME OVER";
        }
    
    }
}
