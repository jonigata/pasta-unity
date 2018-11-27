using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Journey.Model {

public class Player : MonoBehaviour {
    [SerializeField] PlayerFactory playerFactory;

    public CardList poolCardList;
    public CardList deckCardList;

    ReactiveProperty<int> floor = new ReactiveProperty<int>();
    ReactiveProperty<int> gold = new ReactiveProperty<int>();
    public IReadOnlyReactiveProperty<int> Floor { get { return floor; } }
    public ReactiveProperty<int> Gold { get { return gold; } }

    public void ConsumeGold(int n) {
        gold.Value -= n;
    }

    public void MockUp() {
        var player = playerFactory.Create();
        MockUp(player);
    }

    public void MockUp(string json) {
        Debug.Log("Player.MockUp");
        var player = JsonUtility.FromJson<Journey.Data.Player>(json);
        MockUp(player);
    }

    public void MockUp(Data.Player player) {
        floor.Value = player.floor;
        gold.Value = player.gold;
        player.pool.ForEach(poolCardList.Add);
        player.deck.ForEach(deckCardList.Add);
    }

    public void ComposeCard(Data.Card left, Data.Card right) {
        poolCardList.Compose(left, right);
        ConsumeGold(100);
    }
}

}

