using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    public AudioClip buttonSound;   // Sürükle bırak
    private AudioSource audioSource;

    private void Awake()
    {
        // Bu objede AudioSource yoksa otomatik ekle
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.volume = 0.4f; // Ses seviyesi ayarı (isteğe göre)
    }

    // UnityEvent ile butona bağlayacaksın!
    public void PlaySound()
    {
        if (buttonSound != null)
            audioSource.PlayOneShot(buttonSound);
    }
}