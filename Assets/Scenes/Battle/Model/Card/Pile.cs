using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Model {

public class Pile : MonoBehaviour {
    [SerializeField] List<Card> cards = new List<Card>();

    public IReadOnlyList<Card> Cards {
        get { return cards; }
    }

    Subject<Card> invokeSubject = new Subject<Card>();
    public IObservable<Card> OnInvoke { get { return invokeSubject; } }

    const int handCount = 4;

    void MoveCardToLast(Card targetCard) {
        // 抜いた場所に入る
        if (cards.Count <= handCount) { return; }
        
        for (int i = 0 ; i < cards.Count ; i++) {
            if (handCount <= i) { break; }
            if (cards[i] == targetCard) {
                var insertCard = cards[handCount];
                cards.Remove(insertCard);
                cards[i] = insertCard;
                cards.Add(targetCard);
            }
        }
    }

    public void AddCard(Card card) {
        cards.Add(card);
    }

    public void SetUpWithHierarchy(bool shuffle) {
        cards = GetComponentsInChildren<Card>().ToList();
        if (shuffle) {
            cards = cards.OrderBy(i => Guid.NewGuid()).ToList();
        }
    }
}

}