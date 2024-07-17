using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    private bool _isColorChanged = false;
    private int _minLifeTime = 2;
    private int _maxLifeTime = 5;


    private void ChangeColor()
    {
        _renderer.material.color = Random.ColorHSV();
        _isColorChanged = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isColorChanged == false)
            ChangeColor();
    }

    private IEnumerator CreateCubes()
    {
        var waitSomeSeconds = new WaitForSecondsRealtime(Random.Range(_minLifeTime, _maxLifeTime));

        yield return waitSomeSeconds;

        
    }
}
