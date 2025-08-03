using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Player : SingletonBase<Player>
{

    [SerializeField] private int m_NumLives;
    private Cat m_Cat;
    public Cat ActiveCat => m_Cat;

    [SerializeField] private GameObject m_PlayerCatPrefab;

    [SerializeField] private CameraController m_CameraController;
    [SerializeField] private CatController m_MovementController;

    [SerializeField] private Transform _startSpawnPoint;

    public static bool isTeleported;

    public static bool canMove;
    protected override void Awake()
    {
        base.Awake();
        if (_startSpawnPoint)
        {
            print($"isTeleported {isTeleported}");
            StartRespawn1(_startSpawnPoint);
        }
    }
    private void Start()
    {


        //m_Cat.EventOnDeath.AddListener(OnCatDeath);
    }

    private void OnCatDeath()
    {
        m_NumLives--;

        if (m_NumLives > 0)
            Respawn();
        //else
        //LevelSequenceController.Instance.FinishCurrentLevel(false);
    }

    public void Respawn()
    {
        var newPlayerShip = Instantiate(m_PlayerCatPrefab.gameObject);

        m_Cat = newPlayerShip.GetComponent<Cat>();

        m_CameraController.SetTarget(m_Cat.transform);
        m_MovementController.SetTargetShip(m_Cat);

        m_Cat.EventOnDeath.AddListener(OnCatDeath);

        //Player.isTeleported = false;
    }
    public void StartRespawn1(Transform spawnPoint)
    {
        if (spawnPoint != null)
        {
            var newPlayerCat = Instantiate(m_PlayerCatPrefab);
            newPlayerCat.transform.position = spawnPoint.position;
            Vector3 scale = newPlayerCat.transform.localScale;
            if (CheckSpawnPointDirection(spawnPoint) > 0)
            {
                scale.x = scale.x * CheckSpawnPointDirection(spawnPoint);
                //print($"right");
                //newPlayerCat.GetComponent<CatController>()._visualSprite.flipX = true;
                
            }
            else
            {
                //print("left");
                //newPlayerCat.GetComponent<CatController>()._visualSprite.flipX = false;
                scale.x = scale.x * CheckSpawnPointDirection(spawnPoint);
            }
            newPlayerCat.transform.localScale = scale;







            //Vector3 scale = newPlayerCat.transform.localScale;
            //if (CheckSpawnPointDirection(spawnPoint) > 0)
            //{
            //    scale.x = scale.x * CheckSpawnPointDirection(spawnPoint);
            //    print($"right {scale.x}");
            //    //newPlayerCat.GetComponent<CatController>()._visualSprite.flipX = true;

            //}
            //else
            //{

            //    //newPlayerCat.GetComponent<CatController>()._visualSprite.flipX = false;
            //    scale.x = scale.x * CheckSpawnPointDirection(spawnPoint);
            //    print($"left {scale.x}");
            //}
            //newPlayerCat.transform.localScale = scale;

            m_Cat = newPlayerCat.GetComponent<Cat>();
            m_CameraController.SetTarget(m_Cat.transform);
            m_MovementController.SetTargetShip(m_Cat);
            m_Cat.EventOnDeath.AddListener(OnCatDeath);


            if (m_Cat)
            {
                //Debug.LogWarning("spawnPoint is !null! respawn.");
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
}
