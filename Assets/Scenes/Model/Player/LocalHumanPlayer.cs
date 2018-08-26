using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public class LocalHumanPlayer : MonoBehaviour {
    [SerializeField] BoardInput boardInput;
    [SerializeField] Model.Avatar avatar;

    void Start() {
        boardInput.OnClick.Subscribe(Deploy).AddTo(gameObject);
    }

    void Deploy(Vector2 v) {
        BattleCore.Deploy(avatar, v);
    }
}
