using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public bool dontDestroyOnTrigger = false;

    [Space]
    public UnityEvent onDialogTrigger;
    public UnityEvent onDialogEnd;

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue, onDialogEnd);

        onDialogTrigger.Invoke();

        if (!dontDestroyOnTrigger)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }
}
