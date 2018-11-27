using UnityEngine;
using UniRx;
using Zenject;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Command {

public class ComposeProcessor : MonoBehaviour {
    [Inject] Model.Player player;
    
    void Start() {
        CommandHub.subject
            .OfType<Command, Compose>()
            .Subscribe(
                x => {
                    Debug.Log("Compose Command Received");
                    player.ComposeCard(x.left, x.right);
                });
        
   }
}

}