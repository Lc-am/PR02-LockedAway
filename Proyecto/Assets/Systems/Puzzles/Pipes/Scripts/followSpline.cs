using UnityEngine;

public class followSpline : MonoBehaviour
{
    Transform[] puntosTuberia;  // Array para los puntos de paso
    [SerializeField] private float velocidad = 5f;       // Velocidad de la esfera
    private int puntoActual = 0;        // Índice del punto actual
    curvedPipeController curvedpipecontroller;
    straightPipeController straightpipecontroller;
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
        if(other.CompareTag("CurvedPipe"))
        {
            curvedpipecontroller = other.GetComponent<curvedPipeController>();

            curvedpipecontroller.canRotate = false;
            notInPipe = false;
            curvedpipecontroller.canRotate = false;

            if (curvedpipecontroller != null)
            {
                puntosTuberia = new Transform[0];

                puntoActual = 1;

                puntosTuberia = new Transform[curvedpipecontroller.controlPointPosition.Length];

                for(int i = 0; i < curvedpipecontroller.controlPointPosition.Length; i++)
                {
                    puntosTuberia[i] = curvedpipecontroller.controlPointPosition[i];
                }
            }
        }
        
        if(other.CompareTag("StraightPipe"))
        {
            straightpipecontroller = other.GetComponent<straightPipeController>();

            straightpipecontroller.canRotate = false;
            notInPipe = false;
            straightpipecontroller.canRotate = false;

            if (straightpipecontroller != null)
            {
                puntosTuberia = new Transform[0];

                puntoActual = 1;

                puntosTuberia = new Transform[straightpipecontroller.controlPointPosition.Length];

                for (int i = 0; i < straightpipecontroller.controlPointPosition.Length; i++)
                {
                    puntosTuberia[i] = straightpipecontroller.controlPointPosition[i];
                }
            }
        }

        if(other.CompareTag("CurvedFirstPoint"))
        {
            curvedpipecontroller = other.GetComponent<curvedPipeController>();

            curvedpipecontroller.triggerFirstPointCurved();
        }
        
        if (other.CompareTag("StraightFirstPoint"))
        {
            straightpipecontroller = other.GetComponent<straightPipeController>();

            if (curvedpipecontroller != null)
            {
                curvedpipecontroller.canRotate = false;
                // resto del código
                straightpipecontroller.triggerFirstPointStraight();
            }
            else
            {
                Debug.LogError("CurvedPipeController no encontrado en " + other.name);
            }            
        }

        if (other.CompareTag("CurvedLastPoint"))
        {
            curvedpipecontroller = other.GetComponent<curvedPipeController>();

            curvedpipecontroller.changeControlPointPositionsCurved();
        }
        
        if (other.CompareTag("StraightLastPoint"))
        {
            straightpipecontroller = other.GetComponent<straightPipeController>();

            straightpipecontroller.changeControlPointPositionsStraight();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("CurvedPipe"))
        {
            curvedpipecontroller = other.GetComponent<curvedPipeController>();

            curvedpipecontroller.canRotate = true;
        }
        
        if(other.CompareTag("StraightPipe"))
        {
            straightpipecontroller = other.GetComponent<straightPipeController>();

            straightpipecontroller.canRotate = true;
        }
    }

}
