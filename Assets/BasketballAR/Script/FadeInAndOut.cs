using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInAndOut : MonoBehaviour
{
    public Image[] images;
    public float fadeInTime = 2.0f;
    public float displayTime = 3.0f;
    public float fadeOutTime = 2.0f;

    private void Start()
    {
        foreach (Image image in images)
        {
            StartCoroutine(FadeInOutRoutine(image));
        }
    }

    private IEnumerator FadeInOutRoutine(Image image)
    {
        Color color = image.color;
        color.a = 0;
        image.color = color;
        float elapsedTime = 0;
        while (elapsedTime < fadeInTime)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeInTime);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 1;
        image.color = color;
        yield return new WaitForSeconds(displayTime);
        elapsedTime = 0;
        while (elapsedTime < fadeOutTime)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeOutTime);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 0;
        image.color = color;
    }
}
