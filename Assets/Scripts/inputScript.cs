using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class inputScript : MonoBehaviour {
	public TextAsset inputFile;
    public TextAsset inputFile2;
    public TextAsset inputFile3;
    public TextAsset inputFile4;
    public Text username;
    public Text highscoreUI;

    private int highscore = 0;

    public GameObject prefab;
	public InputField inputField;
    public Text score;

    private int level = 1;
    private int incsc = 5;
	private string[] words;
	private int i = -1;
	private GameObject[] wordsToPrint;

    public Text levelText;
    public Button gameoverbutton;
    public int timeLeft = 10;
    public Text countdownText;
    public Text livesleft;
    public Text gameover;
    private int lives = 3;
    private int points = 0;

    // Use this for initialization
    void Start () {

        highscore = PlayerPrefs.GetInt("score");
        highscoreUI.text = "Highscore: "+highscore.ToString();

        username.text = PlayerPrefs.GetString("user");
        gameoverbutton.gameObject.SetActive(false);

        levelText.text = "Level: 1";
        livesleft.text = "Lives: 3";
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
        if (timeLeft <= 0)
        {if (lives < 0) livesleft.text = "Lives: 2";
        if (lives < -5) livesleft.text = "Lives: 1";

            countdownText.text = "Times Up!";
            lives--;

            i = Random.Range(0, words.Length);
            inputField.GetComponent<InputField>().text = "";
            // se printeaza noul cuvant
            printWords(i,level);
            Debug.Log("vieti:"+lives);

            if (lives <= -12) {
                livesleft.text = "Lives: 0";

            
                //Time.timeScale = 0;
                Debug.Log("GAME OVER");
                // TODO GAME OVER.
                StopCoroutine("LoseTime");
                for (int i = 0; i < wordsToPrint.Length; i++) Destroy(wordsToPrint[i]); //sterg cuvintele rosii.
                gameover.text = "GAME OVER";
                Debug.Log("scor final: " + points);
                gameoverbutton.gameObject.SetActive(true);
                // System.IO.File.AppendAllText("C:/Users/moglan/Desktop/tg/Assets/highscore.txt", "\r\n"+points);



            }
        }

        else {
            // daca s-a gasit potrivire intre ce este in input text si cuvantul afisat se genereaza un nou cuvant
            if (inputField.GetComponent<InputField>().text.Equals(words[i].TrimEnd(new char[] { '\r', '\n' })))
            {
                points = points + incsc;
                score.text = "Score: " + points.ToString();

                if (points >= 50) {
                    levelText.text = "Level: 2";
                    level = 2;
                    incsc = 10;
                    words = inputFile2.ToString().Split('\n');
                   

                }
                if (points >= 150)
                {
                    levelText.text = "Level: 3";
                    level = 3;
                    incsc = 20;
                    words = inputFile3.ToString().Split('\n');

                }

                if (points >= 500)
                {
                    levelText.text = "Level: 4";
                    level = 4;
                    incsc = 50;
                    words = inputFile4.ToString().Split('\n');

                }





                // TODO DA PUNCTE
                i = Random.Range(0, words.Length);
                inputField.GetComponent<InputField>().text = "";
                // se printeaza noul cuvant
                printWords(i,level);
                timeLeft = 10;
            }
            else
            {
                // aplic un efect pentru fiecare cuvant
                for (int i = 0; i < wordsToPrint.Length; i++)
                {
                    if (level == 1) {
                        wordsToPrint[i].transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 110 * (i + 1));
                    }
                    if(level == 2) { 
                       
                        wordsToPrint[i].transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 110 * (i + 1));
                    }
                    if (level == 3) {
                        wordsToPrint[i].transform.Rotate(new Vector3(1, 0, 1) * Time.deltaTime * 100 * (i + 1));
                    }
                    if (level == 4) {
                        wordsToPrint[i].transform.Rotate(new Vector3(1, 1, 1) * Time.deltaTime * 300 * (i + 1));
                    }
                }
            }
        }
        highscoreUI.text = "Highscore: " + highscore.ToString();
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
    void printWords(int index, int level)
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
            if (level == 2)
            {
                wordsToPrint[i].GetComponent<Text>().color = Color.magenta;

            }
            if (level == 3)
            {
                wordsToPrint[i].GetComponent<Text>().color = Color.yellow;

            }
            if (level == 4)
            {
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

        // System.IO.File.AppendAllText("C:/Users/moglan/Desktop/tg/Assets/highscore.txt", points+"\r\n");
    }
}
