using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public class LocalHumanPlayer : MonoBehaviour {
    [SerializeField] BoardInput boardInput;
    [SerializeField] Model.Avatar avatar;
    [SerializeField] View.Pile pile;

    void Start() {
        boardInput.OnClick.Subscribe(Deploy).AddTo(gameObject);
    }

    void Deploy(Vector2 v) {
        if (pile.selected == null) {
            Debug.Log("No card selected");
            return;
        }
        
        if (BattleCore.Deploy(avatar, pile.selected.model, v)) {
            pile.Select(null);
        }
    }
}
