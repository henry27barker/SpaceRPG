using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class TITANBotController : MonoBehaviour
{
    public GameObject health;
    public GameObject claw;
    public EnemyMovement enemyMovement;
    public Animator animator;
    public Animator mainAnimator;
    public GameObject missile;
    public GameObject projectile;
    public GameObject explosion;

    public AudioSource powerDown, missileLaunch, projectileLaunch;

    public Queue<int> missileQueue = new Queue<int>();

    public Transform socket1; 
    public Transform socket2; 
    public Transform socket3; 
    public Transform socket4; 
    public Transform socket5; 
    public Transform socket6;

    public Transform shootingPoint;

    private Transform deathExplosionPoint1, deathExplosionPoint2, deathExplosionPoint3, deathExplosionPoint4, deathExplosionPoint5,deathExplosionPoint6;

    private GameObject cameraObject;

    private int maxHealth;
    private float rateCounter = 0;
    private float shootCounter = 0;
    public float rate;
    public float shootRate;
    public float clawWaitTime;
    public int shots = 2;
    private bool phase2, phase3, death, chill;
    private float clawShootCounter = 0;
    private float clawWaitCounter = 0;
    private int count = 0;
    private int count2 = 0;
    private float chillTime = 2;
    private float chillCount = 3;
    private int projectileOffset = 0;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = enemyMovement.health;
        animator = gameObject.transform.Find("Health").gameObject.GetComponent<Animator>();
        deathExplosionPoint1 = transform.Find("DeathExplosionPoints/1").gameObject.transform;
        deathExplosionPoint2 = transform.Find("DeathExplosionPoints/2").gameObject.transform;
        deathExplosionPoint3 = transform.Find("DeathExplosionPoints/3").gameObject.transform;
        deathExplosionPoint4 = transform.Find("DeathExplosionPoints/4").gameObject.transform;
        deathExplosionPoint5 = transform.Find("DeathExplosionPoints/5").gameObject.transform;
        deathExplosionPoint6 = transform.Find("DeathExplosionPoints/6").gameObject.transform;

        cameraObject = GameObject.FindWithTag("Player").transform.Find("Main Camera").gameObject;
        cameraObject.transform.localPosition = new Vector3(0, 3, -10);
    }

    // Update is called once per frame
    void Update()
    {
        //Weapon Flash Material Updates to match parent
        health.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", enemyMovement.spriteRenderer.material.GetFloat("_FlashAmount"));
        health.GetComponent<SpriteRenderer>().material.SetColor("_FlashColor", enemyMovement.spriteRenderer.material.GetColor("_FlashColor"));
        claw.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", enemyMovement.spriteRenderer.material.GetFloat("_FlashAmount"));
        claw.GetComponent<SpriteRenderer>().material.SetColor("_FlashColor", enemyMovement.spriteRenderer.material.GetColor("_FlashColor"));

        //HealthBar
        if (enemyMovement.health < 0 && !death)
        {
            mainAnimator.SetTrigger("Death");
            health.SetActive(false);
            claw.SetActive(false);
            chillTime = 0;
            death = true;
            Instantiate(explosion, deathExplosionPoint1.position, transform.rotation);
            Instantiate(explosion, deathExplosionPoint2.position, transform.rotation);
            Instantiate(explosion, deathExplosionPoint3.position, transform.rotation);
            Instantiate(explosion, deathExplosionPoint4.position, transform.rotation);
            Instantiate(explosion, deathExplosionPoint5.position, transform.rotation);
            Instantiate(explosion, deathExplosionPoint6.position, transform.rotation);
            powerDown.Play();
        }
        else if (enemyMovement.health < (0.3 * maxHealth) && !phase3)
        {
            animator.SetTrigger("Low");
            mainAnimator.SetTrigger("Low");
            rate *= 0.75f;
            shootRate *= 0.5f;
            shots += 2;
            chillTime = 3;
            count2 = 0;
            count = 0;
            chillCount = 30;
            clawShootCounter = 10;
            chill = false;
            phase3 = true;
        }
        else if (enemyMovement.health < (0.6 * maxHealth) && !phase2)
        {
            animator.SetTrigger("Mid");
            rate *= 0.75f;
            shootRate *= 0.5f;
            shots += 2;
            chillTime = 2;
            count2 = 0;
            count = 0;
            chillCount = 20;
            clawShootCounter = 10;
            chill = false;
            phase2 = true;
        }

        if (!death)
        {
            //Missile Logic
            if (shootCounter > shootRate)
            {
                ShootMissiles(shots, 1);
                shootCounter = 0;
            }
            else
            {
                shootCounter += Time.deltaTime;
            }

            if (rateCounter > rate)
            {
                if (missileQueue.Count > 0)
                {
                    FireFromSocket(missileQueue.Dequeue());
                    rateCounter = 0;
                }
            }
            else
            {
                rateCounter += Time.deltaTime;
            }

            //Claw Weapon Logic
            if ((phase2 || phase3) && !chill)
            {
                if (clawShootCounter > 0.1)
                {
                    switch (count)
                    {
                        case 0:
                            Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, 0 + projectileOffset));
                            count++;
                            break;
                        case 1:
                            Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -20 + projectileOffset));
                            count++;
                            break;
                        case 2:
                            Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -40 + projectileOffset));
                            count++;
                            break;
                        case 3:
                            Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -60 + projectileOffset));
                            count++;
                            break;
                        case 4:
                            Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -80 + projectileOffset));
                            count++;
                            break;
                        case 5:
                            Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -100 + projectileOffset));
                            count++;
                            break;
                        case 6:
                            Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -120 + projectileOffset));
                            count++;
                            break;
                        case 7:
                            Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -140 + projectileOffset));
                            count++;
                            break;
                        case 8:
                            Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -160 + projectileOffset));
                            count++;
                            break;
                        default:
                            Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -180 + projectileOffset));
                            count = 0;
                            if (projectileOffset == 0)
                                projectileOffset = 10;
                            else
                                projectileOffset = 0;
                            break;
                    }
                    projectileLaunch.Play();
                    count2++;
                    clawShootCounter = 0;
                }
                else
                {
                    clawShootCounter += Time.deltaTime;
                }
            }
            else if (!chill)
            {
                if (clawShootCounter > 0.75)
                {
                    Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, 0));
                    Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -20));
                    Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -40));
                    Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -60));
                    Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -80));
                    Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -100));
                    Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -120));
                    Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -140));
                    Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -160));
                    Instantiate(projectile, shootingPoint.position, Quaternion.Euler(0, 0, -180));

                    projectileLaunch.Play();
                    count2++;
                    clawShootCounter = 0;
                }
                else
                {
                    clawShootCounter += Time.deltaTime;
                }
            }
            if (count2 >= chillCount)
            {
                chill = true;
                if (clawShootCounter > chillTime)
                {
                    chill = false;
                    count2 = 0;
                    clawShootCounter = 0;
                }
                else
                {
                    clawShootCounter += Time.deltaTime;
                }
            }
        } else
        {
            if (chillTime > 2)
            {
                chillTime = 0;
                Destroy(gameObject);
            }
            else
            {
                chillTime += Time.deltaTime;
            }
        }
    }

    private void ShootMissiles(int num, float rate)
    {
        bool[] sockets = {false, false, false, false, false, false};
        List<int> numbers = new List<int>();

        if (num > 6)
            num = 6;
        if (rate < 0.5)
            rate = 0.5f;

        for (int i = 0; i < 6; i++)
            numbers.Add(i);

        for (int i = 0; i < num; i++)
        {
            int rand = Random.Range(0, numbers.Count);
            sockets[numbers[rand]] = true;
            Debug.Log(numbers[rand]);
            missileQueue.Enqueue(numbers[rand]);
            numbers.RemoveAt(rand);
        }
    }

    private void FireFromSocket(int num)
    {
        switch(num)
        {
            case 1:
                Instantiate(missile, socket1);
                break;
            case 2:
                Instantiate(missile, socket2);
                break;
            case 3:
                Instantiate(missile, socket3);
                break;
            case 4:
                Instantiate(missile, socket4);
                break;
            case 5:
                Instantiate(missile, socket5);
                break;
            default:
                Instantiate(missile, socket6);
                break;
        }

        missileLaunch.Play();
    }

}
