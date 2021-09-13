using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code Inspiration: The BoundsCheck script from Space SCHMUP
public class EdgeCheck : MonoBehaviour {
    [Header("Set in Inspector")]
    public float radius = 0f;

    [Header("Set Dynamically")]
    public float camWidth;
    public float camHeight;

    [HideInInspector]
    public bool isOffScreen;
    
    //Set the camera boundaries
    void Awake() {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    //Check if the object's position is offscreen
    void LateUpdate() {
        Vector3 pos = transform.position;
        
        isOffScreen = false;

        if (pos.x > camWidth - radius) {
            pos.x = camWidth - radius;
            isOffScreen = true;
        }
        if (pos.x < -camWidth + radius) {
            pos.x = -camWidth + radius;
            isOffScreen = true;
        }
        if (pos.y > camHeight - radius) {
            pos.y = camHeight - radius;
            isOffScreen = true;
        }
        if (pos.y < -camHeight + radius) {
            pos.y = -camHeight + radius;
            isOffScreen = true;
        }
    }
}
