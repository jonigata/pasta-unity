using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Model {

public class Stadium : MonoBehaviour {
    [SerializeField] Basecamp basecampPrefab;
    [SerializeField] PartawnPool partawnPool;

    [SerializeField] Castle alphaCastle;
    [SerializeField] Castle betaCastle;

    [SerializeField] Avatar alphaAvatar;
    [SerializeField] Avatar betaAvatar;

    public Avatar AlphaAvatar { get { return alphaAvatar; } }
    public Avatar BetaAvatar { get { return betaAvatar; } }

    List<Pawn> pawns = new List<Pawn>();

    Dictionary<PawnKlass, Pawn> prefabs = new Dictionary<PawnKlass, Pawn>();
    
    Subject<Pawn> deploySubject = new Subject<Pawn>();
    public IObservable<Pawn> OnDeploy { get { return deploySubject; } }

    void Awake() {
        foreach (PawnKlass k in Enum.GetValues(typeof(PawnKlass))) {
            Debug.Log($"PawnModel_{k}");
            prefabs[k] = Resources.Load<Pawn>($"PawnModel_{k}");
        }
    }
    
    public bool Deploy(Avatar avatar, Card card, Vector2 p) {
        var ck = card.Klass;
        Debug.LogFormat("Deploying {0}", ck);
        if (ck != PawnKlass.Basecamp) {
            if (!IsInTheFriendTerritory(avatar, p)) {
                Debug.Log("Not in the friend territory");
                return false;
            }

            if (!avatar.castle.ConsumeEnergy(card.Cost)) {
                Debug.Log("Not enough energy");
                return false;
            }
        }

        Pawn prefab = GetPawnPrefabByType(ck);
        if (prefab != null) {
            Pawn pawn = Instantiate(prefab, transform, false);
            pawn.partawnPool = partawnPool;
            pawn.location = p;
            pawn.teamTag = avatar.teamTag;
            pawn.klass = ck;
            pawns.Add(pawn);

            deploySubject.OnNext(pawn);
            avatar.pile.MoveCardToLast(card);
        } else {
            Debug.Log($"Can't find prefab: {ck}");
        }
        return true;
    }

    void Update() {
        foreach (var pawn in pawns) {
            if (pawn.aimTarget == null) {
                pawn.aimTarget = GetNearestEnemy(pawn);
            }
        }

        foreach (var pawn in pawns) {
            pawn.UpdateManually(Time.deltaTime);
        }
        foreach (var pawn in pawns) {
            pawn.DieIfFatallyInjured();
        }
        pawns = pawns.Where(x => !x.BeLostIfDiedAndAllChildrenDied()).ToList();

        alphaCastle.UpdateManually(Time.deltaTime);
        betaCastle.UpdateManually(Time.deltaTime);
    }

    Pawn GetPawnPrefabByType(PawnKlass ck) {
        switch (ck) {
            case PawnKlass.Basecamp:
                return basecampPrefab;
            default:
                return prefabs[ck];
        }
   }

    bool IsInTheFriendTerritory(Avatar a, Vector2 p) {
        foreach (var pawn in pawns) {
            if (a.teamTag != pawn.teamTag) { continue; }
            if (pawn.died) { continue; }
            if (Vector2.Distance(pawn.location,p) < pawn.territory) {
                return true;
            }
        }
        return false;
    }

    Pawn GetNearestEnemy(Pawn source) {
        float distance = Single.MaxValue;
        Pawn target = null;
        foreach (var pawn in pawns) {
            if (pawn == source) { continue; }
            if (pawn.teamTag == source.teamTag) { continue; }
            if (pawn.died) { continue; }
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
