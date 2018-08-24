using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PartawnModel {
    public Vector2 location;
    public float rotation;
    float speed;

    Vector2 direction;

    public PartawnModel(Vector2 location, float rotation, float speed) {
        this.location = location;
        this.rotation = rotation;
        this.speed = speed;

        var rad = Mathf.Deg2Rad * rotation;
        direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public void UpdateManually(float elapsed) {
        location += direction * elapsed * speed;
    }

}
