using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAutoAimAndFire : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1f;
    [SerializeField] private Vector3 pozisyon;

    [SerializeField] private GameObject Bulletprefab;
    [SerializeField] private Transform gunBulletPozition;
    [SerializeField] private float bulletSpeed = 1f;
    [SerializeField] private float BulletLife = 1f;
    public float atisArasiSuresi = 1.5f;
    private float sonAtisZamani;
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
      
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
        bool dusmanvar = false;

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                dusmanvar = true;
                break;
            }
        }

        if (dusmanvar)
        {
            GameObject Enemy = GameObject.FindGameObjectWithTag("Player");
            if (Enemy != null)
            {
                Vector3 EnemyPozition = Enemy.transform.position;
                transform.LookAt(EnemyPozition);

                if (Time.time >= sonAtisZamani + atisArasiSuresi)
                {
                    FireBullet();

                    sonAtisZamani = Time.time;
                }
            }
        }
       
    }

    private void FireBullet()
    {
        Vector3 bulletDirecton = transform.forward;
        GameObject Bullet = Instantiate(Bulletprefab, gunBulletPozition.position + bulletDirecton, Quaternion.identity);

        Rigidbody rb = Bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = bulletDirecton * bulletSpeed;
        }
        Destroy(Bullet, BulletLife);
    }
   
    
}
