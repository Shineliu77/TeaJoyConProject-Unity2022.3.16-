using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class GoogleApiUse : MonoBehaviour
{
    [Header("GoogleDrive�]�w")]
    //GoogleDrive���
    private DriveService _driveService;
    //202503_GoogleDrive��Ƨ�ID�A��Ƨ��n�]�w������H���i�H�s��
    public string GOOGLE_DRIVE_FOLDER_ID = "1f8uOJCYTAckZ6EKf4S2bKNpr_PnpCnzT";
    public string PicFileName;//�x�s���Ϥ��W��
    public string PicFilePath;//�x�s���Ϥ�_�����|
    public string NowFileID;

    public byte[] ScreenshotByte;
    public string GoogleTextureUrl;
    // Start is called before the first frame update
    void Start()
    {
        //Ū��GoogleDrive���v��Json��
        StartCoroutine(ReadJSon());
    }
    // �o��Google���{�Ҹ��
    IEnumerator ReadJSon()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "api-project.json");
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            GoogleCredential credential;
            credential = GoogleCredential.FromJson(www.downloadHandler.text).CreateScoped(DriveService.ScopeConstants.Drive);
            // Drive APIServer�ͦ�
            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "APiPProject",
            });
            Debug.Log("Service:" + _driveService.Name);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    #region N_�W���ɮ�
    public void N_UploadPic()
    {
        try
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = PicFileName,
                Parents = new[] { GOOGLE_DRIVE_FOLDER_ID }
            };
            FilesResource.CreateMediaUpload request;

            using (var stream = new FileStream(FindObjectOfType<WebCamCapture>().path, FileMode.Open))
            {
                // Create a new file, with metadata and stream.
                request = _driveService.Files.Create(
                    fileMetadata, stream, "image/jpeg");
                request.Fields = "name,id,permissions";
                //  request.IncludePermissionsForView = 
             //   request.SupportsAllDrives = true;
                request.Upload();
            }
            var file = request.ResponseBody;
            // Prints the uploaded file id.
            Debug.Log("File ID: " + file.Id + "Permission");
            NowFileID = file.Id;
            GoogleTextureUrl= "https://drive.google.com/file/d/" + NowFileID;
            Debug.Log("�Ϥ����}�G"+"https://drive.google.com/file/d/" + NowFileID);
        }
        catch (Exception e)
        {
            // TODO(developer) - handle error appropriately
            if (e is AggregateException)
            {
                Debug.Log("Credential Not found");
            }
            else if (e is FileNotFoundException)
            {
                Debug.Log("File not found");
            }
            else
            {
                throw;
            }
        }
    
    }
    #endregion//N_�W���ɮ�
}
