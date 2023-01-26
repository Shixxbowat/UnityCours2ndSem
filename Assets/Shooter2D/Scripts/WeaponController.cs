using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // SHOOTS
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Rigidbody2D rbBullet;

    [SerializeField] Transform _pivotRotation;
    [SerializeField] private SpriteRenderer weaponRenderer;
    [SerializeField] private bool shooting;
    private float speedBullet;
    private float reloadTime;
    [SerializeField] private float resetReload = .1f;

    public Transform pivotRotation => _pivotRotation;
    public bool Shooting
    {
        get => shooting;
        set => shooting = value;   
    }
    // Start is called before the first frame update

    public void SetPivotRotation(Vector3 rotationEuler)
    {
        _pivotRotation.eulerAngles = rotationEuler;
    }
    public void SetDirection(Vector3 direction)
    {
        _pivotRotation.right = direction;
    }

    public void Shoot()
    {
        reloadTime -= Time.deltaTime;
        if (shooting && reloadTime < 0)
        {
            Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            reloadTime = resetReload;
        }
    }
    private void Awake()
    {
        speedBullet = 100f;
        rbBullet = bulletPrefab.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        // Affiche correctement le sprite de larme suivant langle
        weaponRenderer.flipY = Mathf.Abs(_pivotRotation.localEulerAngles.z) > 90f && Mathf.Abs(_pivotRotation.localEulerAngles.z) < 270f;
        Debug.Log(Mathf.Abs(_pivotRotation.localEulerAngles.z));
    }
}
