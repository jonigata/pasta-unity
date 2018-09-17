using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleCore : MonoBehaviour {
    public static BattleCore instance;

    [SerializeField] Model.Stadium stadiumModel;
    [SerializeField] View.Stadium stadiumView;

    [SerializeField] Model.Avatar alphaAvatar;
    [SerializeField] Model.Avatar betaAvatar;

    void Awake() {
        Assert.IsNull(instance);
        instance = this;
    }

    void OnDestroy() {
        Assert.AreEqual(instance, this);
        instance = null;
    }

    void zDeploy(Model.Avatar avatarModel, Vector2 p) {
        stadiumModel.Deploy(avatarModel, Model.CardKlass.Knife, p);
    }

    public static void Deploy(
        Model.Avatar avatarModel, Model.Card card, Vector2 p) {
        instance.zDeploy(avatarModel, p);
    }

    void Start() {
        alphaAvatar.pile.SetUpWithHierarchy(true);
        betaAvatar.pile.SetUpWithHierarchy(true);

        stadiumModel.Deploy(
            alphaAvatar, Model.CardKlass.Basecamp, new Vector2(-180, -180));
        stadiumModel.Deploy(
            betaAvatar, Model.CardKlass.Basecamp, new Vector2(180, 180));
    }
}
