using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaemonTools;
using UnityEngine.Events;

public class DialogueManager : Singleton<DialogueManager>
{
    private List<string> m_dialogues = new List<string>();
    private const float m_waitforseconds = 1.0f;
    public event UnityAction DialogueEnd;

    public delegate void DiologueNextHandler(string dialogue);
    public event DiologueNextHandler DialogueNext;

    public void AddDialogues(string dialogue)
    {
        if (m_dialogues.Count > 0)
        {
            m_dialogues.Add(dialogue);

        }
        else
        {
            m_dialogues.Add(dialogue);
            UIManager.Instance.Open("DialoguePanel", false, m_dialogues[0]);
            Daemon.Instance.StartCoroutine(DialoguesLine());
        }
    }

    IEnumerator DialoguesLine()
    {
        while (m_dialogues.Count > 0)
        {
            yield return new WaitForSeconds(m_waitforseconds);
            while (true)
            {
                if (Input.GetButtonDown("Each Other"))
                {
                    m_dialogues.RemoveAt(0);
                    if (m_dialogues.Count > 0)
                    {
                        if (DialogueNext != null)
                            DialogueNext.Invoke(m_dialogues[0]);
                        UIManager.Instance.Open("DialoguePanel", false, m_dialogues[0]);

                    }
                    else
                    {
                        if (DialogueEnd != null)
                            DialogueEnd.Invoke();
                        UIManager.Instance.close("DialoguePanel");
                    }
                    break;
                }
                yield return null;
            }

        }
    }
}
