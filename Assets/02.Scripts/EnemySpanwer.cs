using UnityEngine;
using System.Collections;
using Redcode.Pools;
using Unity.Mathematics;

public class EnemySpanwer : MonoBehaviour
{
    public PoolManager poolManager;
    public Transform enemySpawnPoint;
    private int _enemyCount = 0;

    private IEnumerator Start()
    {
        while (_enemyCount < 10)
        {
            yield return new WaitForSeconds(3f);

            var enemy = poolManager.GetFromPool<Transform>("Enemy_HoverBot"); 
            enemy.position = enemySpawnPoint.position;
            enemy.rotation = quaternion.identity;
            _enemyCount++;
        }
    }
}
