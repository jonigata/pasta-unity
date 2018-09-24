using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Model {

public class Card : MonoBehaviour {
    [SerializeField] int cost;
    [SerializeField] CardKlass klass;

    public CardKlass Klass { get { return klass; } }
    public int Cost { get { return cost; } }
}

}