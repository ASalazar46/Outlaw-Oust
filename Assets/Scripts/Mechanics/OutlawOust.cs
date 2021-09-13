using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Code Inspiration: The Main script from Space SCHMUP, 
//and the MissionDemolition script from Mission Demolition
public class OutlawOust : MonoBehaviour {
    //Non-instantiable singleton for the game handler
    static public OutlawOust outlawOust;

    [Header("Set in Inspector")]
    //Enemies prefabs to be randomly chosen
    public GameObject[] prefabsEnemies;
    public GameObject timePwUpPrefab;

    //Text Containers to modify
    public Text uiScore;
    public Text uiAccuracy;
    public Text uiTimer;
    public Text uiMessage;
    public Text uiHiScore;

    //Retrieve EdgeCheck for edge checking
    private EdgeCheck edgChk;

    //Counters for the number of shots fired and accuracy
    [HideInInspector] 
    private float shotsFired = 0;
    public static float acc;

    //Retrieve the timer
    private Timer timer;

    //Prepare the game for play
    void Start() {
        outlawOust = this;
        edgChk = GetComponent<EdgeCheck>();
        timer = GetComponent<Timer>();

        uiMessage.gameObject.SetActive(false);
        UpdateUI();
        Invoke("SpawnEnemy", 0.2f);
        Invoke("SpawnTime", 15f);
        timer.StartTimer(60f);
    }

    void Update() {
        //When the game is running...
        if (timer.isRunning) {
            //... increment shots when left mouse button is clicked
            if (Input.GetMouseButtonDown(0)) {
                shotsFired++;
                SoundManager.GetSM.PlayGunshot();
            }

            //... and update the text on screen until time is up
            UpdateUI();
        }

        //End the game and switch to results screen
        if (!timer.isRunning) {
            uiMessage.gameObject.SetActive(true);
            Invoke("ToRestart", 3f);
        }
        
    }

    //Load the restart screen
    void ToRestart() {
        SceneManager.LoadScene("RestartScene");
    }

    //Spawn enemies at a random location on the screen
    public void SpawnEnemy() {
        //Choose an enemy to spawn
        int ndx = Random.Range(0, prefabsEnemies.Length);
        
        //Set the domain and range of the spawn point
        Vector3 pos = Vector3.zero;
        float xMin = -edgChk.camWidth+(edgChk.camWidth/2f);
        float xMax = edgChk.camWidth-(edgChk.camWidth/2f);
        float yMin = -edgChk.camHeight+(edgChk.camHeight/2f);
        float yMax = edgChk.camHeight-(edgChk.camHeight/2f);
        
        //Choose a random point within the domain and range
        pos.x = Random.Range(xMin, xMax);
        pos.y = Random.Range(yMin, yMax);
        pos.z = 0;

        //Spawn the enemy at the generated position
        GameObject go = Instantiate<GameObject>(prefabsEnemies[ndx]);
        go.transform.position = pos;
        
        //Spawn another enemy 
        Invoke("SpawnEnemy", 0.6f);
    }

    //Spawn time powerup at a random location
    public void SpawnTime() {
        //Set the domain and range of the spawn point
        Vector3 pos = Vector3.zero;
        float xMin = -edgChk.camWidth+(edgChk.camWidth/2f);
        float xMax = edgChk.camWidth-(edgChk.camWidth/2f);
        float yMin = -edgChk.camHeight+(edgChk.camHeight/2f);
        float yMax = edgChk.camHeight-(edgChk.camHeight/2f);
        
        //Choose a random point within the domain and range
        pos.x = Random.Range(xMin, xMax);
        pos.y = Random.Range(yMin, yMax);
        pos.z = 0;

        //Spawn the enemy at the generated position
        GameObject go = Instantiate<GameObject>(timePwUpPrefab);
        go.transform.position = pos;
        
        //Spawn another enemy 
        Invoke("SpawnTime", 15.0f);
    }

    //Update the score, accuracy, timer, and hiscores
    public void UpdateUI() {
        uiScore.text = "Bounty: " + SACounter.sacounter.GetScore;

        if (shotsFired == 0) {
            acc = 0;
        }
        else {
            acc = (SACounter.sacounter.GetShotsHit/shotsFired)*100;
        }
        uiAccuracy.text = "Accuracy: " + acc + "%";
        
        uiTimer.text = "" + timer.GetCountdown;

        if ( PlayerPrefs.HasKey("Best_Score") && 
            PlayerPrefs.HasKey("Best_Accuracy") ) {
            uiHiScore.text = "Best Bounty/Accuracy:\n " + 
                            PlayerPrefs.GetInt("Best_Score") + 
                            " / " + PlayerPrefs.GetFloat("Best_Accuracy")
                            + "%";
        }
        else {
            uiHiScore.text = "No Records Yet :)";
        }
    }
}
