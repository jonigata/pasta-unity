using UnityEngine;
using UniRx;
using Zenject;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.UI {

public class EditDeck : MonoBehaviour {
    [SerializeField] MultipleCardChoose choose;
    [SerializeField] Canvas uiCanvas;
    [Inject] Model.Player player;

    bool done;
    
    public void OnExecute() {
        done = true; 
    }

    public IEnumerator Run() {
        Debug.Log("EditDeck.Run");
        done = false;
        choose.SetUp(player.deckCardList, player.poolCardList);
        yield return Misc.SetActiveUntil(uiCanvas.gameObject, () => done);
    }

}

}
