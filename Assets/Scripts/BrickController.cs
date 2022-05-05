using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] private int pointValue;
    [SerializeField] private int brickHp;
    
    private ParticleSystem[] brickParticle = new ParticleSystem[3];
    private int particleIndex;

    [SerializeField] MainManager mainManager;

    private void Awake()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        brickParticle[0] = GameObject.Find("Particle_BrickG").GetComponent<ParticleSystem>();
        brickParticle[1] = GameObject.Find("Particle_BrickR").GetComponent<ParticleSystem>();
        brickParticle[2] = GameObject.Find("Particle_BrickB").GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        mainManager.maxScore += pointValue;
        //if green brick
        if (pointValue == 2)
        {
            particleIndex = 1;
        }
        //if red brick
        if (pointValue == 4)
        {
            particleIndex = 2;
        }
        //if blue brick
        if (pointValue == 8)
        {
            particleIndex = 3;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        brickHp -= 1;

        if (brickHp <= 0)
        {
            switch(particleIndex)
            {
                case 3:
                    brickParticle[2].transform.position = this.transform.position;
                    brickParticle[2].Play();
                    break;
                case 2:
                    brickParticle[1].transform.position = this.transform.position;
                    brickParticle[1].Play();
                    break;
                case 1:
                    brickParticle[0].transform.position = this.transform.position;
                    brickParticle[0].Play();
                    break;
            }

            mainManager.PlayAudioClip(2);
            mainManager.score += pointValue;
            Destroy(gameObject, 0.1f);
        }
    }
}
