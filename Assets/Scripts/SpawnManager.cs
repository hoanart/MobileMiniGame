using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    
    public BoxCollider2D boxCollider2D;
    
    public int count = 50;
    public static int GetSpawnedObjCount;
    [SerializeField]
    private GameObject mUrchin;
    private Stack<GameObject> mUrchins;

    [SerializeField]
    private TMP_Text mScore;
    
    private Dictionary<Vector3, GameObject> mSpawnedObjDict;
    [SerializeField]
    private bool executeInUpdate;

    [SerializeField]
    private float spawnTime =5.0f;

    [SerializeField]
    private float spawnRate = 0.0f;
    [SerializeField]
    private float spawnDelayRate = 2.0f;
    

    [SerializeField]
    private bool mbEnd;
    private IEnumerator spawnRoutine;

    [SerializeField]
    private bool mbOverlapped;
    private void Awake()
    {
        mUrchins = new Stack<GameObject>(count);
        mSpawnedObjDict = new Dictionary<Vector3, GameObject>(count);
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject urchinObj = Instantiate(mUrchin, transform, true);
            urchinObj.GetComponentInChildren<TriggerObject>().scoreText = mScore;
            urchinObj.SetActive(false);
            mUrchins.Push(urchinObj);
        }
        boxCollider2D.size = new Vector2(CameraViewportHandler.Instance.Width*0.8f, CameraViewportHandler.Instance.Height*0.85f);
        spawnRoutine = SpawnRoutine();
        GameManager.instance.OnChangeScore += ShowCurrentScore;
        StartCoroutine(spawnRoutine);
    }

    IEnumerator SpawnRoutine()
    {
        while (GameManager.instance.State == GameState.READY)
        {
            yield return null;
        }

         spawnRate = spawnTime;
        while (!mbEnd)
        {
            if(spawnRate<=0)
            {
            GameObject spawnedObj = mUrchins.Pop();
            
            AnchorGameObject anchor = spawnedObj.GetComponent<AnchorGameObject>();
            var bounds = boxCollider2D.bounds;
            
            anchor.anchorOffset = new Vector3(Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y,bounds.max.y),
                0.0f);
           

            Collider2D[] hits =
                Physics2D.OverlapBoxAll(anchor.anchorOffset, spawnedObj.GetComponent<CircleCollider2D>().bounds.size, 0);
            foreach (Collider2D hit in  hits)
            {
                if (!hit.CompareTag("Enemy"))
                {
                    continue;
                }
                if (!hit.CompareTag("Player"))
                {
                    continue;
                }
                Debug.Log($"겹침 : {anchor.anchorOffset},{hits.GetValue(0)}");
                mbOverlapped = true;
                
            }
            
            if (mbOverlapped)
            {
                spawnedObj.SetActive(false);
                mUrchins.Push(spawnedObj);
                GetSpawnedObjCount -= 1;
                Debug.Log($"GetSpawned : {GetSpawnedObjCount}");
            }
            else
            {
                mSpawnedObjDict.Add(anchor.anchorOffset,spawnedObj);
                GetSpawnedObjCount += 1;
                Debug.Log($"GetSpawned : {GetSpawnedObjCount}");
                spawnedObj.SetActive(true);
                Debug.Log("생성");
                if (mUrchins.Count == 0)
                {
                    mbEnd = true;
                }

            }

            spawnTime -= 0.1f;
            spawnTime = Mathf.Clamp(spawnTime, 0.5f, 2.0f);
            spawnRate = spawnTime;
            }
            if (mbOverlapped)
            {
                yield return new WaitForSeconds(0.01f);
                mbOverlapped = false;
            }
            else
            {
                //yield return new WaitForSeconds(spawnTime);
                spawnRate -= Time.deltaTime;
              
                yield return null;
            }

            if (GameManager.instance.State == GameState.GAMEOVER)
            {
                GameManager.instance.Score = Int32.Parse(mScore.text);
                yield break;
                
            }
            //yield return null;
            
        }
        yield return null;
    }

    int ShowCurrentScore()
    {
        return Int32.Parse(mScore.text);
    }
    // Update is called once per frame
    void Update()
    {
        // #if UNITY_EDITOR
        // if (executeInUpdate)
        // {
        //       boxCollider2D.size = new Vector2(CameraViewportHandler.Instance.Width*0.8f, CameraViewportHandler.Instance.Height*0.85f);
        // }
        // #endif


    }
    
}
