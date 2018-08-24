using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleCore : MonoBehaviour {
    public static BattleCore instance;

    [SerializeField] BoardView boardView;
    [SerializeField] BoardModel boardModel;

    void Awake() {
        Assert.IsNull(instance);
        instance = this;
    }

    void OnDestroy() {
        Assert.AreEqual(instance, this);
        instance = null;
    }

    void zDeploy(Vector2 p) {
        boardModel.Deploy(p);
    }

    public static void Deploy(Vector2 p) {
        instance.zDeploy(p);
    }

}
