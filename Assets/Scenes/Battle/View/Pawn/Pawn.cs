using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

namespace View {

public class Pawn : MonoBehaviour {
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] GameObject territory;
    [SerializeField] GameObject appearance;
    [SerializeField] SpriteRenderer icon;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] LifeGauge lifeGauge;

    ParticleSystem.Particle[] particles;
    public int particleCount;
    Model.Pawn pawnModel;
    float hitEffectTime;

    void Start() {
        particles = new ParticleSystem.Particle[512];
        for (int i = 0 ; i < particles.Length ; i++) {
            particles[i].position = new Vector3(0, 0, -0.5f);
            particles[i].startSize = 15.0f;
            particles[i].startLifetime = 99999.0f;
            particles[i].remainingLifetime = 99999.0f;
            particles[i].axisOfRotation = new Vector3(0, 0, -1);
            particles[i].startColor = new Color32(255,255,255,255);
        }
    }

    public void SetPawnModel(Model.Pawn pawnModel) {
        this.pawnModel = pawnModel;
        var pos = pawnModel.location;
        transform.localPosition = new Vector3(pos.x, pos.y, -0.1f);
        territory.transform.localScale =
            Vector3.one * 2.0f * pawnModel.territory;

        var filename = $"CardImages/{pawnModel.klass}";
        var sprite = Resources.Load<Sprite>(filename);
        if (sprite == null) {
            Debug.Log($"cant't find such file: {filename}");
        }
        icon.sprite = sprite;
        lifeGauge.SetPawnModel(pawnModel);

        pawnModel.OnDie.Subscribe(
            u => {
                territory.SetActive(false);
                appearance.SetActive(false);
            }).AddTo(this);

        pawnModel.OnLost.Subscribe(
            u => {
                Destroy(gameObject);
            }).AddTo(this);
    }

    void Update() {
        if (pawnModel == null) { return; }

        particleCount = pawnModel.partawns.Count;

        var i = 0;
        foreach (var partawn in pawnModel.partawns) {
            particles[i].position = new Vector3(
                partawn.location.x,
                partawn.location.y,
                -0.5f);
            particles[i].rotation = -partawn.rotation + 90.0f;
            particles[i].startColor = new Color(
                1.0f,1.0f,1.0f,partawn.life / partawn.initialLife);
            i++;
        }

        particleSystem.SetParticles(particles, pawnModel.partawns.Count);

        if (hitEffectTime == 0) {
            if (pawnModel.damaged) {
                hitEffectTime = 0.5f;
                hitEffect.Play();
            }
        } else {
            hitEffectTime -= Time.deltaTime;
            if (hitEffectTime < 0) {
                hitEffectTime = 0;
            }
        }
        pawnModel.damaged = false;
    }
	
}

}
