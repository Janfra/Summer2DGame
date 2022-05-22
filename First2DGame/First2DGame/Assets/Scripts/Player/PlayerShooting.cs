using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    PlayerMovement_ScriptableObject playerStats;
    // Aiming
    private Camera mainCam;
    Vector2 mousePosition;

    // Shooting
    [field: SerializeField]
    private Transform firePoint;
    [field: SerializeField]
    private float throwForce = 10.0f;
    bool shooting = false;

    // Ammo
    TMP_Text ammoDisplay;
    public int totalAmmo = 5;
    private int ammo;
    public float rechargeTime = 1.5f;
    private bool recharging = false;

    // Bullet Pooling
    ObjectPooling objectPooler;
    [field: SerializeField]
    Rigidbody2D[] bulletRB;
    int currentBullet = 0;
    [field: SerializeField]
    string ammoType;

    // Start is called before the first frame update
    void Awake()
    {
        // Get camera
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // Set the ammo
        ammo = totalAmmo;

        // Get the text on screen and save it to change it
        GameObject ammoFinder = GameObject.Find("Ammo");
        ammoDisplay = ammoFinder.GetComponent<TMP_Text>();

        // Set the playerRB in the scriptable object
        playerStats.PlayerRB = this.GetComponent<Rigidbody2D>();

        bulletRB = new Rigidbody2D[totalAmmo];
    }

    private void Start()
    {
        objectPooler = ObjectPooling.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position and translate it to something usable
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Shooting
        if (Input.GetMouseButtonDown(1) && !recharging)
        {
            ammo -= 1;
            shooting = true;
            if(ammo <= 0)
            {
                OutOfAmmo();
            }
        } 

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Recharging());
        }
    }

    private void FixedUpdate()
    {
        // If you take a vector from another vector, you get a vector drawing a line in between the two, in this case, from mouse to player.
        Vector2 faceDirection = mousePosition - playerStats.PlayerRB.position;
        // Calculate the angle of the resulting vector and convert it to degrees, then change the rotation to it
        float angle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg - 90f;
        playerStats.PlayerRB.rotation = angle;

        if (shooting)
        {
            ThrowStar();
        }
    }

    // Spawn throw star, store its information and then add force to it to "throw it".
    void ThrowStar()
    {
        objectPooler.SpawnFromPool(ammoType, firePoint.position, firePoint.rotation);
        bulletRB[currentBullet].AddForce(firePoint.up * throwForce, ForceMode2D.Impulse);
        NextBullet();
        ammoDisplay.text = "Ammo: " + ammo;
        shooting = false;
    }

    void OutOfAmmo()
    {
        StartCoroutine(Recharging());
    }

    IEnumerator Recharging()
    {
        recharging = true;
        while (recharging)
        {
            ammoDisplay.text = "Recharging...";
            yield return new WaitForSeconds(rechargeTime);
            recharging = false;
        }
        ammo = totalAmmo;
        ammoDisplay.text = "Ammo: " + ammo;
    }

    void BulletStoring()
    {
        for(int i = 0; i < totalAmmo; i++)
        {
            GameObject bullet = objectPooler.SpawnFromPool(ammoType, firePoint.position, firePoint.rotation);
            bulletRB[i] = bullet.GetComponent<Rigidbody2D>();
            bullet.SetActive(false);
        }
    }

    public void BulletStore(Rigidbody2D rigidbody2D)
    {
        bulletRB[currentBullet] = rigidbody2D;
        NextBullet();
    }

    void NextBullet()
    {
        currentBullet++;
        if(currentBullet == totalAmmo)
        {
            currentBullet = 0;
        }
    }
}
