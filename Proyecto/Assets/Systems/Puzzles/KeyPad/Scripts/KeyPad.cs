using TMPro;
using UnityEngine;

public class KeyPad : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] string Answer = "123456";

    private bool canInput = true;  
    private float blockTime = 2f;   
    private float timeRemaining;
    private int maxDigits = 6;

    [SerializeField] puzzleConditions puzzleConditions;

    public void Number(int number)
    {
        if (canInput && text.text.Length < maxDigits)  
        {
            text.text += number.ToString();
            Debug.Log(number);
        }
    }
    public void DeleteLastNumber()
    {
        if (text.text.Length > 0)  
        {
            text.text = text.text.Remove(text.text.Length - 1);
            Debug.Log("Último número eliminado");
        }
    }
    public void Execute()
    {
        if (text.text == Answer)
        {
            text.text = "Correct";

            puzzleConditions.openTheDoor();

        }
        else
        {
            text.text = "Invalid";
            canInput = false; 
            timeRemaining = blockTime;  
        }
    }

    private void Update()
    {
        if (!canInput)  
        {
            timeRemaining -= Time.deltaTime;  
            if (timeRemaining <= 0) 
            {
                canInput = true;  
                text.text = "";  
            }
        }
    }
}
