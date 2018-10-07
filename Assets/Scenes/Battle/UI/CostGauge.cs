using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public class CostGauge : MonoBehaviour {
    [SerializeField] Image gauge;
    [SerializeField] View.Pile pile;

    void Start() {
        gauge.type = Image.Type.Filled;
        gauge.fillAmount = 0;

        pile.OnSelect.Subscribe(
            card => {
                gauge.fillAmount =
                card == null ? 0 : card.model.Cost / Model.Castle.energyMax;
            }).AddTo(gameObject);
    }

}
