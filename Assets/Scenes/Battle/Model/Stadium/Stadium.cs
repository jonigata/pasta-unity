using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Model {

public class Stadium : MonoBehaviour {
    [SerializeField] Pawn pawnPrefab;
    [SerializeField] Basecamp basecampPrefab;
    [SerializeField] PartawnPool partawnPool;

    [SerializeField] Castle alphaCastle;
    [SerializeField] Castle betaCastle;

    List<Pawn> pawns = new List<Pawn>();
    
    Subject<Pawn> deploySubject = new Subject<Pawn>();
    public IObservable<Pawn> OnDeploy { get { return deploySubject; } }
    
    public void Deploy(Avatar avatar, CardKlass ck, Vector2 p) {
        Debug.LogFormat("Deploying {0}", ck);
        if (ck != CardKlass.Basecamp) {
            if (!IsInTheFriendTerritory(avatar, p)) {
                Debug.Log("Not in the friend territory");
                return;
            }

            if (!avatar.castle.ConsumeEnergy(3.0f)) {
                Debug.Log("Not enough energy");
                return;
            }
        }

        Pawn pawn = Instantiate(GetPawnPrefabByType(ck), transform, false);
        pawn.partawnPool = partawnPool;
        pawn.location = p;
        pawn.teamTag = avatar.teamTag;
        pawn.life = 20;
        pawns.Add(pawn);

        deploySubject.OnNext(pawn);
    }

    void Update() {
        foreach (var pawn in pawns) {
            if (pawn.aimTarget == null) {
                pawn.aimTarget = GetNearesetEnemy(pawn);
            }
        }

        foreach (var pawn in pawns) {
            pawn.UpdateManually(Time.deltaTime);
        }
        pawns = pawns.Where(x => !x.DieIfFatallyInjured()).ToList();

        alphaCastle.UpdateManually(Time.deltaTime);
        betaCastle.UpdateManually(Time.deltaTime);
    }

    Pawn GetPawnPrefabByType(CardKlass ck) {
        switch (ck) {
            case CardKlass.Basecamp:
                return basecampPrefab;
            default:
                return pawnPrefab;
        }
        return pawnPrefab;
   }

    bool IsInTheFriendTerritory(Avatar a, Vector2 p) {
        foreach (var pawn in pawns) {
            if (a.teamTag != pawn.teamTag) { continue; }
            if (Vector2.Distance(pawn.location,p) < pawn.territory) {
                return true;
            }
        }
        return false;
    }

    Pawn GetNearesetEnemy(Pawn source) {
        float distance = Single.MaxValue;
        Pawn target = null;
        foreach (var pawn in pawns) {
            if (pawn == source) { continue; }
            if (pawn.teamTag == source.teamTag) { continue; }
            var d = Vector2.Distance(pawn.location, source.location);
            if (d < distance) {
                distance = d;
                target = pawn;
            }
        }
        return target;
    }
}

}
