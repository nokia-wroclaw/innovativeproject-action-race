using UnityEngine;
using System.Collections.Generic;

public class PlayerKickFoot : MonoBehaviour
{
    Collider2D kickFoot;

    void Start()
    {
        kickFoot = GetComponent<Collider2D>();
    }

    public List<Collider2D> CollidingPlayersBodies
    {
        get
        {
            List<Collider2D> colliders = new List<Collider2D>();
            int layerMask = LayerMask.GetMask("Player");
            kickFoot.OverlapCollider(new ContactFilter2D() { layerMask = layerMask }, colliders);
            return colliders;
        }
    }
}
