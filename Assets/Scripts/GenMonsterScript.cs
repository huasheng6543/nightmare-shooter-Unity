using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenMonsterScript : MonoBehaviour
{
    public GameObject[] monsters;
    public float minSpawnRadius = 6f;   // 刷新圆环的最小半径
    public float maxSpawnRadius = 12f;  // 刷新圆环的最大半径
    private float timer = 3;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            int index = Random.Range(0, monsters.Length);
            // 在玩家周围的圆环上随机取点，避免怪物全部叠在原点
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
            Vector3 spawnPosition = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
            Instantiate(monsters[index], spawnPosition, Quaternion.identity);
            timer = 3;
        }
    }
}
