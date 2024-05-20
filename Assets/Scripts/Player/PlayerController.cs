using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float _timerJump = 1.2f;
    private float _timerSides = 0.4f;


    private bool _cancelSideMoveInJump;

    public int _sidesMoveForce = 1;
    private int _jumpForce = 50;
    public float speed;
    public Rigidbody rb;
    private bool _forceDown;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.deltaTime;



        if (!_cancelSideMoveInJump)
        {
            _timerSides -= Time.deltaTime;
            if (_timerSides <= 0)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    rb.velocity = Vector3.zero;
                    rb.AddForce(0, 0, _sidesMoveForce, ForceMode.Impulse);
                    _timerSides = 0.4f;
                    _timerJump = 0.5f;

                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    rb.velocity = Vector3.zero;
                    rb.AddForce(0, 0, -_sidesMoveForce, ForceMode.Impulse);
                    _timerSides = 0.4f;
                    _timerJump = 0.5f;
                }
            }
        }

        _timerJump -= Time.deltaTime;
        {
            if (_timerJump <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(0, _jumpForce * 2, 0, ForceMode.Impulse);
                    _timerJump = 0.8f;
                    _cancelSideMoveInJump = true;
                }
            }
        }

        if (transform.position.y >= 2.2)
        {
            _forceDown = true;
            _cancelSideMoveInJump = true;
        }

        if (_forceDown)
        {
            rb.AddForce(0, -_jumpForce * 6f, 0, ForceMode.Force);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + 400f, transform.position.y, transform.position.z));
    }

    public void OnCollisionEnter(Collision collision)
    {
        //layer "0" == ground
        if (collision.gameObject.layer == 0)
        {
            //rb.mass = 10;
            //rb.drag = 1;
            _forceDown = false;
            _cancelSideMoveInJump = false;
        }

        //layer "10" == Obstacles
        if (collision.gameObject.layer == 10)
        {
            SceneManager.LoadScene("LevelOne");
        }
    }

    
}
