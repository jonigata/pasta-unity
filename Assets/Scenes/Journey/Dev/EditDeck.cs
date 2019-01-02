using UnityEngine;
using Zenject;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Dev {

public class EditDeck : MonoBehaviour {
    [SerializeField] Journey.UI.EditDeck editDeck;
    [Inject] Journey.Model.Player player;

    IEnumerator Start() {
        Debug.Log("EditDeck");
        player.MockUp();

        yield return editDeck.Run();
    }
}

}
