using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleCore : MonoBehaviour {
    public static BattleCore instance;

    [SerializeField] Board board;

    void Awake() {
        Assert.IsNull(instance);
        instance = this;
    }

    void OnDestroy() {
        Assert.AreEqual(instance, this);
        instance = null;
    }

    void Update () {
    }

    void zDeploy(Vector2 p) {
        board.Deploy(p);
    }

    public static void Deploy(Vector2 p) {
        instance.zDeploy(p);
    }

}
