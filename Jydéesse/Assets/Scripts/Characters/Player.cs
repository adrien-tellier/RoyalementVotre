using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    // Statistics
    [SerializeField]
    float m_money;
    [SerializeField]
    float m_satifaction;
    [SerializeField]
    float m_food;
    [SerializeField]
    float m_time;

    // Statistic events
    public UnityEvent e_foodChanged;
    public UnityEvent e_moneyChanged;
    public UnityEvent e_satisfactionChanged;
    public UnityEvent e_timeChanged;
    public UnityEvent e_occupiedStatusChanged;

    // Sneaky bad coding variables
    private bool m_isOccupied;
    private bool m_isMoving = true;

    private bool m_isOnQuest = false;

    // Movement
    [SerializeField]
    private float m_speed = 1f;

    [SerializeField]
    private float m_stopDistance = 5f;

    private float m_destination;

    //Animations
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;

    public float Destination { get { return m_destination; } set { m_destination = value; m_isMoving = true; } }
    public bool IsMoving { get { return m_isMoving; } set { m_isMoving = value; } }
    public bool IsOnQuest { get { return m_isOnQuest; } set { m_isOnQuest = value; } }

    // Statistic getter
    public float getFood() {return m_food;}
    public float getMoney() {return m_money;}
    public float getSatisfaction() {return m_satifaction;}
    public float getTime() {return m_time;}
    public bool getOccupiedStatus() {return m_isOccupied;}
    public void setOccupiedStatus(bool b)
    {
        m_isOccupied = b;
        e_occupiedStatusChanged.Invoke();
    }

    // the 4 function that are used to change stats, invoke event for observer to adapt (change display)
    public void addMoney(float moneyAdded)
    {
        m_money += moneyAdded;
        if (m_money < 0f)
            m_money = 0f;
        e_moneyChanged.Invoke();
    }
    public void addSatifaction(float satisfactionAdded)
    {
        m_satifaction += satisfactionAdded;
        if (m_satifaction < 0f)
            m_satifaction = 0f;
        e_satisfactionChanged.Invoke();
    }
    public void addFood(float foodAdded)
    {
        m_food += foodAdded;
        if (m_food < 0f)
            m_food = 0f;
        e_foodChanged.Invoke();
    }
    public void addTime(float timeAdded)
    {
        m_time += timeAdded;
        if (m_time < 0f)
            m_time = 0f;
        e_timeChanged.Invoke();
    }

    private void Awake()
    {
        m_destination = transform.position.x;
        m_animator = gameObject.GetComponentInChildren<Animator>();
        m_spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(m_destination - transform.position.x) >= m_stopDistance)
        {
            Vector3 direction = new Vector3((m_destination - transform.position.x),0f,0f).normalized;
            transform.position += direction * m_speed * Time.fixedDeltaTime;

            if (direction.x < 0)
                m_spriteRenderer.flipX = true;
            else
                m_spriteRenderer.flipX = false;
        }
        else if (m_isMoving)
            m_isMoving = false;

        m_animator.SetBool("IsMoving", m_isMoving);
        m_animator.SetBool("IsSpeaking", m_isOccupied && !m_isMoving);
    }
}
