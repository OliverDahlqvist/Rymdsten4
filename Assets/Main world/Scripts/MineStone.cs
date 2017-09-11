using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class MineStone : MonoBehaviour {
    Camera camera;
    GameObject forgeDoor;
    ForgeAnimationScript forgeAnimationScript;

    [SerializeField]
    MiningdrillUpgradeMenu drillUpgradeMenu;

    [SerializeField]
    Camera upgradeCamera;

    [SerializeField]
    ParticleSystem PS_Impact;
    [SerializeField]
    ParticleSystem PS_Sparks;

    GameObject laserBeam;
    [SerializeField]
    GameObject laserSparks;
    [SerializeField]
    GameObject laserImpact;

    CameraShake cameraShake;

    public float stones;
    float upgradeCostPick;
    float upgradeCostInventory;
    public float pickStonePerHit;
    private float timestamp = 0F;
    [SerializeField]
    private float transferMultiplier;
    private bool getCurrentPercentage;
    private float perc;
    private float lerpT;
    private float currentStones;
    private float currentCredits;

    public Stone rayHit;
    public bool addStones;
    public RaycastHit hit;
    public Color textColor;

    private int stonesToAdd;

    private int x;
    private int y;
    public bool hitStone;
    Ray ray;

    void Start () {
        //PlayerClass.displayNotification = false;
        PlayerClass.textValue = 0f;
        forgeDoor = GameObject.FindWithTag("DropOff");
        camera = GetComponent<Camera>();             
		PlayerClass.stones = 0; 
		pickStonePerHit = 10F;
        transferMultiplier = 0;
 
        forgeAnimationScript = forgeDoor.GetComponentInParent<ForgeAnimationScript>();
        cameraShake = GetComponent<CameraShake>();
        laserBeam = GetComponentInChildren<LaserSway>(true).gameObject;

        hitStone = false;
        getCurrentPercentage = true;
        lerpT = 0;

        x = Screen.width / 2;
        y = Screen.height / 2;
    }
    
    void Update () {

        // FORGE && Crafting Station//
        forgeAnimationScript.openDoor = false;
        if (Input.GetKey(KeyCode.E) && PlayerClass.menuActive < 1)
        {
            Ray ray = camera.ScreenPointToRay(new Vector3(x, y, 0));
            if (Physics.Raycast(ray, out hit, 2))
            {
                if (hit.collider.gameObject.GetComponentInParent<ForgeAnimationScript>() && PlayerClass.stones - perc > 0)
                {
                    PlayerClass.usingForge = true;
                    if (getCurrentPercentage)
                    {
                        currentStones = PlayerClass.stones;
                        currentCredits = PlayerClass.credits;
                        getCurrentPercentage = false;
                    }
                    lerpT += Time.deltaTime / 2;
                    forgeAnimationScript = hit.collider.gameObject.GetComponentInParent<ForgeAnimationScript>();
                    forgeAnimationScript.openDoor = true;
                    PlayerClass.stones = Mathf.Lerp(currentStones, 0, lerpT);
                    PlayerClass.credits = Mathf.Lerp(currentCredits, currentCredits + currentStones * PlayerClass.forgeEfficency, lerpT);
                }
                else if (hit.collider.gameObject.CompareTag("CraftingStation"))
                {
                    PlayerClass.menuActive = 1;
                }
                else
                {
                    PlayerClass.usingForge = false;
                    getCurrentPercentage = true;
                    lerpT = 0;
                }
            }
        }
        else
        {
            PlayerClass.usingForge = false;
            getCurrentPercentage = true;
            lerpT = 0;
        }
        //MiningDrill Menu
        if(Input.GetKeyDown(KeyCode.E) && PlayerClass.menuActive < 1)
        {
            Ray ray = camera.ScreenPointToRay(new Vector3(x, y, 0));
            if (Physics.Raycast(ray, out hit, 2))
            {
                if (hit.collider.gameObject.CompareTag("MiningDrill"))
                {
                    PlayerClass.menuActive = 2;
                    drillUpgradeMenu.selectedDrill = hit.collider.gameObject.GetComponentInChildren<DrillPartScript>();
                    drillUpgradeMenu.setSelectedName();
                }
            }
        }

        // MINE //
        if (Input.GetMouseButton(0) && PlayerClass.stones != PlayerClass.inventorySize && PlayerClass.menuActive < 1)
        {
            if (PlayerClass.laserSelected && PlayerClass.stones != PlayerClass.inventorySize)
            {
                laserBeam.gameObject.SetActive(true);
                if (laserBeam.transform.localScale.x < 1)
                {
                    laserBeam.transform.localScale = new Vector3(laserBeam.transform.localScale.x + 0.1f, laserBeam.transform.localScale.y + 0.1f, laserBeam.transform.localScale.z);
                }
            }

            if (!PlayerClass.laserSelected)
            {
                ray = camera.ScreenPointToRay(new Vector3(x, y, 0));
            }
            else
            {
                ray = new Ray(laserBeam.transform.position, laserBeam.transform.forward);

                if (Physics.Raycast(ray, out hit, PlayerClass.mineLength))
                {
                    laserBeam.transform.localScale = new Vector3(laserBeam.transform.localScale.x, laserBeam.transform.localScale.y, hit.distance);
                }
                else
                {
                    laserBeam.transform.localScale = new Vector3(laserBeam.transform.localScale.x, laserBeam.transform.localScale.y, PlayerClass.mineLength);
                }
            }

            if (Time.time >= timestamp)
            {
                timestamp = Time.time + PlayerClass.mineRate;
                
                if (Physics.Raycast(ray, out hit, PlayerClass.mineLength))
                {
                    if (hit.collider.GetComponentInParent<Stone>())
                    {
                        hitStone = true;
                        rayHit = hit.collider.GetComponentInParent<Stone>();
                        if (PlayerClass.laserSelected)
                        {
                            Stone stoneHit = rayHit;
                            if (stoneHit.destroyObject == false)
                            {

                                if (PlayerClass.stones + PlayerClass.stonesPerHitLaser > PlayerClass.inventorySize)
                                {
                                    PlayerClass.stones = PlayerClass.inventorySize;
                                }
                                else
                                {
                                    PlayerClass.stones += PlayerClass.stonesPerHitLaser;
                                }
                                
                                //stoneHit.transform.localScale -= new Vector3(Mathf.Clamp(stoneHit.perc, 0, 0.9f), Mathf.Clamp(stoneHit.perc, 0, 0.9f), Mathf.Clamp(stoneHit.perc, 0, 0.9f));
                                stoneHit.amountStones -= 1;

                                if (!PlayerClass.laserSelected)
                                {
                                    Instantiate(PS_Impact, hit.point, Quaternion.LookRotation(hit.normal));
                                    Instantiate(PS_Sparks, hit.point, Quaternion.LookRotation(hit.normal));
                                }
                                else
                                {
                                    Instantiate(laserSparks, hit.point, Quaternion.LookRotation(hit.normal));
                                    Instantiate(laserImpact, hit.point, Quaternion.LookRotation(hit.normal));
                                }
                                if(cameraShake.shakeDuration < 1f)
                                {
                                    cameraShake.shakeDuration += 0.2f;
                                }
                            }
                            if (Mathf.Floor(stoneHit.amountStones) <= 0)
                            {
                                stoneHit.destroyObject = true;
                            }
                        }
                    }
                    else
                    {
                        hitStone = false;
                    }
                }
                else
                {
                    hit = new RaycastHit();
                    rayHit = null;
                    hitStone = false;
                }
            }
            
        }
        if(!Input.GetMouseButton(0) || !PlayerClass.laserSelected || PlayerClass.stones == PlayerClass.inventorySize)
        {
            // DISABLE LASER //
            if(laserBeam.transform.localScale.x > 0)
            {
                laserBeam.transform.localScale = new Vector3(laserBeam.transform.localScale.x - 0.1f, laserBeam.transform.localScale.y - 0.1f, laserBeam.transform.localScale.z);
            }
            if(laserBeam.transform.localScale.x <= 0)
            {
                laserBeam.gameObject.SetActive(false);
            }
            
        }
    }
}