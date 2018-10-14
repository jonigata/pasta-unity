using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RecycleBin : MonoBehaviour {
    [SerializeField] GameObject prefab;
    [SerializeField] int prepareCount;

    Stack<GameObject> unused = new Stack<GameObject>();

    void Awake() {
        for (int i = 0 ; i < prepareCount ; i++) {
            GameObject o = Instantiate(prefab);
            o.SetActive(false);
            unused.Push(o);
        }
    }

    public GameObject Allocate() {
        if (0 < unused.Count) {
            return unused.Pop();
        }
        return null;
    }

    public void Deallocate(GameObject o) {
        o.SetActive(false);
        unused.Push(o);
    }

    
}
