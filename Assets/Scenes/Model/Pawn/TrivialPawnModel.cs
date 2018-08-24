using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

public class TrivialPawnModel : PawnModel {
    [SerializeField] public float emitInterval;
    [SerializeField] public float speed;

    float emitTimer;

    void Update() {
        base.Update();

        emitTimer += Time.deltaTime;
        if (emitInterval <= emitTimer) {
            float rotation = UnityEngine.Random.Range(0, 360.0f);

            PartawnModel p = new PartawnModel(location, rotation, speed);
            partawnModels.Add(p);

            emitSubject.OnNext(p);
            emitTimer = 0;
        }
    }
}
