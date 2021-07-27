using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private int _damageValue;

    public float Speed { get { return _speed; } set { _speed = value; } }
    public int DamageValue { get { return _damageValue; } set { _damageValue = value; } }

    public delegate void Damage(int _value);
    public static event Damage damage;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Unit"))
        {
            collision.gameObject.GetComponent<Unit>().TakeDamage(5);//damage?.Invoke(1);
        }
    }

    void Update()
    {
        transform.position += transform.right * _speed * Time.deltaTime;
    }
}
