using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;

[RequireComponent(typeof(Rigidbody))]
public class BallControl : MonoBehaviour
{
    public float m_ThrowForce = 10f;
    public float m_ThrowDirectionX = 0.17f;
    public float m_ThrowDirectionY = 0.67f;

    public Vector3 m_BallCameraOffset = new Vector3(0, -0.4f, 1f);

    private Vector3 startPosition;
    private Vector3 direction;
    private float startTime;
    private float endTime;
    private float duration;
    private bool isDirectionChosen = false;
    private bool isThrowingStarted = false;

    [SerializeField] private GameObject XRCam;
    [SerializeField] private XROrigin m_XROrigin;

    private Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        m_XROrigin = GameObject.Find("XR Origin").GetComponent<XROrigin>();
        XRCam = m_XROrigin.transform.Find("Camera Offset/Main Camera").gameObject;
        transform.parent = XRCam.transform;
        ResetBall();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            startPosition = Input.mousePosition;
            startTime = Time.time;
            isThrowingStarted = true;
            isDirectionChosen = false;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
            duration = endTime - startTime;
            direction = Input.mousePosition - startPosition;
            isDirectionChosen = true;
        }

        if (isDirectionChosen)
        {
            rb.mass = 1;
            rb.useGravity = true;

            rb.AddForce(XRCam.transform.forward * m_ThrowForce / duration
                       + XRCam.transform.up * direction.y * m_ThrowDirectionY
                       + XRCam.transform.right * direction.x * m_ThrowDirectionX);

            startTime = 0f;
            endTime = 0f;
            duration = 0f;

            startPosition = new Vector3(0, 0, 0);
            direction = new Vector3(0, 0, 0);

            isThrowingStarted = false;
            isDirectionChosen = false;
        }

        if (Time.time - endTime >= 5 && Time.time - endTime <= 6)
        {
            ResetBall();
        }

        if (Vector3.Distance(transform.position, XRCam.transform.position) > 20f)
        {
            ResetBall();
            return;
        }
    }

    public void ResetBall()
    {
        rb.mass = 0;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        endTime = 0f;

        Vector3 ballPos = XRCam.transform.position + XRCam.transform.forward * m_BallCameraOffset.z + XRCam.transform.up * m_BallCameraOffset.y;
        transform.position = ballPos;
    }
}

