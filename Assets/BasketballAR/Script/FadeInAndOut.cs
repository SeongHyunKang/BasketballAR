using UnityEngine;
using System.Collections;

public class FadeInAndOut : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    public float fadeInTime = 2.0f;
    public float displayTime = 3.0f;
    public float fadeOutTime = 2.0f;

    private void Start()
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            StartCoroutine(FadeInOutRoutine(sprite));
        }
    }

    private IEnumerator FadeInOutRoutine(SpriteRenderer sprite)
    {
        Color color = sprite.color;
        color.a = 0;
        sprite.color = color;
        float elapsedTime = 0;
        while (elapsedTime < fadeInTime)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeInTime);
            sprite.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 1;
        sprite.color = color;
        yield return new WaitForSeconds(displayTime);
        elapsedTime = 0;
        while (elapsedTime < fadeOutTime)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeOutTime);
            sprite.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 0;
        sprite.color = color;
    }
}