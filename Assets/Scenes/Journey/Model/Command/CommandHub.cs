using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Command {

public class CommandHub : MonoBehaviour {
    static Subject<Command> subject = new Subject<Command>();

    public static void Post(Command c) { subject.OnNext(c); }
    public static IDisposable Subscribe<T>(Action<T> f) {
        return subject.OfType<Command, T>().Subscribe(f);
    }
}

}