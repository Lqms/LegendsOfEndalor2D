using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherShotScript : MonoBehaviour
{
    // Objects
    ArcherLegsController LegsScript;
    ArcherArmsController ArmsScript;
    
    // Projectile objects
    public ArcherProjectileScript projectile;
    public ArcherProjectileScript altProjectile;
    ArcherProjectileScript activeProjectile;

    // Shot logic
    public float reloadTime;
    float shotTime = Mathf.Infinity;
    bool canShot = false;


    private void Start()
    {
        LegsScript = FindObjectOfType<ArcherLegsController>();
        ArmsScript = FindObjectOfType<ArcherArmsController>();
    }

    void Update()
    {
        CanShot();
    }
    void CanShot()
    {
        if (canShot) return;
        shotTime += Time.deltaTime;
        if (shotTime > reloadTime)
        {
            shotTime = 0;
            canShot = true;
        }
    }
    public void Shot()
    {
        if (canShot)
        {
            float x = transform.position.x + LegsScript.speedVector;
            float y = transform.position.y + Random.Range(-0.2f, 0.2f);
            Vector2 spot = new Vector3(x, y);

            canShot = false;

            if (ArmsScript.isAltShot)
            {
                activeProjectile = altProjectile;
            }
            else
            {
                activeProjectile = projectile;
            }

            ArcherProjectileScript projectileObj = Instantiate(activeProjectile, spot, Quaternion.identity);
            projectileObj.GetComponent<SpriteRenderer>().flipX = LegsScript.speedVector < 0;
            projectileObj.gameObject.transform.Rotate(transform.rotation.eulerAngles, Space.World);
            projectileObj.SetDirection(transform.right * LegsScript.speedVector);
        }
    }
}
