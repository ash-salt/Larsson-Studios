using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [ColorUsage(true, true)]
    [SerializeField] private Color _flashcolor = Color.white;
    [SerializeField] private float _flashTime = 0.25f;

    private SpriteRenderer _spriteRenderer;
    private Material _material;

    private Coroutine _damageFlashCoroutine;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Init();
    }

    private void Init()
    {
        _material = _spriteRenderer.material;
    }

    public void CallFlashDamage()
    {
        _damageFlashCoroutine = StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        SetFlashColor();

        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while(elapsedTime < _flashTime)
        {
            elapsedTime += Time.deltaTime;
            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime/_flashTime));
            SetFlashAmount(currentFlashAmount);

            yield return null;
        }
    }

    private void SetFlashColor()
    {
        _material.SetColor("_FlashColor", _flashcolor);
    }

    private void SetFlashAmount(float amount)
    {
        _material.SetFloat("_FlashAmount", amount);        
    }
}
