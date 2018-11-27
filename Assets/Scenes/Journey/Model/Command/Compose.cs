using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Command {

public class Compose : Command {
    public Data.Card left;
    public Data.Card right;
    public Compose(Data.Card left, Data.Card right) {
        this.left = left;
        this.right = right;
    }
}

}
