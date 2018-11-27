using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.UI {

public class Card : MonoBehaviour {
    [SerializeField] Image image;
    [SerializeField] Transform attributes;
    [SerializeField] CardAttribute attributePrefab;

    [NonSerialized] public Data.Card card;
    Action onSelect;
    
    public void SetUp(Data.Card card, Action onSelect) {
        this.card = card;
        this.onSelect = onSelect;

        var filename = $"CardImages/{card.Klass}";
        var sprite = Resources.Load<Sprite>(filename);
        if (sprite == null) {
            Debug.Log($"cant't find such file: {filename}");
        }
        image.sprite = sprite;

        foreach (var attr in card.Attributes) {
            UI.CardAttribute attrUI =
                Instantiate(attributePrefab, attributes, false);
            attrUI.SetUp(attr);
        }
    }

    public void OnClick() {
        onSelect();
    }
}

}