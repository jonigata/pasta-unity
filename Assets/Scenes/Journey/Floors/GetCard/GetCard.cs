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
    [Inject] Model.Player player;

    Data.Card[] cards;
    Data.Card chosen;

    void Init() {
        chosen = null;
        cards = new Data.Card[3];
        for (int i = 0 ; i < 3 ; i++) {
            cards[i] = cardFactory.Create();
        }
    }

    public override IEnumerator Run() {
        Init();

        gameObject.SetActive(true);
        yield return new WaitUntil(() => chosen != null);
        gameObject.SetActive(false);
        player.poolCardList.Add(chosen);

        Command.CommandHub.Post(new Command.GetCard(chosen));
    }

    public void OnClickChooseButton(int index) {
        chosen = cards[index];
    }
}

}

