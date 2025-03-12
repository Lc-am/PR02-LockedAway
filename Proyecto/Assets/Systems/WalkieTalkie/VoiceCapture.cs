using UnityEngine;

public class VoiceCapture : MonoBehaviour
{
    private AudioClip micInput;
    private string microphoneName;

    void Start()
    {
        // Obtener el nombre del primer micrófono disponible
        microphoneName = Microphone.devices[0];

        // Iniciar la grabación desde el micrófono
        micInput = Microphone.Start(microphoneName, true, 10, 44100);  
    }

    void Update()
    {
        // Agregar lógica para manejar la grabación o detenerla si es necesario.
    }

    // Este método devuelve los datos de audio en un array de float
    public float[] GetAudioData()
    {
        float[] samples = new float[micInput.samples];
        micInput.GetData(samples, 0);  // Obtiene los datos del micrófono
        return samples;
    }

    // Detener la grabación cuando el objeto es destruido
    void OnDestroy()
    {
        Microphone.End(microphoneName);  // Detener el micrófono al destruir el objeto
    }
}
