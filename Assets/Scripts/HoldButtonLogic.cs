using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButtonLogic : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Referencias")]
    public RectTransform maskRectTransform; 
    public GameObject objectToToggle;

    [Header("Configuración")]
    public float holdDuration = 2f;         // Segundos que hay que mantener pulsado

    private float currentHoldTime = 0f;
    private bool isHolding = false;
    private float maxWidth;

    void Start()
    {
        // Guardamos el ancho máximo del botón para saber hasta dónde llenar
        maxWidth = GetComponent<RectTransform>().rect.width;
        // Empezamos con la máscara cerrada (ancho 0)
        maskRectTransform.sizeDelta = new Vector2(0, maskRectTransform.sizeDelta.y);
    }

    void Update()
    {
        if (isHolding)
        {
            currentHoldTime += Time.deltaTime;
            float progress = Mathf.Clamp01(currentHoldTime / holdDuration);

            // Ampliamos el ancho de la máscara según el progreso
            maskRectTransform.sizeDelta = new Vector2(
                Mathf.Lerp(0, maxWidth, progress),
                maskRectTransform.sizeDelta.y
            );

            if (currentHoldTime >= holdDuration)
            {
                // Acción completada: toggle del objeto
                objectToToggle.SetActive(!objectToToggle.activeSelf);
                ResetButton();
            }
        }
        else if (currentHoldTime > 0)
        {
            // Si sueltas antes de tiempo, la barra baja (el doble de rápido)
            currentHoldTime -= Time.deltaTime * 2f;
            if (currentHoldTime < 0) currentHoldTime = 0;

            float progress = Mathf.Clamp01(currentHoldTime / holdDuration);
            maskRectTransform.sizeDelta = new Vector2(
                Mathf.Lerp(0, maxWidth, progress),
                maskRectTransform.sizeDelta.y
            );
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
    }

    private void ResetButton()
    {
        isHolding = false;
        currentHoldTime = 0f;
        maskRectTransform.sizeDelta = new Vector2(0, maskRectTransform.sizeDelta.y);
    }
}
