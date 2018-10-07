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

    Subject<Card> selectSubject = new Subject<Card>();
    public IObservable<Card> OnSelect { get { return selectSubject; } }

    public void SetUp(Model.Pile model) {
        this.model = model;

        model.OnRestructure.Subscribe(u => Restructure()).AddTo(gameObject);
    }

    public void Select(Card card) {
        selected = card;
        if (card != null) {
            Debug.Log($"Select card: {card.model.Klass}");
        } else {
            Debug.Log($"Unselect card");
        }

        group.Select(card?.GetComponent<ExclusiveGroupItem>());
        selectSubject.OnNext(card);
    }

    void Restructure() {
        Debug.Log("Restructure");
        int i = 0 ;
        foreach (Card card in GetComponentsInChildren<Card>()) {
            card.SetModel(model.Cards[i++]);
        }
        Select(null);
    }
}

}
