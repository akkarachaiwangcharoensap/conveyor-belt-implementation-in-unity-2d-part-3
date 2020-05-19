using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    /**
     * <summary>
     * Speed
     * </summary>
     */
    private float _speed = 1;

    public float speed
    {
        set
        {
            this._speed = value;
        }

        get
        {
            return this._speed;
        }
    }

    /**
     * <summary>
     * The direction the item is moving
     * </summary>
     */
    private ItemDirection _direction;

    public ItemDirection direction
    {
        set
        {
            this._direction = value;
        }

        get
        {
            return this._direction;
        }
    }

    /**
     * <summary>
     * Should the item stopped
     * </summary>
     */
    private bool _stop = false;

    public bool stop
    {
        set
        {
            this._stop = value;
        }

        get
        {
            return this._stop;
        }
    }

    /**
     * <summary>
     * The 2d collision bounds
     * </summary>
     */
    private Bounds _bounds;

    // Start is called before the first frame update
    private void Start()
    {
        // Set default values
        this.direction = ItemDirection.Right;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {

    }

    /**
     * <summary>
     * Check the front collision
     * </summary>
     *
     */
    private void CheckItemsCollision()
    {
        this._bounds = this.GetComponent<Collider2D>().bounds;

        Collider2D[] colliders;
        float offset = 0.01f;

        Vector2 point;
        Vector2 size;

        // Left direction
        if (this.direction == ItemDirection.Left)
        {
            size = new Vector2(0.01f, this._bounds.size.y / 2);
            point = new Vector2(this._bounds.min.x - offset, this._bounds.center.y);
            colliders = Physics2D.OverlapBoxAll(point, size, 0);

            foreach (Collider2D collider in colliders)
            {
                // If colliding with other items
                if (collider.tag == Tags.Item && collider.name != this.name)
                {
                    this.stop = true;
                    return;
                }
            }
        }

        // Right direction
        else if (this.direction == ItemDirection.Right)
        {
            size = new Vector2(0.01f, this._bounds.size.y / 2);
            point = new Vector2(this._bounds.max.x + offset, this._bounds.center.y);
            colliders = Physics2D.OverlapBoxAll(point, size, 0);

            foreach (Collider2D collider in colliders)
            {
                // If colliding with other items
                if (collider.tag == Tags.Item && collider.name != this.name)
                {
                    this.stop = true;
                    return;
                }
            }
        }

        // Up direction
        else if (this.direction == ItemDirection.Up)
        {
            size = new Vector2(this._bounds.size.x / 2, 0.01f);
            point = new Vector2(this._bounds.center.x, this._bounds.max.y + offset);
            colliders = Physics2D.OverlapBoxAll(point, size, 0);

            foreach (Collider2D collider in colliders)
            {
                // If colliding with other items
                if (collider.tag == Tags.Item && collider.name != this.name)
                {
                    this.stop = true;
                    return;
                }
            }
        }

        // Down direction
        else if (this.direction == ItemDirection.Down)
        {
            size = new Vector2(this._bounds.size.x / 2, 0.01f);
            point = new Vector2(this._bounds.center.x, this._bounds.min.y - offset);
            colliders = Physics2D.OverlapBoxAll(point, size, 0);

            foreach (Collider2D collider in colliders)
            {
                // If colliding with other items
                if (collider.tag == Tags.Item && collider.name != this.name)
                {
                    this.stop = true;
                    return;
                }
            }
        }

        this.stop = false;
    }

    /**
     * <summary>
     * Move left
     * </summary>
     *
     * <returns>
     * void
     * </returns>
     */
    public void MoveLeft()
    {
        this.CheckItemsCollision();

        // Should the item be stopped?
        if (this.stop)
        {
            return;
        }

        Vector3 nextPosition = new Vector3(
            this.transform.position.x - this.speed * Time.deltaTime,
            this.transform.position.y,
            this.transform.position.z
        );

        this.transform.position = nextPosition;
    }

    /**
     * <summary>
     * Move right
     * </summary>
     *
     * <returns>
     * void
     * </returns>
     */
    public void MoveRight()
    {
        this.CheckItemsCollision();

        // Should the item be stopped?
        if (this.stop)
        {
            return;
        }

        Vector3 nextPosition = new Vector3(
            this.transform.position.x + this.speed * Time.deltaTime,
            this.transform.position.y,
            this.transform.position.z
        );

        this.transform.position = nextPosition;
    }

    /**
     * <summary>
     * Move down
     * </summary>
     *
     * <returns>
     * void
     * </returns>
     */
    public void MoveDown()
    {
        this.CheckItemsCollision();

        // Should the item be stopped?
        if (this.stop)
        {
            return;
        }

        Vector3 nextPosition = new Vector3(
            this.transform.position.x,
            this.transform.position.y - this.speed * Time.deltaTime,
            this.transform.position.z
        );

        this.transform.position = nextPosition;
    }

    /**
     * <summary>
     * Move up
     * </summary>
     *
     * <returns>
     * void
     * </returns>
     */
    public void MoveUp()
    {
        this.CheckItemsCollision();

        // Should the item be stopped?
        if (this.stop)
        {
            return;
        }

        Vector3 nextPosition = new Vector3(
            this.transform.position.x,
            this.transform.position.y + this.speed * Time.deltaTime,
            this.transform.position.z
        );

        this.transform.position = nextPosition;
    }
}
