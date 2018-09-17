using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace View {

public class Card : MonoBehaviour {
    [NonSerialized] public Model.Card model;

    public void SetModel(Model.Card card) { model = card; }
}

}