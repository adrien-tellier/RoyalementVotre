using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Statistics
    [SerializeField]
    float m_money = 0;
    [SerializeField]
    float m_satisfaction = 0;
    [SerializeField]
    public float m_requiredSatisfaction = 0;
    [SerializeField]
    float m_food = 0;
    [SerializeField]
    float m_time = 0;

    [SerializeField]
    private CameraMovement m_camera = null;

    // Statistic events
    public UnityEvent e_foodChanged;
    public UnityEvent e_moneyChanged;
    public UnityEvent e_satisfactionChanged;
    public UnityEvent e_timeChanged;
    public UnityEvent e_occupiedStatusChanged;

    // Sneaky ugly variables
    private bool m_isOccupied;
    private bool m_isMoving = true;

    public string m_actionTargetName;

    private bool m_isOnQuest = false;

    // Movement
    [SerializeField]
    private float m_speed = 1f;

    [SerializeField]
    private float m_stopDistanceX = 1.5f;
    [SerializeField]
    private float m_stopDistanceY = 0.5f;

    [SerializeField]
    public float m_minY;

    [SerializeField]
    public float m_maxY;

    private Vector3 m_destination;

    [SerializeField]
    Vector3[] m_positions = new Vector3[3];

    int m_currentPos;

    //Animations
    [SerializeField]
    private Sprite m_kingSprite;
    [SerializeField]
    private Sprite m_queenSprite;
    [SerializeField]
    private AnimatorController m_kingAnimatorController;
    [SerializeField]
    private AnimatorController m_queenAnimatorController;
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;

    public Vector3 Destination { get { return m_destination; } set { m_destination = value; m_isMoving = true; } }
    public bool IsMoving { get { return m_isMoving; } set { m_isMoving = value; } }
    public bool IsOnQuest { get { return m_isOnQuest; } set { m_isOnQuest = value; } }

    // Statistic getter
    public float getFood() {return m_food;}
    public float getMoney() {return m_money;}
    public float getSatisfaction() {return m_satisfaction;}
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
        m_satisfaction += satisfactionAdded;
        if (m_satisfaction < 0f)
            m_satisfaction = 0f;
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
        m_destination = transform.position;
        m_animator = gameObject.GetComponentInChildren<Animator>();
        m_spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        if (PlayerPrefs.GetInt("IsQueen") == 1)
        {
            m_animator.runtimeAnimatorController = m_queenAnimatorController;
            m_spriteRenderer.sprite = m_kingSprite;
        }
        else
        {
            m_animator.runtimeAnimatorController = m_kingAnimatorController;
            m_spriteRenderer.sprite = m_queenSprite;
        }
    }

    private void Start() 
    {
        m_currentPos = 1;
        m_camera.e_goLeft.AddListener(MoveLeft);
        m_camera.e_goRight.AddListener(MoveRight);
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(m_destination.y - transform.position.y) >= m_stopDistanceY || Mathf.Abs(m_destination.x - transform.position.x) >= m_stopDistanceX)
        {
            Vector3 direction = (m_destination - transform.position);

            if (direction.x < 0)
                m_spriteRenderer.flipX = true;
            else
                m_spriteRenderer.flipX = false;

            if (Mathf.Abs(m_destination.y - transform.position.y) <= m_stopDistanceY)
                direction.y = 0f;
                
            if (Mathf.Abs(m_destination.x - transform.position.x) <= m_stopDistanceX)
                direction.x = 0f;

            direction.Normalize();
            transform.position += direction * m_speed * Time.fixedDeltaTime;
        }
        else if (m_isMoving)
            m_isMoving = false;

        m_animator.SetBool("IsMoving", m_isMoving);
        m_animator.SetBool("IsSpeaking", m_isOccupied && !m_isMoving);
    }

    public void CheckLost()
    {
        if (m_money == 0 || m_food == 0 || m_satisfaction == 0)
            SceneManager.LoadScene("LoseMenu");

        if (m_time <= 0)
        {
            if (m_satisfaction < m_requiredSatisfaction)
                SceneManager.LoadScene("LooseMenu");
            else 
                SceneManager.LoadScene("WinMenu");
        }
    }
    void MoveLeft()
    {
        if (m_currentPos > 0)
            Destination = m_positions[--m_currentPos];
    }

    void MoveRight()
    {
        if (m_currentPos < 2)
            Destination = m_positions[++m_currentPos];
    }
}
