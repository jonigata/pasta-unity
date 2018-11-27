using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Command {

public class GetCardProcessor : MonoBehaviour {
    [SerializeField] Model.CardList cardList;
    
    void Start() {
        CommandHub.subject
            .OfType<Command, GetCard>()
            .Subscribe(
                x => {
                    Debug.Log("GetCard Command Received");
                    cardList.Add(x.card);
                });
        
   }
}

}
