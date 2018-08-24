using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class PawnModel : MonoBehaviour {
    [NonSerialized] public Vector2 location;

    protected Subject<PartawnModel> emitSubject = new Subject<PartawnModel>();
    public IObservable<PartawnModel> OnEmit { get { return emitSubject; } }

    [NonSerialized] public List<PartawnModel> partawnModels =
        new List<PartawnModel>();

    protected void Update() {
        foreach (var p in partawnModels) {
            p.UpdateManually(Time.deltaTime);
        }
    }
}
