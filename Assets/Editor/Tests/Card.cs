using UnityEngine;
using UnityEngine.TestTools;
using UniRx;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests {

public class Card {
    [Test]
    public void Compose_Simple() {
        var player = MakeSamplePlayer1();

        player.pool[0].Compose(player.pool[1]);
        Assert.AreEqual(player.pool[0].Attributes[0].level, 2);
    }

    [Test]
    public void Compose_Cascade() {
        var player = MakeSamplePlayer1();

        player.pool[0].Compose(player.pool[2]);
        Assert.AreEqual(player.pool[0].Attributes.Count, 1);
        Assert.AreEqual(player.pool[0].Attributes[0].level, 4);
    }

    [Test]
    public void Compose_Overflow() {
        var player = MakeSamplePlayer1();

        player.pool[2].Compose(player.pool[3]);
        Assert.AreEqual(player.pool[2].Attributes.Count, 4);
    }

    [Test]
    public void ComposeInList() {
        var player = MakeSamplePlayer1();

        var go = new GameObject("foo");
        var c = go.AddComponent<Journey.Model.CardList>();
        c.SetUp(player.pool);
        c.Compose(c.Nth(0), c.Nth(1));
        Assert.AreEqual(c.Count, 7);
        Assert.AreEqual(c.Nth(6).Attributes[0].level, 2);
    }

    Journey.Data.Player MakeSamplePlayer1() {
        var playerJson = @"
{
  ""pool"": [
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1}]},
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1}]},
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1},{""klass"":0, ""level"":2},{""klass"":0, ""level"":3}]},
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":4},{""klass"":0, ""level"":5}]},
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1}]},
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1}]},
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1}]},
    {""klass"": 3, ""attributes"":[{""klass"":0, ""level"":1}]}
  ], 
  ""deck"": [], 
  ""floor"":0 
}
";
        var player = JsonUtility.FromJson<Journey.Data.Player>(playerJson);
        return player;
    }

    [Test]
    public void SubscribeTest() {
        var r = new ReactiveProperty<int>();
        r.Value = 10;

        r.Subscribe(x => Debug.Log(x));
    }
}

}
