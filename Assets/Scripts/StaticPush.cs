using UnityEngine;
using System.Collections;

public class StaticPush : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        // force is how forcefully we will push the player away from the enemy.
        float force = 100;

        // If the object we hit is the enemy
        if (col.gameObject.tag == "Player")
        {
            // Calculate Angle Between the collision point and the player
            Vector2 dir = col.transform.position - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * force);
        }
    }
}
