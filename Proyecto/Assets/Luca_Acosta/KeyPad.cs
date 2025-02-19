using TMPro;
using UnityEngine;

public class KeyPad : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private string Answer = "123456";
    private bool canInput = true;  
    private float blockTime = 2f;   
    private float timeRemaining;

    [SerializeField] puzzleConditions puzzleConditions;

    public void Number(int number)
    {
        if (canInput)  
        {
            text.text += number.ToString();
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
