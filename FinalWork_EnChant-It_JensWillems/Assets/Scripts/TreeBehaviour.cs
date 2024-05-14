using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public float burnDuration = 10f;
    public float colorChangeDuration = 5f; // Duration over which the color changes to black
    public bool startBurningOnStart = false;

    public Color topColorWhenNotBurning = new Color(89f / 255f, 19f / 255f, 76f / 255f);
    public Color groundColorWhenNotBurning = new Color(142f / 255f, 4f / 255f, 255f / 255f);

    private Material[] initialMaterials;
    private bool isBurning = false;
    private float burnTimer = 0f;
    private float colorChangeTimer = 0f;
    private Color initialTopColor;
    private Color initialGroundColor;

    void Start()
    {
        initialMaterials = GetComponent<Renderer>().materials;
        // Set initial colors if the tree is not set to start burning on start
        if (!startBurningOnStart)
        {
            SetColors(topColorWhenNotBurning, groundColorWhenNotBurning);
        }
        else
        {
            StartBurning(); // Start burning immediately if set to do so
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fireball") && !isBurning)
        {
            StartBurning();
        }
    }

    void Update()
    {
        if (startBurningOnStart)
        {
            StartBurning();
        }
        if (isBurning)
        {
            burnTimer += Time.deltaTime;
            if (burnTimer >= burnDuration)
            {
                Debug.Log("Burning stopped!");
                StopBurning();
            }
        }
    }

    void StartBurning()
    {
        isBurning = true;
        Debug.Log("Tree is now burning!");
        Material[] materials = GetComponent<Renderer>().materials;
        // Store initial colors
        initialTopColor = materials[1].GetColor("_TopColor");
        initialGroundColor = materials[1].GetColor("_GroundColor");
        // Change the color properties to black instantly
        materials[1].SetColor("_TopColor", Color.black);
        materials[1].SetColor("_GroundColor", Color.black);
        GetComponent<Renderer>().materials = materials;
        fireParticles.Play();

        // Start gradual color change back to initial color over specified duration
        colorChangeTimer = 0f;
        InvokeRepeating("GradualColorChange", 0f, colorChangeDuration / 50f); // Adjust the division factor to control the smoothness of color change
    }

    void GradualColorChange()
    {
        colorChangeTimer += Time.deltaTime;
        float colorChangeProgress = Mathf.Clamp01(colorChangeTimer / colorChangeDuration);
        Color currentTopColor = Color.Lerp(Color.black, initialTopColor, colorChangeProgress);
        Color currentGroundColor = Color.Lerp(Color.black, initialGroundColor, colorChangeProgress);
        Material[] materials = GetComponent<Renderer>().materials;
        materials[1].SetColor("_TopColor", currentTopColor);
        materials[1].SetColor("_GroundColor", currentGroundColor);
        GetComponent<Renderer>().materials = materials;

        if (colorChangeProgress >= 1f)
        {
            // Color change completed, stop repeating the color change method
            CancelInvoke("GradualColorChange");
        }
    }

    void StopBurning()
    {
        isBurning = false;
        colorChangeTimer = 0f;
        Material[] materials = GetComponent<Renderer>().materials;
        // Restore the initial color properties
        materials[1].SetColor("_TopColor", initialTopColor);
        materials[1].SetColor("_GroundColor", initialGroundColor);
        GetComponent<Renderer>().materials = materials;
        fireParticles.Stop();
        burnTimer = 0f;
    }

    void SetColors(Color topColor, Color groundColor)
    {
        Debug.Log("Setting colors - Top: " + topColor.ToString() + ", Ground: " + groundColor.ToString());
        Material[] materials = GetComponent<Renderer>().materials;
        materials[1].SetColor("_TopColor", topColor);
        materials[1].SetColor("_GroundColor", groundColor);
        GetComponent<Renderer>().materials = materials;
    }
}
