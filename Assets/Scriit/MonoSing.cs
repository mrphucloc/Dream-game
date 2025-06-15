using UnityEngine;

public class MonoSing<T> : MonoBehaviour
{
    public static T Instance { get; private set; }

   
    private void Awake()
    {
        if (instance == null)
        {
            instance = (T)(object)this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Debug.LogWarning($"An instance of {typeof(T).Name} already exists. Destroying duplicate.");
            Destroy(gameObject);
        }
    }
}
