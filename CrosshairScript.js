#pragma strict

@script ExecuteInEditMode()
enum preset { none, shotgunPreset, crysisPreset }
var crosshairPreset : preset = preset.none;
 
var showCrosshair : boolean = true;
var verticalTexture : Texture;
var horizontalTexture : Texture;
 
//Size of boxes
var cLength : float = 10;
var cWidth : float = 3;
 
//Spreed setup
var minSpread : float = 45.0;
var maxSpread : float = 250.0;
var spreadPerSecond : float = 150.0;
 
//Rotation
var rotAngle : float = 0.0;
var rotSpeed : float = 0.0;
 
private var temp : Texture;
private var spread : float;
 
function Update(){
        //Used just for test (weapon script should change spread).
        if(Input.GetKey(KeyCode.Mouse0)) spread += spreadPerSecond * Time.deltaTime;
        else spread -= spreadPerSecond * 2 * Time.deltaTime;   
       
        //Rotation
        rotAngle += rotSpeed * Time.deltaTime;
}
 
function OnGUI(){
        if(showCrosshair && verticalTexture && horizontalTexture){
                var verticalT = GUIStyle();
                var horizontalT = GUIStyle();
                verticalT.normal.background = verticalTexture;
                horizontalT.normal.background = horizontalTexture;
                spread = Mathf.Clamp(spread, minSpread, maxSpread);
                var pivot : Vector2 = Vector2(Screen.width/2, Screen.height/2);
               
                if(crosshairPreset == preset.crysisPreset){
                       
                        GUI.Box(Rect((Screen.width - 2)/2, (Screen.height - spread)/2 - 14, 2, 14), temp, horizontalT);
                        GUIUtility.RotateAroundPivot(45,pivot);
                        GUI.Box(Rect((Screen.width + spread)/2, (Screen.height - 2)/2, 14, 2), temp, verticalT);
                        GUIUtility.RotateAroundPivot(0,pivot);
                        GUI.Box(Rect((Screen.width - 2)/2, (Screen.height + spread)/2, 2, 14), temp, horizontalT);
                }
               
                if(crosshairPreset == preset.shotgunPreset){
               
                        GUIUtility.RotateAroundPivot(45,pivot);
                       
                        //Horizontal
                        GUI.Box(Rect((Screen.width - 14)/2, (Screen.height - spread)/2 - 3, 14, 3), temp, horizontalT);
                        GUI.Box(Rect((Screen.width - 14)/2, (Screen.height + spread)/2, 14, 3), temp, horizontalT);
                        //Vertical
                        GUI.Box(Rect((Screen.width - spread)/2 - 3, (Screen.height - 14)/2, 3, 14), temp, verticalT);
                        GUI.Box(Rect((Screen.width + spread)/2, (Screen.height - 14)/2, 3, 14), temp, verticalT);
                }
               
                if(crosshairPreset == preset.none){
               
                        GUIUtility.RotateAroundPivot(rotAngle%360,pivot);
                       
                        //Horizontal
                        GUI.Box(Rect((Screen.width - cWidth)/2, (Screen.height - spread)/2 - cLength, cWidth, cLength), temp, horizontalT);
                        GUI.Box(Rect((Screen.width - cWidth)/2, (Screen.height + spread)/2, cWidth, cLength), temp, horizontalT);
                        //Vertical
                        GUI.Box(Rect((Screen.width - spread)/2 - cLength, (Screen.height - cWidth)/2, cLength, cWidth), temp, verticalT);
                        GUI.Box(Rect((Screen.width + spread)/2, (Screen.height - cWidth)/2, cLength, cWidth), temp, verticalT);
                }
        }
}