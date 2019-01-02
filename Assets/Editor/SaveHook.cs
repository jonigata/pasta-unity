using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class SaveHook : AssetModificationProcessor {
    static string[] OnWillSaveAssets(string[] paths) {
        var os = GameObject.FindObjectsOfType<DisableOnSave>();
        foreach (var s in paths) {
            var scene = SceneManager.GetSceneByPath(s);
            foreach (var o in os) {
                if (o.gameObject.scene.name == scene.name) {
                    o.gameObject.SetActive(false);
                }
            }
        }
        return paths;
    }
}

