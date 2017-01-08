using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class readHighscore : MonoBehaviour {

    public TextAsset highfile;
    public Text hightext;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        hightext.text = highfile.ToString();
	}
}
