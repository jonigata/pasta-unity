using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LocalComputerPlayer : MonoBehaviour {
    [SerializeField] Model.Avatar avatar;
    [SerializeField] Model.Pile pile;

    void Start() {
    }

    void Update() {
        if (UnityEngine.Random.Range(0, 10) < 1) {
            Vector2 v = new Vector2(
                -256 + UnityEngine.Random.Range(0, 512),
                -256 + UnityEngine.Random.Range(0, 512));

            var l = pile.Cards;
            var card = l[UnityEngine.Random.Range(0, l.Count)];
            BattleCore.Deploy(avatar, card, v);
        }
    }
}
