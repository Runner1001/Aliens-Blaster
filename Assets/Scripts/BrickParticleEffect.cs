using UnityEngine;

public class BrickParticleEffect : MonoBehaviour
{   
    void Start()
    {
        var particleSystem = GetComponent<ParticleSystem>();
        Destroy(gameObject, particleSystem.main.duration);
    }

}
