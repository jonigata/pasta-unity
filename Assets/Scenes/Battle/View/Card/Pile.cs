using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace View {

public class Pile : MonoBehaviour {
    [NonSerialized] public Card selected;
    [SerializeField] ExclusiveGroup group;

    public void Select(Card card) {
        selected = card;
        Debug.Log($"Select card: {card}");
        if (card == null) { return; }

        group.Select(card.GetComponent<ExclusiveGroupItem>());
    }
}

}
