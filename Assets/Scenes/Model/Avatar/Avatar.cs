using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Model {

public enum TeamTag {
    Alpha,
    Beta,
}

public class Avatar : MonoBehaviour {
    public Castle castle;
    public TeamTag teamTag;

    void Update () {
    }
}

}