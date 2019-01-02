using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Floor {

public abstract class AbstractFloor : MonoBehaviour {
    static AbstractFloor instance;

    public static IEnumerator Run() {
        Debug.Log("AbstractFloor: waiting loading");
        yield return new WaitUntil(() => instance != null);
        Debug.Log("AbstractFloor: RunInstance");
        yield return instance.RunInstance();
    }

    public abstract IEnumerator RunInstance();

    void OnEnable() {
        instance = this;
    }

    void OnDisable() {
        instance = null;
    }
    
}

}
