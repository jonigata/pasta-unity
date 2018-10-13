using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public class PartawnHitEffect : MonoBehaviour {
    public ParticleSystem particleSystem;
    
    Action onStop;
    
    public void Play(Vector2 p0, Vector2 p1, Action onStop) {
        transform.position = (p0+p1)*0.5f;
        this.onStop = onStop;
        particleSystem.Play();
        Observable.Timer(TimeSpan.FromMilliseconds(200))
            .Subscribe(u => OnStop()).AddTo(gameObject);

    }

    public void OnStop() {
        if (onStop != null) {
            onStop();
        }
    }
}
