using UnityEngine;

public class followSpline : MonoBehaviour
{
    Transform[] puntosTuberia;  // Array para los puntos de paso
    [SerializeField] private float velocidad = 5f;       // Velocidad de la esfera
    private int puntoActual = 0;        // Índice del punto actual
    pipeController pipecontroller;

    void Update()
    {
        if(puntosTuberia != null)
        {
            // Si la esfera no ha llegado al último punto, mueve la esfera hacia el siguiente punto
            if (puntoActual < puntosTuberia.Length)
            {
                MoverHaciaPunto();
            }
        }
    }

    void MoverHaciaPunto()
    {
        // Mover la esfera hacia el punto actual
        Transform objetivo = puntosTuberia[puntoActual];
        float step = velocidad * Time.deltaTime;  // Calcula el paso para el movimiento
        transform.position = Vector3.MoveTowards(transform.position, objetivo.position, step);

        // Cuando la esfera llega al punto, avanzamos al siguiente
        if (transform.position == objetivo.position)
        {
            puntoActual++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pipe"))
        {
            pipecontroller = other.GetComponent<pipeController>();

            if (pipecontroller != null)
            {
                puntoActual = 0;

                puntosTuberia = new Transform[pipecontroller.controlPoint.Length];

                for (int i = 0; i < pipecontroller.controlPoint.Length; i++)
                {
                    puntosTuberia[i] = pipecontroller.controlPoint[i].transform;

                }
            }

        }
    }

}
