using System.Drawing.Text;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Splines;

public class followSpline : NetworkBehaviour
{
    public SplineContainer ruta;
    [SerializeField] float velocidad = 5f;

    [SerializeField] float umbralLlegada = 1f;

    float distanciaEntrePuntos = 0.05f;

    Vector3[] pathPointsCache;
    Vector3[] pathLocalPointsCache;
    Vector3 siguientePosicion;
    int indiceSiguientePosicion = 0;

    private void Awake()
    {
        UpdatePoints();
    }

    private void UpdatePoints()
    {
        float longitudRuta = ruta.CalculateLength();
        int cantidadPuntos = Mathf.CeilToInt(longitudRuta / distanciaEntrePuntos) + 1;

        pathPointsCache = new Vector3[cantidadPuntos];
        pathLocalPointsCache = new Vector3[cantidadPuntos];

        for (int i = 0; i < cantidadPuntos; i++)
        {
            float t = (float)i / (float) (cantidadPuntos - 1);
            pathPointsCache[i] = ruta.EvaluatePosition(t);
            pathLocalPointsCache[i] = ruta.transform.InverseTransformPoint(pathPointsCache[i]);
        }

        //transform.position = pathPointsCache[0];
        //siguientePosicion = pathPointsCache[indiceSiguientePosicion];
        transform.position = ruta.transform.TransformPoint(pathLocalPointsCache[0]);
        siguientePosicion = ruta.transform.TransformPoint(pathLocalPointsCache[indiceSiguientePosicion]);
    }

    private void Update()
    {
        Vector3 direccion = siguientePosicion - transform.position;
        Vector3 velocidadMovimiento = direccion.normalized * velocidad;
        transform.position += velocidadMovimiento * Time.deltaTime;

        if (Vector3.Distance(siguientePosicion, transform.position) < umbralLlegada)
        {
            indiceSiguientePosicion++;
            if (indiceSiguientePosicion >= pathLocalPointsCache.Length)
            {
                //Destroy(gameObject);
            }
            else
            {
                //siguientePosicion = pathPointsCache[indiceSiguientePosicion];
                siguientePosicion = ruta.transform.TransformPoint(pathLocalPointsCache[indiceSiguientePosicion]);
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StraightPipe"))
        {
            straightPipeController straightpipecontroller = other.gameObject.GetComponent<straightPipeController>();
            ruta = straightpipecontroller.splineContainerStraight;

            indiceSiguientePosicion = 0;
            UpdatePoints();
        }

        if (other.CompareTag("CurvedPipe"))
        {
            curvedPipeController curvedpipecontroller = other.gameObject.GetComponent<curvedPipeController>();
            ruta = curvedpipecontroller.splineContainerCurved;

            indiceSiguientePosicion = 0;
            UpdatePoints();
        }

        if(other.CompareTag("ObjectDestroyer"))
        {
            Destroy(gameObject);
        }

        if(other.CompareTag("WinCondition"))
        {
            puzzleConditions puzzleConditions = other.gameObject.GetComponent<puzzleConditions>();
            if(puzzleConditions != null)
            {
                puzzleConditions.openTheDoor();
            }
        }
    }
}
