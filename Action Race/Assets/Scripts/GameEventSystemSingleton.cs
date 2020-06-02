using UnityEngine;

public class GameEventSystemSingleton : MonoBehaviour
{
    static GameEventSystemSingleton _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
            DestroyImmediate(gameObject);
        else
        {
            _instance = this;
            ObjectExtension.DontDestroyOnLoad(gameObject);
        }
    }
}
