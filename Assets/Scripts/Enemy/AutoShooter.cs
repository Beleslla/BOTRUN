using System.Collections;
using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    public GameObject[] bulletPrefabs;
    public Transform shootPoint;
    public float minInitialDelay = 1f;
    public float maxInitialDelay = 3f;
    public float minRepeatingDelay = 10f;
    public float maxRepeatingDelay = 20f;
    public int maxBulletsOnScreen = 10;  // Límite de balas en pantalla

    private int bulletsOnScreen = 0;

    private void Start()
    {
        float initialDelay = Random.Range(minInitialDelay, maxInitialDelay);
        InvokeRepeating("ShootBullet", initialDelay, Random.Range(minRepeatingDelay, maxRepeatingDelay));
    }

    private void ShootBullet()
    {
        if (bulletsOnScreen < maxBulletsOnScreen)
        {
            GameObject selectedPrefab = bulletPrefabs[Random.Range(0, bulletPrefabs.Length)];

            GameObject bullet = Instantiate(selectedPrefab, shootPoint.position, shootPoint.rotation);

            // Agrega el componente BulletDestroyNotifier y configura el evento OnBulletDestroy
            BulletDestroyNotifier destroyNotifier = bullet.AddComponent<BulletDestroyNotifier>();
            destroyNotifier.OnBulletDestroy += () => OnBulletDestroy(bullet);

            bulletsOnScreen++;
        }
    }

    private void OnBulletDestroy(GameObject bullet)
    {
        // Reduce el contador de balas en pantalla cuando una bala se destruye
        bulletsOnScreen = Mathf.Max(0, bulletsOnScreen - 1);
    }
}