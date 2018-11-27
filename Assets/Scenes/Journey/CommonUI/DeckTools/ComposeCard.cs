using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.UI {

public class ComposeCard : MonoBehaviour {
    [Inject] Model.Player player;
    [SerializeField] Model.CardList composeCardListModel;
    [SerializeField] MultipleCardChoose choose;
    [SerializeField] Button executeButton;
    
    void Start() {
        Debug.Log(player.poolCardList.Count);
        choose.SetUp(composeCardListModel, player.poolCardList);

        Observable.CombineLatest(
            composeCardListModel.Cards.ObserveCountChanged(true),
            player.Gold, (a,b) => Tuple.Create(a, b))
            .Select(x => x.Item1 == 2 && GetCost() <= x.Item2)
            .SubscribeToInteractable(executeButton);
    }

    public void OnExecute() {
        Command.CommandHub.subject.OnNext(
            new Command.Compose(
                composeCardListModel.Nth(0), 
                composeCardListModel.Nth(1)));
        composeCardListModel.Clear();
    }

    int GetCost() {
        return 100;
    }
}

}
