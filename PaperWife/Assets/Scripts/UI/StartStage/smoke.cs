using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke : MonoBehaviour
{

    [SerializeField]List<Animator> animators;
    [SerializeField]List<float> time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SA());
        for (int i = 0; i < animators.Count; i++)
        {
            animators[i].speed = 0;
        }
    }

    IEnumerator SA()
    {
        for (int i = 0; i < animators.Count; i++)
        {
            yield return new WaitForSeconds(time[i]);
            animators[i].speed = 1;
        }
        yield return 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
