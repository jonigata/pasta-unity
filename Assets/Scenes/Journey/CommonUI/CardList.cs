using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.UI {

public class CardList : MonoBehaviour {
    [SerializeField] Card cardPrefab;

    Subject<Data.Card> selectSubject = new Subject<Data.Card>();
    public IObservable<Data.Card> OnSelect { get { return selectSubject; } }

    IDisposable d1;
    IDisposable d2;
    IDisposable d3;

    public void SetUp(
        Model.CardList cardList,
        Func<Data.Card, bool> filter,
        Func<Data.Card, bool> show) {
        Clear();

        foreach (var card in cardList.Cards) {
            if (filter(card)) {
                var c = AddCardUI(card);
                c.gameObject.SetActive(show(card));
            }
        }

        d1 = cardList.Cards.ObserveAdd().Subscribe(
            x => {
                if (filter(x.Value)) {
                    Debug.Log("AddCard");
                    AddCardUI(x.Value);
                }
            }).AddTo(gameObject);
        d2 = cardList.Cards.ObserveRemove().Subscribe(
            x => {
                foreach (Transform c in transform) {
                    if (c.GetComponent<UI.Card>().card == x.Value) {
                        Destroy(c.gameObject);
                    }
                }
            }).AddTo(gameObject);
        d3 = cardList.Cards.ObserveReset().Subscribe(
            x => {
                foreach (Transform c in transform) {
                    Destroy(c.gameObject);
                }
            });
    }

    public void TearDown() {
        d1.Dispose();
        d2.Dispose();
        d3.Dispose();
    }

    public void Clear() {
        foreach (Transform t in transform) {
            Destroy(t.gameObject);
        }
    }

    UI.Card AddCardUI(Data.Card card) {
        UI.Card cardUi = Instantiate(cardPrefab, transform, false);
        cardUi.SetUp(card, () => { selectSubject.OnNext(card); });
        return cardUi;
    }

    public void Show(Data.Card card) {
        FindCardUI(card).gameObject.SetActive(true);
    }

    public void Hide(Data.Card card) {
        FindCardUI(card).gameObject.SetActive(false);
    }

    UI.Card FindCardUI(Data.Card card) {
        foreach (UI.Card cardUI in GetComponentsInChildren<UI.Card>(true)) {
            if (cardUI.card == card) {
                return cardUI;
            }
        }
        return null;
    }

}

}
