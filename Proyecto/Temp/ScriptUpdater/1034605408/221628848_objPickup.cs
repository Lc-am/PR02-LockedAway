//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class objPickup : MonoBehaviour
//{
//    public GameObject crosshair1, crosshair2;
//    public Transform objTransform, cameraTrans;
//    public bool interactable, pickedup;
//    public Rigidbody objRigidbody;
//    public float throwAmount;

//    void OnTriggerStay(Collider other)
//    {
//        if (other.CompareTag("MainCamera"))
//        {
//            crosshair1.SetActive(false);
//            crosshair2.SetActive(true);
//            interactable = true;
//        }
//    }
//    void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("MainCamera"))
//        {
//            if (pickedup == false)
//            {
//                crosshair1.SetActive(true);
//                crosshair2.SetActive(false);
//                interactable = false;
//            }
//            if (pickedup == true)
//            {
//                objTransform.parent = null;
//                objRigidbody.useGravity = true;
//                crosshair1.SetActive(true);
//                crosshair2.SetActive(false);
//                interactable = false;
//                pickedup = false;
//            }
//        }
//    }
//    void Update()
//    {
//        if (interactable == true)
//        {
//            if (Input.GetMouseButtonDown(0))
//            {
//                objTransform.parent = cameraTrans;
//                objRigidbody.useGravity = false;
//                pickedup = true;
//            }
//            if (Input.GetMouseButtonUp(0))
//            {
//                objTransform.parent = null;
//                objRigidbody.useGravity = true;
//                pickedup = false;
//            }
//            if (pickedup == true)
//            {
//                if (Input.GetMouseButtonDown(1))
//                {
//                    objTransform.parent = null;
//                    objRigidbody.useGravity = true;
//                    objRigidbody.linearVelocity = cameraTrans.forward * throwAmount * Time.deltaTime;
//                    pickedup = false;
//                }
//            }
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objPickup : MonoBehaviour
{
    public GameObject crosshair1, crosshair2;
    public Transform objTransform, cameraTrans;
    public bool interactable, pickedup;
    public Rigidbody objRigidbody;
    public float throwAmount;
    public float pickupDistance = 2f;  // Distancia a la que el objeto se mantendr� de la c�mara

    private Vector3 offset;  // Offset entre el objeto y la c�mara

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            crosshair1.SetActive(false);
            crosshair2.SetActive(true);
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (pickedup == false)
            {
                crosshair1.SetActive(true);
                crosshair2.SetActive(false);
                interactable = false;
            }
            if (pickedup == true)
            {
                objTransform.parent = null;
                objRigidbody.useGravity = true;
                crosshair1.SetActive(true);
                crosshair2.SetActive(false);
                interactable = false;
                pickedup = false;
            }
        }
    }

    void Update()
    {
        if (interactable == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                objTransform.parent = cameraTrans;
                objRigidbody.useGravity = false;  // Desactivamos la gravedad
                offset = objTransform.localPosition;  // Guardamos el offset inicial entre la c�mara y el objeto
                pickedup = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                objTransform.parent = null;
                objRigidbody.useGravity = true;  // Volvemos a activar la gravedad
                pickedup = false;
            }
            if (pickedup == true)
            {
                // Actualizamos la posici�n del objeto para que siga a la c�mara
                objTransform.localPosition = offset + cameraTrans.forward * pickupDistance;

                if (Input.GetMouseButtonDown(1))
                {
                    objTransform.parent = null;
                    objRigidbody.useGravity = true;
                    objRigidbody.linearVelocity = cameraTrans.forward * throwAmount;  // Lanzamiento
                    pickedup = false;
                }
            }
        }
    }
}
