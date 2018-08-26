using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace View {

public class Stadium : MonoBehaviour {
    [SerializeField] Model.Stadium stadiumModel;
    [SerializeField] Pawn pawnViewPrefab;

    List<Pawn> pawns = new List<Pawn>();


    void Start() {
        stadiumModel.OnDeploy.Subscribe(Deploy).AddTo(gameObject);
    }

    public void Deploy(Model.Pawn pawnModel) {
        Pawn pawn = Instantiate(pawnViewPrefab, transform, false);
        pawn.SetPawnModel(pawnModel);
        pawns.Add(pawn);
    }
}

}