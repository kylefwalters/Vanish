using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupItem : ItemBase
{
    public enum types
    {
        candy,
    }
    public types itemType = types.candy;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<topdown_Player>().lastItem = gameObject;
            /*ItemEffect();

            Destroy(this);*/
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject _lastItem = collision.GetComponent<topdown_Player>().lastItem;
            if (_lastItem == gameObject)
                collision.GetComponent<topdown_Player>().lastItem = null;
        }
    }
}
