using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceAnimator : MonoBehaviour
{
    List<Animator> animators;
    public float waitBetween = 0.15F;
    public float waitEnd = 0.2F;

    // Start is called before the first frame update
    void Start()
    {
        animators = new List<Animator>(GetComponentsInChildren<Animator>());
        StartCoroutine(doAnimation());
    }

    IEnumerator doAnimation()
    {
        while(true)
        {
            foreach (var animator in animators)
            {
                animator.SetTrigger("DoAnimation");
                yield return new WaitForSeconds(waitBetween);
            }

            yield return new WaitForSeconds(waitEnd);
        }
    }
}
