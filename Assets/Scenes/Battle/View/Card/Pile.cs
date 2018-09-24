using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace View {

public class Pile : MonoBehaviour {
    [NonSerialized] public Card selected;
    [SerializeField] ExclusiveGroup group;

    Model.Pile model;

    public void SetUp(Model.Pile model) {
        this.model = model;

        model.OnRestructure.Subscribe(u => Restructure()).AddTo(gameObject);
    }

    public void Select(Card card) {
        selected = card;
        Debug.Log($"Select card: {card}");
        if (card == null) { return; }

        group.Select(card.GetComponent<ExclusiveGroupItem>());
    }

    void Restructure() {
        int i = 0 ;
        foreach (Card card in GetComponentsInChildren<Card>()) {
            card.model = model.Cards[i++];
        }
        
    }
}

}
