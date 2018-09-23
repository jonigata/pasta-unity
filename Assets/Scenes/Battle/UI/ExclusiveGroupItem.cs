using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

public class ExclusiveGroupItem : MonoBehaviour {
    public UnityEvent OnSelect;
    public UnityEvent OnDeselect;

    [NonSerialized] public bool selected;
}
