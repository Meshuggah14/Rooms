using System.Collections;
using UnityEngine;

namespace Utils
{
    public interface ICoroutineExecutor
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}