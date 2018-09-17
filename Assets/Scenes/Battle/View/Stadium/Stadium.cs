using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace View {

public class Stadium : MonoBehaviour {
    [SerializeField] Model.Stadium stadiumModel;
    [SerializeField] Pawn bluePawnViewPrefab;
    [SerializeField] Pawn redPawnViewPrefab;

    List<Pawn> pawns = new List<Pawn>();


    void Awake() {
        stadiumModel.OnDeploy.Subscribe(Deploy).AddTo(gameObject);
    }

    public void Deploy(Model.Pawn pawnModel) {
        Debug.Log("A");
        Pawn prefab = bluePawnViewPrefab;
        if (pawnModel.teamTag == Model.TeamTag.Beta) {
            prefab = redPawnViewPrefab;
        }
        Pawn pawn = Instantiate(prefab, transform, false);
        pawn.SetPawnModel(pawnModel);
        pawns.Add(pawn);
    }
}

}
