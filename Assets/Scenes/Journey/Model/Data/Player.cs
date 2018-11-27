using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Data {

public class Player {
    public List<Card> pool = new List<Card>();
    public List<Card> deck = new List<Card>();
    public int floor;
    public int gold;
}

}