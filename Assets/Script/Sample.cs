using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Sample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var cts = new CancellationTokenSource();
        var ct = cts.Token;
        Debug.Log($"IsCabsel={ct.IsCancellationRequested}");
        cts.Cancel();
        Debug.Log($"IsCabsel ={ ct.IsCancellationRequested}");

        StartCoroutine(WaitClickAsync(cts));
        StartCoroutine(DoAsync(cts.Token));
    }

    private bool _isCancel;
    private IEnumerator WaitClickAsync(CancellationTokenSource cts)
    {
        Debug.Log("入力待機");
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        cts.Cancel();
        Debug.Log("入力終了");
    }

    private IEnumerator DoAsync(CancellationToken ct)
    {
        Debug.Log("処理の開始");
        while (!ct.IsCancellationRequested)
        {
            yield return null;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
