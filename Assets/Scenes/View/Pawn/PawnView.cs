using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

public class PawnView : MonoBehaviour {
    [SerializeField] ParticleSystem particleSystem;

    ParticleSystem.Particle[] particles;
    PawnModel pawnModel;

    public void SetPawnModel(PawnModel pawnModel) {
        this.pawnModel = pawnModel;
        var pos = pawnModel.location;
        transform.localPosition = new Vector3(pos.x, pos.y, -0.1f);

        pawnModel.OnEmit.Subscribe(
            partawnModel => {

                var p = new ParticleSystem.EmitParams();
                p.position = new Vector3(pos.x, pos.y, -0.5f);
                p.startSize= 15.0f;
                p.startLifetime = 99999.0f;
                p.axisOfRotation = new Vector3(0, 0, 1);
                p.rotation = -partawnModel.rotation + 90.0f;
                particleSystem.Emit(p, 1);

                particles = null;
            });
    }

    void Update() {
        if (pawnModel == null) { return; }

        // TODO: ちゃんと同期すればわざわざ獲得する必要はない
        if (particles == null) {
            particles =
                new ParticleSystem.Particle[particleSystem.particleCount];
            particleSystem.GetParticles(particles);
        }

        var i = 0;
        foreach (var partawn in pawnModel.partawnModels) {
            particles[i].position = partawn.location;
            i++;
        }

        particleSystem.SetParticles(particles, particles.Length);
    }
	
}
