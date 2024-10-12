////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Martin Bustos @FronkonGames <fronkongames@gmail.com>. All rights reserved.
//
// THIS FILE CAN NOT BE HOSTED IN PUBLIC REPOSITORIES.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;
using static FronkonGames.Retro.OldFilms.Inspector;

namespace FronkonGames.Retro.OldFilms.Editor
{
  /// <summary> Retro Old Films inspector. </summary>
  [CustomPropertyDrawer(typeof(OldFilms.Settings))]
  public class OldFilmsFeatureSettingsDrawer : Drawer
  {
    private OldFilms.Settings settings;

    protected override void ResetValues() => settings?.ResetDefaultValues();

    protected override void InspectorGUI()
    {
      settings ??= GetSettings<OldFilms.Settings>();

      /////////////////////////////////////////////////
      // Common.
      /////////////////////////////////////////////////
      settings.intensity = Slider("Intensity", "Controls the intensity of the effect [0, 1]. Default 1.", settings.intensity, 0.0f, 1.0f, 1.0f);

      /////////////////////////////////////////////////
      // Old Films.
      /////////////////////////////////////////////////
      Separator();

      settings.manufacturer = (Manufacturers)EnumPopup("Manufacturer", "Film manufacturer. Default Black & White.", settings.manufacturer, Manufacturers.Black_white);
      if (settings.manufacturer == Manufacturers.Custom)
      {
        IndentLevel++;
        settings.customCDL.slope = Vector3Field("Slope", "Slope of the transfer function without shifting the black level.", settings.customCDL.slope, Vector3.one);        
        settings.customCDL.offset = Vector3Field("Offset", "Raises or lowers overall brightness of a component.", settings.customCDL.offset, Vector3.zero);        
        settings.customCDL.power = Vector3Field("Power", "Changes the intermediate shape of the transfer function.", settings.customCDL.power, Vector3.one);        
        settings.customCDL.saturation = Slider("Saturation", "Color saturation [0.0, 2.0]. Default 1.", settings.customCDL.saturation, 0.0f, 2.0f, 1.0f);        
        settings.customCDL.contrast = Slider("Contrast", "Color contrast [0.0, 2.0]. Default 1.", settings.customCDL.contrast, 0.0f, 2.0f, 1.0f);        
        settings.customCDL.gamma = Slider("Gamma", "Color gamma [0.0, 2.0]. Default 1.", settings.customCDL.gamma, 0.0f, 2.0f, 1.0f);        
        settings.customCDL.filmContrast = Toggle("Film contrast", "Extra color contrast.", settings.customCDL.filmContrast);        
        IndentLevel--;
      }

      settings.vignette = Slider("Vignette", "Natural vignette [0.0, 2.0]. Default 0.1.", settings.vignette, 0.0f, 2.0f, 0.1f);        
      settings.sepia = Slider("Sepia", "Sepia tones [0.0, 1.0]. Default 0.", settings.sepia, 0.0f, 1.0f, 0.0f);
      settings.grain = Slider("Grain", "Grain strength [0.0, 1.0]. Default 0.5.", settings.grain, 0.0f, 1.0f, 0.5f);

      settings.jumpFrame = Slider("Jump frame", "Frame jumping [0.0, 1.0]. Default 0.1.", settings.jumpFrame, 0.0f, 1.0f, 0.1f);        
      IndentLevel++;
      settings.moveFrame = Vector2Field("Movement", "Frame movement. Default (0.2, 0.2).", settings.moveFrame, Vector2.one * 0.2f);        
      IndentLevel--;
      
      settings.blinkStrength = Slider("Blink strength", "Blink strength [0.0, 1.0]. Default 0.", settings.blinkStrength, 0.0f, 1.0f, 0.0f);        
      IndentLevel++;
      settings.blinkSpeed = FloatField("Speed", "Blink speed. Default 10.", settings.blinkSpeed, 10.0f);        
      IndentLevel--;

      settings.blotches = Slider("Blotches", "Film blotches [0 - 6]. Default 3.", settings.blotches, 0, 6, 3);        
      IndentLevel++;
      settings.blotchSize = Slider("Size", "Blotch size [0.0 - 10.0]. Default 1.0.", settings.blotchSize, 0.0f, 10.0f, 1.0f);        
      IndentLevel--;

      settings.scratches = Slider("Scratches", "Film scratches [0.0 - 1.0]. Default 0.5.", settings.scratches, 0.0f, 1.0f, 0.5f);        
      IndentLevel++;
      IndentLevel--;

      settings.lines = Slider("Lines", "Film lines [0 - 8]. Default 4.", settings.lines, 0, 8, 4);        
      IndentLevel++;
      settings.linesStrength = Slider("Strength", "Lines strength [0.0 - 1.0]. Default 0.25.", settings.linesStrength, 0.0f, 1.0f, 0.25f);        
      IndentLevel--;
      
      /////////////////////////////////////////////////
      // Color.
      /////////////////////////////////////////////////
      Separator();

      if (Foldout("Color") == true)
      {
        IndentLevel++;

        settings.brightness = Slider("Brightness", "Brightness [-1.0, 1.0]. Default 0.", settings.brightness, -1.0f, 1.0f, 0.0f);
        settings.contrast = Slider("Contrast", "Contrast [0.0, 10.0]. Default 1.", settings.contrast, 0.0f, 10.0f, 1.0f);
        settings.gamma = Slider("Gamma", "Gamma [0.1, 10.0]. Default 1.", settings.gamma, 0.01f, 10.0f, 1.0f);
        settings.hue = Slider("Hue", "The color wheel [0.0, 1.0]. Default 0.", settings.hue, 0.0f, 1.0f, 0.0f);
        settings.saturation = Slider("Saturation", "Intensity of a colors [0.0, 2.0]. Default 1.", settings.saturation, 0.0f, 2.0f, 1.0f);

        IndentLevel--;
      }
      
      /////////////////////////////////////////////////
      // Advanced.
      /////////////////////////////////////////////////
      Separator();

      if (Foldout("Advanced") == true)
      {
        IndentLevel++;

        settings.affectSceneView = Toggle("Affect the Scene View?", string.Empty, settings.affectSceneView);
        settings.filterMode = (FilterMode)EnumPopup("Filter mode", "", settings.filterMode, FilterMode.Bilinear);
        settings.whenToInsert = (UnityEngine.Rendering.Universal.RenderPassEvent)EnumPopup("RenderPass event",
          string.Empty,
          settings.whenToInsert,
          UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing);
        settings.enableProfiling = Toggle("Enable profiling", string.Empty, settings.enableProfiling);

        IndentLevel--;
      }
    }
  }
}
