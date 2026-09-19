using System.Collections;
using UnityEngine;

public class SansAttack : MonoBehaviour
{
    public static bool sansAttack;
    public static Transform Transform;

    private void Awake()
    {
        Transform = transform;
    }

    public void SansStartAttack()
    {
        sansAttack = true;
        StartCoroutine(AttackDurationCoroutine());
    }

    public void SansStopAttack()
    {
        sansAttack = false;
    }

    IEnumerator AttackDurationCoroutine()
    {
        yield return new WaitForSeconds(10f);
        SansStopAttack();
    }

}
