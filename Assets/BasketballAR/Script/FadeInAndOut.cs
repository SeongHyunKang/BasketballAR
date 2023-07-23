using UnityEngine;
using System.Collections;

public class FadeInAndOut : MonoBehaviour
{
    public GameObject spritePrefab;
    private SpriteRenderer spriteRenderer;
    public float fadeInTime = 2.0f;
    public float displayTime = 3.0f;
    public float fadeOutTime = 2.0f;

    private void OnEnable()
    {
        if (spritePrefab != null)
        {
            GameObject spriteObject = Instantiate(spritePrefab, transform.position, Quaternion.identity);
            spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
            StartCoroutine(FadeInOutRoutine());
        }
    }

    private IEnumerator FadeInOutRoutine()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        float elapsedTime = 0;

        while (elapsedTime < fadeInTime)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeInTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);

        yield return new WaitForSeconds(displayTime);
        elapsedTime = 0;

        while (elapsedTime < fadeOutTime)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeOutTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
    }
}



