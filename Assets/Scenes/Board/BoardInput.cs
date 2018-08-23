using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public class BoardInput : MonoBehaviour {
    [SerializeField] Camera camera;

    Subject<Vector2> clickSubject = new Subject<Vector2>();
    public IObservable<Vector2> OnClick { get { return clickSubject; } }

    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            var p = Input.mousePosition;
            clickSubject.OnNext(camera.ScreenToWorldPoint(p));
        }
    }
}
