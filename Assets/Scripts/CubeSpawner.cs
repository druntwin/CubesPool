using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapasity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private int _minLifeTime = 2;
    private int _maxLifeTime = 5;
    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>
            (
            createFunc: () => CreateFunc(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => DestroyCube(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapasity,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    private GameObject CreateFunc()
    {
        GameObject obj = Instantiate(_cubePrefab);
        obj.TryGetComponent(out Cube cube);
        cube.OnCollided += ReleaseCube;

        return obj;
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void ActionOnGet(GameObject obj)
    {
        int areaDivider = 2;

        _spawnArea.TryGetComponent(out MeshRenderer spawnAreaRenderer);
        Vector3 spawnAreaSize = spawnAreaRenderer.bounds.size;
        float minXPos = _spawnArea.transform.position.x - spawnAreaSize.x / areaDivider;
        float maxXPos = _spawnArea.transform.position.x + spawnAreaSize.x / areaDivider;
        Vector3 startPosition = new Vector3(Random.Range(minXPos, maxXPos),
                                            _spawnArea.transform.position.y,
                                            _spawnArea.transform.position.z);

        obj.transform.position = startPosition;
        obj.TryGetComponent(out Rigidbody rigidbody);
        rigidbody.velocity = Vector3.zero;
        obj.SetActive(true);
    }


    private void ReleaseCube(GameObject obj)
    {
        if(_pool.CountInactive > _poolMaxSize)
        {
            DestroyCube(obj);
        }
        else
        {
            StartCoroutine(DelayedDisappear(obj));
        }
    }

    private void DestroyCube(GameObject obj)
    {
        obj.TryGetComponent(out Cube cube);
        cube.OnCollided -= ReleaseCube;
        Destroy(obj);
    }

    IEnumerator DelayedDisappear(GameObject obj)
    {
        float randomLifeTime = Random.Range(_minLifeTime, _maxLifeTime);

        yield return new WaitForSeconds(randomLifeTime);

        obj.TryGetComponent(out Cube cube);
        cube.SetDefaultState();
        _pool.Release(obj);
    }
}
