using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IPC.Camera
{
    public class CameraConfig
    {
        public String DateStamp = "";
        public String DontCheckPattern = "";
        public String MaxImageConcurrentRequests = "";
        public String Modified = "";
        public String NETCAMADDPARAMHTML1 = "";
        public String NETCAMADDPARAMINITVALUE1 = "";
        public String NETCAMADDPARAMNAME1 = "";
        public String NetworkCamBrightnessIsDigit = "";
        public String NetworkCamBrightnessMax = "";
        public String NetworkCamBrightnessMin = "";
        public String NetworkCamCenterCommand = "";
        public String NetworkCamCenterMaxXEqWidth = "";
        public String NetworkCamCenterMaxY = "";
        public String NetworkCamCenterMaxYEqHeight = "";
        public String NetworkCamCenterMinY = "";
        public String NetworkCamCenterXMax = "";
        public String NetworkCamCenterXMin = "";
        public String NetworkCamContrastIsDigit = "";
        public String NetworkCamContrastMax = "";
        public String NetworkCamContrastMin = "";
        public String NetworkCamDefID = "";
        public String NetworkCamDefIP = "";
        public String NetworkCamDefPassword = "";
        public String NetworkCamDefPort = "";
        public String NetworkCameraAdminWebPage = "";
        public String NetworkCameraAudioBeginSignature = "";
        public String NetworkCameraAudioBitsPerSample = "";
        public String NetworkCameraAudioCaptureInterval = "";
        public String NetworkCameraAudioEndSignature = "";
        public String NetworkCameraAudioSampleRate = "";
        public String NetworkCameraAudioSampleSize = "";
        public String NetworkCameraAudioURL = "";
        public String NetworkCameraCameraWebPage = "";
        public String NetworkCameraJPEGimage = "";
        public String NetworkCameraRestartCmnd = "";
        public String NetworkCamImageSize1024x768 = "";
        public String NetworkCamImageSize1280x1024 = "";
        public String NetworkCamImageSize1280x960 = "";
        public String NetworkCamImageSize160x120 = "";
        public String NetworkCamImageSize320x240 = "";
        public String NetworkCamImageSize640x480 = "";
        public String NetworkCamImageSize800x600 = "";
        public String NetworkCamInitString1 = "";
        public String NetworkCamInitString2 = "";
        public String NetworkCamInitString3 = "";
        public String NetworkCamInterfaceDll = "";
        public String NetworkCamPanLeft = "";
        public String NetworkCamPanLeft_Up = "";
        public String NetworkCamPanRight = "";
        public String NetworkCamPanRight_Up = "";
        public String NetworkCamRepeatInterval = "";
        public String NetworkCamSearchPattern = "";
        public String NetworkCamTiltDown = "";
        public String NetworkCamTiltDown_Up = "";
        public String NetworkCamTiltUp = "";
        public String NetworkCamTiltUp_Up = "";
        public String NetworkCamURLtoChangeBrightness = "";
        public String NetworkCamURLtoChangeContrast = "";
        public String NetworkCamURLtoChangeImageSize = "";
        public String NetworkCamZoomIN = "";
        public String NetworkCamZoomIN_Up = "";
        public String NetworkCamZoomOut = "";
        public String NetworkCamZoomOut_Up = "";
        public String NUMBEROFADDPARAMS = "";
        public String NumberOfInitStrings = "";

     

//additionals
        //private string extendedConfigFilePath;
        //public String CameraType; //MJPEG, JPEG, MPEG
/*
        public String CameraName;
        public String CameraIP;
        public String LoginName;
        public String LoginPassword;
        public String CameraSourceAddress;
        public String PanLeftStartCommand;
        public String PanLeftStopCommand;
        public String PanRightStartCommand;
        public String PanRightStopCommand;
        public String TiltDownStartCommand;
        public String TiltDownStopCommand;
        public String TiltUpStartCommand;
        public String TiltUpStopCommand;
        public String ZoomInStartCommand;
        public String ZoomInStopCommand;
        public String ZoomOutStartCommand;
        public String ZoomOutStopCommand;
//*/


        public string ConfigFilePath;

        public CameraConfig() { }
        public CameraConfig(String file) 
        {
            this.ConfigFilePath = file;
            InitializeParametersFromINI();
        }
        private void InitializeParametersFromINI()
        {
            this.DateStamp = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "DateStamp");
            this.DontCheckPattern = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "DontCheckPattern");
            this.MaxImageConcurrentRequests = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "MaxImageConcurrentRequests");
            this.Modified = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "Modified");
            this.NETCAMADDPARAMHTML1 = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NETCAMADDPARAMHTML1");
            this.NETCAMADDPARAMINITVALUE1 = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NETCAMADDPARAMINITVALUE1");
            this.NETCAMADDPARAMNAME1 = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NETCAMADDPARAMNAME1");
            this.NetworkCamBrightnessIsDigit = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamBrightnessIsDigit");
            this.NetworkCamBrightnessMax = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamBrightnessMax");
            this.NetworkCamBrightnessMin = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamBrightnessMin");
            this.NetworkCamCenterCommand = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamCenterCommand");
            this.NetworkCamCenterMaxXEqWidth = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamCenterMaxXEqWidth");
            this.NetworkCamCenterMaxY = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamCenterMaxY");
            this.NetworkCamCenterMaxYEqHeight = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamCenterMaxYEqHeight");
            this.NetworkCamCenterMinY = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamCenterMinY");
            this.NetworkCamCenterXMax = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamCenterXMax");
            this.NetworkCamCenterXMin = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamCenterXMin");
            this.NetworkCamContrastIsDigit = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamContrastIsDigit");
            this.NetworkCamContrastMax = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamContrastMax");
            this.NetworkCamContrastMin = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamContrastMin");
            this.NetworkCamDefID = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamDefID");
            this.NetworkCamDefIP = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamDefIP");
            this.NetworkCamDefPassword = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamDefPassword");
            this.NetworkCamDefPort = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamDefPort");
            this.NetworkCameraAdminWebPage = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCameraAdminWebPage");
            this.NetworkCameraAudioBeginSignature = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCameraAudioBeginSignature");
            this.NetworkCameraAudioBitsPerSample = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCameraAudioBitsPerSample");
            this.NetworkCameraAudioCaptureInterval = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCameraAudioCaptureInterval");
            this.NetworkCameraAudioEndSignature = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCameraAudioEndSignature");
            this.NetworkCameraAudioSampleRate = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCameraAudioSampleRate");
            this.NetworkCameraAudioSampleSize = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCameraAudioSampleSize");
            this.NetworkCameraAudioURL = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCameraAudioURL");
            this.NetworkCameraCameraWebPage = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCameraCameraWebPage");
            this.NetworkCameraJPEGimage = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCameraJPEGimage");
            this.NetworkCameraRestartCmnd = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCameraRestartCmnd");
            this.NetworkCamImageSize1024x768 = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamImageSize1024x768");
            this.NetworkCamImageSize1280x1024 = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamImageSize1280x1024");
            this.NetworkCamImageSize1280x960 = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamImageSize1280x960");
            this.NetworkCamImageSize160x120 = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamImageSize160x120");
            this.NetworkCamImageSize320x240 = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamImageSize320x240");
            this.NetworkCamImageSize640x480 = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamImageSize640x480");
            this.NetworkCamImageSize800x600 = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamImageSize800x600");
            this.NetworkCamInitString1 = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamInitString1");
            this.NetworkCamInitString2 = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamInitString2");
            this.NetworkCamInitString3 = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamInitString3");
            this.NetworkCamInterfaceDll = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamInterfaceDll");
            this.NetworkCamPanLeft = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamPanLeft");
            this.NetworkCamPanLeft_Up = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamPanLeft_Up");
            this.NetworkCamPanRight = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamPanRight");
            this.NetworkCamPanRight_Up = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamPanRight_Up");
            this.NetworkCamRepeatInterval = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamRepeatInterval");
            this.NetworkCamSearchPattern = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamSearchPattern");
            this.NetworkCamTiltDown = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamTiltDown");
            this.NetworkCamTiltDown_Up = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamTiltDown_Up");
            this.NetworkCamTiltUp = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamTiltUp");
            this.NetworkCamTiltUp_Up = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamTiltUp_Up");
            this.NetworkCamURLtoChangeBrightness = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamURLtoChangeBrightness");
            this.NetworkCamURLtoChangeContrast = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamURLtoChangeContrast");
            this.NetworkCamURLtoChangeImageSize = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamURLtoChangeImageSize");
            this.NetworkCamZoomIN = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamZoomIN");
            this.NetworkCamZoomIN_Up = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamZoomIN_Up");
            this.NetworkCamZoomOut = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamZoomOut");
            this.NetworkCamZoomOut_Up = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NetworkCamZoomOut_Up");
            this.NUMBEROFADDPARAMS = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NUMBEROFADDPARAMS");
            this.NumberOfInitStrings = INI.IniFile.ReadValue(this.ConfigFilePath, "Main", "NumberOfInitStrings");
        }

        public void saveConfigFile(string filePath) 
        {
            if (!String.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    if (!String.IsNullOrEmpty(this.DateStamp)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "DateStamp", this.DateStamp);
                    if (!String.IsNullOrEmpty(this.DontCheckPattern)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "DontCheckPattern", this.DontCheckPattern);
                    if (!String.IsNullOrEmpty(this.MaxImageConcurrentRequests)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "MaxImageConcurrentRequests", this.MaxImageConcurrentRequests);
                    if (!String.IsNullOrEmpty(this.Modified)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "Modified", this.Modified);
                    if (!String.IsNullOrEmpty(this.NETCAMADDPARAMHTML1)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NETCAMADDPARAMHTML1", this.NETCAMADDPARAMHTML1);
                    if (!String.IsNullOrEmpty(this.NETCAMADDPARAMINITVALUE1)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NETCAMADDPARAMINITVALUE1", this.NETCAMADDPARAMINITVALUE1);
                    if (!String.IsNullOrEmpty(this.NETCAMADDPARAMNAME1)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NETCAMADDPARAMNAME1", this.NETCAMADDPARAMNAME1);
                    if (!String.IsNullOrEmpty(this.NetworkCamBrightnessIsDigit)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamBrightnessIsDigit", this.NetworkCamBrightnessIsDigit);
                    if (!String.IsNullOrEmpty(this.NetworkCamBrightnessMax)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamBrightnessMax", this.NetworkCamBrightnessMax);
                    if (!String.IsNullOrEmpty(this.NetworkCamBrightnessMin)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamBrightnessMin", this.NetworkCamBrightnessMin);
                    if (!String.IsNullOrEmpty(this.NetworkCamCenterCommand)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamCenterCommand", this.NetworkCamCenterCommand);
                    if (!String.IsNullOrEmpty(this.NetworkCamCenterMaxXEqWidth)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamCenterMaxXEqWidth", this.NetworkCamCenterMaxXEqWidth);
                    if (!String.IsNullOrEmpty(this.NetworkCamCenterMaxY)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamCenterMaxY", this.NetworkCamCenterMaxY);
                    if (!String.IsNullOrEmpty(this.NetworkCamCenterMaxYEqHeight)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamCenterMaxYEqHeight", this.NetworkCamCenterMaxYEqHeight);
                    if (!String.IsNullOrEmpty(this.NetworkCamCenterMinY)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamCenterMinY", this.NetworkCamCenterMinY);
                    if (!String.IsNullOrEmpty(this.NetworkCamCenterXMax)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamCenterXMax", this.NetworkCamCenterXMax);
                    if (!String.IsNullOrEmpty(this.NetworkCamCenterXMin)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamCenterXMin", this.NetworkCamCenterXMin);
                    if (!String.IsNullOrEmpty(this.NetworkCamContrastIsDigit)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamContrastIsDigit", this.NetworkCamContrastIsDigit);
                    if (!String.IsNullOrEmpty(this.NetworkCamContrastMax)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamContrastMax", this.NetworkCamContrastMax);
                    if (!String.IsNullOrEmpty(this.NetworkCamContrastMin)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamContrastMin", this.NetworkCamContrastMin);
                    if (!String.IsNullOrEmpty(this.NetworkCamDefID)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamDefID", this.NetworkCamDefID);
                    if (!String.IsNullOrEmpty(this.NetworkCamDefIP)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamDefIP", this.NetworkCamDefIP);
                    if (!String.IsNullOrEmpty(this.NetworkCamDefPassword)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamDefPassword", this.NetworkCamDefPassword);
                    if (!String.IsNullOrEmpty(this.NetworkCamDefPort)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamDefPort", this.NetworkCamDefPort);
                    if (!String.IsNullOrEmpty(this.NetworkCameraAdminWebPage)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCameraAdminWebPage", this.NetworkCameraAdminWebPage);
                    if (!String.IsNullOrEmpty(this.NetworkCameraAudioBeginSignature)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCameraAudioBeginSignature", this.NetworkCameraAudioBeginSignature);
                    if (!String.IsNullOrEmpty(this.NetworkCameraAudioBitsPerSample)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCameraAudioBitsPerSample", this.NetworkCameraAudioBitsPerSample);
                    if (!String.IsNullOrEmpty(this.NetworkCameraAudioCaptureInterval)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCameraAudioCaptureInterval", this.NetworkCameraAudioCaptureInterval);
                    if (!String.IsNullOrEmpty(this.NetworkCameraAudioEndSignature)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCameraAudioEndSignature", this.NetworkCameraAudioEndSignature);
                    if (!String.IsNullOrEmpty(this.NetworkCameraAudioSampleRate)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCameraAudioSampleRate", this.NetworkCameraAudioSampleRate);
                    if (!String.IsNullOrEmpty(this.NetworkCameraAudioSampleSize)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCameraAudioSampleSize", this.NetworkCameraAudioSampleSize);
                    if (!String.IsNullOrEmpty(this.NetworkCameraAudioURL)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCameraAudioURL", this.NetworkCameraAudioURL);
                    if (!String.IsNullOrEmpty(this.NetworkCameraCameraWebPage)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCameraCameraWebPage", this.NetworkCameraCameraWebPage);
                    if (!String.IsNullOrEmpty(this.NetworkCameraJPEGimage)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCameraJPEGimage", this.NetworkCameraJPEGimage);
                    if (!String.IsNullOrEmpty(this.NetworkCameraRestartCmnd)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCameraRestartCmnd", this.NetworkCameraRestartCmnd);
                    if (!String.IsNullOrEmpty(this.NetworkCamImageSize1024x768)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamImageSize1024x768", this.NetworkCamImageSize1024x768);
                    if (!String.IsNullOrEmpty(this.NetworkCamImageSize1280x1024)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamImageSize1280x1024", this.NetworkCamImageSize1280x1024);
                    if (!String.IsNullOrEmpty(this.NetworkCamImageSize1280x960)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamImageSize1280x960", this.NetworkCamImageSize1280x960);
                    if (!String.IsNullOrEmpty(this.NetworkCamImageSize160x120)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamImageSize160x120", this.NetworkCamImageSize160x120);
                    if (!String.IsNullOrEmpty(this.NetworkCamImageSize320x240)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamImageSize320x240", this.NetworkCamImageSize320x240);
                    if (!String.IsNullOrEmpty(this.NetworkCamImageSize640x480)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamImageSize640x480", this.NetworkCamImageSize640x480);
                    if (!String.IsNullOrEmpty(this.NetworkCamImageSize800x600)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamImageSize800x600", this.NetworkCamImageSize800x600);
                    if (!String.IsNullOrEmpty(this.NetworkCamInitString1)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamInitString1", this.NetworkCamInitString1);
                    if (!String.IsNullOrEmpty(this.NetworkCamInitString2)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamInitString2", this.NetworkCamInitString2);
                    if (!String.IsNullOrEmpty(this.NetworkCamInitString3)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamInitString3", this.NetworkCamInitString3);
                    if (!String.IsNullOrEmpty(this.NetworkCamInterfaceDll)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamInterfaceDll", this.NetworkCamInterfaceDll);
                    if (!String.IsNullOrEmpty(this.NetworkCamPanLeft)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamPanLeft", this.NetworkCamPanLeft);
                    if (!String.IsNullOrEmpty(this.NetworkCamPanLeft_Up)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamPanLeft_Up", this.NetworkCamPanLeft_Up);
                    if (!String.IsNullOrEmpty(this.NetworkCamPanRight)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamPanRight", this.NetworkCamPanRight);
                    if (!String.IsNullOrEmpty(this.NetworkCamPanRight_Up)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamPanRight_Up", this.NetworkCamPanRight_Up);
                    if (!String.IsNullOrEmpty(this.NetworkCamRepeatInterval)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamRepeatInterval", this.NetworkCamRepeatInterval);
                    if (!String.IsNullOrEmpty(this.NetworkCamSearchPattern)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamSearchPattern", this.NetworkCamSearchPattern);
                    if (!String.IsNullOrEmpty(this.NetworkCamTiltDown)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamTiltDown", this.NetworkCamTiltDown);
                    if (!String.IsNullOrEmpty(this.NetworkCamTiltDown_Up)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamTiltDown_Up", this.NetworkCamTiltDown_Up);
                    if (!String.IsNullOrEmpty(this.NetworkCamTiltUp)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamTiltUp", this.NetworkCamTiltUp);
                    if (!String.IsNullOrEmpty(this.NetworkCamTiltUp_Up)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamTiltUp_Up", this.NetworkCamTiltUp_Up);
                    if (!String.IsNullOrEmpty(this.NetworkCamURLtoChangeBrightness)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamURLtoChangeBrightness", this.NetworkCamURLtoChangeBrightness);
                    if (!String.IsNullOrEmpty(this.NetworkCamURLtoChangeContrast)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamURLtoChangeContrast", this.NetworkCamURLtoChangeContrast);
                    if (!String.IsNullOrEmpty(this.NetworkCamURLtoChangeImageSize)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamURLtoChangeImageSize", this.NetworkCamURLtoChangeImageSize);
                    if (!String.IsNullOrEmpty(this.NetworkCamZoomIN)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamZoomIN", this.NetworkCamZoomIN);
                    if (!String.IsNullOrEmpty(this.NetworkCamZoomIN_Up)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamZoomIN_Up", this.NetworkCamZoomIN_Up);
                    if (!String.IsNullOrEmpty(this.NetworkCamZoomOut)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamZoomOut", this.NetworkCamZoomOut);
                    if (!String.IsNullOrEmpty(this.NetworkCamZoomOut_Up)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NetworkCamZoomOut_Up", this.NetworkCamZoomOut_Up);
                    if (!String.IsNullOrEmpty(this.NUMBEROFADDPARAMS)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NUMBEROFADDPARAMS", this.NUMBEROFADDPARAMS);
                    if (!String.IsNullOrEmpty(this.NumberOfInitStrings)) INI.IniFile.WriteValue(this.ConfigFilePath, "Main", "NumberOfInitStrings", this.NumberOfInitStrings);
                }
            }
        }
    

    }
}
