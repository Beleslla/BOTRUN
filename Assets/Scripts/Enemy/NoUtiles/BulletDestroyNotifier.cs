using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyNotifier : MonoBehaviour
{
    public delegate void BulletDestroyAction();
    public event BulletDestroyAction OnBulletDestroy;

    private void OnDestroy()
    {
        OnBulletDestroy?.Invoke();
    }
}
