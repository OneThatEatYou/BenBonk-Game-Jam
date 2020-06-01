using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    #region Singleton

    public static DialogueManager instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Multiple instances of DialogueManager found. Destroying : " + gameObject.name);
            Destroy(gameObject);
        }
    }

    #endregion

    public TextMeshProUGUI dialogueText;

    Queue<string> sentences;

    public float timeBetweenLetter;
    public float timeBeforeEnding;

    public AudioSource source;
    public AudioClip clip;

    [HideInInspector]
    public UnityEvent onDialogueEnd;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, UnityEvent onEnd)
    {
        //Debug.Log("Starting conversation with " + dialogue.name);

        sentences.Clear();

        onDialogueEnd = onEnd;

        //adds dialogue to queue
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            PlayAudio();
            yield return new WaitForSeconds(timeBetweenLetter);
        }

        yield return new WaitForSeconds(timeBeforeEnding);
        DisplayNextSentence();
    }

    void EndDialogue()
    {
        //Debug.Log("End of sentences");

        dialogueText.text = "";

        onDialogueEnd.Invoke();
    }

    void PlayAudio()
    {
        source.clip = clip;
        source.Play();
    }
}
