using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public class LocalHumanPlayer : MonoBehaviour {
    [SerializeField] BoardInput boardInput;

    void Start() {
        boardInput.OnClick.Subscribe(Deploy);
    }

    void Deploy(Vector2 v) {
        BattleCore.Deploy(v);
    }
}
