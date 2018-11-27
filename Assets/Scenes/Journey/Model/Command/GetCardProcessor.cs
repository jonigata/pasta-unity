using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Command {

public class GetCardProcessor : MonoBehaviour {
    [SerializeField] Model.CardList cardList;
    
    void Start() {
        CommandHub.Subscribe<GetCard>(
            x => {
                Debug.Log("GetCard Command Received");
                cardList.Add(x.card);
            });
   }
}

}
