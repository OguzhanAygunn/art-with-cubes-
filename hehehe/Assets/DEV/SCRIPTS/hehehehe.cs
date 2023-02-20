using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class hehehehe : MonoBehaviour
{
    public Camera cam;
    public GameObject cube;
    Texture2D texture;
    float heheValue = 5;
    MeshRenderer Renderer;
    Collider collider;
    bool heheheactive;
    Vector3 pos, cubeScale, scale;
    public float CubeSize;
    void Start()
    {
        cam      = GetComponent<Camera>();
        Renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<MeshCollider>();
        texture  = Renderer.material.mainTexture as Texture2D;
        cube.transform.localScale = Vector3.one * CubeSize;

        pos       = transform.position;
        cubeScale = cube.transform.localScale;
        scale     = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !heheheactive)
        {
            StartCoroutine(nameof(heheheho));
        }
    }

    void hehePaintOP(Vector3 rayPos)
    {
        RaycastHit hit;
        if (!Physics.Raycast(rayPos, Vector3.forward, out hit, 3f))
            return;

        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= texture.width;
        pixelUV.y *= texture.height;
        Color color = texture.GetPixel((int)pixelUV.x, (int)pixelUV.y);
        //ColorControl(hit.textureCoord);
        GameObject cube_ = Instantiate(cube, hit.point, Quaternion.identity);
        cube_.transform.localScale = cubeScale * 2f;
        cube_.transform.DOScale(cubeScale, 2f).SetEase(Ease.OutElastic);
        cube_.GetComponent<MeshRenderer>().material.color = color;
    }

    bool ColorControl(Vector3 rayPos)
    {
        RaycastHit hit;
        if (!Physics.Raycast(rayPos, Vector3.forward, out hit, 3f))
            return false;

        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= texture.width;
        pixelUV.y *= texture.height;
        Color color = texture.GetPixel((int)pixelUV.x, (int)pixelUV.y);
        if (color != Color.white && color != Color.clear)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator heheheho()
    {
        yield return new WaitForSeconds(1f);
        Vector3 rayPos = pos;
        rayPos.y += scale.y * 5 - (cubeScale.y / 2);
        rayPos.x -= scale.x * 5 - (cubeScale.x / 2);
        rayPos.z -= 1f;
        //Instantiate(cube,pos,Quaternion.identity);
        while (true)
        {
            if (ColorControl(rayPos)) {
                hehePaintOP(rayPos);
                yield return new WaitForSeconds(0.001f);
            }
            rayPos.x += cubeScale.x;
            if (rayPos.x >= pos.x + scale.x * 5)
            {
                rayPos.x = pos.x;
                rayPos.x -= scale.x * 5 - (cubeScale.x / 2);
                rayPos.y -= cubeScale.y;
            }

            if (rayPos.y < -(scale.y * 5 - (cubeScale.y / 2)))
            {
                collider.enabled = false;
                yield return new WaitForSeconds(4f);
                //CubeHandler.Instance.AddComponentCubes();
                //CubeHandler.Instance.ExplosionForceOP();
                break;
            }
        }
    }
}
