using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform _spawnLocation;
    [SerializeField] float _speed;
    [SerializeField] Animator _canvas;
    Animator _animator;
    AudioSource _audioSource;
    bool _isPlaying = false;
    bool _Win;
    bool _isDead = false;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Ragdoll(false);
    }

    private void Update()
    {
        if (!_isDead)
        {
            if (Input.GetMouseButton(0))
            {
                _isPlaying = true;
                _animator.SetBool("walking", true);
            }
        }

        else if (_isDead)
        {
            Dead();
        }

        if (_Win)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                this.transform.position = _spawnLocation.position;
                _Win = false;
            }
        }

    }

    private void FixedUpdate()
    {
        if (_isPlaying)
        {
            transform.Translate(transform.right * (-_speed) * Time.deltaTime);
            _isPlaying = false;
            _animator.SetBool("walking", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Trap"))
        {
            if (!_isDead)
            {
                Debug.Log("lose");
                _isDead = true;
            }
        }

        else if (other.tag.Equals("Finish"))
        {
            Debug.Log("win");
            _Win = true;
        }
    }

    void Dead()
    {
        _audioSource.Play();
        Ragdoll(true);
        _canvas.gameObject.SetActive(true);
        _canvas.Play("died");
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.transform.position = _spawnLocation.position;
            _canvas.gameObject.SetActive(false);
            _isDead = false;
            Ragdoll(false);
            _audioSource.Stop();
        }
    }

    public void Ragdoll(bool active)
    {
        _animator.enabled = !active;

        Rigidbody[] rigidbodies = transform.GetChild(0).GetComponentsInChildren<Rigidbody>();
        Collider[] colliders = transform.GetChild(0).GetComponentsInChildren<Collider>();

        foreach (Rigidbody rig in rigidbodies)
            rig.isKinematic = !active;

        foreach (Collider col in colliders)
            col.enabled = active;

        GetComponent<Collider>().enabled = !active;
    }


}
