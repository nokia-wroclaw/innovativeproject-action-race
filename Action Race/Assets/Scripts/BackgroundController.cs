using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] Sprite dayBackground;
    [SerializeField] Sprite nightBackground;

    static BackgroundController _instance;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (_instance != null && _instance != this)
            DestroyImmediate(gameObject);
        else
        {
            _instance = this;
            ObjectExtension.DontDestroyOnLoad(gameObject);

            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void ChangeBackground(bool night)
    {
        if (night)
            spriteRenderer.sprite = nightBackground;
        else
            spriteRenderer.sprite = dayBackground;
    }
}
