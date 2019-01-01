using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ReactiveCollectionEx {

public static class ReactiveCollectionEx {
    public static void ForEach<T>(this ReactiveCollection<T> c, Action<T> a) {
        foreach (var e in c) {
            a(e);
        }
    }
}

}
