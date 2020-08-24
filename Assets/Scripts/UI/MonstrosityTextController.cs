using extensions;
using UnityEngine;

public class MonstrosityTextController : MonoBehaviour
{
    private float _breathingFrequencyBase = 0.3f;

    private PlayerController _pc;
    
    // Start is called before the first frame update
    void Start()
    {
        _pc = PlayerController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        var freq = _breathingFrequencyBase * _pc.MonstrosityLevel.Interpolate(1, 5);

        gameObject.transform.Let(t =>
        {
            var scaleAmt = Mathf.Sin(Time.time * freq).ReScale(-1, 1, 1f, 1.5f);
            t.localScale = Vector3.one * scaleAmt;
            
            t.eulerAngles = t.eulerAngles.WithZ(Mathf.Sin(Time.time * _pc.MonstrosityLevel.Interpolate(2, 7)) * _pc.MonstrosityLevel * 65);
        });
    }
}
