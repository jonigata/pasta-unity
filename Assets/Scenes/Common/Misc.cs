using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Misc {
    public static T RandomEnum<T>() {
        var values = Enum.GetValues(typeof(T));
        int random = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(random);
    }

    public static IEnumerator SetActiveUntil(GameObject g, Func<bool> f) {
        g.SetActive(true);
        yield return new WaitUntil(f);
        g.SetActive(false);
    }

    public static IEnumerator SetActiveUntil(GameObject g, IEnumerator e) {
        g.SetActive(true);
        yield return e;
        g.SetActive(false);
    }
}
