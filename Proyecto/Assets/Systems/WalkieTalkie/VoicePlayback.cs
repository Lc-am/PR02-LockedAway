using UnityEngine;

public class VoicePlayback : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Obtener el AudioSource para poder reproducir el audio
        audioSource = GetComponent<AudioSource>();
    }

    // Este método crea un AudioClip a partir de los datos recibidos y lo reproduce
    public void PlayAudio(float[] audioData)
    {
        if (audioSource != null)
        {
            // Crear un AudioClip con los datos recibidos
            AudioClip clip = AudioClip.Create("ReceivedAudio", audioData.Length, 1, 44100, false);
            clip.SetData(audioData, 0);  // Asignar los datos de audio al AudioClip

            // Reproducir el audio
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
