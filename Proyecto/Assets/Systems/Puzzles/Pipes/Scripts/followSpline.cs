using System.Drawing.Text;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Splines;

public class followSpline : MonoBehaviour
{
    private SplineContainer ruta;
    [SerializeField] float velocidad = 5f;

    [SerializeField] float umbralLlegada = 1f;

    float distanciaEntrePuntos = 5f;

    Vector3[] pathPointsCache;
    Vector3 siguientePosicion;
    int indiceSiguientePosicion = 1;

    private bool inPipe = false;

    SphereCreator creator;

    private void Awake()
    {
        creator = GetComponent<SphereCreator>();

        ruta = creator.firstPipe;
    }

    private void UpdatePoints()
    {
        float longitudRuta = ruta.CalculateLength();
        int cantidadPuntos = Mathf.CeilToInt(longitudRuta / distanciaEntrePuntos) + 1;

        pathPointsCache = new Vector3[cantidadPuntos];

        for (int i = 0; i < cantidadPuntos; i++)
        {
            float t = (float)i / (float)cantidadPuntos;
            pathPointsCache[i] = ruta.EvaluatePosition(t);
        }

        transform.position = pathPointsCache[0];
        siguientePosicion = pathPointsCache[indiceSiguientePosicion];
    }

    private void Update()
    {
        Vector3 direccion = siguientePosicion - transform.position;
        Vector3 velocidadMovimiento = direccion.normalized * velocidad;
        transform.position += velocidadMovimiento * Time.deltaTime;

        if (Vector3.Distance(siguientePosicion, transform.position) < umbralLlegada)
        {
            indiceSiguientePosicion++;
            if (indiceSiguientePosicion == pathPointsCache.Length)
            {
                Destroy(gameObject);
            }
            else
            {
                siguientePosicion = pathPointsCache[indiceSiguientePosicion];
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("StraightPipe"))
        {
            straightPipeController straightpipecontroller = other.gameObject.GetComponent<straightPipeController>();
            ruta = straightpipecontroller.splineContainerStraight;

            UpdatePoints();

            inPipe = true;
        }

        if (other.CompareTag("CurvedPipe"))
        {
            curvedPipeController curvedpipecontroller = other.gameObject.GetComponent<curvedPipeController>();
            ruta = curvedpipecontroller.splineContainerCurved;

            UpdatePoints();

            inPipe = true;
        }
    }

}
