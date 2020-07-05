using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera _camera;
    private Ray _ray;
    private RaycastHit[] _hits;
    private Vector3 _centerPoint;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    public void Shoot()
    {
        _centerPoint = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0.0f);
        _ray = _camera.ScreenPointToRay(_centerPoint);
        _hits = Physics.RaycastAll(_ray);

        Debug.Log("Shoot");
        foreach (RaycastHit hit in _hits)
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("EnemyHited");
                hit.collider.gameObject.GetComponent<Character>().TakeHit();
                return;
            }
        }
    }
}
