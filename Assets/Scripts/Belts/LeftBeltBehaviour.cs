﻿using UnityEngine;
using System.Collections;

public class LeftBeltBehaviour : BeltBehaviour
{
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    /**
     * <summary>
     * Watch for the item on top of the belt
     * </summary>
     */
    protected override void WatchForItem()
    {
        Bounds bounds = this.transform.GetComponent<Collider2D>().bounds;
        Vector2 size = bounds.size;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(this.transform.position, size, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == Tags.Item)
            {
                Transform item = collider.GetComponent<Transform>();
                ItemBehaviour itemBehaviour = item.GetComponent<ItemBehaviour>();
                Bounds itemBounds = item.GetComponent<Collider2D>().bounds;
                Vector2 itemPoint = new Vector2(itemBounds.max.x, itemBounds.min.y);
                
                // Do not move the item if the item point is not on the belt
                if (!bounds.Contains(itemPoint))
                {
                    continue;
                }

                // Determine the direction of the item it is moving.
                // If the item is heading up and it has not reached the origin point. Keep moving up.
                if (itemBehaviour.direction == ItemDirection.Up && item.position.y < this.transform.position.y)
                {
                    itemBehaviour.direction = ItemDirection.Up;
                    itemBehaviour.speed = 5f;
                    itemBehaviour.MoveUp();
                }
                // If the item is heading down and it has not reached the origin point. Keep moving down.
                else if (itemBehaviour.direction == ItemDirection.Down && item.position.y > this.transform.position.y)
                {
                    itemBehaviour.direction = ItemDirection.Down;
                    itemBehaviour.speed = 5f;
                    itemBehaviour.MoveDown();
                }
                // Move left
                else
                {
                    itemBehaviour.direction = ItemDirection.Left;
                    itemBehaviour.speed = 5f;
                    itemBehaviour.MoveLeft();
                }
            }
        }
    }
}
