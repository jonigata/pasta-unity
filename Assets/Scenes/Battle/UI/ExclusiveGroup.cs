using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ExclusiveGroup : MonoBehaviour {
    public void Select(ExclusiveGroupItem target) {
        var items = GetComponentsInChildren<ExclusiveGroupItem>();
        foreach (var item in items) {
            if (item == target) {
                if (!item.selected) {
                    item.OnSelect.Invoke();
                    item.selected = true;
                }
            } else {
                if (item.selected) {
                    item.OnDeselect.Invoke();
                    item.selected = false;
                }
            }
        }
    }
}
