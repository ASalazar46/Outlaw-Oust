using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
    //Retrieve the buttons and text containers
    public Button toGameBtn;
    public Button toInstructionsBtn;
    public Text hiScore;

    private static AudioSource musicInstance = null;

    //Give the buttons functionality
    //and display high score
    void Awake(){  
        if (musicInstance == null) { 
            musicInstance = SoundManager.GetSM.PlayMusic();
            DontDestroyOnLoad(musicInstance);
        }

        Button tgb = toGameBtn.GetComponent<Button>();
        Button tib = toInstructionsBtn.GetComponent<Button>();
        Text hs = hiScore.GetComponent<Text>();

        tgb.onClick.AddListener(OnTGB);
        tib.onClick.AddListener(OnTIB);
        if ( PlayerPrefs.HasKey("Best_Score") && 
            PlayerPrefs.HasKey("Best_Accuracy") ) {
            hs.text = "Best Bounty/Accuracy:\n " + 
                PlayerPrefs.GetInt("Best_Score") + 
                " / " + PlayerPrefs.GetFloat("Best_Accuracy") + "%";
        }
        else {
            hs.text = "No Records Yet :)";
        }
    }

    //Start the game
    void OnTGB() {
        SceneManager.LoadScene("PlayScene");
    }

    //Read the instructions
    void OnTIB() {
        SceneManager.LoadScene("InstructionScene");
    }
}
