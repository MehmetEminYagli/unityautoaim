using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1f;
    [SerializeField] private float JumpForce = 2f;


    [SerializeField] private GameObject Bulletprefab;
    [SerializeField] private Transform gunBulletPozition;
    [SerializeField] private float bulletSpeed = 1f;
    [SerializeField] private float BulletLife = 1f;
    [SerializeField] private Vector3 pozisyon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 Move = new Vector3(Horizontal,0,Vertical);
        pozisyon = transform.position + Move;
        transform.position = Vector3.MoveTowards(transform.position, pozisyon, MoveSpeed * Time.deltaTime);


        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
        bool dusmanvar = false;

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                dusmanvar = true;
                break;
            }
        }

        if (dusmanvar)
        {
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

            if (enemy != null)
            {
                Vector3 enemypozition = enemy.transform.position;
                transform.LookAt(enemypozition);
            }

        }
        else
        {
            if (Move != Vector3.zero)
            {
                transform.forward = Move.normalized;
            }
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }
    }

    private void FireBullet() 
    {
        Vector3 bulletDirecton = transform.forward;
        GameObject Bullet = Instantiate(Bulletprefab, gunBulletPozition.position+ bulletDirecton,Quaternion.identity);

        Rigidbody rb= Bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = bulletDirecton * bulletSpeed;
        }
        Destroy(Bullet,BulletLife);
    }
}
