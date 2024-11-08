using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    private bool _isCollided = false;
    private Color _defaultColor;

    public delegate void CollideDelegate(GameObject gameObject);
    public event CollideDelegate OnCollided;

    private void Start()
    {
        _defaultColor = _renderer.material.color;
    }

    public void SetDefaultState()
    {
        _isCollided = false;
        _renderer.material.color = _defaultColor;
    }

    private void ChangeColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollided == false)
        {
            ChangeColor();
            OnCollided?.Invoke(gameObject);
            _isCollided = true;
        }
    }
}
