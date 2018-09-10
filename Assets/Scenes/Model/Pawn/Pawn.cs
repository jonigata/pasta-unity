using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Model {

public abstract class Pawn : MonoBehaviour {
    public float territory;

    [NonSerialized] public Vector2 location;
    [NonSerialized] public PartawnPool partawnPool;
    [NonSerialized] public Pawn aimTarget;
    [NonSerialized] public TeamTag teamTag;
    [NonSerialized] public float life;

    protected Subject<Partawn> emitSubject = new Subject<Partawn>();
    public IObservable<Partawn> OnEmit { get { return emitSubject; } }

    protected Subject<Unit> dieSubject = new Subject<Unit>();
    public IObservable<Unit> OnDie { get { return dieSubject; } }

    [NonSerialized] public List<Partawn> partawns = new List<Partawn>();

    public virtual void UpdateManually(float elapsed) {
        foreach (var p in partawns) {
            p.UpdateManually(elapsed);
        }

        // TODO: 順序はどうでもよいので後ろから持ってくるようにすれば速くなる可能性がある
        // TODO: PartawnPoolに移動したほうがよいのでは
        partawns.RemoveAll(x => x.IsFatallyInjured());

        partawnPool.CollectAttack(this);

        if (life <= 0) {
            dieSubject.OnNext(Unit.Default);
            dieSubject.OnCompleted();
            Destroy(gameObject);
        }
    }
}

}
