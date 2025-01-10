using UnityEngine;
using UnityEngine.EventSystems;

public class MovementObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //Перемещение по локации реализовал довольно просто через скроллвью,
    //но также можно было использовать перемещение пальцем и cinemachine, чтобы ограничить экран
    
    
    private RectTransform objectPosition; //используется для перемещение объекта по экрану
    private Rigidbody2D rigidbody; //добавил для физики

    //Размеры объекта, если он будет лежать на какой-то поверхности
    private Vector3 normalScale = new Vector3(1, 1, 1);
    private Vector3 farScale = new Vector3(.7f, .7f, .7f);
    private Vector3 nearScale = new Vector3(1.5f, 1.5f, 1.5f);

    //Лежит ли объект на поверхности
    private bool objectOnTheSurface = false;
    
    private void Awake()
    {
        objectPosition = GetComponent<RectTransform>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    //Перемещение реализовал через встроенные интерфейы
    //Также можно было реализовать через тачи либо нажатие мышкой на объект
    public void OnDrag(PointerEventData eventData)
    {
        objectPosition.anchoredPosition += eventData.delta;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!objectOnTheSurface)
        {
            rigidbody.constraints = RigidbodyConstraints2D.None;
        }
    }

    //Методы, которые проверяют положили ли мы объект на какую-то поверхность, либо убрали его
    //Заморозку гравитации, когда объект на поверхности реализовал через заморожение перемещение по y
    //Для отслеживания на какой поверхности лежит объект использую пустой объект с коллайдером и нужным тэгом
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FarSurface"))
        {
            objectOnTheSurface = true;
            this.gameObject.transform.localScale = farScale;
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        }

        if (other.CompareTag("NearSurface"))
        {
            objectOnTheSurface = true;
            this.gameObject.transform.localScale = nearScale;
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FarSurface"))
        {
            objectOnTheSurface = false;
            this.gameObject.transform.localScale = normalScale;
            rigidbody.constraints = RigidbodyConstraints2D.None;
        }

        if (other.CompareTag("NearSurface"))
        {
            objectOnTheSurface = false;
            this.gameObject.transform.localScale = normalScale;
            rigidbody.constraints = RigidbodyConstraints2D.None;
        }
    }
}
