using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Command {

public class CommandHub : MonoBehaviour {
    public static Subject<Command> subject = new Subject<Command>();
}

}