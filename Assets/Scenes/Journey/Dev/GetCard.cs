using UnityEngine;
using Zenject;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Dev {

public class GetCard : MonoBehaviour {
    [Inject] Journey.Model.Player player;
    [SerializeField] Journey.Floor.GetCard getCard;

    void Awake() {
        player.MockUp();
    }

    void Start () {
        StartCoroutine(PlayOneDay());
    }
	
    IEnumerator PlayOneDay() {
        Journey.Floor.AbstractFloor floor = ChooseFloor();
        yield return floor.Run();
    }

    Journey.Floor.AbstractFloor ChooseFloor() {
        return getCard;
    }
}

}
