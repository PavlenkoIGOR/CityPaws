
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityController : MonoBehaviour
{
    protected string horiz = "Horizontal";
    //private string vert = "Vertical";
    protected Rigidbody2D _rb2D;
    private float lastDirectionX = 1f;

    [SerializeField]    protected float _speed;
    public float speed { get => _speed; set => _speed = value; }

    [SerializeField]    protected float _jumpSpeed;
    [SerializeField]    public SpriteRenderer _visualSprite;
    [SerializeField]    protected Transform _groundChecker;
    [SerializeField]    protected float _groundCheckerRadius = 0.04f;
    [SerializeField]    public Collider2D _entityCollider;
    public LayerMask groundMask;
    protected bool _isGrounded = false;
    [SerializeField] protected Animator _animator;




    [Header("Attack settings")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    protected virtual void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }
}
