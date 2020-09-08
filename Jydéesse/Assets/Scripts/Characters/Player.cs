using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 1f;

    [SerializeField]
    private float m_stopDistance = 5f;

    private Vector3 m_destination;

    public Vector3 Destination { get { return m_destination; } set { m_destination = value; } }

    private void Awake()
    {
        m_destination = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, m_destination) >= m_stopDistance)
            transform.position += (m_destination - transform.position).normalized * m_speed * Time.fixedDeltaTime;
    }
}
