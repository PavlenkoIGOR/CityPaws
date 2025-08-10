
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class CatController : EntityController
{
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        float moveX = Input.GetAxis(horiz) * speed;

        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, groundMask);
        

        Vector3 scale = transform.localScale;
        if (moveX > 0)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        else if (moveX < 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;



        transform.position += new Vector3(moveX, 0, 0) * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded )
        {
            _rb2D.AddForce(new Vector2(0, _jumpSpeed), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.F) && _isGrounded)
        {
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
        else if (_isGrounded && moveX <= 0.01f )
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

    private void Attack()
    {
        _animator.SetTrigger("attackTrigger");

        Collider2D[] hitEenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEenemies)
        {
            Debug.Log($"enemy name {enemy.transform.parent.name}");
            enemy.transform.parent.GetComponent<Destructible>().ApplyDamage(transform.GetComponent<Destructible>().damage);
        }
    }
}
