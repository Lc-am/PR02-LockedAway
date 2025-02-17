using TMPro;
using UnityEngine;

public class KeyPad : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMeshPro;
    public void Number(int number)
    {
        _textMeshPro.text += number.ToString();
    }
}
