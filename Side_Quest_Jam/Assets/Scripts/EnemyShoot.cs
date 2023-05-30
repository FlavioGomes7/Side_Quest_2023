using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private float timerMax;
    private GameObject player;
    private float timer;
    private float distance;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);
        if(distance < 10)
        {
            timer += Time.deltaTime;
            if(timer > timerMax)
            {
                timer = 0;
                Shoot();
            } 
        }   
    }

    private void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }



}
