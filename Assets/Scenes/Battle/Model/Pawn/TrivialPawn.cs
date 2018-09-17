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

    float emitTimer;
    float initialLife;

    void Start() {
        initialLife = life;
    }

    public override void UpdateManually(float elapsed) {
        base.UpdateManually(elapsed);

        emitTimer += elapsed;
        if (emitInterval <= emitTimer) {
            float rotation = UnityEngine.Random.Range(0, 360.0f);
            if (aimTarget != null) {
                rotation = Vector2.SignedAngle(
                    Vector2.right, aimTarget.location - location);
            }

            Partawn p = partawnPool.Emit(
                teamTag, location, rotation,
                partawnSpeed, partawnDps, partawnLife);
            partawns.Add(p);

            emitSubject.OnNext(p);
            emitTimer = 0;
        }

        life -= initialLife * elapsed / lifeSpan;
    }
}

}
