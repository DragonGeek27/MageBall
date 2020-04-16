using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpells : MonoBehaviour
{
    [System.Serializable]
    public struct RangeOfIntegers
    {
        public int Minimum;
        public int Maximum;
    }

    [System.Serializable]
    public struct RangeOfFloats
    {
        public float Minimum;
        public float Maximum;
    }
    public float StartTime = 1.0f;
    public float StopTime = 3.0f;
    public float Duration = 2.0f;

    private float startTimeMultiplier;
    private float startTimeIncrement;
    private float stopTimeMultiplier;
    private float stopTimeIncrement;

    public delegate void SpellSwarmCollisionDelegate(RainSpells script, GameObject Spell);
    public GameObject spellPrefab;
    public float DestinationRadius;
    public Vector3 Source;
    public float SourceRadius;
    public float TimeToImpact = 1.0f;
    public RangeOfIntegers SpellsPerSecondRange = new RangeOfIntegers { Minimum = 5, Maximum = 10 };
    public RangeOfFloats ScaleRange = new RangeOfFloats { Minimum = 0.25f, Maximum = 1.5f };
    public RangeOfFloats SpellLifeTimeRange = new RangeOfFloats { Minimum = 4.0f, Maximum = 8.0f };
    
    [HideInInspector]
    public event SpellSwarmCollisionDelegate CollisionDelegate;

    private float elapsedSecond = 1.0f;

    public GameObject player;
    public float spellHeiight;
    private IEnumerator SpawnSpell()
    {
        {
            float delay = UnityEngine.Random.Range(0.0f, 1.0f);
            yield return new WaitForSeconds(delay);
        }
        player = GameObject.Find("FireMage(Clone)");
        Source.x = player.transform.position.x;
        Source.z = player.transform.position.z;
        Source.y = player.transform.position.y + spellHeiight;

        Vector3 src = Source + (UnityEngine.Random.insideUnitSphere * SourceRadius);
        GameObject spell = GameObject.Instantiate(spellPrefab);
        float scale = UnityEngine.Random.Range(ScaleRange.Minimum, ScaleRange.Maximum);
        spell.transform.localScale = new Vector3(scale, scale, scale);
        spell.transform.position = src;
        Vector3 dest = gameObject.transform.position + (UnityEngine.Random.insideUnitSphere * DestinationRadius);
        dest.y = 0.0f;
        // get the direction and set speed based on how fast the spell should arrive at the destination
        Vector3 dir = (dest - src);
        Vector3 vel = dir / TimeToImpact;
        Rigidbody r = spell.GetComponent<Rigidbody>();
        r.velocity = vel;
        float xRot = UnityEngine.Random.Range(-90.0f, 90.0f);
        float yRot = UnityEngine.Random.Range(-90.0f, 90.0f);
        float zRot = UnityEngine.Random.Range(-90.0f, 90.0f);
        r.angularVelocity = new Vector3(xRot, yRot, zRot);
        r.mass *= (scale * scale);

    }

    private IEnumerator CleanupEverythingCoRoutine()
    {
        // 2 extra seconds just to make sure animation and graphics have finished ending
        yield return new WaitForSeconds(StopTime + 2.0f);

        GameObject.Destroy(gameObject);
    }
    public virtual void Stop()
    {
        if (Stopping)
        {
            return;
        }
        Stopping = true;

        // cleanup particle systems
        foreach (ParticleSystem p in gameObject.GetComponentsInChildren<ParticleSystem>())
        {
            p.Stop();
        }

        StartCoroutine(CleanupEverythingCoRoutine());
    }

    public bool Starting
    {
        get;
        private set;
    }

    public float StartPercent
    {
        get;
        private set;
    }

    public bool Stopping
    {
        get;
        private set;
    }

    public float StopPercent
    {
        get;
        private set;
    }

protected virtual void Awake()
    {
        Starting = true;
        int spellLayer = UnityEngine.LayerMask.NameToLayer("Spell");
        UnityEngine.Physics.IgnoreLayerCollision(spellLayer, spellLayer);
    }

     void Start()
    {
        stopTimeMultiplier = 1.0f / StopTime;
        startTimeMultiplier = 1.0f / StartTime;
    }

        private void SpawnSpells()
    {
        int count = (int)UnityEngine.Random.Range(SpellsPerSecondRange.Minimum, SpellsPerSecondRange.Maximum);
        for (int i = 0; i < count; i++)
        {
            StartCoroutine(SpawnSpell());
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        Duration -= Time.deltaTime;
        if (Stopping)
        {
            // increase the stop time
            stopTimeIncrement += Time.deltaTime;
            if (stopTimeIncrement < StopTime)
            {
                StopPercent = stopTimeIncrement * stopTimeMultiplier;
            }
        }
        else if (Starting)
        {
            // increase the start time
            startTimeIncrement += Time.deltaTime;
            if (startTimeIncrement < StartTime)
            {
                StartPercent = startTimeIncrement * startTimeMultiplier;
            }
            else
            {
                Starting = false;
            }
        }
        else if (Duration <= 0.0f)
        {
            // time to stop, no duration left
            Stop();
        }

        if (Duration > 0.0f && (elapsedSecond += Time.deltaTime) >= 1.0f)
        {
            elapsedSecond = elapsedSecond - 1.0f;
            SpawnSpells();
        }
    }
    private IEnumerator CleanupSpells(float delay, GameObject obj)
    {
        yield return new WaitForSeconds(delay);

        GameObject.Destroy(obj.GetComponent<Collider>());
        GameObject.Destroy(obj.GetComponent<Rigidbody>());
        GameObject.Destroy(obj.GetComponent<TrailRenderer>());
    }
    public void HandleCollision(GameObject obj, Collision col)
    {
        Renderer r = obj.GetComponent<Renderer>();
        if (r == null)
        {
            return;
        }
        else if (CollisionDelegate != null)
        {
            CollisionDelegate(this, obj);
        }

        Vector3 pos, normal;
        if (col.contacts.Length == 0)
        {
            pos = obj.transform.position;
            normal = -pos;
        }
        else
        {
            pos = col.contacts[0].point;
            normal = col.contacts[0].normal;
        }

        GameObject.Destroy(r);

        StartCoroutine(CleanupSpells(0.1f, obj));
        GameObject.Destroy(obj, 4.0f);
    }
}