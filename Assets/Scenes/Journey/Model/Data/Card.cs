using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Journey.Data {

[Serializable]
public class Card {
    [Serializable]
    public class Attribute {
        public enum Klass {
            Power,
            LifeSpan,
            Life,
            Speed,
            MotherLife,
        }

        [SerializeField] public Klass klass;
        [SerializeField] public int level;
    }

    [SerializeField] PawnKlass klass;
    [SerializeField] List<Attribute> attributes = new List<Attribute>();

    public PawnKlass Klass {
        get { return klass; }
    }

    public List<Attribute> Attributes {
        get { return attributes; }
    }

    public void SetUp(PawnKlass klass, List<Attribute> attributes) {
        this.klass = klass;
        this.attributes = attributes;
    }

    public void Compose(Card c) {
        attributes = attributes.Concat(c.attributes).ToList();

      retry:
        for (int i = 0 ; i < attributes.Count ; i++) {
            for (int j = i + 1 ; j < attributes.Count ; j++) {
                var a = attributes[i];
                var b = attributes[j];
                if (a.klass == b.klass && a.level == b.level) {
                    a.level++;
                    attributes.RemoveAt(j);
                    goto retry;
                }
            }
        }

        if (attributes.Count <= 4) {
            return;
        }

        attributes = attributes.OrderBy(x => Guid.NewGuid()).Take(4).ToList();
    }
    
    public void DropAttribute(int n) {
        attributes.RemoveAt(n);
    }
    
}

}
