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
////    public Spline newSpline;      // El nuevo spline al que se cambiar�
////    private float t = 0f;         // Posici�n a lo largo del spline (0 a 1)
////    private bool isFollowing = true; // Indicador de si est� siguiendo el spline o no

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
////                // Aqu� se puede manejar lo que pasa al final del spline
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
//    public Spline nextSpline;       // El siguiente spline al que cambiar despu�s de la colisi�n
//    private float t = 0f;           // Posici�n a lo largo del spline (0 a 1)

//    private void Update()
//    {
//        // Mueve el objeto a lo largo del spline
//        t += Time.deltaTime * speed;

//        if (t > 1f) // Si el objeto llega al final del spline
//        {
//            t = 1f;
//            Debug.Log("El objeto ha llegado al final del spline.");
//        }

//        // Obtener la posici�n en el spline usando t
//        transform.position = spline.GetPoint(t); // Aqu� es donde necesitas saber c�mo obtener un punto del spline
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        // Si el objeto colisiona con un trigger (por ejemplo, con un tag 'Player')
//        if (other.CompareTag("Player"))
//        {
//            // Cambia al siguiente spline
//            spline = nextSpline;
//            t = 0f; // Reinicia la posici�n en el nuevo spline
//            Debug.Log("El spline ha cambiado!");
//        }
//    }
//}





