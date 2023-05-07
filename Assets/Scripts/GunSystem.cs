using UnityEngine;
using TMPro;
using EZCameraShake;

public class GunSystem : MonoBehaviour
{

    public Animator anim;
    // the animation can be selected in the main Unity design space
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    // these are the different editable variables that determine different aspects of the gun. This gives me more control.

    bool shooting, readyToShoot, reloading;
    //boolean values

    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    // references for other aspects, like recognising what an enemy is and when the raycast bullets hit them.

    public GameObject muzzleFlash, bulletHoleGraphic;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;
    //takes the values from the camera shake script to make them individually editable again.

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

        anim = gameObject.GetComponent<Animator>();
        // on start the game will get the information of the animation
    }
    private void Update()
    {
        MyInput();

        text.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);
        // the shoot button can be either tapped once or held down, dependant on if it is allowed in the allowButtonHold variable.

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        // the player can press 'R' to reload, but only if they dont have a full mag or they arent already reloading.


        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            //when calling the shoot function, the amount of bullets fired will be the same as the amount of times the input key is pressed.
            Shoot();
        }
        // the program will carry out the shooting function, if any of the above variables are met.
    }
    private void Shoot()
    {
        readyToShoot = false;


        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        // the bigger the spread, the bigger difference between x and y values.


        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);
        // the spread will still be contained to a forwards direction.


        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"));
                //rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage);

            // a raycast bullet is an invisible ray that gets shot out, this code makes it so that the start point of the ray is the camera,
            // and it goes in a straight line from there. theres also the setup for enemy damage code.
        }


        CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        Debug.Log("Working shake");
        //just enables the camera shake to play once following the set values for magnitude, roughness, fade in and fade out.

        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;
        // these two values will count down in order to recognise how they well correlate in the other parts of the code.


        Invoke("ResetShot", timeBetweenShooting);
        // there will be a slight delay between resetting your shoot(from reloading ect.), and then continuing to.

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
        //if the reload key is pressed, the ReloadFinished function will run.

        anim.Play("sgreload");
        // if the reload key is pressed, the "reload" animation will play.

    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}