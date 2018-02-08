using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace IPC.Camera
{
    public class CameraModel
    {
        public static String DefaultPath = Application.StartupPath + @"\NetCams Models";
        private static List<CameraModel> cameraModels = null;
        public static List<CameraModel> CameraModels 
        {
            get 
            {
                if (cameraModels == null) cameraModels = GetCameraModelsFromFolder(CameraModel.DefaultPath);
                return cameraModels; 
            }
            set 
            {
                cameraModels = value;
            }
        }

        public string Name = "";
        public CameraConfig Config = null;

        public CameraModel()
        {
    
        }

        public static List<CameraModel> GetCameraModelsFromFolder(string path) 
        {
            List<CameraModel> cameraModels = new List<CameraModel>();
            try
            {
                string[] cameraFiles = Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories);
                foreach (string cameraFile in cameraFiles)
                {
                    CameraModel cameraModel = new CameraModel();
                    cameraModel.Name = Path.GetFileName(cameraFile).TrimEnd(".txt".ToCharArray());
                    cameraModel.Config = new CameraConfig(cameraFile);
                    cameraModels.Add(cameraModel);
                }
            }
            catch (IOException e)
            {
                
            }
            
            return cameraModels;
        }

        
    }
}
