using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Model {

public class Partawn {
    public int id;
    public TeamTag teamTag;
    public Vector2 location;
    public Vector2 force;
    public float rotation;
    public float dps;
    public float initialLife;
    public float life;
    float speed;

    Vector2 direction;

    public Partawn(
        int id,
        TeamTag teamTag, 
        Vector2 location, float rotation, float speed, float dps, float life) {
        this.id = id;
        this.teamTag = teamTag;
        this.location = location;
        this.rotation = rotation;
        this.speed = speed;
        this.dps = dps;
        this.initialLife = life;
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
