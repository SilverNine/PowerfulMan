using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour, IGameObject
{
    [SerializeField]
    private Rigidbody2D _rigidBody = null;

    [SerializeField]
    private float _jumpValue = 0.2f;

    private Vector3 _startPosition = Vector3.zero;
    private Quaternion _startRotation = Quaternion.identity;

    private void Awake()
    {
        _startPosition = transform.position;
        _startRotation = Quaternion.identity;
    }

    public void Init()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
    }

    public void FreezePositionY(bool value)
    {
        if (value)
        {
            _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }
    }

    private void Start()
    {
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    public void GameUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _rigidBody.AddForce(new Vector2(0, _jumpValue));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log (collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Manager.Instance.isPlay = false;
                break;
        }
    }
}
