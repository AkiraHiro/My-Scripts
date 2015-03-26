#pragma strict

var FPScamera : Camera;
var TPcamera : Camera;
private var camSwitch : boolean = false;

function Start()
{
   FPScamera.camera.enabled = true;
   TPcamera.camera.enabled = false;
}

function Update()
{
    if(Input.GetKeyDown(KeyCode.Tab))
    {
        camSwitch = !camSwitch;
    }
    
    if(camSwitch == true)
    {
       FPScamera.camera.enabled = false;
       TPcamera.camera.enabled = true;
    }
    
    else
    {
       FPScamera.camera.enabled = true;
       TPcamera.camera.enabled = false;
    }
 }