using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Model {

public class CardList : MonoBehaviour {
    public string comment;

    ReactiveCollection<Data.Card> cards = new ReactiveCollection<Data.Card>();

    public ReactiveCollection<Data.Card> Cards {
        get { return cards; }
    }

    public void SetUp(List<Data.Card> cards) {
        foreach (var card in cards) {
            this.cards.Add(card);
        }
    }

    public void Add(Data.Card card) {
        cards.Add(card);
    }

    public void Remove(Data.Card card) {
        cards.Remove(card);
    }

    public void Clear() {
        cards.Clear();
    }

    public int Count { get { return cards.Count; } }

    public Data.Card Nth(int n) {
        return cards[n];
    }

    public void Compose(Data.Card x, Data.Card y) {
        x.Compose(y);
        cards.Remove(x);
        cards.Remove(y);
        cards.Add(x);
    }
}

}