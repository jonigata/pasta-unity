using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Journey {

public class Root : MonoBehaviour {
    [SerializeField] string[] scenes;
    [Inject] ZenjectSceneLoader sceneLoader;
    [Inject] Model.Player player;

    void Start () {
        Debug.Log("Running Root");
        StartCoroutine(MainLoop());
    }
	
    void Update () {
		
    }

    IEnumerator MainLoop() {
        while (true) {
            yield return PlayOneDay();
        }
        
    }

    IEnumerator PlayOneDay() {
        sceneLoader.LoadScene(
            scenes[UnityEngine.Random.Range(0, scenes.Length)],
            LoadSceneMode.Additive);

        yield return Floor.AbstractFloor.Run();
    }
    
}

}
