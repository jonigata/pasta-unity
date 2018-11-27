using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Dev {

public class EditDeck : MonoBehaviour {
    [SerializeField] Journey.Model.CardList deckCardList;
    [SerializeField] Journey.Model.CardList playerCardList;
    [SerializeField] Journey.UI.EditDeck deckEdit;

    void Start() {
        Debug.Log("DeckEdit");
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
  ""floor"":0 
}
";
        var player = JsonUtility.FromJson<Journey.Data.Player>(playerJson);

        deckCardList.SetUp(player.deck);
        playerCardList.SetUp(player.pool);
        deckEdit.SetUp(deckCardList, playerCardList);
    }
}

}
