using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour {
    [SerializeField] Image gauge;

    Model.Pawn pawn;

    public void SetPawnModel(Model.Pawn pawn) {
        this.pawn = pawn;
    }

    void Update () {
        gauge.fillAmount = pawn.life / pawn.initialLife;
        if (pawn.died) {
            gameObject.SetActive(false);
        }
    }
}
