using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Model {

public class Partawn {
    public Vector2 location;
    public Vector2 force;
    public float rotation;
    public float life;
    float speed;

    Vector2 direction;

    public Partawn(
        Vector2 location, float rotation, float speed, float life) {
        this.location = location;
        this.rotation = rotation;
        this.speed = speed;
        this.life = life;

        var rad = Mathf.Deg2Rad * rotation;
        direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public void UpdateManually(float elapsed) {
        location += direction * elapsed * speed;
        life -= elapsed;
    }

    public bool IsFatallyInjured() {
        return life <= 0;
    }

}

}