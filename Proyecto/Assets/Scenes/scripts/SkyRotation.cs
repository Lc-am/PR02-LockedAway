using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class SkyRotation : MonoBehaviour
{
    public Volume volume;
    private HDRISky hdriSky;
    public float rotationSpeed = 10f;
    private HDRenderPipeline hdPipeline;

    void Start()
    {
        if (volume == null)
        {
            Debug.LogError("¡No se ha asignado un Volume en el Inspector!");
            return;
        }

        if (!volume.profile.TryGet(out hdriSky))
        {
            Debug.LogError("No se encontró HDRI Sky en el Volume.");
            return;
        }

        // Obtener el pipeline de render (importante para forzar la actualización)
        hdPipeline = RenderPipelineManager.currentPipeline as HDRenderPipeline;
    }

    void Update()
    {
        if (hdriSky != null)
        {
            // Aplicar rotación
            hdriSky.rotation.value += rotationSpeed * Time.deltaTime;

            // Si pasa de 360°, reiniciamos a 0 para evitar bugs
            if (hdriSky.rotation.value >= 360f)
            {
                hdriSky.rotation.value = 0f;
            }

            //  Forzar actualización del cielo 
            if (hdPipeline != null)
            {
                hdPipeline.RequestSkyEnvironmentUpdate();
            }
        }
    }
}
