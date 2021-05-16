using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]

public class RayShooter : MonoBehaviour
{
    private new Camera camera;

    void Start()
    {
        this.camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootRay();
        }
    }

    private void ShootRay()
    {
        Vector3 point = new Vector3(this.camera.pixelWidth / 2, this.camera.pixelHeight / 2, 0);
        Ray ray = this.camera.ScreenPointToRay(point);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            GameObject hitObject = raycastHit.transform.gameObject;
            ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

            if (target)
            {
                target.ReactToHit();
                //StartCoroutine(SpinTransform(hitObject.transform, 7f, 2f));
            } else
            {
                StartCoroutine(SpawnSphereIndicator(raycastHit.point));
            }
        }
    }

    private IEnumerator SpinTransform(Transform transform, float speed, float duration)
    {
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            transform.Rotate(0, speed, 0);

            yield return null;
        }

        yield break;
    }

    private IEnumerator SpawnSphereIndicator(Vector3 pos)
    {
        int visibleDuration = 1;
        Time.timeScale = 1;

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(visibleDuration);

        Destroy(sphere);
    }

    private void OnGUI()
    {
        int size = 12;
        float posX = this.camera.pixelWidth / 2 - size / 4;
        float posY = this.camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
