using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    float _actionTime;
    Animator _animation;
    float _timer;

    private void Start()
    {
        _actionTime = Random.Range(1f, 2.5f);
    }

    private void Awake()
    {
        _animation = GetComponent<Animator>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _actionTime)
        {
            _animation.Play("trap");
            _timer = 0;
            _actionTime = Random.Range(1f, 2.5f);
        }
    }
}
