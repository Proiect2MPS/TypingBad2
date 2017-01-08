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

	// Use this for initialization
	void Start () {
		char[] delims = {'\n'};
		words = inputFile.ToString ().Split(delims);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (i < 0 || inputField.GetComponent<InputField> ().text.Equals (words [i].TrimEnd (new char[] { '\r', '\n' }))) {
			// daca nu am afisat un cuvant sau s-a gasit potrivire intre ce este in input text si cuvantul afisat se
			// genereaza un nou cuvant
			i = Random.Range (0, words.Length);
			inputField.GetComponent<InputField> ().text = "";
			// se printeaza noul cuvant
			printWords (i);
		} else {
			// aplic un efect pentru fiecare cuvant
			for (int i = 0; i < wordsToPrint.Length; i++) {
				//aici am aplicat un efect
				wordsToPrint [i].transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * 100 * (i + 1));
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
