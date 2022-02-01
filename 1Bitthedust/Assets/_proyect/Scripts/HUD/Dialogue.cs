using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public DialogueData dialogue;
    public Canvas canvasController;
    public Canvas canvasText;

    public bool dialogueEnabled;

    public void Update()
    {
        if (dialogueEnabled)
        {
            if (Input.GetKey(KeyCode.E))
            {
                canvasController.GetComponent<DialogueController>().StartDialogue(dialogue);
            }
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        dialogueEnabled = true;
        canvasText.enabled = true;

        canvasText.transform.position = new Vector2(transform.position.x, transform.position.y + 0.2f);
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        dialogueEnabled = false;
        canvasController.enabled = false;
        canvasText.enabled = false;
    }
}
