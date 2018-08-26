using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Model {

public enum PawnType {
    Basecamp,
    Knife,
}


public class Stadium : MonoBehaviour {
    [SerializeField] Pawn pawnPrefab;
    [SerializeField] Basecamp basecampPrefab;
    [SerializeField] PartawnPool partawnPool;

    [SerializeField] Castle alphaCastle;
    [SerializeField] Castle betaCastle;

    List<Pawn> pawns = new List<Pawn>();
    
    Subject<Pawn> deploySubject = new Subject<Pawn>();
    public IObservable<Pawn> OnDeploy { get { return deploySubject; } }
    
    public void Deploy(Avatar avatar, PawnType pt, Vector2 p) {
        if (pt != PawnType.Basecamp &&
            !avatar.castle.ConsumeEnergy(3.0f)) {
            return;
        }

        Pawn pawn = Instantiate(GetPawnPrefabByType(pt), transform, false);
        pawn.partawnPool = partawnPool;
        pawn.location = p;
        pawns.Add(pawn);

        deploySubject.OnNext(pawn);
    }

    void Update() {
        foreach (var pawn in pawns) {
            pawn.UpdateManually(Time.deltaTime);
        }
        alphaCastle.UpdateManually(Time.deltaTime);
        betaCastle.UpdateManually(Time.deltaTime);
    }

    Pawn GetPawnPrefabByType(PawnType pt) {
        switch (pt) {
            case PawnType.Basecamp:
                return basecampPrefab;
            case PawnType.Knife:
                return pawnPrefab;
        }
        return pawnPrefab;
    }
}

}