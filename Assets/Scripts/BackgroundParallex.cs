using UnityEngine;
using UnityEngine.UI;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private RawImage _img;

    // Reference to player or camera
    [SerializeField] private Transform _target; 

    // How strong the parallax effect is
    [SerializeField] private float _parallaxX = 0.1f;
    [SerializeField] private float _parallaxY = 0.1f;

    private Vector3 _lastPos;

    void Start()
    {
        _lastPos = _target.position;
    }

    void Update()
    {
        // Calculate movement since last frame
        Vector3 delta = _target.position - _lastPos;

        // Convert movement into UV offset
        // NOTE: swapped x and y due to weird but that applied rotated material
        Vector2 uvOffset = new Vector2(
            delta.y * _parallaxX,
            delta.x * _parallaxY
        );

        // Apply offset
        _img.uvRect = new Rect(
            _img.uvRect.position + uvOffset,
            _img.uvRect.size
        );

        _lastPos = _target.position;
    }
}