using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.UI {

public class EditDeck : MonoBehaviour {
    [SerializeField] MultipleCardChoose choose;
    
    public void SetUp(
        Model.CardList deckCardListModel,
        Model.CardList poolCardListModel) {
        choose.SetUp(deckCardListModel, poolCardListModel);
    }

    public void OnExecute() {
    }
}

}
