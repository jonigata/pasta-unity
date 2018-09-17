using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LocalComputerPlayer : MonoBehaviour {
    [SerializeField] Model.Avatar avatar;

    void Start() {
    }

    void Update() {
        if (UnityEngine.Random.Range(0, 10) < 1) {
            Vector2 v = new Vector2(
                -256 + UnityEngine.Random.Range(0, 512),
                -256 + UnityEngine.Random.Range(0, 512));
            BattleCore.Deploy(avatar, null, v);
        }
    }
}
