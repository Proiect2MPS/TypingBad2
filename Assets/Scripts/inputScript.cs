using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class inputScript : MonoBehaviour {

    public TextAsset inputFile, inputFile2, inputFile3, inputFile4;

    public Text username, highscoreUI, score, levelText, countdownText, livesleft, gameover;

    public GameObject prefab;
    public InputField inputField;
    public Button gameoverbutton;
    public int timeLeft = 10;

    private GameObject[] wordsToPrint;
    private string[] words;

    private int highscore = 0, level = 1, incsc = 5, i = -1, lives = 3, points = 0;

    // Use this for initialization
    void Start () {

        highscore = PlayerPrefs.GetInt("score");
        username.text = PlayerPrefs.GetString("user");
        highscoreUI.text = "Highscore: " + highscore.ToString();
        levelText.text = "Level: 1";
        livesleft.text = "Lives: 3";   
        gameoverbutton.gameObject.SetActive(false);

        // afisez primul cuvant 
        char[] delims = {'\n'};
		words = inputFile.ToString ().Split(delims);
        i = Random.Range(0, words.Length);
        inputField.GetComponent<InputField>().text = "";
        printWords(i,1);
        StartCoroutine("LoseTime");
    }
	
	// Update is called once per frame
	void Update () {

        countdownText.text = ("Countdown: " + timeLeft);

        if (timeLeft <= 0){
            if (lives < 0)
                livesleft.text = "Lives: 2";
            if (lives < -5)
                livesleft.text = "Lives: 1";

            countdownText.text = "Times Up!";
            lives--;

            // se printeaza noul cuvant
            i = Random.Range(0, words.Length);
            inputField.GetComponent<InputField>().text = "";
            printWords(i,level);

            // GAME OVER
            if (lives <= -12){
                livesleft.text = "Lives: 0";
                StopCoroutine("LoseTime");
                for (int i = 0; i < wordsToPrint.Length; i++)
                    Destroy(wordsToPrint[i]); //sterg cuvintele.
                gameover.text = "GAME OVER";
                gameoverbutton.gameObject.SetActive(true);
            }
        }
        // daca mai e timp
        else {
            // daca s-a gasit potrivire intre ce este in input text si cuvantul afisat se genereaza un nou cuvant
            if (inputField.GetComponent<InputField>().text.Equals(words[i].TrimEnd(new char[] { '\r', '\n' }))){
                points = points + incsc;
                score.text = "Score: " + points.ToString();

                if (points >= 50) {
                    levelText.text = "Level: 2";
                    level = 2;
                    incsc = 10;
                    words = inputFile2.ToString().Split('\n');
                }
                if (points >= 150){
                    levelText.text = "Level: 3";
                    level = 3;
                    incsc = 20;
                    words = inputFile3.ToString().Split('\n');
                }
                if (points >= 500) {
                    levelText.text = "Level: 4";
                    level = 4;
                    incsc = 50;
                    words = inputFile4.ToString().Split('\n');
                }

                // se printeaza noul cuvant
                i = Random.Range(0, words.Length);
                inputField.GetComponent<InputField>().text = "";
                printWords(i,level);
                timeLeft = 10;
            }
            else{
                // aplic un efect pentru fiecare cuvant
                for (int i = 0; i < wordsToPrint.Length; i++){
                    if (level == 1){
                        wordsToPrint[i].transform.Rotate(new Vector3(1, 0, 1) * Time.deltaTime * 100 * (i + 1));
                        //wordsToPrint[i].transform.RotateAround(Vector3.zero, Vector3.up, 210 * Time.deltaTime);
                        //wordsToPrint[i].transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 500 * (i + 1));
                        //wordsToPrint[i].transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * 500 * (i + 1));
                    }
                    if (level == 2) {
                        wordsToPrint[i].transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 110 * (i + 1));
                        wordsToPrint[i].transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 500 * (i + 1));
                    }
                    if (level == 3) {
                        wordsToPrint[i].transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 200 * (i + 1));
                        wordsToPrint[i].transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 200 * (i + 1));
                    }
                    if (level == 4) {
                        wordsToPrint[i].transform.Rotate(new Vector3(1, 1, 1) * Time.deltaTime * 600 * (i + 1));
                        wordsToPrint[i].transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * 300 * (i + 1));
                    }
                }
            }
        }
        highscoreUI.text = "Highscore: " + highscore.ToString();
    }

    IEnumerator LoseTime(){
        while (true){
            yield return new WaitForSeconds(1);
            timeLeft--;
            if (timeLeft == 0){
                yield return new WaitForSeconds(0.1f);
                timeLeft = 10;
            }
        }
    }

    void printWords(int index, int level){
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
            if (level == 2) {
                wordsToPrint[i].GetComponent<Text>().color = Color.magenta;}
            if (level == 3){
                wordsToPrint[i].GetComponent<Text>().color = Color.yellow;}
            if (level == 4){
                wordsToPrint[i].GetComponent<Text>().color = Color.red;
            }
            wordsToPrint [i].GetComponent<Text> ().text = str[i];
		}
	}

    public void gameoverfunction(string level) {

        SceneManager.LoadScene(level);
        if (points > highscore) {
            highscore = points;
        }
        PlayerPrefs.SetInt("score", highscore);
        PlayerPrefs.SetString("best", username.text);
    }
}
