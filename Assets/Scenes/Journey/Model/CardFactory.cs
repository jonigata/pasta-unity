using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Model {

public class CardFactory : MonoBehaviour {
    public Data.Card Create() {
        List<Data.Card.Attribute> attributes = new List<Data.Card.Attribute>();
        int n = UnityEngine.Random.Range(0, 4);
        for (int i = 0 ; i < n ; i++) {
            var a = new Data.Card.Attribute();
            a.klass = Misc.RandomEnum<Data.Card.Attribute.Klass>();
            a.level = 1;
            attributes.Add(a);
        }

        var card = new Data.Card();
        card.SetUp(PawnKlass.Knife, attributes);
        return card;
    }
}


}
