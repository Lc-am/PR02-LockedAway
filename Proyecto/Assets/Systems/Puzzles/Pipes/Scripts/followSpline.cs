////using NUnit.Framework;
////using Unity.Mathematics;
////using UnityEngine;
////using UnityEngine.Rendering.PostProcessing;
////using UnityEngine.Splines;
////using Spline = UnityEngine.Rendering.PostProcessing.Spline;


////public class followSpline : MonoBehaviour
////{
////    public Spline currentSpline;  // El spline actual que el objeto sigue
////    public float speed = 5f;      // Velocidad de movimiento
////    public Spline newSpline;      // El nuevo spline al que se cambiará
////    private float t = 0f;         // Posición a lo largo del spline (0 a 1)
////    private bool isFollowing = true; // Indicador de si está siguiendo el spline o no

////    SplineAnimate splineAnimate;

////    private void Awake()
////    {
////        splineAnimate = GetComponent<SplineAnimate>();
////    }

////    private void Update()
////    {
////        if (isFollowing)
////        {
////            // Mueve el objeto a lo largo del spline
////            t += Time.deltaTime * speed;

////            if (t > 1f) // Si el objeto llega al final del spline
////            {
////                t = 1f;
////                // Aquí se puede manejar lo que pasa al final del spline
////                Debug.Log("El objeto ha llegado al final del spline.");
////            }

////            // Mover el objeto a lo largo del spline
////            transform.position = currentSpline.GetPoint(t);
////        }
////    }

////    private void OnTriggerEnter(Collider other)
////    {
////        if(other.CompareTag("Pipe"))
////        {
////            splineAnimate.Container = other.gameObject.GetComponentInChildren<SplineContainer>();
////        }
////    }
////}

//using UnityEngine;
//using UnityEngine.Rendering.PostProcessing;

//public class SplineFollower : MonoBehaviour
//{
//    public Spline spline;           // El spline actual que el objeto sigue
//    public float speed = 5f;        // Velocidad de movimiento
//    public Spline nextSpline;       // El siguiente spline al que cambiar después de la colisión
//    private float t = 0f;           // Posición a lo largo del spline (0 a 1)

//    private void Update()
//    {
//        // Mueve el objeto a lo largo del spline
//        t += Time.deltaTime * speed;

//        if (t > 1f) // Si el objeto llega al final del spline
//        {
//            t = 1f;
//            Debug.Log("El objeto ha llegado al final del spline.");
//        }

//        // Obtener la posición en el spline usando t
//        transform.position = spline.GetPoint(t); // Aquí es donde necesitas saber cómo obtener un punto del spline
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        // Si el objeto colisiona con un trigger (por ejemplo, con un tag 'Player')
//        if (other.CompareTag("Player"))
//        {
//            // Cambia al siguiente spline
//            spline = nextSpline;
//            t = 0f; // Reinicia la posición en el nuevo spline
//            Debug.Log("El spline ha cambiado!");
//        }
//    }
//}





