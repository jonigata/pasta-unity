using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

public class DisableOnAwake : MonoBehaviour {
    void Awake() {
        gameObject.SetActive(false);
    }
}
