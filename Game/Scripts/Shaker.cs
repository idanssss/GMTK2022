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
        float time = 0f;
        Vector2 startPos = obj.transform.position;

        while (time < duration)
        {
            Vector2 pos = obj.transform.position;
            obj.transform.position = new Vector2(pos.x + Random.Range(-0.1f, 0.1f) * strength, pos.y + Random.Range(-0.1f, 0.1f) * strength);
            float halfDelta = Time.deltaTime / 2;
            yield return new WaitForSeconds(halfDelta);

            obj.transform.position = startPos;
            yield return new WaitForSeconds(halfDelta);
            
            time += Time.deltaTime;
        }
        
        obj.transform.position = startPos;
        onEnd();
    }
}