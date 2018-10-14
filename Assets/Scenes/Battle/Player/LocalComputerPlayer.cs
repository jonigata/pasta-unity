using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LocalComputerPlayer : MonoBehaviour {
    [SerializeField] Model.Avatar avatar;
    [SerializeField] Model.Pile pile;

    float contextOffset;

    void Start() {
    }

    void Update() {
        bool deployed = false;

        if (UnityEngine.Random.Range(0, 10) < 1) {
            var card = pile.Cards[UnityEngine.Random.Range(0, pile.HandCount)];

            var roff = contextOffset;

            var rmin = -128.0f + roff ;
            var rmax = 128.0f + roff;
            if (rmin < -256.0f) { rmin = -256.0f; }
            if (256.0f < rmax) { rmax = 256.0f; }

            Vector2 v = new Vector2(
                UnityEngine.Random.Range(rmin, rmax),
                UnityEngine.Random.Range(rmin, rmax));

            deployed = BattleCore.Deploy(avatar, card, v);
        }
        if (deployed) {
            contextOffset = -200;
        } else {
            contextOffset += 0.5f;
            if (200.0f < contextOffset) {
                contextOffset = -200;
            }
        }
    }
}
