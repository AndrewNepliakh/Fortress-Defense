using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NPLH
{
    
    public static class TextMeshUtils 
    {
        private static Coroutine _fadeOutRoutine;

        public static void FadeOut(this TextMeshProUGUI textMesh, MonoBehaviour monoBehaviour, float duration = 1.0f)
        {
            StartFadeOutCoroutine(textMesh, monoBehaviour, duration);
        }

        private static void StartFadeOutCoroutine(TextMeshProUGUI textMesh, MonoBehaviour monoBehaviour, float duration)
        {
            if (_fadeOutRoutine == null)
            {
                _fadeOutRoutine = monoBehaviour.StartCoroutine(FadeEnumerator(textMesh, duration));
            }
            else
            {
                monoBehaviour.StopCoroutine(_fadeOutRoutine);
                _fadeOutRoutine = null;
                _fadeOutRoutine = monoBehaviour.StartCoroutine(FadeEnumerator(textMesh, duration));
            }
        }

        private static IEnumerator FadeEnumerator(TextMeshProUGUI textMesh, float duration)
        {
            textMesh.gameObject.SetActive(true);
            textMesh.alpha = 1.0f;

            yield return new WaitForSeconds(1.0f);
            
            while (textMesh.alpha > 0.0f)
            {
                textMesh.alpha -= Time.deltaTime / duration;
                yield return null;
            }
            
            textMesh.gameObject.SetActive(false);
        }
    }
}
