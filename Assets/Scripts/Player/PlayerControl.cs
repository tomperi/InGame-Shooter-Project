using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define data types
public enum Player { right, left };

public class PlayerControl : MonoBehaviour
{
    public GameObject PlayerBullet;
    public GameObject ShotSpawn1;
    public GameObject ShotSpawn2;
    public Player player;
    public float speed, rotateSpeed;
    public float maxAngle;
    public bool dock;

    private string verticalAxis, horizontalAxis, fire;
    private Vector2 min, max;
    private Vector3 shotDirection;
    private float maxAngleUp, maxAngleDown, defaultAngle;
    

	// Use this for initialization
	void Start () {
		if (player.Equals(Player.right))
        {
            verticalAxis = "RightVertical";
            horizontalAxis = "RightHorizontal";
            fire = "right ctrl";
            defaultAngle = 0;
            maxAngleUp = defaultAngle - maxAngle;
            maxAngleDown = defaultAngle + maxAngle;
            shotDirection = new Vector3(0, 0, -90);
        } else
        {
            verticalAxis = "LeftVertical";
            horizontalAxis = "LeftHorizontal";
            fire = "left ctrl";
            defaultAngle = 0;
            maxAngleUp = defaultAngle - maxAngle;
            maxAngleDown = defaultAngle + maxAngle;
            shotDirection = new Vector3(0, 0, 90);
        }
        Debug.Log(player + " " + maxAngleUp + " " + maxAngleDown);
	}
	
	// Update is called once per frame
	void Update () {
        // Move along the y axis
        if (!dock)
        {
            float y = Input.GetAxisRaw(verticalAxis);
            Vector2 yAxisMovement = new Vector2(0, y * speed);
            Move(yAxisMovement);
        }

        // Rotate
        float rotate = Input.GetAxisRaw(horizontalAxis);
        Rotate(rotate);

        if (Input.GetKeyDown(fire))
        {
            Shot();
        }
	}

    void Move(Vector2 direction)
    {
        // Limit the player movement, including the sprite size 
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        // Get the player current position
        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        
        transform.position = pos;
    }

    void Rotate(float rotate)
    {
        Transform staffRotation; 
        defaultAngle += rotate * rotateSpeed *-1;
        defaultAngle = Mathf.Clamp(defaultAngle, maxAngleUp, maxAngleDown);
        staffRotation = transform.GetChild(0);
        staffRotation.localEulerAngles = new Vector3(0, 0, defaultAngle);
        // transform.Rotate(new Vector3(0, 0, rotate * rotateSpeed * -1));
    }

    void Shot()
    {
        GameObject bullet1 = (GameObject)Instantiate(PlayerBullet);
        bullet1.transform.position = ShotSpawn1.transform.position;
        bullet1.transform.rotation = ShotSpawn1.transform.rotation;
        bullet1.transform.eulerAngles += shotDirection;

        //GameObject bullet2 = (GameObject)Instantiate(PlayerBullet);
        //bullet2.transform.position = ShotSpawn2.transform.position;
        //bullet2.transform.rotation = ShotSpawn1.transform.rotation;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Shot")
        {
            Destroy(other.gameObject);
        }
    }
}

