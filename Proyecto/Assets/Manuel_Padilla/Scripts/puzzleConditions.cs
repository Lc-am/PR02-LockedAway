using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class puzzleConditions : NetworkBehaviour
{
    public UnityEvent puzzlesEvent; //Añadir mas lineas si se quiere que sean diferentes


    //Funciones de cada evento llamando al respectivo
    //Para asignarlos se hace desde el editor
    //
    //Un gameobject por ejemplo una placa en el suelo llamara a este evento y funcion
    //Este evento hace de intermediario, el objeto que tiene que hacer la interaccion tiene un script con lo que tiene que hacer, esa funcion tiene que ser publica
    //Desde el editor, dentro del gamebject que tenga el script de este evento se tiene que añadir un evento
    //Se selecciona el gamobject que tiene que hacer la interaccion, y despues en la barra de la derecha escoges la funcion que estaba publica
    public void openTheDoor()
    {
        puzzlesEvent.Invoke();
    }
}
