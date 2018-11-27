using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Model {

public class PlayerFactory : MonoBehaviour {
    [SerializeField] CardFactory cardFactory;

    public Data.Player Create() {
        Data.Player p = new Data.Player();
        p.floor = 0;
        p.gold = 100;
        for (int i = 0 ; i < 4 ; i++) {
            var card = cardFactory.Create();
            p.pool.Add(card);
            p.deck.Add(card);
        }
        return p;
    }

}

}
