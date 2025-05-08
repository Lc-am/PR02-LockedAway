//using Unity.Cinemachine;
//using UnityEngine;
//using UnityEngine.UI;

//public class InvertMouse : MonoBehaviour
//{
//    [SerializeField] private string prefKey = "InvertMouse"; // Clave para guardar si se invierte el eje
//    [SerializeField] private bool defaultInvert = false; // Valor por defecto (invertido o no)
//    [SerializeField] private Toggle invertToggle; // Toggle para invertir el ratón
//    //[SerializeField] private CinemachineFreeLook freeLookCamera; // Referencia a la cámara

//    [SerializeField] private bool invertXAxis; // Indica si se está invirtiendo el eje X
//    [SerializeField] private bool invertYAxis; // Indica si se está invirtiendo el eje Y

//    //private void Awake()
//    //{
//    //    // Cargar el valor de PlayerPrefs o usar el valor por defecto
//    //    bool isInverted = PlayerPrefs.GetInt(prefKey, defaultInvert ? 1 : 0) == 1;

//    //    // Asignar valor al Toggle y añadir listener
//    //    invertToggle.isOn = isInverted;
//    //    invertToggle.onValueChanged.AddListener(OnToggleInvert);

//    //    // Aplicar el valor invertido
//    //    ApplyInversion(isInverted);
//    //}

//    //private void OnToggleInvert(bool value)
//    //{
//    //    // Guardar la preferencia
//    //    PlayerPrefs.SetInt(prefKey, value ? 1 : 0);
//    //    PlayerPrefs.Save();

//    //    // Aplicar el cambio de inversión
//    //    ApplyInversion(value);
//    //}

//    //private void ApplyInversion(bool invert)
//    //{
//    //    if (freeLookCamera != null)
//    //    {
//    //        if (invertXAxis)
//    //            freeLookCamera.m_XAxis.m_InvertInput = invert;

//    //        if (invertYAxis)
//    //            freeLookCamera.m_YAxis.m_InvertInput = invert;
//    //    }
//    //}
//}
