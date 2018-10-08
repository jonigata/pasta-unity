using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyGauge : MonoBehaviour {
    [SerializeField] Image gaugeGreen;
    [SerializeField] Image gaugeRed;
    [SerializeField] Model.Castle castleModel;
    [SerializeField] View.Pile pile;

    void Update () {
        var green = 
            pile.selected == null ||
            pile.selected.model.Cost <= castleModel.energy;

        gaugeGreen.gameObject.SetActive(green);
        gaugeRed.gameObject.SetActive(!green);
        var gauge = green ? gaugeGreen : gaugeRed;

        gauge.fillAmount = castleModel.energy / Model.Castle.energyMax;
    }
}
