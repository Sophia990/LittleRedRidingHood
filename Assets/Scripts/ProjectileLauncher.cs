using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.transform.position, launchPoint.transform.rotation);
        Vector3 origScale = projectile.transform.localScale;

        // flip the projectile's facing direction and movement based on the direction the player is facing at time of launch
        projectile.transform.localScale = new Vector3(origScale.x * (transform.localScale.x > 0 ? 1 : -1),
            origScale.y, origScale.z);
    }
}
