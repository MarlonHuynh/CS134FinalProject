using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour
{
    public GameObject target;
    public float flickerDuration = 0.5f;
    public float minDelay = 3f;
    public float maxDelay = 9f;

    private Coroutine flickerCoroutine;

    void OnEnable()
    {
        if (target == null)
            target = gameObject;

        flickerCoroutine = StartCoroutine(FlickerRoutine());
    }

    void OnDisable()
    {
        if (flickerCoroutine != null)
            StopCoroutine(flickerCoroutine);
    }

    IEnumerator FlickerRoutine()
    {
        target.SetActive(false);

        while (true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(minDelay, maxDelay));

            target.SetActive(true);
            yield return new WaitForSecondsRealtime(flickerDuration);

            target.SetActive(false);
        }
    }
}