using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PartawnHitEffectStopReceiver : MonoBehaviour {
    [SerializeField] PartawnHitEffect hitEffect;

    public void OnParticleSystemStopped() {
        // Debug.Log("OnParticleSystemStopped");
        // hitEffect.OnStop();
    }
}
