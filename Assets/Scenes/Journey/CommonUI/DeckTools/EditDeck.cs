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

    public void OnExecute() {
        choose.Exit();
    }

    public IEnumerator Run() {
        Debug.Log("EditDeck.Run");
        yield return Misc.SetActiveUntil(
            uiCanvas.gameObject,
            choose.Run(player.deckCardList, player.poolCardList));
    }

}

}
