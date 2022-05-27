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
    [SerializeField]
    BulletsSpawn bulletGeneration;
    bool shooting = false;

    // Ammo display
    TMP_Text ammoDisplay;
    public int totalAmmo = 5;
    private int ammo;
    public float rechargeTime = 1.5f;
    private bool recharging = false;

    // Start is called before the first frame update
    void Awake()
    {
        // Get camera
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // Get the text on screen and save it to change it
        GameObject ammoFinder = GameObject.Find("Ammo");
        ammoDisplay = ammoFinder.GetComponent<TMP_Text>();

        // Set the playerRB in the scriptable object
        playerStats.PlayerRB = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (bulletGeneration == null)
            bulletGeneration = this.GetComponent<BulletsSpawn>();
        // Set the ammo
        totalAmmo = Mathf.Clamp(totalAmmo, 1, ObjectPooling.Instance.GetPoolSize(bulletGeneration.GetAmmoType()));
        ammo = totalAmmo;
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
        float angle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg - 90.0f;
        playerStats.PlayerRB.rotation = angle;

        if (shooting)
        {
            Shoot();
        }
    }

    // Spawn throw star, store its information and then add force to it to "throw it".
    void Shoot()
    {
        bulletGeneration.SpawnBullet();
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

}
