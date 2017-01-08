using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class countdownScript : MonoBehaviour
{
    public int timeLeft = 5;
    public Text countdownText;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("LoseTime");
    }

    // Update is called once per frame
    void Update()
    {
        countdownText.text = ("CD: " + timeLeft);

        if (timeLeft <= 0)
        {
            
            countdownText.text = "Times Up!";
            
        }
    }

   

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            if (timeLeft == 0) {
          
                yield return new WaitForSeconds(1);
                timeLeft = 10;
            }
        }
    }
    
}