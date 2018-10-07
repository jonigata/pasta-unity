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

    Dictionary<CardKlass, Pawn> prefabs = new Dictionary<CardKlass, Pawn>();
    
    Subject<Pawn> deploySubject = new Subject<Pawn>();
    public IObservable<Pawn> OnDeploy { get { return deploySubject; } }

    void Awake() {
        foreach (CardKlass k in Enum.GetValues(typeof(CardKlass))) {
            Debug.Log($"PawnModel_{k}");
            prefabs[k] = Resources.Load<Pawn>($"PawnModel_{k}");
        }
    }
    
    public void Deploy(Avatar avatar, Card card, Vector2 p) {
        var ck = card.Klass;
        Debug.LogFormat("Deploying {0}", ck);
        if (ck != CardKlass.Basecamp) {
            if (!IsInTheFriendTerritory(avatar, p)) {
                Debug.Log("Not in the friend territory");
                return;
            }

            if (!avatar.castle.ConsumeEnergy(card.Cost)) {
                Debug.Log("Not enough energy");
                return;
            }
        }

        Pawn prefab = GetPawnPrefabByType(ck);
        if (prefab != null) {
            Pawn pawn = Instantiate(prefab, transform, false);
            pawn.partawnPool = partawnPool;
            pawn.location = p;
            pawn.teamTag = avatar.teamTag;
            pawn.life = 20;
            pawns.Add(pawn);

            deploySubject.OnNext(pawn);
            avatar.pile.MoveCardToLast(card);
        } else {
            Debug.Log($"Can't find prefab: {ck}");
        }
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
        foreach (var pawn in pawns) {
            pawn.DieIfFatallyInjured();
        }
        pawns = pawns.Where(x => !x.BeLostIfDiedAndAllChildrenDied()).ToList();

        alphaCastle.UpdateManually(Time.deltaTime);
        betaCastle.UpdateManually(Time.deltaTime);
    }

    Pawn GetPawnPrefabByType(CardKlass ck) {
        switch (ck) {
            case CardKlass.Basecamp:
                return basecampPrefab;
            default:
                return prefabs[ck];
        }
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
