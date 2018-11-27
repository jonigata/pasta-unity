using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Command {

public class GetCard : Command {
    public Data.Card card;
    public GetCard(Data.Card card) {
        this.card = card;
    }
}

}
