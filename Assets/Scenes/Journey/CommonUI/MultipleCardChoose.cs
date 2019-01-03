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
    
    public void SetUp(
        Model.CardList dishCardListModel,
        Model.CardList poolCardListModel) {
        dishCardList.SetUp(dishCardListModel, card => true);
        poolCardList.SetUp(poolCardListModel, card => true);

        poolCardList.OnSelect.Subscribe(
            card => {
                if (dishCardListModel.Count < maxCount) {
                    dishCardListModel.Add(card);
                    poolCardList.Hide(card);
                }
            }).AddTo(gameObject);
        dishCardList.OnSelect.Subscribe(
            card => {
                if (minCount < dishCardListModel.Count) {
                    dishCardListModel.Remove(card);
                    poolCardList.Show(card);
                }
            }).AddTo(gameObject);
        dishCardListModel.Cards.ObserveCountChanged(true)
            .Select(x => x == maxCount || x == poolCardListModel.Count)
            .SubscribeToInteractable(okButton);
    }

    void UpdateUI() {
        
    }
}

}
