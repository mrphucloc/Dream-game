using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private ObjectPool ObjectPool;
    [SerializeField] private string pooledObjectId;
    private float currentX = 0;
    public void spawn(string pooledObjectId)
    {
        var go = ObjectPool.GetObject(pooledObjectId);
        if (go != null)
        {
            go.transform.position = new Vector3(currentX,0,0);
            go.transform.rotation = Quaternion.identity;
            currentX++;

            //StartCoroutine(ReturnObject(go));
        }
        else { 
             
        }
        
    }
    private void ReturnObject()
    {
       
    }
    private void OnGUI()
    {
        if(GUILayout.Button("Spawn Object"))
        {
           spawn(pooledObjectId);
        }
    }
}
