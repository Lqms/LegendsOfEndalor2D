using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageShotScript : MonoBehaviour
{
    // Objects
    MageLegsController LegsScript;
    MageArmsController ArmsScript;

    // Projectile objects
    public MageProjectileScript projectile;
    public MageProjectileScript altProjectile;
    MageProjectileScript activeProjectile;

    // Shot logic
    public float reloadTime;
    float shotTime = Mathf.Infinity;
    bool canShot = false;


    private void Start()
    {
        LegsScript = FindObjectOfType<MageLegsController>();
        ArmsScript = FindObjectOfType<MageArmsController>();
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

            MageProjectileScript projectileObj = Instantiate(activeProjectile, spot, Quaternion.identity);
            projectileObj.GetComponent<SpriteRenderer>().flipX = LegsScript.speedVector < 0;
            projectileObj.gameObject.transform.Rotate(transform.rotation.eulerAngles, Space.World);
            projectileObj.SetDirection(transform.right * LegsScript.speedVector);
        }
    }
}
