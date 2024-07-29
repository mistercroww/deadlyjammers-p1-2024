using UnityEngine;

namespace _Scripts.Homunculo
{
    public class HomSFXController : MonoBehaviour
    {
        public AudioSource mixAudio;
        public AudioClip hungrySFX;


        public void Hungry()
        {
            mixAudio.clip = hungrySFX;
            mixAudio.Play();
        }
    }
}