using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {
    [SerializeField] Pawn pawnPrefab;

    List<Pawn> pawns;

    public void Deploy(Vector2 p) {
        Pawn pawn = Instantiate(pawnPrefab, transform, false);
        pawn.transform.localPosition = new Vector3(p.x, p.y, -0.1f);
        pawns.Add(pawn);
    }
}
