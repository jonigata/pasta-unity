using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleCore : MonoBehaviour {
    public static BattleCore instance;

    [SerializeField] Model.Stadium stadiumModel;

    void Awake() {
        Assert.IsNull(instance);
        instance = this;
    }

    void OnDestroy() {
        Assert.AreEqual(instance, this);
        instance = null;
    }

    void zDeploy(Model.Avatar avatarModel, Model.Card card, Vector2 p) {
        stadiumModel.Deploy(avatarModel, card, p);
    }

    public static void Deploy(
        Model.Avatar avatarModel, Model.Card card, Vector2 p) {
        instance.zDeploy(avatarModel, card, p);
    }

    void Start() {
        var aa = stadiumModel.AlphaAvatar;
        var ba = stadiumModel.BetaAvatar;

        aa.pile.SetUpWithHierarchy(true);
        ba.pile.SetUpWithHierarchy(true);

        stadiumModel.Deploy(aa, aa.basecamp, new Vector2(-180, -180));
        stadiumModel.Deploy(ba, ba.basecamp, new Vector2(180, 180));
    }
}
