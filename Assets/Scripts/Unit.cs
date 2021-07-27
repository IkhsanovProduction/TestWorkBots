using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Team
{
    Red,
    Blue
}

public class Unit : MonoBehaviour
{
    [SerializeField] Team _selectTeam;
    [SerializeField] private int _instanceID;

    [SerializeField] private int _speed;
    [SerializeField] private int _health;
    [SerializeField] private GameObject _explosionEffect;

    [SerializeField] private List<GameObject> _targets = new List<GameObject>();
    [SerializeField] private Weapon _weapon;

    private Animator _animator;

    private bool _isAim = false;
    private bool _isMove = false;
    private bool _isAttack = false;
    
    public int InstanceID { get { return _instanceID; } set { _instanceID = value; } }

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit"));
        InvokeRepeating(nameof(RecoveryHealth), 5f, 10f);

        Bullet.damage += TakeDamage;
    }

    void Update()
    {
        Aim();
        Move();

        if (_isAim) { Attack();}
    }

    void Move()
    {
        if (!_isAim)
        {
            _isMove = true;
            transform.position = Vector3.MoveTowards(transform.position, RandomPosition(), _speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(RandomPosition() - transform.position), 3 * Time.deltaTime);
        }

        else
        {
            _isMove = false;
        }
    }

    Vector3 RandomPosition()
    {
        Vector3 _pos = new Vector3(Random.Range(0, 1000), transform.position.y, Random.Range(0, 1000));
        return _pos;
    }

    void RecoveryHealth()
    {
        _health += 5;
    }

    void Die()
    {
        if (_health <= 0)
        {
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Points()
    {
        switch (_selectTeam)
        {
            case Team.Red:
                GameManager.instance.BluePoint += 1;
                break;
            case Team.Blue:
                GameManager.instance.RedPoint += 1;
                break;
        }
    }

    public void TakeDamage(int _value)
    {
        _health -= _value;
        Points();
        Die();
    }

    void CheckDistance(GameObject _target, Team team)
    {
        if(_target.gameObject != null)
        {
            if (Vector3.Distance(transform.position, _target.transform.position) < 10 && Vector3.Distance(transform.position, _target.transform.position) > 5 && _target.GetComponent<Unit>()._selectTeam == team)
            {
                _isAim = true;
                _animator.Play("assault_combat_run");

                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_target.transform.position - transform.position), 3 * Time.deltaTime);
            }
        }

        else
        {
            _isAim = false;
        }  
    }

    void Aim()
    {  
        foreach(GameObject targ in _targets)
        {
            switch (_selectTeam)
            {
                case Team.Red:
                    CheckDistance(targ, Team.Blue);
                    break;
                case Team.Blue:
                    CheckDistance(targ, Team.Red);
                    break;
            }
        }
    }

    void Attack()
    {
        _weapon.Shoot();
    }
}
