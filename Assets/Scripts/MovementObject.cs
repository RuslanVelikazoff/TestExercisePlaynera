using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementObject : MonoBehaviour, IDragHandler
{
    private RectTransform objectPosition;
    private Rigidbody2D rigidbody;

    private Vector3 normalScale = new Vector3(1, 1, 1);
    private Vector3 farScale = new Vector3(.7f, .7f, .7f);
    private Vector3 nearScale = new Vector3(1.3f, 1.3f, 1.3f);
    
    private void Awake()
    {
        objectPosition = GetComponent<RectTransform>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        objectPosition.anchoredPosition += eventData.delta;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FarSurface"))
        {
            this.gameObject.transform.localScale = farScale;
        }

        if (other.CompareTag("NearSurface"))
        {
            this.gameObject.transform.localScale = nearScale;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FarSurface"))
        {
            this.gameObject.transform.localScale = normalScale;
        }

        if (other.CompareTag("NearSurface"))
        {
            this.gameObject.transform.localScale = normalScale;
        }
    }
}
