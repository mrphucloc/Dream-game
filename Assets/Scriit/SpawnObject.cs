using System.Collections;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private string pooledObjectId;
    private float currentX = 0;

    public static object Instance { get; internal set; }

    void Start()
    {
        objectPool = objectPool.Instance;
    }
    public void Spawn(string pooledObjectId)
    {
        this.pooledObjectId = pooledObjectId;
        var go = objectPool.GetObject(pooledObjectId);
        if (go != null)
        {
            go.transform.position = new Vector3(currentX,0,0);
            go.transform.rotation = Quaternion.identity;
            currentX++;

            StartCoroutine(ReturnObject(go));
        }
        else {
            Debug.LogWarning($"No object found with ID: {pooledObjectId}");
        }
        
    }
    private IEnumerator ReturnObject(GameObject go)
    {
        yield return new WaitForSeconds(1f);
       objectPool.ReturnObject(pooledObjectId, go);
        currentX = 0;
    }
    private void OnGUI()
    {
        if(GUILayout.Button("Spawn Object"))
        {
           Spawn(pooledObjectId);
        }
    }
}
