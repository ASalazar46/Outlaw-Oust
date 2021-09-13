using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionScreen : MonoBehaviour {
    //Retreive the button
    public Button toTitleBtn;

    //Give the buttons functionality
    void Start() {
        Button ttb = toTitleBtn.GetComponent<Button>();

        ttb.onClick.AddListener(OnTTB);
    }

    //Return to the title screen
    void OnTTB() {
        SceneManager.LoadScene("TitleScene");
    }

}
