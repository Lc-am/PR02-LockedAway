//using Unity.Cinemachine;
//using UnityEngine;
//using UnityEngine.UI;

//public class AutoSavedSlider_ForMouseAxisSensitivity : AutoSavedSlider
//{
//    [SerializeField] private CinemachineFreeLook  freeLookCamera; 
//    [SerializeField] private bool isXAxis = true; 
//    [SerializeField] private float minMultiplier = 0.1f; 
//    [SerializeField] private float maxMultiplier = 5f;   

//    protected override void InternalValueChanged(float value)
//    {
//        float sensitivity = Mathf.Lerp(minMultiplier, maxMultiplier, value);

//        if (freeLookCamera != null)
//        {
//            if (isXAxis)
//            {
//                freeLookCamera.m_XAxis.m_MaxSpeed = sensitivity; 
//            }
//            else
//            {
//                freeLookCamera.m_YAxis.m_MaxSpeed = sensitivity; 
//            }
//        }
//        else
//        {
//            Debug.LogWarning("Cámara no asignada.");
//        }
//    }
//}
