using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Shaker
{
    public static void Shake(this Transform transform, float duration, float strength, Action onEnd)
    {
        ShakeMono shaker = new GameObject("Shaker").AddComponent<ShakeMono>();
        shaker.ShakeObj(transform.gameObject, duration, strength, onEnd);
    }
}

class ShakeMono : MonoBehaviour
{
    public void ShakeObj(GameObject go, float duration, float strength, Action onEnd) => StartCoroutine(ShakeCoroutine(go, duration, strength, onEnd));

    public IEnumerator ShakeCoroutine(GameObject obj, float duration, float strength, Action onEnd)
    {
        if (!Application.isPlaying)
        {
            onEnd();
            DestroyImmediate(gameObject);
            yield break;
        }

        float time = 0f;
        Vector2 startPos = obj.transform.position;

        while (time < duration)
        {
            var pos = Vector2.zero;
            try { pos = obj.transform.position; } catch { break; }
            
            try
            {
                obj.transform.position = new Vector2(pos.x + Random.Range(-0.1f, 0.1f) * strength,
                    pos.y + Random.Range(-0.1f, 0.1f) * strength);
            }
            catch (MissingReferenceException)
            {
                break;
            }

            float halfDelta = Time.deltaTime / 2;
            yield return new WaitForSeconds(halfDelta);

            try
            {
                obj.transform.position = startPos;
            }
            catch (MissingReferenceException)
            {
                break;
            }

            yield return new WaitForSeconds(halfDelta);

            time += Time.deltaTime;
        }

        try { obj.transform.position = startPos; } catch (MissingReferenceException) { }
        onEnd();
        
        Destroy(gameObject);
    }
}