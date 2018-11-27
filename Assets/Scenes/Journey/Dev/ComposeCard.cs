using UnityEngine;
using Zenject;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Dev {

public class ComposeCard : MonoBehaviour {
    [Inject] Journey.Model.Player player;
    [SerializeField] Journey.UI.ComposeCard composer;

    void Awake() {
        var playerJson = @"
{
  ""pool"": [
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1}]},
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1}]},
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1}]},
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1}]},
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1}]},
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1}]},
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1}]},
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1}]}
  ], 
  ""deck"": [], 
  ""gold"":100,
  ""floor"":0 
}
";
        player.MockUp(playerJson);
    }
}

}
