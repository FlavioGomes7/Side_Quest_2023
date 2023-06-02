using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private float timerMax;
    [SerializeField] private float distanceMin;
    private GameObject player;
    private float timer;
    private float distance;
    private Vector2 targetPosition;
    private Vector2 fowardNew;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        targetPosition = player.transform.position;
        fowardNew = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);
        transform.right = fowardNew;

        distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance < distanceMin)
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
