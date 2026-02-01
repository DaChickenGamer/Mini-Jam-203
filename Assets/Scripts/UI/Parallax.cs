using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    // Had to switch to hard-coded for now
    private float _length = 17.856f;
    private float _startpos = 0;
    public GameObject cam;
    public float parallaxEffect;

    void Update()
    {
        float distanceMoved = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(_startpos + distanceMoved, transform.position.y, transform.position.z);

        if (cam.transform.position.x * (1 - parallaxEffect) > _startpos + _length)
        {
            _startpos += _length;
        }
        else if (cam.transform.position.x * (1 - parallaxEffect) < _startpos - _length)
        {
            _startpos -= _length;
        }
    }
}
