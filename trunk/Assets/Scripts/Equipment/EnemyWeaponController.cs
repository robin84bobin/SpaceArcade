using UnityEngine;

public class EnemyWeaponController : WeaponController
{
    void Update()
    {
       /* _fwd = transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(bulletSpawnPoint.position, fwd * direction, Color.cyan);
        if ( Physics.Raycast( bulletSpawnPoint.position, _fwd, out _hit, distance))
        {
            if (_hit.collider.CompareTag(targetTag))
            {
                Attack();
            }
        }*/
    }

}
