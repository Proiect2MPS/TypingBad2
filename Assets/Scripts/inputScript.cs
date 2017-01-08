using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputScript : MonoBehaviour {
	public TextAsset inputFile;
	public GameObject prefab;
	public InputField inputField;

	private string[] words;
	private int i = -1;
	private GameObject[] wordsToPrint;


    public int timeLeft = 5;
    public Text countdownText;
    public Text livesleft;
    public Text gameover;
    private int lives = 3;

    // Use this for initialization
    void Start () {
        livesleft.text = "Lives: 3";
        // afisez primul cuvant 
        char[] delims = {'\n'};
		words = inputFile.ToString ().Split(delims);
        i = Random.Range(0, words.Length);
        inputField.GetComponent<InputField>().text = "";
        printWords(i);
        StartCoroutine("LoseTime");
    }
	
	// Update is called once per frame
	void Update () {

        countdownText.text = ("Countdown: " + timeLeft);
        if (timeLeft <= 0)
        {if (lives < 0) livesleft.text = "Lives: 2";
        if (lives < -5) livesleft.text = "Lives: 1";

            countdownText.text = "Times Up!";
            lives--;
           


            i = Random.Range(0, words.Length);
            inputField.GetComponent<InputField>().text = "";
            // se printeaza noul cuvant
            printWords(i);
            Debug.Log("vieti:"+lives);
            if (lives <= -12) {
                livesleft.text = "Lives: 0";
                //Time.timeScale = 0;
                Debug.Log("GAME OVER");
                // TODO GAME OVER.
                StopCoroutine("LoseTime");
                for (int i = 0; i < wordsToPrint.Length; i++) Destroy(wordsToPrint[i]); //sterg cuvintele rosii.
                gameover.text = "GAME OVER"; 
                
            }
        }

        else {

            // daca s-a gasit potrivire intre ce este in input text si cuvantul afisat se genereaza un nou cuvant
            if (inputField.GetComponent<InputField>().text.Equals(words[i].TrimEnd(new char[] { '\r', '\n' })))
            {
                // TODO DA PUNCTE
                i = Random.Range(0, words.Length);
                inputField.GetComponent<InputField>().text = "";
                // se printeaza noul cuvant
                printWords(i);
                timeLeft = 10;
            }
            else
            {
                // aplic un efect pentru fiecare cuvant
                for (int i = 0; i < wordsToPrint.Length; i++)
                {
                    //aici am aplicat un efect
                    wordsToPrint[i].transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * 100 * (i + 1));
                }
            }

        }

        
	}

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            if (timeLeft == 0)
            {
                yield return new WaitForSeconds(0.1f);
                timeLeft = 10;
            }
        }
    }
    void printWords(int index)
	{
		// daca avem deja afisat un cuvant atunci il stergem
		if (wordsToPrint != null) {
			for (int i = 0; i < wordsToPrint.Length; i++) {
				Destroy (wordsToPrint[i]);
			}
		}

		// se face split pentru noul rand din fisierul de intrare
		char[] delim = { ' ' };
		string[] str = words [index].TrimEnd (new char[] { '\r', '\n' }).Split(delim);
		wordsToPrint = new GameObject[str.Length];

		// se contruieste cate o clone pentru fiecare cuvant
		for (int i = 0; i < str.Length; i++) {
			GameObject g = Instantiate (prefab, transform) as GameObject;
			g.transform.SetParent (transform);
			g.GetComponent<LayoutElement> ().minWidth = 20 * str [i].Length;
			wordsToPrint [i] = g;
			wordsToPrint [i].GetComponent<Text> ().text = str[i];
		}
	}
}
