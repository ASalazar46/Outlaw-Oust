using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartScreen : MonoBehaviour {
    //Retrieve the buttons and text containers
    public Button toTitleButton;
    public Button toGameButton;
    public Text currScore;
    public Text bestScore;
    public Text newScore;

    //Give the buttons functionality and modify text
    //Also modify the high score if it was exceeded
    void Start() {
        Button ttb = toTitleButton.GetComponent<Button>();
        Button tgb = toGameButton.GetComponent<Button>();
        Text cs = currScore.GetComponent<Text>();
        Text bs = bestScore.GetComponent<Text>();
        Text ns = newScore.GetComponent<Text>();

        ns.gameObject.SetActive(false);

        ttb.onClick.AddListener(OnTTB);
        tgb.onClick.AddListener(OnTGB);
        
        cs.text = "" + SACounter.sacounter.GetScore + 
            " / " + OutlawOust.acc + "%";
        if ( !PlayerPrefs.HasKey("Best_Score") 
        && !PlayerPrefs.HasKey("Best_Accuracy") ) {
            PlayerPrefs.SetInt("Best_Score", 0);
            PlayerPrefs.SetFloat("Best_Accuracy", 0f);
        }
        if (SACounter.sacounter.GetScore > PlayerPrefs.GetInt("Best_Score")
        && OutlawOust.acc > PlayerPrefs.GetFloat("Best_Accuracy") ) {
            PlayerPrefs.SetInt("Best_Score", SACounter.sacounter.GetScore);
            PlayerPrefs.SetFloat("Best_Accuracy", OutlawOust.acc);
            ns.gameObject.SetActive(true);
        }
        bs.text = "" + PlayerPrefs.GetInt("Best_Score")
            + " / " + PlayerPrefs.GetFloat("Best_Accuracy") + "%";
        
    }

    //Return to the title screen
    void OnTTB() {
        SACounter.sacounter.ClearCount();
        SceneManager.LoadScene("TitleScene");
    }


    //Return to the game screen
    void OnTGB() {
        SACounter.sacounter.ClearCount();
        SceneManager.LoadScene("PlayScene");
    }
}
