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
    [NonSerialized] public TeamTag teamTag;
    [NonSerialized] public float life;

    protected Subject<Partawn> emitSubject = new Subject<Partawn>();
    public IObservable<Partawn> OnEmit { get { return emitSubject; } }

    protected Subject<Unit> dieSubject = new Subject<Unit>();
    public IObservable<Unit> OnDie { get { return dieSubject; } }

    protected Subject<Unit> lostSubject = new Subject<Unit>();
    public IObservable<Unit> OnLost { get { return lostSubject; } }

    [NonSerialized] public List<Partawn> partawns = new List<Partawn>();

    [NonSerialized] public bool died;
    [NonSerialized] public bool damaged;

    Pawn aimTarget_;
    IDisposable aimTargetSubscription;
    public Pawn aimTarget {
        get { return aimTarget_; }
        set {
            if (aimTarget_ != null) {
                aimTargetSubscription.Dispose();
            }
            aimTarget_ = value;
            aimTargetSubscription = aimTarget_.OnDie.Subscribe(
                u => aimTarget_ = null).AddTo(gameObject);
        }
    }


    public virtual void UpdateManually(float elapsed) {
        foreach (var p in partawns) {
            p.UpdateManually(elapsed);
        }

        // TODO: 順序はどうでもよいので後ろから持ってくるようにすれば速くなる可能性がある
        // TODO: PartawnPoolに移動したほうがよいのでは
        partawns.RemoveAll(x => x.IsFatallyInjured());

        if (!died) {
            partawnPool.CollectAttack(this);
        }
    }

    public virtual void DieIfFatallyInjured() {
        if (life <= 0 && !died) {
            died = true;
            dieSubject.OnNext(Unit.Default);
            dieSubject.OnCompleted();
            Debug.Log("Died");
        }
    }

    public bool BeLostIfDiedAndAllChildrenDied() {
        if (died && partawns.Count == 0) {
            lostSubject.OnNext(Unit.Default);
            lostSubject.OnCompleted();
            Destroy(gameObject);
            Debug.Log("Lost");
            return true;
        }
        return false;
    }
}

}
