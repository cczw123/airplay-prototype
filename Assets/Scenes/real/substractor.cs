using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;

public class substractor : OpenCvSharp.Demo.WebCamera
{
    Mat frame;
    Mat framemask;
    public Texture2D texture;
    BackgroundSubtractorKNN SubtractorKNN;
    
    // Start is called before the first frame update
    void Start()
    {
        frame = OpenCvSharp.Unity.TextureToMat(texture);
        framemask = new Mat();
        SubtractorKNN = BackgroundSubtractorKNN.Create();
        SubtractorKNN.Apply(frame, framemask);
        this.GetComponent<RawImage>().texture = OpenCvSharp.Unity.MatToTexture(framemask);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        throw new System.NotImplementedException();
    }
}
