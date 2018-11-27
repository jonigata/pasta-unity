using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Journey {

public class Root : MonoBehaviour {
    [SerializeField] Model.Player player;
    [SerializeField] Floor.GetCard getCard;

    void Start () {
        StartCoroutine(MainLoop());
    }
	
    void Update () {
		
    }

    IEnumerator MainLoop() {
        while (true) {
            yield return PlayDay();
        }
        
    }

    IEnumerator PlayDay() {
        Journey.Floor.AbstractFloor floor = ChooseFloor();
        yield return floor.Run();
    }
    
    Journey.Floor.AbstractFloor ChooseFloor() {
        return getCard;
    }
}

}
