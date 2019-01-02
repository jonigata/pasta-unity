using UnityEngine;
using Zenject;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Dev {

public class GetCard : MonoBehaviour {
    [SerializeField] Journey.Floor.GetCard getCard;
    [Inject] Journey.Model.Player player;

    IEnumerator Start() {
        player.MockUp();
        yield return getCard.RunInstance();
    }
}

}
