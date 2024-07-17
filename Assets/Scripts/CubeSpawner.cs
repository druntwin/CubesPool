using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;


    //private IEnumerator CreateCubes()
    //{
    //    int spawnDelay;
    //    var waitSomeSeconds = new WaitForSecondsRealtime(Random.Range(_minSpawnDelay, _maxSpawnDelay));

    //    yield return waitSomeSeconds;

    //    for (int i = 0; i < _diamondsMaxCount; i++)
    //    {
    //        float spawnPositionX = Random.Range(_spawnMinPositionX, _spawnMaxPositionX);
    //        float spawnPositionZ = Random.Range(_spawnMinPositionZ, _spawnMaxPositionZ);
    //        int spawnPositionY = 0;

    //        Diamond diamond = Instantiate(_diamondPrefab, new Vector3(spawnPositionX, spawnPositionY, spawnPositionZ), _diamondPrefab.transform.rotation, transform);
    //        _diamonds.Enqueue(diamond);

    //        yield return waitSomeSeconds;
    //    }
    //}
}
