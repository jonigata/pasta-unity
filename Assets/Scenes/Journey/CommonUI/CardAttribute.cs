using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Journey {

namespace UI {

public class CardAttribute : MonoBehaviour {
    [SerializeField] Image icon;
    [SerializeField] Text value;

    public void SetUp(Data.Card.Attribute attr) {
        var filename = $"AttributeImages/{attr.klass}";
        var sprite = Resources.Load<Sprite>(filename);
        if (sprite == null) {
            Debug.Log($"cant't find such file: {filename}");
        }
        icon.sprite = sprite;
        
        value.text = $"+{attr.level}";
    }
}

}

}

