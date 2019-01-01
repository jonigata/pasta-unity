using UnityEngine;
using UniRx;
using Zenject;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Command {

public class EditDeck : Command {
    public List<Data.Card> cards;
    public EditDeck(IEnumerable<Data.Card> cards) {
        this.cards = new List<Data.Card>(cards);
    }
}

public class EditDeckProcessor : MonoBehaviour {
    [Inject] Model.Player player;
    
    void Start() {
        CommandHub.Subscribe<Compose>(
            x => {
                Debug.Log("EditDeck Command Received");
            });
        
   }
}

}
