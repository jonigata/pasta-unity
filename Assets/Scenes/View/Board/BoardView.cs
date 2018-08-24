using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

public class BoardView : MonoBehaviour {
    [SerializeField] BoardModel boardModel;
    [SerializeField] PawnView pawnViewPrefab;

    List<PawnView> pawnViews = new List<PawnView>();


    void Start() {
        boardModel.OnDeploy.Subscribe(Deploy).AddTo(gameObject);
    }

    public void Deploy(PawnModel pawnModel) {
        PawnView pawnView = Instantiate(pawnViewPrefab, transform, false);
        pawnView.SetPawnModel(pawnModel);
        pawnViews.Add(pawnView);
    }
}
