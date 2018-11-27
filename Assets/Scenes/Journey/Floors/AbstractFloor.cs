using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Floor {

public abstract class AbstractFloor : MonoBehaviour {
    public abstract IEnumerator Run();
}

}
