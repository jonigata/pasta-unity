using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HitEffectEmitter : MonoBehaviour {
    [SerializeField] Model.PartawnPool pool;
    [SerializeField] PartawnHitEffect hitEffectPrefab;

    Dictionary<long, PartawnHitEffect> effects =
        new Dictionary<long, PartawnHitEffect>();

    void Update() {
        for (int i = 0 ; i < pool.affectedPairs ; i++) {
            var ao = pool.contacts[i].a;
            var bo = pool.contacts[i].b;
            if (ao.teamTag == bo.teamTag) { continue; }

            long c = (ao.id << 32) | bo.id;
            if (!effects.ContainsKey(c)) {
                PartawnHitEffect hitEffect = Instantiate(hitEffectPrefab);
                hitEffect.Play(
                    ao.location, bo.location,
                    () => effects.Remove(c));
            }
        }
    }

}
