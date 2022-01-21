using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lumberjack : MonoBehaviour
{

    public NavMeshAgent workerNavMeshAgent;
    public GameObject currentTarget = null;
    [SerializeField]private int carryCapacity = 10;
    [SerializeField]private int carryWeight = 0;
    private float meleeRange = 5f;

    private bool busy = false;
    void Start()
    {
        
    }


    void Update()
    {
        PerformJob();
    }

    void PerformJob()
    {
        if(carryWeight >= carryCapacity)
        {
            FindClosestStockpile(); 
            if(currentTarget !=null)DeliverCarried();
        }
        else
        {
            FindClosestTree();
            if(currentTarget != null)
            {
                ChopTrees();
            }
            else if(carryWeight != 0)
             {
                 FindClosestStockpile();
                 DeliverCarried();
             }
             else
             {
                 //be confused about what to do, and warn the player that you have no tasks
                 //idle
             }
            
            
        }
    }

    void FindClosestTree()
    {            
            float distanceToClosestTree = Mathf.Infinity;
            TreeController closestTree = null;
            TreeController[] allTrees = GameObject.FindObjectsOfType<TreeController>();


            if(allTrees.Length < 1)return;
            foreach(TreeController currentTree in allTrees)
            {
                float distanceToTree = (currentTree.transform.position - this.transform.position).sqrMagnitude;
                if(distanceToTree < distanceToClosestTree)
                {
                    distanceToClosestTree = distanceToTree;
                    closestTree = currentTree;   
                }
            }
            currentTarget = closestTree.gameObject;
    }
    public void ChopTrees()
    {

        float dist = Vector3.Distance(currentTarget.transform.position, transform.position);
        var chopping = "executionTime";
        if(dist > meleeRange)
        {
            workerNavMeshAgent.SetDestination(currentTarget.transform.position);
        }
        else if(dist < meleeRange)
        {
            //put this in a coroutine
            var tree = currentTarget.GetComponent<TreeController>();

            if(tree.woodCount > 0 && carryWeight < carryCapacity && !busy)
            {
                StartCoroutine(chopping);
                tree.woodCount -= 1;
                carryWeight += 1;
                
            }
            if(tree.woodCount <= 0)
            {
                tree.KillTree();
                currentTarget = null;
            }
        }
    }

    void FindClosestStockpile()
    {
        
        float distanceToclosestStockpile = Mathf.Infinity;
        Stockpile closestStockpile = null;
        Stockpile[] allSTockpiles = GameObject.FindObjectsOfType<Stockpile>();
        if(allSTockpiles.Length < 1)return;
        foreach(Stockpile currentStockpile in allSTockpiles )
        {
            float distanceToStockpile = (currentStockpile.transform.position - this.transform.position).sqrMagnitude;
            if(distanceToStockpile < distanceToclosestStockpile)
            {
                distanceToclosestStockpile = distanceToStockpile;
                closestStockpile = currentStockpile;
            }

        }
        currentTarget = closestStockpile.gameObject;      
    }

    void DeliverCarried()
    {
        var stockpile = currentTarget.GetComponent<Stockpile>();
        float dist = Vector3.Distance(stockpile.accessPoint.position, transform.position);
        if(dist > meleeRange && !busy)
        {
            workerNavMeshAgent.SetDestination(currentTarget.transform.position);
        }
        else if(dist < meleeRange && !busy)
        {
            stockpile.storedWood += carryWeight;
            carryWeight = 0;
            Debug.Log("Stockpile now has " + stockpile.storedWood +" wood stored");
        }
        currentTarget = null;        
    }

    IEnumerator executionTime()
    {
        busy = true;
        yield return new WaitForSeconds(1.5f);
        //play animation and stuff here. might have to do 1 IEnumerator per action. I.e delivery/chopping/getting equipment etc.
        busy = false;
        Debug.Log("chopped some wood");

    }
}
