using System.Collections;
using System.Collections.Generic;

//Code Inspirations: The singleton used in Mission Demolition to 
//detect collsions 
public class SACounter {
    //Singleton for counting score and shots hit
    private static SACounter SAC;

    public static SACounter sacounter {
        get {
            if (SAC == null) SAC = new SACounter();
            return SAC;
        }
    }

    //Constructor
    private SACounter() {
        SAC = this;
    }

    //Score and Shots Fired and Shots Hit counters
    private int score = 0;
    private int shotsHit = 0;

    //Get current score
    public int GetScore {
        get {
            return score;
        }
    }

    //Shot enemies add to the current score
    public void AddToScore(int a) {
        score += a;
    }

    //Get shots hit
    public float GetShotsHit {
        get {
            return shotsHit;
        }
    }

    //Shot enemies add to the current hit count
    public void IncrementHits() {
        shotsHit++;
    }

    //Reset status of counters to 0
    public void ClearCount() {
        score = 0;
        shotsHit = 0;
    }
}
