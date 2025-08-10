
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : EntityController
{
    


    
    private float _speedTmp;





    [Header("Patroling")]
    public int patrolLength;
    public Transform originOfPatrolArea;
    public float stoppingDist;
    bool _movingRight;
    Transform _player;
    bool _chill = false;
    bool _angry = false;
    bool _goBack = false;

    protected override void Start()
    {
        base.Start();
        _player = Player.instance.ActiveCat.transform;
        _speedTmp = _speed;
    }

    void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, groundMask);



        Vector3 scale = transform.localScale;
        if (_movingRight)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        else if (!_movingRight)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;



        if (_player)
        {


            if (Vector2.Distance(transform.position, originOfPatrolArea.position) < patrolLength && _angry == false)
            {
                _chill = true;
            }
            else if (Vector2.Distance(transform.position, _player.position) < stoppingDist)
            {
                _angry = true;
                _chill = false;
                _goBack = false;
            }
            else if (Vector2.Distance(transform.position, _player.position) > stoppingDist)
            {
                _goBack = true;
                _angry = false;
            }
        }
        if (_chill)
        {
            Chill();
        }
        else if (_angry == true)
        {
            Angry();
        }
        else if (_goBack)
        {
            GoBack();
        }

        //transform.position += new Vector3(moveX, 0, 0) * Time.deltaTime;
        //if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        //{
        //    _rb2D.AddForce(new Vector2(0, _jumpSpeed), ForceMode2D.Impulse);
        //}

        //if (Input.GetKeyDown(KeyCode.F) && _isGrounded || Input.GetKeyDown(KeyCode.F) && _isOnEnemy)
        //{
        //    //_animator.SetTrigger("attackTrigger");
        //    //if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("catAttackAnim"))
        //    //{
        //    //    _animator.SetTrigger("attackTrigger");
        //    //}
        //    Attack();
        //}

        SetAnimation();
        
    }

    private void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, originOfPatrolArea.position, _speed * Time.deltaTime*0.5f);
    }

    private void Angry()
    {
        if (_player)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
            if (transform.position.x > _player.position.x)
            {
                _movingRight = false;
            }
            else if (transform.position.x < _player.position.x)
            {
                _movingRight = true;
            }
        }
    }

    private void Chill()
    {
        if (transform.position.x > originOfPatrolArea.position.x + patrolLength)
        {
            _movingRight = false;
        }
        else if (transform.position.x < originOfPatrolArea.position.x - patrolLength)
        {
            _movingRight = true;
        }

        if (_movingRight)
        {
            transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - _speed * Time.deltaTime, transform.position.y);
        }
    }

    void SetAnimation()
    {
        
        if (_isGrounded && _movingRight || _isGrounded && !_movingRight)
        {
            _animator.SetBool("isRunning", true);
            _animator.SetBool("isJumping", false);
            _animator.SetBool("isFalling", false);
            //print("isRunning");
        }
        else if (_isGrounded && _rb2D.velocity.x <= 0.01f)
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
        StartCoroutine(PauseMove());
        Collider2D[] hitEenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEenemies)
        {
            enemy.transform.parent.GetComponent<Destructible>().ApplyDamage(transform.GetComponent<Destructible>().damage);

           //print("enemy attack");
        }
        
    }

    public float attackTimeInterval = 1f; // интервал между вызовами Attack()
    private float lastAttackTime = -Mathf.Infinity; // время последнего вызова
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.transform.root.GetComponent<Cat>())
        {
            if (Time.time - lastAttackTime >= attackTimeInterval)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }

    }

    IEnumerator PauseMove()
    {        

        float clipLength = default;
        RuntimeAnimatorController rac = _animator.runtimeAnimatorController;
        for (int i = 0; i < rac.animationClips.Length; i++)
        {
            if (rac.animationClips[i].name == "easyEnemyAttackAnim")
            {
                clipLength = rac.animationClips[i].length;
                break;
            }
        }
        _speedTmp = _speed;
        _speed = 0;
        yield return new WaitForSeconds(clipLength);
        _speed = _speedTmp;
    }
}
