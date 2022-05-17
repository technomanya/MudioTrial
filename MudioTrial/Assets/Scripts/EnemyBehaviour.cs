using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private NavMeshAgent navAgent;

    [SerializeField] private bool enemyMove;

    [SerializeField] private Vector3 currTarget;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyMove && Vector3.Distance(GameManager.GM.player.transform.position,transform.position) < 5f)
        {
            StartCoroutine(SetNewTarget());
            enemyMove = true;
        }
        else
        {
            if (Vector3.Distance(currTarget,transform.position) < 0.1f)
            {
                enemyMove = false;
                StopAllCoroutines();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.CompareTag("Bullet"))
        {
            GameManager.GM.EnemyDead(gameObject);
        }
    }

    IEnumerator SetNewTarget()
    {
        var randomDelay = Random.Range(0.5f, 2f);
        
        Transform player = GameManager.GM.player.transform;
        var distanceX = Mathf.Min(transform.position.x - player.position.x,5/1.4f);
        var distanceZ = Mathf.Min(transform.position.z - player.position.z,5/1.4f);
        currTarget = new Vector3(transform.position.x + distanceX, transform.position.y, transform.position.z + distanceZ);
        
        yield return new WaitForSeconds(randomDelay);
        
        
        navAgent.destination = currTarget;
    }
}
