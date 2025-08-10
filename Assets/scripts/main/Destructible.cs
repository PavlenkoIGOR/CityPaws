using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Destructible : MonoBehaviour
{
    #region Properties

    /// <summary>
    /// Объект игнорирует повреждения.
    /// </summary>
    [SerializeField] private bool _isDestructible;
    [SerializeField] private Animator _animatorController;
    public bool isDestructible { get { return _isDestructible; } set { _isDestructible = value; } }

    /// <summary>
    /// Стартовое кол-во хитпоинтов.
    /// </summary>
    public int _hitPoints;
    private int _currentHitPoints;
    public int currentHitPoints { get { return _currentHitPoints; } set { _currentHitPoints = value; } }

    public int damage;


    [SerializeField] private Image _healthBarMain;
    protected Image healthBarMain => _healthBarMain;



    #endregion

    #region Unity events





    protected virtual void Start()
    {
        _currentHitPoints = _hitPoints;
    }

    #region Безтеговая коллекция скриптов на сцене

    private static HashSet<Destructible> m_AllDestructibles;

    public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

    protected virtual void OnEnable()
    {
        if (m_AllDestructibles == null)
            m_AllDestructibles = new HashSet<Destructible>();

        m_AllDestructibles.Add(this);
    }

    protected virtual void OnDestroy()
    {
        m_AllDestructibles.Remove(this);

    }

    #endregion

    #endregion

    #region Public API

    public int takenDmg { get; set; }
    /// <summary>
    /// Применение дамага к объекту.
    /// </summary>
    /// <param name="damage"></param>
    public void ApplyDamage(int damage)
    {
        takenDmg = damage;
        print(damage);
        if (!_isDestructible)
            return;

        _currentHitPoints -= damage;
        if (!GetComponent<Cat>())
        {
            if (_healthBarMain != null)
            {
                _healthBarMain.fillAmount = (float)_currentHitPoints / _hitPoints;
                if (_healthBarMain.fillAmount < 0)
                {
                    _healthBarMain.fillAmount = 0;
                }
            }
        }
        else if(GetComponent<Cat>())
        {
            //print("cat");
        }
        if (_currentHitPoints <= 3)
        {
            OnDeath();
        }
    }

    public void AddHitPoints(int hp)
    {
        _currentHitPoints += hp;
        _currentHitPoints = (int)Mathf.Clamp(_currentHitPoints, 0, _hitPoints);
        
    }

    #endregion

    protected virtual void OnDeath()
    {
        GetComponent<EntityController>().speed = 0;
        var controller = GetComponent<EntityController>();
        GetComponent<Rigidbody2D>().simulated = false;
        controller._entityCollider.enabled = false;
        GetComponent<EntityController>().enabled = false;
        StartCoroutine(PLayDeath());
        Player.instance.AddScore(scoreValue);
        m_EventOnDeath?.Invoke();
    }







    public void StartDeathEnemy()
    {
        OnDeath();
    }
    [SerializeField] private UnityEvent m_EventOnDeath;
    public UnityEvent EventOnDeath => m_EventOnDeath;

    #region Teams

    /// <summary>
    /// Полностью нейтральный тим ид. Боты будут игнорировать такие объекты.
    /// </summary>
    public const int TeamIdNeutral = 0;

    /// <summary>
    /// ИД стороны. Боты будут атаковать всех кто не свой.
    /// </summary>
    [SerializeField] private int m_TeamId;
    public int TeamId => m_TeamId;

    #endregion

    #region Score

    /// <summary>
    /// Кол-во очков за уничтожение.
    /// </summary>
    [SerializeField] private int _scoreValue;
    public int scoreValue => _scoreValue;

    #endregion





    #region animation
    private IEnumerator PLayDeath()
    {
        if (_animatorController != null)
        {
            _animatorController.SetTrigger("deathTrigger");

            float clipLength = default;
            RuntimeAnimatorController rac = _animatorController.runtimeAnimatorController;
            for (int i = 0; i < rac.animationClips.Length; i++)
            {
                if (rac.animationClips[i].name == "death")
                {
                    clipLength = rac.animationClips[i].length;
                }
            }
            yield return new WaitForSeconds(clipLength);
        }
        
        Destroy(gameObject);
    }
    #endregion
}

