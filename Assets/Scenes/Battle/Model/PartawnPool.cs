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
        TeamTag teamTag,
        Vector2 location, float rotation,float speed, float dps, float life) {
        Partawn p = new Partawn(teamTag, location, rotation, speed, dps, life);
        partawns.Add(p);
        return p;
    }

    struct Contact {
        public Vector2 diff;
        public float dSq;
        public Partawn a;
        public Partawn b;
    }

    Contact[] contacts = new Contact[65536];

    void Update() {
        float elapsed = Time.deltaTime;
        float thresholdSq = threshold * threshold;

        // detection
        affectedPairs = 0;
        for (int i = 0 ; i < partawns.Count ; i++) {
            var pi = partawns[i];
            for (int j = i+1 ; j < partawns.Count ; j++) {
                var pj = partawns[j];
                var diff = pj.location - pi.location;
                var dSq = diff.sqrMagnitude;

                if (0.001f < dSq && dSq < thresholdSq) {
                    contacts[affectedPairs].diff = diff;
                    contacts[affectedPairs].dSq = dSq;
                    contacts[affectedPairs].a = pi;
                    contacts[affectedPairs].b = pj;
                    affectedPairs++;
                }
            }
        }

        // 殴り合い
        for (int i = 0 ; i < affectedPairs ;i++) {
            var c = contacts[i];
            if (c.a.teamTag != c.b.teamTag) {
                c.a.life -= c.b.dps * elapsed;
                c.b.life -= c.a.dps * elapsed;
            }
        }

        // 押し合い(力計算)
        for (int i = 0 ; i < affectedPairs ;i++) {
            var c = contacts[i];
            var power = (thresholdSq - c.dSq) / thresholdSq;
            var force = c.diff * (power * powerFactor / Mathf.Sqrt(c.dSq));
            c.a.force -= force;
            c.b.force += force;
        }

        // 押し合い(力適用)
        for (int i = 0 ; i < partawns.Count ; i++) {
            var pi = partawns[i];
            pi.location += pi.force;
            pi.force = Vector2.zero;
        }
    }

    public void CollectAttack(Pawn pawn) {
        float thresholdSq = threshold * threshold;

        for (int i = 0 ; i < partawns.Count ; i++) {
            var pi = partawns[i];
            if (pi.teamTag == pawn.teamTag) { continue; }

            var diff = pawn.location - pi.location;
            var dSq = diff.sqrMagnitude;
            if (dSq < thresholdSq) {
                pawn.life -= pi.dps * Time.deltaTime;
            }
        }
    }

}

}
