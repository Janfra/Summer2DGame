using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    PlayerScriptableObject playerStats;
    // Aiming
    private Camera mainCam;
    Vector2 mousePosition;
    // Shooting
    public Transform firePoint;
    public GameObject throwStarPrefab;
    public float throwForce = 10.0f;
        // Ammo
    [field: SerializeField]
    GameObject textAmmo;
    public int totalAmmo = 5;
    public int ammo;
    public float rechargeTime = 1.5f;
    private bool recharging = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get camera
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        ammo = totalAmmo;
        textAmmo = GameObject.Find("Ammo");
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
            ThrowStar();
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
    }

    // Spawn throw star, store its information and then add force to it to "throw it".
    void ThrowStar()
    {
        GameObject star = Instantiate(throwStarPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D starRB = star.GetComponent<Rigidbody2D>();
        starRB.AddForce(firePoint.up * throwForce, ForceMode2D.Impulse);
        textAmmo.GetComponent<TMP_Text>().text = "Ammo: " + ammo;
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
            textAmmo.GetComponent<TMP_Text>().text = "Recharging...";
            yield return new WaitForSeconds(rechargeTime);
            recharging = false;
        }
        ammo = totalAmmo;
        textAmmo.GetComponent<TMP_Text>().text = "Ammo: " + ammo;
    }
}
