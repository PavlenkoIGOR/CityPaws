using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : SingletonBase<Player>
{
    [SerializeField] private int _playerHP_Room = 80;
    [SerializeField] private int _playerHP_Street = 80;
    [SerializeField] private int _playerHP_Roof = 100;
    [SerializeField] private int _playerHP_Boss = 1000;

    [SerializeField] private int m_NumLives;
    private Cat m_Cat;
    public Cat ActiveCat => m_Cat;

    public int playerHP;

    [SerializeField] private GameObject m_PlayerCatPrefab;


    [SerializeField] private CatController m_MovementController;

    private CameraController m_CameraController;
    private Transform _startSpawnPoint;
    private Transform _spawnPointFromPreviousLvl;

    public bool isGoingBack;
    public static bool canMove;


    protected override void Awake()
    {
        base.Awake();
        PlayerPrefs.SetString("isGoingBack", "false");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Start()
    {
        playerHP = ActiveCat.GetComponent<Destructible>().currentHitPoints;
    }

    private void OnCatDeath()
    {
        m_NumLives--;

        if (m_NumLives > 0)
            Respawn();
    }

    public void Respawn()
    {
        var newPlayerShip = Instantiate(m_PlayerCatPrefab.gameObject);

        print("newPlayerShip");

        m_Cat = newPlayerShip.GetComponent<Cat>();

        //m_CameraController.SetTarget(m_Cat.transform);
        m_MovementController.SetTargetShip(m_Cat);

        m_Cat.EventOnDeath.AddListener(OnCatDeath);

        //Player.isTeleported = false;
    }
    public void Respawn(Transform spawnPoint)
    {
        if (spawnPoint != null)
        {
            var newPlayerCat = Instantiate(m_PlayerCatPrefab);
            if (newPlayerCat)
            {
                newPlayerCat.transform.position = spawnPoint.position;
                Vector3 scale = newPlayerCat.transform.localScale;
                if (CheckSpawnPointDirection(spawnPoint) > 0)
                {
                    scale.x = scale.x * CheckSpawnPointDirection(spawnPoint);
                }
                else
                {
                    scale.x = scale.x * CheckSpawnPointDirection(spawnPoint);
                }
                newPlayerCat.transform.localScale = scale;


                m_Cat = newPlayerCat.GetComponent<Cat>();

                m_CameraController = Camera.main.GetComponent<CameraController>();
                m_CameraController.SetTarget(m_Cat.transform);
                m_MovementController.SetTargetShip(m_Cat);
                m_Cat.EventOnDeath.AddListener(OnCatDeath);
            }



        }
        else
        {
            //Debug.LogError("spawnPoint is null! Cannot respawn.");
        }
    }


    #region Score (current level only)

    public int Score { get; private set; }

    public int NumKills { get; private set; }

    public void AddKill()
    {
        NumKills++;
    }

    public void AddScore(int num)
    {
        Score += num;
    }


    public int starsQuantity { get; set; }
    public void AddStar(int num)
    {
        starsQuantity += num;
    }
    #endregion

    private sbyte CheckSpawnPointDirection(Transform tran)
    {
        Vector3 globalRight = Vector3.right;

        // Текущий локальный +X объекта в глобальных координатах
        Vector3 localRight = tran.right;

        // Вычисляем скалярное произведение
        float dotProduct = Vector3.Dot(localRight.normalized, globalRight);

        if (dotProduct < -0.5f)
        {
            return 1;
        }
        return -1;
    }


    public void FindSpawnPoints()
    {
        var spawnPoints = FindObjectsByType<SpawnPoint>(sortMode: FindObjectsSortMode.None);
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.directionSpawnPoint == DirectionSpawnPoint.MoveForeward)
            {
                _startSpawnPoint = spawnPoint.transform;
            }
            else if (spawnPoint.directionSpawnPoint == DirectionSpawnPoint.MoveBack)
            {
                _spawnPointFromPreviousLvl = spawnPoint.transform;
            }

        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindSpawnPoints();
        SetSpawnPoints();
    }

    public void SetSpawnPoints()
    {
        if (PlayerPrefs.GetString("isGoingBack") == "true")
        {
            if (_spawnPointFromPreviousLvl)
            {
                Respawn(_spawnPointFromPreviousLvl);
            }
        }
        else if (PlayerPrefs.GetString("isGoingBack") == "false")

        {
            if (_startSpawnPoint)
            {
                Respawn(_startSpawnPoint);
            }
        }
    }
}
