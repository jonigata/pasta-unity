using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Model {

public class Card : MonoBehaviour {
    [SerializeField] int cost;
    [SerializeField] CardKlass klass;
    [SerializeField] Pawn pawn;

    Pawn pawnInstance;

    public CardKlass Klass { get { return klass; } }
    public int Cost { get { return cost; } }
    public Pawn Pawn { get { return pawnInstance; } }

    void Start() {
        pawnInstance = Instantiate(pawn, transform, false);
    }
}

}