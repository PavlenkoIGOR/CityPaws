
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rb2D;

    [SerializeField]private float _speed;
    [SerializeField]private float _jumpSpeed;
    [SerializeField]public SpriteRenderer _visualSprite;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckerRadius = 0.04f;
    [SerializeField] private Collider2D _catCollider;
    public LayerMask groundMask;

    [SerializeField] private Animator _animator;

    private bool _isGrounded = false;
    private bool _isOnEnemy = false;

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {


        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, groundMask);
        _isOnEnemy = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, enemyLayer);

        //Vector3 scale = transform.localScale;
        ////if (moveX > 0)
        ////{
        ////    scale.x = -Mathf.Abs(scale.x);
        ////}
        ////else if (moveX < 0)
        ////{
        ////    scale.x = Mathf.Abs(scale.x);
        ////}

        //transform.localScale = scale;



        //transform.position += new Vector3(moveX, 0, 0) * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded || Input.GetKeyDown(KeyCode.Space) && _isOnEnemy)
        {
            _rb2D.AddForce(new Vector2(0, _jumpSpeed), ForceMode2D.Impulse);
        }

        //if (Input.GetKeyDown(KeyCode.F) && _isGrounded || Input.GetKeyDown(KeyCode.F) && _isOnEnemy)
        //{
        //    //_animator.SetTrigger("attackTrigger");
        //    //if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("catAttackAnim"))
        //    //{
        //    //    _animator.SetTrigger("attackTrigger");
        //    //}
        //    Attack();
        //}

        //SetAnimation(moveX);
    }

    void SetAnimation(float moveX)
    {
        
        if (_isGrounded && Mathf.Abs(moveX) > 0.01f || Mathf.Abs(moveX) > 0.01f && _isOnEnemy)
        {
            _animator.SetBool("isRunning", true);
            _animator.SetBool("isJumping", false);
            _animator.SetBool("isFalling", false);
            //print("isRunning");
        }
        else if (_isGrounded && moveX <= 0.01f || _isOnEnemy && moveX <= 0.01f)
        {
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isJumping", false);
            _animator.SetBool("isFalling", false);
            //print("idle");
        }
        else if (!_isGrounded || !_isOnEnemy)
        {
            if (_rb2D.velocity.y > 0.01f)
            {
                _animator.SetBool("isJumping", true);
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isFalling", false);
                //print("isJumping");
            }
            else if (_rb2D.velocity.y < 0)
            {
                _animator.SetBool("isJumping", false);
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isFalling", true);
                //print("isFalling");
            }
        }
    }

    internal void SetTargetShip(Cat m_Cat)
    {
    }






    [Header("Attack settings")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    private void Attack()
    {
        _animator.SetTrigger("attackTrigger");

        Collider2D[] hitEenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEenemies)
        {
            //Debug.Log($"enemy name {enemy.transform.parent.name}");
            enemy.transform.root.GetComponent<Destructible>().ApplyDamage(transform.GetComponent<Destructible>().damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.root.GetComponent<Cat>())
        {
            print("hero comes");
            Attack();
        }

    }
}
