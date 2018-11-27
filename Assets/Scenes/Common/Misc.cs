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
}
