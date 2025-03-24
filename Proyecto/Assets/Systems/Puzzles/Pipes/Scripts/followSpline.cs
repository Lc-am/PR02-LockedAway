using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Splines;
using Spline = UnityEngine.Rendering.PostProcessing.Spline;


public class followSpline : MonoBehaviour
{
    public Spline currentSpline;  // El spline actual que el objeto sigue
    public float speed = 5f;      // Velocidad de movimiento
    public Spline newSpline;      // El nuevo spline al que se cambiar�
    private float t = 0f;         // Posici�n a lo largo del spline (0 a 1)
    private bool isFollowing = true; // Indicador de si est� siguiendo el spline o no

    SplineAnimate splineAnimate;

    private void Awake()
    {
        splineAnimate = GetComponent<SplineAnimate>();
    }

    //private void Update()
    //{
    //    if (isFollowing)
    //    {
    //        // Mueve el objeto a lo largo del spline
    //        t += Time.deltaTime * speed;

    //        if (t > 1f) // Si el objeto llega al final del spline
    //        {
    //            t = 1f;
    //            // Aqu� se puede manejar lo que pasa al final del spline
    //            Debug.Log("El objeto ha llegado al final del spline.");
    //        }

    //        // Mover el objeto a lo largo del spline
    //        transform.position = currentSpline.GetPoint(t);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pipe"))
        {
            splineAnimate.Container = other.gameObject.GetComponentInChildren<SplineContainer>();
        }
    }
}
