using UnityEngine;

public class followSpline : MonoBehaviour
{
    Transform[] puntosTuberia;  // Array para los puntos de paso
    [SerializeField] private float velocidad = 5f;       // Velocidad de la esfera
    private int puntoActual = 0;        // Índice del punto actual
    pipeController pipecontroller;
    [SerializeField] private Vector3 movientoNoTuberia; //Moivmiento para entrar en las tuberias

    private bool notInPipe = true;
    private Vector3 lastPointPosition;

    void Update()
    {
        if (notInPipe)
        {
            transform.position = transform.position + movientoNoTuberia * Time.deltaTime;
        }

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
        Debug.Log("Objetivo actual: " + puntoActual + "Posicion: " + objetivo.transform.position);

        // Cuando la esfera llega al punto, avanzamos al siguiente
        if (transform.position == objetivo.position)
        {
            puntoActual++;
            lastPointPosition = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pipe"))
        {
            pipecontroller = other.GetComponent<pipeController>();

            notInPipe = false;
            pipecontroller.canRotate = false;

            if (pipecontroller != null)
            {
                puntosTuberia = new Transform[0];

                puntoActual = 1;

                if(pipecontroller.isStraight)
                {
                    puntosTuberia = new Transform[pipecontroller.controlPointStraight.Length];

                    for(int i = 0; i < pipecontroller.controlPointStraight.Length; i++)
                    {
                        puntosTuberia[i] = pipecontroller.controlPointStraight[i].transform;
                    }
                }
                else
                {
                    puntosTuberia = new Transform[pipecontroller.controlPointCurved.Length];

                    for(int i = 0; i < pipecontroller.controlPointCurved.Length; i++)
                    {
                        puntosTuberia[i] = pipecontroller.controlPointCurved[i].transform;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Pipe"))
        {
            pipecontroller.canRotate = true;
        }
    }

}
