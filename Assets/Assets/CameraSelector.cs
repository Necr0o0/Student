using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CameraSelector : MonoBehaviour
{
	[SerializeField]
	RectTransform crosshair = default;
	[SerializeField]
	Transform holdingPoint = default;

	[SerializeField, Space]
	float maxDistance = 20f;
	[SerializeField]
	float grabbingSpeed = 1f;
	[SerializeField]
	float throwingForce = 1000f;
	[SerializeField]
	float grabbingDistance = 0.5f;
	bool grabbed = false;

	[SerializeField, Space]
	Vector2 crosshairSize = new Vector2(10f, 50f);
	[SerializeField]
	float crosshairChangeSpeed = 5f;
	float size;

    new Camera camera;
	Rigidbody holdingObject;

    Interactive currentInteractive;

    [SerializeField]
    public Material standard;
    public Material outline;

    [Space]
    public GameObject moreInfo;

    private Transform target;

    private void Start()
	{
		camera = GetComponent<Camera>();
		size = crosshair.sizeDelta.x;
	}

	private void Update()
	{
        if (target != null)
        {
            target.GetComponent<Renderer>().material = standard;
        }
       

		//Czy trzyma się jakiegoś przedmiotu
		if (holdingObject == null)
		{
            crosshair.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            crosshair.gameObject.SetActive(true);


			RaycastHit hit;
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			// czy cos sie trafilo
			if (Physics.Raycast(ray, out hit, maxDistance))
			{
                target = hit.transform;
               
				Rigidbody rb = target.GetComponent<Rigidbody>();
                Interactive interactive = target.GetComponent<Interactive>();
                currentInteractive = interactive;

                hit.transform.GetComponent<Renderer>().material = outline;

                // czy trafiono w coś do podnoszenia
                if (rb != null && !rb.isKinematic)
				{

                    // czy trafiono w coś interaktywnego
                    if (interactive != null)
                    {
                        size += crosshairChangeSpeed * Time.deltaTime;
                        crosshair.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                        crosshair.GetComponentInChildren<TextMeshProUGUI>().text = interactive.interactionType;

                        if(interactive.moreInfo != null)
                        {
                            moreInfo.GetComponent<TextMeshProUGUI>().text = interactive.moreInfo;
                        }
                        else
                        {
                            moreInfo.GetComponent<TextMeshProUGUI>().text = " ";
                        }

                        if (Input.GetKeyDown(KeyCode.E))
                            interactive.Interaction();
                    }

                    // Czy naciśnięto przycisk
                    if (Input.GetMouseButtonDown(0) && rb.mass < 8.0f)
                    {
                        hit.transform.GetComponent<Renderer>().material = outline;
                        //crosshair.gameObject.SetActive(false);
                        holdingObject = rb;
						holdingObject.useGravity = false;
					}
                    else if(Input.GetMouseButtonDown(0) && rb.mass >= 8.0f)
                    {
                        moreInfo.GetComponent<TextMeshProUGUI>().text = "Too heavy to lift!";

                    }
                    size += 2 * crosshairChangeSpeed * Time.deltaTime;
                }

                size += 2 * crosshairChangeSpeed * Time.deltaTime;

                
				
				size = Mathf.Clamp(size, crosshairSize.x, crosshairSize.y);
				crosshair.sizeDelta = new Vector2(size, size);
			}
		}
		else
		{
            target.transform.GetComponent<Renderer>().material = outline;

            if (currentInteractive != null)
            {
                size += crosshairChangeSpeed * Time.deltaTime;
                crosshair.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                crosshair.GetComponentInChildren<TextMeshProUGUI>().text = currentInteractive.interactionType;
                if (Input.GetKeyDown(KeyCode.E))
                    currentInteractive.Interaction();
            }

            if (Input.GetMouseButtonDown(0))
			{
				holdingObject.useGravity = true;
				holdingObject = null;
				return;
			}

			if (Input.GetMouseButtonDown(1))
			{
				holdingObject.useGravity = true;
				holdingObject.AddForce(transform.forward * throwingForce);
				holdingObject = null;
				return;
			}
		}
        size -= crosshairChangeSpeed * Time.deltaTime;

        size = Mathf.Clamp(size, crosshairSize.x, crosshairSize.y);
        crosshair.sizeDelta = new Vector2(size, size);
    }

	private void FixedUpdate()
	{
		if (holdingObject != null)
		{
			if (!grabbed)
			{
				holdingObject.MovePosition(Vector3.Lerp(holdingObject.position, holdingPoint.position, Time.deltaTime * grabbingSpeed));
				if (Vector3.Distance(holdingObject.position, holdingPoint.position) <= grabbingDistance)
					grabbed = true;
			}
			else
			{
				holdingObject.MovePosition(holdingPoint.position);
			}
		}
	}
}
