using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Model {

public class TrivialPawn : Pawn {
    [SerializeField] public float emitInterval;
    [SerializeField] public float speed;
    [SerializeField] public float life;

    float emitTimer;

    public override void UpdateManually(float elapsed) {
        base.UpdateManually(elapsed);

        emitTimer += elapsed;
        if (emitInterval <= emitTimer) {
            Partawn p = partawnPool.Emit(location, speed, life);
            partawns.Add(p);

            emitSubject.OnNext(p);
            emitTimer = 0;
        }
    }
}

}