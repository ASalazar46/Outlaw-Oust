using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code Inspiration: The Enemy script from Space SCHMUP
public class Allonso : MonoBehaviour, ISpawnable {
    //Set the speed of this enemy
    public float speed;

    //Give boundaries to this enemy
    private EdgeCheck edgChk;

    //Get and set position, required by the ISpawnable interface
    public Vector3 pos {
        get {
            return (this.transform.position);
        }
        set {
            this.transform.position = value;
        }
    }

    //Acquire a movement modifier, required by the ISpawnable interface
    private int a = 999;
    public int moveMod {
        get {
            if (this.a == 999) {
                this.a = Random.Range(0,4);
            }
            return this.a;
        }
    }

    //This enemy's movement method, required by the ISpawnable interface
    //Based on the movement modifier, choose how to move
    public void Move() {
        Vector3 temp = pos;
        switch(this.moveMod) {
            case 0: 
                temp.y -= speed * Time.deltaTime;
                break;
            case 1:
                temp.y += speed * Time.deltaTime;
                break;
            case 2:
                temp.x -= speed * Time.deltaTime;
                break;
            case 3:
                temp.x += speed * Time.deltaTime;
                break;
            default:
                break;
        }
        pos = temp;
    }

    //Setup the enemy
    void Awake() {
        edgChk = GetComponent<EdgeCheck>();
    }

    void Update() {
        //Move enemy and check for bounds
        Move();
        if (edgChk != null && edgChk.isOffScreen) {
            Destroy (gameObject);
        }
    }

    //Give the player points, play a hit noise, delete object
    //and update shots hit
    public void OnMouseDown() {
        SACounter.sacounter.AddToScore(200);
        SACounter.sacounter.IncrementHits();
        SoundManager.GetSM.PlayHitNoise();
        Destroy (gameObject);
    }
}
