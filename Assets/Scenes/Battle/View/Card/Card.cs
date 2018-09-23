using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace View {

public class Card : MonoBehaviour {
    [NonSerialized] public Model.Card model;
    [SerializeField] RectTransform pivot;

    public void SetModel(Model.Card card) { model = card; }

    public void OnSelect() {
        pivot.anchoredPosition = new Vector2(0, 10);
    }
    public void OnDeselect() {
        pivot.anchoredPosition = new Vector2(0, 0);
    }
}

}