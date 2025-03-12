using UnityEngine;

public class VoiceCapture : MonoBehaviour
{
    private AudioClip micInput;
    private string microphoneName;

    void Start()
    {
        // Obtener el nombre del primer micr�fono disponible
        microphoneName = Microphone.devices[0];

        // Iniciar la grabaci�n desde el micr�fono
        micInput = Microphone.Start(microphoneName, true, 10, 44100);  
    }

    void Update()
    {
        // Agregar l�gica para manejar la grabaci�n o detenerla si es necesario.
    }

    // Este m�todo devuelve los datos de audio en un array de float
    public float[] GetAudioData()
    {
        float[] samples = new float[micInput.samples];
        micInput.GetData(samples, 0);  // Obtiene los datos del micr�fono
        return samples;
    }

    // Detener la grabaci�n cuando el objeto es destruido
    void OnDestroy()
    {
        Microphone.End(microphoneName);  // Detener el micr�fono al destruir el objeto
    }
}
