using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Model {

public class TrivialPawn : Pawn {
    [SerializeField] public float lifeSpan;
    [SerializeField] public float emitInterval;
    [SerializeField] public float partawnSpeed;
    [SerializeField] public float partawnDps;
    [SerializeField] public float partawnLife;
    [SerializeField] public int way = 1;
    [SerializeField] public float wayRange;
    [SerializeField] public float jitter;
    
    float emitTimer;

    public override void UpdateManually(float elapsed) {
        base.UpdateManually(elapsed);

        if (died) { return; }

        emitTimer += elapsed;
        if (emitInterval <= emitTimer) {
            EmitPartawns();
            emitTimer = 0;
        }

        life -= initialLife * elapsed / lifeSpan;
    }

    void EmitPartawns() {
        if (way == 0) { return; }

        float rotation;
        if (aimTarget != null) {
            rotation = Vector2.SignedAngle(
                Vector2.right, aimTarget.location - location);
        } else {
            rotation = UnityEngine.Random.Range(0, 360.0f);
        }

        for (int i = 0 ; i < way ; i++) {
            var r = rotation;
            if (1 < way) {
                r += - wayRange * 0.5f + (wayRange * i / (way - 1)) ;
                Debug.Log(r);
            }
            r += UnityEngine.Random.Range(0, jitter) - jitter * 0.5f;

            Partawn p = partawnPool.Emit(
                teamTag, location, r,
                partawnSpeed, partawnDps, partawnLife);
            partawns.Add(p);
            emitSubject.OnNext(p);
        }
    }
}

}
