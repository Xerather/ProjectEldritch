using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FOVMechanic))]
public class ShootingMechanic : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 3.0f;
    public float rotationSpeed = 25.0f;
    public float stayAtDistanceRange = 3.0f;
    public bool facePlayer = true;
    public bool followPlayer = true;
    public bool stayAtDistance = false;

    [Header("Shooting Settings")]
    public float shootDelay = 1.0f;
    public float bulletDestroyDelay = 5.0f;
    public bool shootAtSight;

    private GameObject player;
    private GameObject bullet;
    private FOVMechanic fov;
    private float nextBullet;

    //Spawning in a bullet and setting up the OnSpottedPlayer event
    private void Awake()
    {
        bullet = Resources.Load("Bullet") as GameObject;
        fov = GetComponent<FOVMechanic>();
        fov.OnSpottedPlayer += UpdatePlayer;
    }

    //Setting the local player to the fov player and handeling the nextBullet
    private void UpdatePlayer()
    {
        player = fov._spottedPlayer;
        nextBullet = (shootAtSight) ? 0 : Time.time + shootDelay;
    }

    //Shooting and moving every frame
    private void Update()
    {
        Shoot();
        Move();
    }

    //Spawning a bullet with the correct rotation only if the cooldown has expired
    private void Shoot()
    {
        if (Time.time >= nextBullet)
        {
            GameObject bulletClone = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            bulletClone.GetComponent<Rigidbody2D>().rotation = GetAngle(player.transform.position, transform.position) - 90.0f;
            Destroy(bulletClone, bulletDestroyDelay);
            nextBullet = Time.time + shootDelay;
        }
    }

    //All the movement logic
    private void Move()
    {
        if (player == null)
            return;

        if (followPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        }
        else if (stayAtDistance && Vector2.Distance(player.transform.position, transform.position) < stayAtDistanceRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.position - player.transform.position).normalized, movementSpeed * Time.deltaTime);
        }

        if (facePlayer)
        {
            Quaternion newRotation = Quaternion.AngleAxis(GetAngle(player.transform.position, transform.position), Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }

    }

    private float GetAngle(Vector3 pos1, Vector3 pos2)
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

}
