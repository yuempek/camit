using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPC.Camera
{
    class CameraConnectionString
    {
        public static String Pattern_Password = "{PASSWORD}";
        public static String Pattern_UserName = "{USER_ID}";
        public static String Pattern_ImageSize = "{IMAGE_SIZE}";
        public static String Pattern_IP = "{CAMERA_IP}";
        public static String Pattern_FPS = "{FPS}";
        public static String Pattern_X = "{X}";
        public static String Pattern_Y = "{Y}";


        public static String ReplaceUserNamePattern(ref String connectionString, String UserName)
        {
            connectionString = connectionString.Replace(CameraConnectionString.Pattern_UserName, UserName);
            return connectionString;
        }

        public static String ReplacePasswordPattern(ref String connectionString, String Password)
        {
            connectionString = connectionString.Replace(CameraConnectionString.Pattern_Password, Password);
            return connectionString;
        }

        public static String ReplaceIPPattern(ref String connectionString, String IP)
        {
            connectionString = connectionString.Replace(CameraConnectionString.Pattern_IP, IP);
            return connectionString;
        }

        public static String ReplaceImageSizePattern(ref String connectionString, String ImageSize)
        {
            connectionString = connectionString.Replace(CameraConnectionString.Pattern_ImageSize, ImageSize);
            return connectionString;
        }

        public static String ReplaceFPSPattern(ref String connectionString, String FPS)
        {
            connectionString = connectionString.Replace(CameraConnectionString.Pattern_FPS, FPS);
            return connectionString;
        }

        public static String ReplaceXPattern(ref String connectionString, String X)
        {
            connectionString = connectionString.Replace(CameraConnectionString.Pattern_X, X);
            return connectionString;
        }

        public static String ReplaceYPattern(ref String connectionString, String Y)
        {
            connectionString = connectionString.Replace(CameraConnectionString.Pattern_Y, Y);
            return connectionString;
        }

        public static String ReplaceAllPattern(ref String connectionString, 
            String UserName,
            String Password,
            String IP,
            String FPS,
            String ImageSize,
            String X,
            String Y)
        {
            connectionString = connectionString.Replace(CameraConnectionString.Pattern_UserName, UserName);
            connectionString = connectionString.Replace(CameraConnectionString.Pattern_Password, Password);
            connectionString = connectionString.Replace(CameraConnectionString.Pattern_IP, IP);
            connectionString = connectionString.Replace(CameraConnectionString.Pattern_FPS, FPS);
            connectionString = connectionString.Replace(CameraConnectionString.Pattern_ImageSize, ImageSize);
            connectionString = connectionString.Replace(CameraConnectionString.Pattern_X, X);
            connectionString = connectionString.Replace(CameraConnectionString.Pattern_Y, Y);
            return connectionString;
        }

        public static String ReplaceAllPattern(ref String connectionString, IPCFileFormat.CamerasCamera IPCFileCamera) 
        { 
            String UserName = IPCFileCamera.UserName;
            String Password = IPCFileCamera.Password;
            String IP = IPCFileCamera.Ip;
            String FPS = "";
            String ImageSize = "";
            String X = "";
            String Y = "";

            return ReplaceAllPattern(ref connectionString, UserName, Password, IP, FPS, ImageSize, X, Y);
        }
    }
}
