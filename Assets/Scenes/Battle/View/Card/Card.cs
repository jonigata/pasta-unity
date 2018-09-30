using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace View {

public class Card : MonoBehaviour {
    [NonSerialized] public Model.Card model;
    [SerializeField] RectTransform pivot;
    [SerializeField] Image image;

    public void SetModel(Model.Card card) {
        model = card;
        var filename = $"CardImages/{card.Klass}";
        var sprite = Resources.Load<Sprite>(filename);
        if (sprite == null) {
            Debug.Log($"cant't find such file: {filename}");
        }
        image.sprite = sprite;
    }

    public void OnSelect() {
        pivot.anchoredPosition = new Vector2(0, 10);
    }
    public void OnDeselect() {
        pivot.anchoredPosition = new Vector2(0, 0);
    }
}

}