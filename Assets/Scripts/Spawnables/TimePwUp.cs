using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code Inspiration: The Enemy script from Space SHMUP, but its a powerup
public class TimePwUp : MonoBehaviour, ISpawnable {
    //Declare a speed
    public float speed;

    //Give boundaries to the powerup
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

    //This powerup's movement method, required by the ISpawnable interface
    //Based on the movement modifier, choose how to move
    public void Move() {
        Vector3 temp = pos;
        switch(this.moveMod) {
            case 0: 
                temp.x -= speed * Time.deltaTime;
                temp.y -= speed * Time.deltaTime;
                break;
            case 1:
                temp.x += speed * Time.deltaTime;
                temp.y += speed * Time.deltaTime;
                break;
            case 2:
                temp.x -= speed * Time.deltaTime;
                temp.y += speed * Time.deltaTime;
                break;
            case 3:
                temp.x += speed * Time.deltaTime;
                temp.y -= speed * Time.deltaTime;
                break;
            default:
                break;
        }
        pos = temp;
    }

    //Setup the powerup
    void Awake() {
        edgChk = GetComponent<EdgeCheck>();
    }

    void Update() {
        //Move and delete powerup if offscreen
        Move();
        if (edgChk != null && edgChk.isOffScreen) {
            Destroy (gameObject);
        }
    }

    //Add to the timer, delete object,
    //and update shots hit
    public void OnMouseDown() {
        Timer.AddTime();
        SACounter.sacounter.IncrementHits();
        Destroy (gameObject);
    }
}
