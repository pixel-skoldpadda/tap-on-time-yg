using UnityEngine;

namespace Components
{
    public class Confetti : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;

        public void Play()
        {
            particle.Play();
        }
    }
}