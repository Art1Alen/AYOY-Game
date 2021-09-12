using UnityEngine;
using System;

public class BollController : MonoBehaviour
{
    [SerializeField] private float _thrust = 150f;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _wallDistance = 5f;
    [SerializeField] private float _minCamDistance = 3f;

    private Vector2 _lastMousePos;

    void Update()
    {
        if (GameManager.singleton.GameEnded)
            return;

        Vector2 deltaPos = Vector2.zero;

        if (Input.GetMouseButton(0))
        {
            if (!GameManager.singleton.GameStarted)
                GameManager.singleton.StartGame();

            Vector2 currentMousePos = Input.mousePosition;

            if (_lastMousePos == Vector2.zero)
                _lastMousePos = currentMousePos;

            deltaPos = currentMousePos - _lastMousePos;
            _lastMousePos = currentMousePos;

            Vector3 force = new Vector3(deltaPos.x, 0, deltaPos.y) * _thrust;
            _rb.AddForce(force);
        }
        else
        {
            _lastMousePos = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.singleton.GameEnded)
            return;
        if (GameManager.singleton.GameStarted)
        {
            _rb.MovePosition(transform.position + Vector3.forward * 5 * Time.fixedDeltaTime);
        }
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;

        if(transform.position.x < - _wallDistance)
        {
            pos.x = - _wallDistance;
        }
        // DO TO надо подумать камера заднего вида типа 
        //if (transform.position.z < Camera.main.transform.position.z + _minCamDistance)
        //{
        //    pos.z = Camera.main.transform.position.z + _minCamDistance;
        //}
        else if (transform.position.x > _wallDistance)
        {
            pos.x = _wallDistance;
        }     

        transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.singleton.GameEnded)
            return;

        if (collision.gameObject.tag == "Death")
            GameManager.singleton.EndGame(false);
    }
}
