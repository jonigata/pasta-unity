using UnityEngine;
using System.Collections;

public class PawnEmitter : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;

    ParticleSystem.Particle[] particles;

    struct PawnInfo {
        public Vector3 force;
    }

    PawnInfo[] infos;


    void Start() {
        int width = 15;
        int height = 15;

        int[,] indices = new int[width, height];
        particleSystem.Emit(indices.Length);
        particles = new ParticleSystem.Particle[indices.Length];
        particleSystem.GetParticles(particles);

        infos = new PawnInfo[indices.Length];

        int count = 0;
        for (int y = 0; y <height; y++) {
            for (int x = 0; x <width; x++) {
                indices[x, y] = count;
                particles[count].position = new Vector3(
                    (x - width / 2) * 0.2f,
                    (y - height / 2) * 0.2f, 0);
                particles[count].size = 0.15f;
                particles[count].startLifetime = 99999.0f;
                particles[count].remainingLifetime = 99999.0f;
                particles[count].axisOfRotation = new Vector3(0, 0, -1);
                particles[count].rotation = Random.Range(0, 360.0f);
                count++;
            }
        }

        particleSystem.SetParticles(particles, indices.Length);
    }

    void Update() {
/*
        ランダム移動
        for (int i = 0 ; i < particles.Length ; i++) {
            var p = particles[i].position;
            particles[i].position = new Vector3(
                p.x + Random.Range(-0.01f, 0.01f),
                p.y + Random.Range(-0.01f, 0.01f),
                0);
        }
*/
        // 押し合い
        int n = particles.Length;

        float threshold = 0.4f;
        float powerFactor = 0.01f;
        float thresholdSq = threshold * threshold;

        for (int i = 0 ; i < n ; i++) {
            for (int j = i+1 ; j < n ; j++) {
                var diff = particles[j].position - particles[i].position;
                var dSq = diff.sqrMagnitude;

                if (dSq < thresholdSq) {
                    var power = (thresholdSq - dSq) / thresholdSq;
                    var force = diff * (power * powerFactor / Mathf.Sqrt(dSq));
                    infos[i].force -= force;
                    infos[j].force += force;
                }
            }
        }

        for (int i = 0 ; i < n; i++) {
            particles[i].position += infos[i].force;
            infos[i].force = Vector3.zero;
        }
        particleSystem.SetParticles(particles, particles.Length);
    }
}
