using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Sprite cherrySprite;  
    private float spawnInterval = 10f;  
    private float moveSpeed = 5f;  
    private Vector2 levelCenter;  

    private void Start()
    {
        
        levelCenter = Vector2.zero;

        
        InvokeRepeating("SpawnCherry", spawnInterval, spawnInterval);
    }

    private void SpawnCherry()
    {
        Vector2 startPoint = GetRandomStartPoint();
        Vector2 endPoint = GetEndPointFromStart(startPoint);

        GameObject cherryInstance = new GameObject("Cherry");
        SpriteRenderer sr = cherryInstance.AddComponent<SpriteRenderer>();
        sr.sprite = cherrySprite;
        cherryInstance.transform.position = startPoint;

        StartCoroutine(MoveCherry(cherryInstance, endPoint));
    }

    private Vector2 GetRandomStartPoint()
    {
        float randomX = Random.Range(-1.1f, 1.1f);
        float randomY = Random.Range(-1.1f, 1.1f);

        if (Mathf.Abs(randomX) > Mathf.Abs(randomY))
        {
            return new Vector2(Mathf.Sign(randomX) * Camera.main.aspect, randomY);
        }
        else
        {
            return new Vector2(randomX, Mathf.Sign(randomY));
        }
    }

    private Vector2 GetEndPointFromStart(Vector2 startPoint)
    {
        Vector2 direction = (levelCenter - startPoint).normalized;
        float maxDistance = 2f * Camera.main.aspect + 2f;  
        return startPoint + direction * maxDistance;
    }

    private System.Collections.IEnumerator MoveCherry(GameObject cherry, Vector2 endPoint)
    {
        float journeyLength = Vector2.Distance(cherry.transform.position, endPoint);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * moveSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            cherry.transform.position = Vector2.Lerp(cherry.transform.position, endPoint, fractionOfJourney);
            yield return null;
        }

        Destroy(cherry);
    }
}
