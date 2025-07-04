using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WaterFlowAnimation : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 1f;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1f);
        Vector2 offset = new Vector2(x, 0);
        sr.material.SetTextureOffset("_MainTex", offset);
    }
}
