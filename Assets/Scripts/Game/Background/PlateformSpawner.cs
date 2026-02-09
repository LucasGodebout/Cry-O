using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] platformPrefabs;

    [Header("Spawn Zone")]
    public float minX = -2.5f;
    public float maxX = 2.5f;

    [Header("Vertical Spacing")]
    public float minYGap = 1.5f;
    public float maxYGap = 3f;

    [Header("References")]
    public Transform player;
    public Transform background;
    
    private Camera mainCamera;
    private float lastSpawnY;
    private float lastSpawnX;
    private float minXDistance = 2f;

    private List<GameObject> activePlatforms = new List<GameObject>();

    void Start()
    {
        mainCamera = Camera.main;
        lastSpawnY = player.position.y;
        lastSpawnX = 0f;
    }

    void Update()
    {
        if (player.position.y + 15f > lastSpawnY)
        {
            SpawnPlatform();
        }

        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            GameObject platform = activePlatforms[i];
            
            if (platform == null) {
                activePlatforms.RemoveAt(i);
                continue;
            }

            Vector3 viewPos = mainCamera.WorldToViewportPoint(platform.transform.position);
            
            if (viewPos.y < -0.5f && platform.transform.position.y < player.position.y)
            {
                Destroy(platform);
                activePlatforms.RemoveAt(i);
            }
        }
    }

    void SpawnPlatform()
    {
        float randomX = Random.Range(minX, maxX);
        int safety = 0;

        while (Mathf.Abs(randomX - lastSpawnX) < minXDistance && safety < 20)
        {
            randomX = Random.Range(minX, maxX);
            safety++;
        }

        float randomYGap = Random.Range(minYGap, maxYGap);
        lastSpawnY += randomYGap;

        int randomIndex = Random.Range(0, platformPrefabs.Length);
        
        GameObject newPlatform = Instantiate(
            platformPrefabs[randomIndex],
            new Vector3(randomX, lastSpawnY, 0f),
            Quaternion.identity,
            background
        );

        lastSpawnX = randomX;
        newPlatform.transform.localScale = Vector3.one;

        activePlatforms.Add(newPlatform);
    }
}