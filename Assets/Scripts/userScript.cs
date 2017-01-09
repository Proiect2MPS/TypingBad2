using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class userScript : MonoBehaviour {

    private string nume;

    public void charaterfiled(string inputFiledString) {
        nume = inputFiledString;
        Debug.Log("ai bagat asta:" + nume);
        PlayerPrefs.SetString("user", nume);
    }
}
