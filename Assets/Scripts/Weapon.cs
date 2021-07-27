using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    [Header("Specifications")]
    [SerializeField] private int _damage;
    [SerializeField] private int _damageDistance;
    [SerializeField] private int _rateOfFire;

    [Header("Bullet prefab")]
    [SerializeField] private Bullet _bullet;

    public void Shoot()
    {
        GameObject _temporary_Bullet_Handler;
        _temporary_Bullet_Handler = Instantiate(_bullet.gameObject, gameObject.transform.position, transform.rotation) as GameObject;

        _temporary_Bullet_Handler.transform.Rotate(Vector3.left * 360);

        Rigidbody _temporary_RigidBody;
        _temporary_RigidBody = _temporary_Bullet_Handler.GetComponent<Rigidbody>();

        _temporary_RigidBody.AddForce(transform.forward * _rateOfFire);

        Destroy(_temporary_Bullet_Handler, 2.0f);
    }
}
