using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Splines;

public class followSpline : MonoBehaviour
{
    SplineAnimate splineAnimate;

    private void Awake()
    {
        splineAnimate = GetComponent<SplineAnimate>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pipe"))
        {
            splineAnimate.Container = other.gameObject.GetComponentInChildren<SplineContainer>();
        }
    }
}
