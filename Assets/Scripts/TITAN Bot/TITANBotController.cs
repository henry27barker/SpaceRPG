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
    public GameObject missile;

    public Queue<int> missileQueue = new Queue<int>();

    public Transform socket1; 
    public Transform socket2; 
    public Transform socket3; 
    public Transform socket4; 
    public Transform socket5; 
    public Transform socket6;

    private int maxHealth;
    private float rateCounter = 0;
    private float shootCounter = 9;
    public float rate;
    public float shootRate;
    public int shots = 2;
    private bool phase2, phase3;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = enemyMovement.health;
        animator = gameObject.transform.Find("Health").gameObject.GetComponent<Animator>();
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
        if (enemyMovement.health < (0.3 * maxHealth) && !phase3)
        {
            animator.SetTrigger("Low");
            rate *= 0.5f;
            shootRate *= 0.5f;
            shots += 2;
            phase3 = true;
        } else if (enemyMovement.health < (0.6 * maxHealth) && !phase2)
        {
            animator.SetTrigger("Mid");
            rate *= 0.5f;
            shootRate *= 0.5f;
            shots += 2;
            phase2 = true;
        }

        if (shootCounter > shootRate)
        {
            Shoot(shots, 1);
            Debug.Log("Start Shooting");
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
                Debug.Log("Fire");
                FireFromSocket(missileQueue.Dequeue());
                rateCounter = 0;
            }
        }
        else
        {
            rateCounter += Time.deltaTime;
        }
    }

    private void Shoot(int num, float rate)
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
    }


}
