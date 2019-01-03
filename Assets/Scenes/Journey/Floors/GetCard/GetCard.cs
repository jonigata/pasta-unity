using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using Zenject;

namespace Journey.Floor {

public class GetCard : AbstractFloor {
    [SerializeField] Model.CardFactory cardFactory;
    [SerializeField] Canvas uiCanvas;
    [Inject] Model.Player player;
    [Inject] UI.EditDeck editDeck;

    Data.Card[] cards;
    Data.Card chosen;

    void Init() {
        chosen = null;
        cards = new Data.Card[3];
        for (int i = 0 ; i < 3 ; i++) {
            cards[i] = cardFactory.Create();
        }
    }

    public override IEnumerator RunInstance() {
        Init();
        yield return ChooseCard();
        yield return EditDeck();
    }

    IEnumerator ChooseCard() {
        uiCanvas.gameObject.SetActive(true);
        yield return new WaitUntil(() => chosen != null);
        uiCanvas.gameObject.SetActive(false);

        Command.CommandHub.Post(new Command.GetCard(chosen));
    }

    IEnumerator EditDeck() {
        yield return editDeck.Run();
    }

    public void OnClickChooseButton(int index) {
        chosen = cards[index];
    }
}

}

