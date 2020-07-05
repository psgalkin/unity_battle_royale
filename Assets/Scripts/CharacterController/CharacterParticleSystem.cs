using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem _gunFire;
    [SerializeField] private ParticleSystem _blood;

    public void PlayGunFire()
    {
        _gunFire.Play();
    }

    public void PlayBlood()
    {
        _blood.Play();
    }
}
