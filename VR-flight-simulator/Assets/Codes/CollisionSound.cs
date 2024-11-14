using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Obtén el componente AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayCollisionSound();
        }
    }

    void PlayCollisionSound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
