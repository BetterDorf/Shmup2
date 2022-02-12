using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawnPoint;

    private List<GameObject> _availableSpawnPoint;

    public void ResetSpawn()
    {
        _availableSpawnPoint = _spawnPoint;
    }

    public void Spawn(ref List<GameObject> enemyList, int numbers, GameObject enemyGameObject)
    {
        for (int i = 0; i < numbers; i++)
        {
            if (_availableSpawnPoint.Count > 0)
            {
                var RandomSpawnListNumber = Random.Range(0, _availableSpawnPoint.Count);
                enemyList.Add(Instantiate(enemyGameObject,
                    _availableSpawnPoint[RandomSpawnListNumber].transform.position, Quaternion.identity));
                _availableSpawnPoint.RemoveAt(RandomSpawnListNumber);
            }
            else
            {
                Debug.Log("too much enemy!");
            }
        }

    }
}
