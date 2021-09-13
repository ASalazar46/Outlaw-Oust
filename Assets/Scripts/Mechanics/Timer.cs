using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Code inspiration: Timer script from
//gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds
//By: In Unity By John, February 25, 2020
public class Timer : MonoBehaviour {
    //Duration of countdown
    private static float countdown;

    //Property to retrieve the current countdown
    public float GetCountdown {
        get {
            return countdown;
        }
    }

    [HideInInspector]
    //Flag to check for running timer
    public bool isRunning = false;

    //Start the timer
    public void StartTimer(float duration) {
        countdown = duration;
        isRunning = true;
    }

    //Add 3 seconds to the countdown when time powerup is hit
    public static void AddTime() {
        countdown += 8f;
    }

    //Update the timer
    void Update() {
        if (isRunning) {
            if (countdown > 0) {
                countdown -= Time.deltaTime;
            }
            else {
                countdown = 0;
                isRunning = false;
                //do stuff in other classes once time is up
            }
        }
    }
}
