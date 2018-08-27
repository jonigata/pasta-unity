using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Model {

public class PartawnPool : MonoBehaviour {
    public float threshold = 10.0f;
    public float powerFactor = 0.01f;

    // for debug
    public int affectedPairs = 0;

    List<Partawn> partawns = new List<Partawn>();

    public Partawn Emit(
        Vector2 location, float rotation,float speed, float life) {
        Partawn p = new Partawn(location, rotation, speed, life);
        partawns.Add(p);
        return p;
    }

    void Update() {
        // 押し合い
        float thresholdSq = threshold * threshold;

        affectedPairs = 0;
        for (int i = 0 ; i < partawns.Count ; i++) {
            var pi = partawns[i];
            for (int j = i+1 ; j < partawns.Count ; j++) {
                var pj = partawns[j];
                var diff = pj.location - pi.location;
                var dSq = diff.sqrMagnitude;

                if (dSq < thresholdSq) {
                    var power = (thresholdSq - dSq) / thresholdSq;
                    var force = diff * (power * powerFactor / Mathf.Sqrt(dSq));
                    pi.force -= force;
                    pj.force += force;
                    affectedPairs++;
                }
            }
        }

        for (int i = 0 ; i < partawns.Count ; i++) {
            var pi = partawns[i];
            pi.location += pi.force;
            pi.force = Vector2.zero;
        }
    }
}

}