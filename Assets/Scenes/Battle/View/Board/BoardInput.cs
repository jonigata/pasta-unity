using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public class BoardInput : MonoBehaviour {
    [SerializeField] Camera camera;

    Subject<Vector2> clickSubject = new Subject<Vector2>();
    public IObservable<Vector2> OnClick { get { return clickSubject; } }

    public void OnSpriteClick(BaseEventData d) {
        var pd = (PointerEventData)d;
        Debug.Log(camera.ScreenToWorldPoint(pd.position));
        clickSubject.OnNext(camera.ScreenToWorldPoint(pd.position));
    }
}
