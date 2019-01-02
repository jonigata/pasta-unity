using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentPriority : MonoBehaviour {
    void Awake() {
        Debug.Log($"processing {gameObject.scene.name}");
        for (int i = 0 ; i < SceneManager.sceneCount; ++i) {
            var s = SceneManager.GetSceneAt(i);
            if (s.buildIndex < gameObject.scene.buildIndex) {
                gameObject.SetActive(false);
            }
        }
    }
}
