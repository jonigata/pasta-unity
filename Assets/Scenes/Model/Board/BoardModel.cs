using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

public class BoardModel : MonoBehaviour {
    [SerializeField] PawnModel pawnModelPrefab;

    List<PawnModel> pawnModels = new List<PawnModel>();
    
    Subject<PawnModel> deploySubject = new Subject<PawnModel>();
    public IObservable<PawnModel> OnDeploy { get { return deploySubject; } }
    
    public void Deploy(Vector2 p) {
        PawnModel pawnModel = Instantiate(pawnModelPrefab, transform, false);
        pawnModel.location = p;
        pawnModels.Add(pawnModel);

        deploySubject.OnNext(pawnModel);
    }
}
