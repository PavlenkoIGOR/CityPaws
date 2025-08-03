
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class CatController : MonoBehaviour
{
    private string horiz = "Horizontal";
    //private string vert = "Vertical";
    private Rigidbody2D _rb2D;
    private float lastDirectionX = 1f;

    [SerializeField]private float _speed;
    [SerializeField]private float _jumpSpeed;
    [SerializeField]public SpriteRenderer _visualSprite;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckerRadius = 0.04f;
    [SerializeField] private Collider2D _catCollider;
    public LayerMask groundMask;

    [SerializeField] private Animator _animator;
    //private string _catRunAnimName = "CatRunAnim";
    //private string _catFallsDownAnimName = "catFallsDownAnim";
    //private string _catJumpAnimAnimName = "catJumpAnim";
    //private bool _isRunning = false;
    private bool _isJumping = false;
    //private bool _isFalling = false;
    private bool _isGrounded = false;

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxis(horiz) * _speed;

        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, groundMask);


        //if (Mathf.Abs(moveX) > 0.01f)
        //{
        //    lastDirectionX = Mathf.Sign(moveX);
        //    _visualSprite.flipX = lastDirectionX > 0;

        //}
        //else
        //{
        //    _visualSprite.flipX = lastDirectionX > 0;

        //}
        Vector3 scale = transform.localScale;
        if (moveX > 0)
        {
            // ƒвижение вправо Ч делаем масштаб отрицательным (отзеркаливание)
            scale.x = -Mathf.Abs(scale.x);
        }
        else if (moveX < 0)
        {
            // ƒвижение влево Ч делаем масштаб положительным
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;



        transform.position += new Vector3(moveX, 0, 0) * Time.deltaTime;
        //print($"_isGrounded {_isGrounded}");
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _isJumping = true;

            _rb2D.AddForce(new Vector2(0, _jumpSpeed), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.F) && _isGrounded)
        {
            //print("F");
            //_animator.SetTrigger("attackTrigger");
            //if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("catAttackAnim"))
            //{
            //    _animator.SetTrigger("attackTrigger");
            //}
            Attack();
        }

        SetAnimation(moveX);
    }

    void SetAnimation(float moveX)
    {
        
        if (_isGrounded && Mathf.Abs(moveX) > 0.01f)
        {
            _animator.SetBool("isRunning", true);
            _animator.SetBool("isJumping", false);
            _animator.SetBool("isFalling", false);
            //print("isRunning");
        }
        else if (_isGrounded && moveX <= 0.01f)
        {
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isJumping", false);
            _animator.SetBool("isFalling", false);
            //print("idle");
        }
        else if (!_isGrounded)
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
            Debug.Log($"enemy name {enemy.transform.parent.name}");
            enemy.transform.parent.GetComponent<Destructible>().ApplyDamage(10);
        }
    }
}
