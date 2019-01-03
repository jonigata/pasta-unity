using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.UI {

public class MultipleCardChoose : MonoBehaviour {
    [SerializeField] CardList dishCardList;
    [SerializeField] CardList poolCardList;
    [SerializeField] int minCount;
    [SerializeField] int maxCount;
    [SerializeField] bool hideChosenCard;
    [SerializeField] Button okButton;
    
    public bool aborted;
    public bool done;

    public IEnumerator Run(
        Model.CardList dishCardListModel,
        Model.CardList poolCardListModel) {

        Debug.Log("MultipleCardChoose.SetUp");
        done = false;

        dishCardList.SetUp(dishCardListModel, card => true, card => true);
        poolCardList.SetUp(
            poolCardListModel,
            card => true,
            card => dishCardListModel.IndexOf(card) < 0);

        var a1 = poolCardList.OnSelect.Subscribe(
            card => {
                if (dishCardListModel.Count < maxCount) {
                    dishCardListModel.Add(card);
                    poolCardList.Hide(card);
                }
            });
        var a2 = dishCardList.OnSelect.Subscribe(
            card => {
                if (minCount < dishCardListModel.Count) {
                    dishCardListModel.Remove(card);
                    poolCardList.Show(card);
                }
            }).AddTo(gameObject);
        var a3 = dishCardListModel.Cards.ObserveCountChanged(true)
            .Select(x => x == maxCount || x == poolCardListModel.Count)
            .SubscribeToInteractable(okButton);

        yield return new WaitUntil(() => done);

        a1.Dispose();
        a2.Dispose();
        a3.Dispose();

        dishCardList.TearDown();
        poolCardList.TearDown();
    }

    public void Exit() {
        aborted = false;
        done = true;
    }

    public void Abort() {
        aborted = true;
        done = true;
    }

}

}
