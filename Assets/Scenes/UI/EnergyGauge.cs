using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyGauge : MonoBehaviour {
    [SerializeField] Image gauge;
    [SerializeField] Model.Castle castleModel;

    void Start() {
        gauge.type = Image.Type.Filled;
    }

    void Update () {
        gauge.fillAmount = castleModel.energy / Model.Castle.energyMax;
    }
}
