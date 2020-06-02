using UnityEngine;

public class GameHudSingleton : MonoBehaviour
{
    static GameHudSingleton _instance;

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
