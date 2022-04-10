using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerPick : MonoBehaviour
{
    public bool rockTaken;
    public bool treeTaken;
    public bool goldTaken;

    public int rockAmount = 0;
    public int treeAmount = 0;
    public int goldAmount = 0;

    public int archeryRockAmount = 0;
    public int archeryTreeAmount = 0;
    public int archeryGoldAmount = 0;

    public int archerySpawnRockAmount = 0;
    public int archerySpawnTreeAmount = 0;
    public int archerySpawnGoldAmount = 0;

    public int soldierRockAmount = 0;
    public int soldierTreeAmount = 0;
    public int soldierGoldAmount = 0;

    public int soldierSpawnRockAmount = 0;
    public int soldierSpawnTreeAmount = 0;
    public int soldierSpawnGoldAmount = 0;

    public Transform rockSpawnpoint;
    public GameObject rockPrefab;
    public GameObject treePrefab;
    public GameObject goldPrefab;

    public Transform archeryBuildPosition;
    public Transform soldierBuildPosition;

    public GameObject archeryBuildPrefab;
    public GameObject soldierBuildPrefab;

    public GameObject archerPrefab;
    public GameObject soldierPrefab;

    private GameObject[] archeryBuildStacked;
    private GameObject[] soldieryBuildStacked;

    public int archerAmount = 0;
    public int soldierAmount = 0;

    private bool archerSpawnOn;
    private bool soldierSpawnOn;


    public Transform stack;
    public Transform firstObject;
    public Transform lastObject;

    public Transform archeryStack;
    public Transform archeryFirstObject;
    public Transform archerFirstObject;
    public Transform archerSpawnPoint;
    public Transform soldierStack;
    public Transform soldierFirstObject;
    public Transform soldFirstObject;
    public Transform soldierSpawnPoint;

    public GameObject archerBT;
    public GameObject soldierBT;




    public float distanceBetweenObjects = 1f;


    public void Start()
    {
        
    }

    public void Update()
    {

        GameObject.Find("Archer Building Text").GetComponent<TextMesh>().text = (3- archeryRockAmount).ToString()+" Rock "+(3 - archeryTreeAmount).ToString() + " Tree "+(3 - archeryGoldAmount).ToString() + " Gold";
        GameObject.Find("Soldier Building Text").GetComponent<TextMesh>().text = (3 - soldierRockAmount).ToString() + " Rock " + (3 - soldierTreeAmount).ToString() + " Tree " + (3 - soldierGoldAmount).ToString() + " Gold";

        if (archerSpawnOn)
        {
            GameObject.Find("Archer Building Text").GetComponent<TextMesh>().text = (2 - archeryTreeAmount).ToString() + " Tree " + (2 - archeryGoldAmount).ToString() + " Gold";
        }
        if (soldierSpawnOn)
        {
            GameObject.Find("Soldier Building Text").GetComponent<TextMesh>().text = (2 - soldierRockAmount).ToString() + " Rock " + (2 - soldierTreeAmount).ToString() + " Tree";
        }

        if (archeryRockAmount==3 & archeryTreeAmount== 3 & archeryGoldAmount == 3 & !archerSpawnOn)
        {
            archerSpawnOn = true;
            archeryRockAmount = 0;
            archeryTreeAmount = 0;
            archeryGoldAmount = 0;
            DestroyArcheryStacks();
            archeryBuildPosition = GameObject.Find("ArcheryCube").transform;
            GameObject myArcheryBuild = Instantiate(archeryBuildPrefab, archeryBuildPosition.position, archeryBuildPosition.rotation) as GameObject;
            
        }

        if (soldierRockAmount == 3 & soldierTreeAmount == 3 & soldierGoldAmount == 3 & !soldierSpawnOn)
        {
            soldierSpawnOn = true;
            soldierRockAmount = 0;
            soldierTreeAmount = 0;
            soldierGoldAmount = 0;
            DestroySoldierStacks();
            soldierBuildPosition = GameObject.Find("SoldierCube").transform;
            GameObject myArcheryBuild = Instantiate(soldierBuildPrefab, soldierBuildPosition.position, soldierBuildPosition.rotation) as GameObject;
            
        }

        if (soldierRockAmount == 2 & soldierTreeAmount == 2 & soldierSpawnOn)
        {
            soldierRockAmount = 0;
            soldierTreeAmount = 0;

            soldierAmount++;

            DestroySoldierStacks();
            Vector3 desPosS = soldierSpawnPoint.position;
            GameObject mySoldier = Instantiate(soldierPrefab, desPosS, soldierSpawnPoint.rotation) as GameObject;
            desPosS.x += 10;
            soldierSpawnPoint.position = desPosS;
        }
        if (archeryGoldAmount == 2 & archeryTreeAmount == 2 & archerSpawnOn)
        {
            archeryGoldAmount = 0;
            archeryTreeAmount = 0;

            archerAmount++;

            DestroyArcheryStacks();
            Vector3 desPosA = archerSpawnPoint.position;
            GameObject myArcher = Instantiate(archerPrefab, desPosA, archerSpawnPoint.rotation) as GameObject;
            desPosA.x += 10;
            archerSpawnPoint.position = desPosA;
        }


    }


    public void OnTriggerEnter(Collider other)
   {

        if (other.tag == "Rock")
        {
            print("Rock");
            InvokeRepeating("RockInstantiate", 1f, 2f);
        }

        if (other.tag == "Tree")
        {
            print("Tree");
            InvokeRepeating("TreeInstantiate", 1f, 2f);
        }

        if (other.tag == "Gold")
        {
            print("Gold");
            InvokeRepeating("GoldInstantiate", 1f, 2f);
        }

        if (other.tag == "Archer")
        {
            print("ArcherBuilding");
            InvokeRepeating("ArcherBuilding", 1f, 2f);
        }

        if (other.tag == "Soldier")
        {
            print("SoldierBuilding");
            InvokeRepeating("SoldierBuilding", 1f, 2f);
        }


    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rock")
        {
            CancelInvoke();
        }
        if (other.tag == "Tree")
        {
            CancelInvoke();
        }
        if (other.tag == "Gold")
        {
            CancelInvoke();
        }
        if (other.tag == "Archer")
        {
            CancelInvoke();
        }
        if (other.tag == "Soldier")
        {
            CancelInvoke();
        }
    }



        public void RockInstantiate()
        {
        GameObject myRock = Instantiate(rockPrefab, rockSpawnpoint.position, rockSpawnpoint.rotation) as GameObject;
        myRock.name = "rockStacked";
        myRock.transform.parent = stack;
        if(firstObject == null)
        {
            lastObject = GameObject.Find("GFXCube").transform;
            firstObject = lastObject;
        }
        Vector3 desPos = firstObject.localPosition;
        desPos.y += distanceBetweenObjects;
        myRock.transform.localPosition = desPos;

        firstObject = myRock.transform;
        //rockTaken = true;
        rockAmount++;
        }
        public void TreeInstantiate()
        {
        GameObject myTree = Instantiate(treePrefab, rockSpawnpoint.position, rockSpawnpoint.rotation) as GameObject;
        myTree.name = "treeStacked";
        myTree.transform.parent = stack;
        if (firstObject == null)
        {
            lastObject = GameObject.Find("GFXCube").transform;
            firstObject = lastObject;
        }
        Vector3 desPos = firstObject.localPosition;
        desPos.y += distanceBetweenObjects;
        myTree.transform.localPosition = desPos;

        firstObject = myTree.transform;
        //treeTaken = true;
        treeAmount++;
        }
        public void GoldInstantiate()
        {
        GameObject myGold = Instantiate(goldPrefab, rockSpawnpoint.position, rockSpawnpoint.rotation) as GameObject;
        myGold.name = "goldStacked";
        myGold.transform.parent = stack;
        if (firstObject == null)
        {
            lastObject = GameObject.Find("GFXCube").transform;
            firstObject = lastObject;
        }
        Vector3 desPos = firstObject.localPosition;
        desPos.y += distanceBetweenObjects;
        myGold.transform.localPosition = desPos;

        firstObject = myGold.transform;
        //rockTaken = true;
        goldAmount++;
        }





    public void ArcherBuilding()
    {
        if(archeryRockAmount < 3 && rockAmount > 0 && !archerSpawnOn)
        {
            GameObject myRock = GameObject.Find("rockStacked");
            Destroy(myRock);
            archeryRockAmount++;
            rockAmount--;
            GameObject myRock2 = Instantiate(rockPrefab, rockSpawnpoint.position, rockSpawnpoint.rotation) as GameObject;
            myRock2.name = "rockStackedArchery";
            myRock2.tag = "StackedArchery";
            myRock2.transform.parent = archeryStack;
            if (archeryFirstObject == null)
            {
                archeryFirstObject = archerFirstObject;
            }
            Vector3 desPos = archeryFirstObject.localPosition;
            desPos.y += distanceBetweenObjects;
            myRock2.transform.localPosition = desPos;

            archeryFirstObject = myRock2.transform;
        }
        
        if (archeryTreeAmount < 3 && treeAmount > 0 && !archerSpawnOn)
        {
            GameObject myTree = GameObject.Find("treeStacked");
            Destroy(myTree);
            archeryTreeAmount++;
            treeAmount--;
            GameObject myTree2 = Instantiate(treePrefab, rockSpawnpoint.position, rockSpawnpoint.rotation) as GameObject;
            myTree2.name = "treeStackedArchery";
            myTree2.tag = "StackedArchery";
            myTree2.transform.parent = archeryStack;
            if (archeryFirstObject == null)
            {
                archeryFirstObject = archerFirstObject;
            }
            Vector3 desPos = archeryFirstObject.localPosition;
            desPos.y += distanceBetweenObjects;
            myTree2.transform.localPosition = desPos;

            archeryFirstObject = myTree2.transform;
        }
        if (archeryTreeAmount < 2 && treeAmount > 0 && archerSpawnOn)
        {
            GameObject myTree = GameObject.Find("treeStacked");
            Destroy(myTree);
            archeryTreeAmount++;
            treeAmount--;
            GameObject myTree2 = Instantiate(treePrefab, rockSpawnpoint.position, rockSpawnpoint.rotation) as GameObject;
            myTree2.name = "treeStackedArchery";
            myTree2.tag = "StackedArchery";
            myTree2.transform.parent = archeryStack;
            if (archeryFirstObject == null)
            {
                archeryFirstObject = archerFirstObject;
            }
            Vector3 desPos = archeryFirstObject.localPosition;
            desPos.y += distanceBetweenObjects;
            myTree2.transform.localPosition = desPos;

            archeryFirstObject = myTree2.transform;
        }
        if (archeryGoldAmount < 3 && goldAmount > 0 && !archerSpawnOn)
        {
            GameObject myGold = GameObject.Find("goldStacked");
            Destroy(myGold);
            archeryGoldAmount++;
            goldAmount--;
            GameObject myGold2 = Instantiate(goldPrefab, rockSpawnpoint.position, rockSpawnpoint.rotation) as GameObject;
            myGold2.name = "goldStackedArchery";
            myGold2.tag = "StackedArchery";
            myGold2.transform.parent = archeryStack;
            if (archeryFirstObject == null)
            {
                archeryFirstObject = archerFirstObject;
            }
            Vector3 desPos = archeryFirstObject.localPosition;
            desPos.y += distanceBetweenObjects;
            myGold2.transform.localPosition = desPos;

            archeryFirstObject = myGold2.transform;
        }
        if (archeryGoldAmount < 2 && goldAmount > 0 && archerSpawnOn)
        {
            GameObject myGold = GameObject.Find("goldStacked");
            Destroy(myGold);
            archeryGoldAmount++;
            goldAmount--;
            GameObject myGold2 = Instantiate(goldPrefab, rockSpawnpoint.position, rockSpawnpoint.rotation) as GameObject;
            myGold2.name = "goldStackedArchery";
            myGold2.tag = "StackedArchery";
            myGold2.transform.parent = archeryStack;
            if (archeryFirstObject == null)
            {
                archeryFirstObject = archerFirstObject;
            }
            Vector3 desPos = archeryFirstObject.localPosition;
            desPos.y += distanceBetweenObjects;
            myGold2.transform.localPosition = desPos;

            archeryFirstObject = myGold2.transform;
        }


    }


    public void SoldierBuilding()
    {
        if (soldierRockAmount < 3 && rockAmount > 0 && !soldierSpawnOn)
        {
            GameObject myRock = GameObject.Find("rockStacked");
            Destroy(myRock);
            soldierRockAmount++;
            rockAmount--;
            GameObject myRock2 = Instantiate(rockPrefab, rockSpawnpoint.position, rockSpawnpoint.rotation) as GameObject;
            myRock2.name = "rockStackedSoldier";
            myRock2.tag = "StackedSoldiery";
            myRock2.transform.parent = soldierStack;
            if (soldierFirstObject == null)
            {
                soldierFirstObject = soldFirstObject;
            }
            Vector3 desPos = soldierFirstObject.localPosition;
            desPos.y += distanceBetweenObjects;
            myRock2.transform.localPosition = desPos;

            soldierFirstObject = myRock2.transform;
        }
        if (soldierRockAmount < 2 && rockAmount > 0 && soldierSpawnOn)
        {
            GameObject myRock = GameObject.Find("rockStacked");
            Destroy(myRock);
            soldierRockAmount++;
            rockAmount--;
            GameObject myRock2 = Instantiate(rockPrefab, rockSpawnpoint.position, rockSpawnpoint.rotation) as GameObject;
            myRock2.name = "rockStackedSoldier";
            myRock2.tag = "StackedSoldiery";
            myRock2.transform.parent = soldierStack;
            if (soldierFirstObject == null)
            {
                soldierFirstObject = soldFirstObject;
            }
            Vector3 desPos = soldierFirstObject.localPosition;
            desPos.y += distanceBetweenObjects;
            myRock2.transform.localPosition = desPos;

            soldierFirstObject = myRock2.transform;
        }
        if (soldierTreeAmount < 3 && treeAmount > 0 && !soldierSpawnOn)
        {
            GameObject myTree = GameObject.Find("treeStacked");
            Destroy(myTree);
            soldierTreeAmount++;
            treeAmount--;
            GameObject myTree2 = Instantiate(treePrefab, rockSpawnpoint.position, rockSpawnpoint.rotation) as GameObject;
            myTree2.name = "treeStackedSoldier";
            myTree2.tag = "StackedSoldiery";
            myTree2.transform.parent = soldierStack;
            if (soldierFirstObject == null)
            {
                soldierFirstObject = soldFirstObject;
            }
            Vector3 desPos = soldierFirstObject.localPosition;
            desPos.y += distanceBetweenObjects;
            myTree2.transform.localPosition = desPos;

            soldierFirstObject = myTree2.transform;
        }
        if (soldierTreeAmount < 2 && treeAmount > 0 && soldierSpawnOn)
        {
            GameObject myTree = GameObject.Find("treeStacked");
            Destroy(myTree);
            soldierTreeAmount++;
            treeAmount--;
            GameObject myTree2 = Instantiate(treePrefab, rockSpawnpoint.position, rockSpawnpoint.rotation) as GameObject;
            myTree2.name = "treeStackedSoldier";
            myTree2.tag = "StackedSoldiery";
            myTree2.transform.parent = soldierStack;
            if (soldierFirstObject == null)
            {
                soldierFirstObject = soldFirstObject;
            }
            Vector3 desPos = soldierFirstObject.localPosition;
            desPos.y += distanceBetweenObjects;
            myTree2.transform.localPosition = desPos;

            soldierFirstObject = myTree2.transform;
        }
        if (soldierGoldAmount < 3 && goldAmount > 0 && !soldierSpawnOn)
        {
            GameObject myGold = GameObject.Find("goldStacked");
            Destroy(myGold);
            soldierGoldAmount++;
            goldAmount--;
            GameObject myGold2 = Instantiate(goldPrefab, rockSpawnpoint.position, rockSpawnpoint.rotation) as GameObject;
            myGold2.name = "goldStackedSoldier";
            myGold2.tag = "StackedSoldiery";
            myGold2.transform.parent = soldierStack;
            if (soldierFirstObject == null)
            {
                soldierFirstObject = soldFirstObject;
            }
            Vector3 desPos = soldierFirstObject.localPosition;
            desPos.y += distanceBetweenObjects;
            myGold2.transform.localPosition = desPos;

            soldierFirstObject = myGold2.transform;
        }

    }

    public void DestroyArcheryStacks()
    {
        archeryBuildStacked = GameObject.FindGameObjectsWithTag("StackedArchery");

        for (var i = 0; i < archeryBuildStacked.Length; i++)
        {
            Destroy(archeryBuildStacked[i]);
        }
    }


    public void DestroySoldierStacks()
    {
        soldieryBuildStacked = GameObject.FindGameObjectsWithTag("StackedSoldiery");

        for (var i = 0; i < soldieryBuildStacked.Length; i++)
        {
            Destroy(soldieryBuildStacked[i]);
        }
    }
}
