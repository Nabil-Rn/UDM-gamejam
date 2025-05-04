using UnityEngine;
using System.Collections;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI charName;
    public string[] lines;
    public string[] names;
    public float textSpeed;
    public bool isFinished = false; // Added to track if dialogue is finished
     // Assuming you have a script class that handles the fade screen

    public int index;

    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked!"); // Debugging line to check if the click is registered
            // No stray semicolon here!
            if (dialogueText.text == lines[index])
            {
                NextLine();            // spelling matches method
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }

    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
        
    }

    IEnumerator TypeLine()
    {
        charName.text = names[index]; // Assuming you want to set the character name here
        dialogueText.text = "";
        foreach (char letter in lines[index].ToCharArray() )
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    //If index 3 (meaning third bit of text), this is the name of the character.

    // Moved _inside_ the class, spelling corrected
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            isFinished = true; // Set to true when dialogue is finished
            gameObject.SetActive(false);
            // End of dialogue
        }
    }
}
