using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable {
    //Spawnables must have a position
    Vector3 pos { get; set; }

    //Some spawnables can "choose" how they move depending on a modifier
    int moveMod { get; }

    //Spawnables could move
    void Move();

    //Spawnables must do something  when clicked
    void OnMouseDown();
}
