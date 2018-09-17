using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Model {

public class Castle : MonoBehaviour {
    public const float energyMax = 10.0f;
    [NonSerialized] public float energy;

    [SerializeField] float energyChargeSpeed;

    void Start() {
        energy = 5.0f;
    }

    public void UpdateManually(float elapsed) {
        energy = Mathf.Clamp(
            energy + elapsed * energyChargeSpeed, 0, energyMax);
    }

    public bool ConsumeEnergy(float quantity) {
        if (energy < quantity) { return false; }
        energy -= quantity;
        return true;
    }
}

}