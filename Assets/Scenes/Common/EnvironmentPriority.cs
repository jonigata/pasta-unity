using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentPriority : MonoBehaviour {
    [SerializeField] int priority;

    // EnvironmentPriorityを装備したオブジェクトのうち、最小のものだけactivate
    void Awake() {
        Debug.Log($"EnvironmentPriority: {gameObject.name}, {priority}");
        var a = FindObjectsOfType<EnvironmentPriority>();
        foreach (var e in a) {
            if (priority < e.priority) {
                Debug.Log($"EnvironmentPriority: disable {e.priority}");
                e.gameObject.SetActive(false);
            }
        }
    }
}
