﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;

    public Transform groundDetectPos;

    float speed;
    bool isRun;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
      Movement();
      Footstep();
    }

    void Movement()
    {

        #region move
        if (isRun)
            speed = Mathf.Lerp(speed,runSpeed,.07f);
        else
            speed = Mathf.Lerp(speed, walkSpeed, .07f);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 rot = transform.TransformDirection(new Vector3(x,0,z)).normalized;

        rb.position += rot * speed * Time.deltaTime;
        #endregion

        #region jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            StartCoroutine(jump());
        }
        #endregion
        //ground Detect
        isGrounded = groundDetector();

        #region run
        if (Input.GetKey(KeyCode.LeftShift))
            isRun = true;
        else
            isRun = false;
        #endregion
        //Funcs
        bool groundDetector()
        {

            Collider[] colls = Physics.OverlapSphere(groundDetectPos.position, .1f);
            foreach (Collider coll in colls)
            {
                if (coll.gameObject.tag == "ground")
                {
                    return true;
                }
            }
            return false;
        }
        IEnumerator jump()
        {
            yield return new WaitForSeconds(.15f);
            rb.AddForce(transform.up * jumpForce);
        }

    }
}
