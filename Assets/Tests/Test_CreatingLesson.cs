using System.Collections;
using System.Collections.Generic;
using Controllers;
using Diploma.Controllers;
using Diploma.Controllers.AssembleController;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Managers;
using Diploma.Tables;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class Test_CreatingLesson
    {
        private GameObject _gameObject;

        /*[Test]
        public void Test_CreatingLessonOKPass()
        {
            
        }
        */
        
    [UnityTest]
    public IEnumerator Test_CreatingLessonOrderEmpty()
    {
        _gameObject = null;
        var DataBaseController = new DataBaseController();
        AssemliesTable assemblies = new AssemliesTable();
        LessonsTable lessons = new LessonsTable();
        TextsTable texts = new TextsTable();
        TypesTable types = new TypesTable();
        UsersTable users = new UsersTable();
        VideosTable videos = new VideosTable();
        List<IDataBase> tables = new List<IDataBase>();
        tables.Add(assemblies); // 0 - assembles
        tables.Add(lessons); // 1 - lessons
        tables.Add(texts); // 2 - text
        tables.Add(types); // 3 - types
        tables.Add(users); // 4 - users
        tables.Add(videos); // 5 - videos
        AssemblyCreator assemblyCreator = new AssemblyCreator();
        var _gameContextWithViews = new GameContextWithViews();
        var _gameContextWithLogic = new GameContextWithLogic();
        LessonConstructorController lessonConstructorController 
            = new LessonConstructorController(DataBaseController,tables,
                _gameContextWithViews,
                new GameContextWithLessons(),
                new GameContextWithUI(),
                _gameContextWithLogic,
                new FileManagerController(),
                new FileManager(),
                assemblyCreator,
                new RectTransform()
                );
        lessonConstructorController.GiveMeGameObject += GivingGameObj;
        lessonConstructorController.Initialization();
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadModel,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadModel].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadModel].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
       
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadVideo,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadVideo].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadVideo].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
       
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadPDF,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadPDF].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadPDF].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
        
        lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadModel,
            $@"D:\Unity-Storage\Diploma_Copy\diploma-unity\AssetBundles\StandaloneWindows\Тормоз\brake",
            @"D:\Unity-Storage\Diploma_Copy\diploma-unity\AssetBundles\StandaloneWindows\Тормоз\brake");
        lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadVideo,
            "Выберите видео-фаил (*.mp4)","Выберите видео-фаил (*.mp4)");
        lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadPDF,
            @"D:\Voprosy_k_GOS_2021.pdf",
            @"D:\Voprosy_k_GOS_2021.pdf");
        
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.SetNameToLesson,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].AddComponent<TMP_InputField>();
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].
            GetComponent<TMP_InputField>().text = "SomeNewLesson";
        
        
        _gameContextWithViews.AddToggles(0,new GameObject());
        _gameContextWithViews.ChoosenToggles[0].AddComponent<Toggle>();
        _gameContextWithViews.ChoosenToggles[0].GetComponentInChildren<Toggle>().isOn = true;
        var DefCamera = new GameObject();
        DefCamera.AddComponent<Camera>();
        var mainCamera = GameObject.Instantiate(DefCamera);
        _gameContextWithLogic.SetAMainCamera(mainCamera.GetComponent<Camera>());
        _gameContextWithLogic.SetAScreenShotCamera(mainCamera.GetComponent<Camera>());
        
        //CreateAssemblyDis
        ErrorCodes error;
        lessonConstructorController.OpenAnUIInitialization();
        yield return new WaitUntil(()=> _gameObject != null);
        Assert.AreEqual(ErrorCodes.None, lessonConstructorController.CheckForErrors());
        lessonConstructorController.SavingAssemblyDis("");
        //CreateLesson
        lessonConstructorController.CreateALesson();
        lessonConstructorController.GiveMeGameObject -= GivingGameObj;
        yield return new WaitForSeconds(10);
        Assert.AreNotEqual(ErrorCodes.None,lessonConstructorController.CheckForErrors());
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Test_CreatingModelEmpty()
    {
        _gameObject = null;
        var DataBaseController = new DataBaseController();
        AssemliesTable assemblies = new AssemliesTable();
        LessonsTable lessons = new LessonsTable();
        TextsTable texts = new TextsTable();
        TypesTable types = new TypesTable();
        UsersTable users = new UsersTable();
        VideosTable videos = new VideosTable();
        List<IDataBase> tables = new List<IDataBase>();
        tables.Add(assemblies); // 0 - assembles
        tables.Add(lessons); // 1 - lessons
        tables.Add(texts); // 2 - text
        tables.Add(types); // 3 - types
        tables.Add(users); // 4 - users
        tables.Add(videos); // 5 - videos
        AssemblyCreator assemblyCreator = new AssemblyCreator();
        var _gameContextWithViews = new GameContextWithViews();
        var _gameContextWithLogic = new GameContextWithLogic();
        LessonConstructorController lessonConstructorController 
            = new LessonConstructorController(DataBaseController,tables,
                _gameContextWithViews,
                new GameContextWithLessons(),
                new GameContextWithUI(),
                _gameContextWithLogic,
                new FileManagerController(),
                new FileManager(),
                assemblyCreator,
                new RectTransform()
                );
        lessonConstructorController.GiveMeGameObject += GivingGameObj;
        lessonConstructorController.Initialization();
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadModel,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadModel].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadModel].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
       
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadVideo,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadVideo].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadVideo].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
       
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadPDF,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadPDF].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadPDF].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
        
        lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadModel,
            $@"Выберите UnityBundle ()",
            @"Выберите UnityBundle ()");
        lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadVideo,
            "Выберите видео-фаил (*.mp4)","Выберите видео-фаил (*.mp4)");
        lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadPDF,
            @"D:\Voprosy_k_GOS_2021.pdf",
            @"D:\Voprosy_k_GOS_2021.pdf");
        
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.SetNameToLesson,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].AddComponent<TMP_InputField>();
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].
            GetComponent<TMP_InputField>().text = "SomeNewLesson";
        
        
        _gameContextWithViews.AddToggles(0,new GameObject());
        _gameContextWithViews.ChoosenToggles[0].AddComponent<Toggle>();
        _gameContextWithViews.ChoosenToggles[0].GetComponentInChildren<Toggle>().isOn = true;
        var DefCamera = new GameObject();
        DefCamera.AddComponent<Camera>();
        var mainCamera = GameObject.Instantiate(DefCamera);
        _gameContextWithLogic.SetAMainCamera(mainCamera.GetComponent<Camera>());
        _gameContextWithLogic.SetAScreenShotCamera(mainCamera.GetComponent<Camera>());
        
        //CreateAssemblyDis
        lessonConstructorController.OpenAnUIInitialization();
        yield return new WaitUntil(()=> _gameObject != null);
        Assert.AreNotEqual(ErrorCodes.None, lessonConstructorController.CheckForErrors());
        lessonConstructorController.SavingAssemblyDis("1 2 3 4 5");
        //CreateLesson
        lessonConstructorController.CreateALesson();
        lessonConstructorController.GiveMeGameObject -= GivingGameObj;
        yield return new WaitForSeconds(10);
        Assert.AreNotEqual(ErrorCodes.None,lessonConstructorController.CheckForErrors());
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Test_CreatingPDFEmpty()
    {
        _gameObject = null;
        var DataBaseController = new DataBaseController();
        AssemliesTable assemblies = new AssemliesTable();
        LessonsTable lessons = new LessonsTable();
        TextsTable texts = new TextsTable();
        TypesTable types = new TypesTable();
        UsersTable users = new UsersTable();
        VideosTable videos = new VideosTable();
        List<IDataBase> tables = new List<IDataBase>();
        tables.Add(assemblies); // 0 - assembles
        tables.Add(lessons); // 1 - lessons
        tables.Add(texts); // 2 - text
        tables.Add(types); // 3 - types
        tables.Add(users); // 4 - users
        tables.Add(videos); // 5 - videos
        AssemblyCreator assemblyCreator = new AssemblyCreator();
        var _gameContextWithViews = new GameContextWithViews();
        var _gameContextWithLogic = new GameContextWithLogic();
        LessonConstructorController lessonConstructorController 
            = new LessonConstructorController(DataBaseController,tables,
                _gameContextWithViews,
                new GameContextWithLessons(),
                new GameContextWithUI(),
                _gameContextWithLogic,
                new FileManagerController(),
                new FileManager(),
                assemblyCreator,
                new RectTransform()
                );
        lessonConstructorController.GiveMeGameObject += GivingGameObj;
        lessonConstructorController.Initialization();
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadModel,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadModel].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadModel].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
       
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadVideo,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadVideo].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadVideo].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
       
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadPDF,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadPDF].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadPDF].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
        
        lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadModel,
            $@"D:\Unity-Storage\Diploma_Copy\diploma-unity\AssetBundles\StandaloneWindows\Тормоз\brake",
            @"D:\Unity-Storage\Diploma_Copy\diploma-unity\AssetBundles\StandaloneWindows\Тормоз\brake");
        lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadVideo,
            "Выберите видео-фаил (*.mp4)","Выберите видео-фаил (*.mp4)");
        lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadPDF,
            @"Выберите текстовый фаил(*.pdf)",
            @"Выберите текстовый фаил(*.pdf)");
        
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.SetNameToLesson,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].AddComponent<TMP_InputField>();
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].
            GetComponent<TMP_InputField>().text = "SomeNewLesson";
        
        
        _gameContextWithViews.AddToggles(0,new GameObject());
        _gameContextWithViews.ChoosenToggles[0].AddComponent<Toggle>();
        _gameContextWithViews.ChoosenToggles[0].GetComponentInChildren<Toggle>().isOn = true;
        var DefCamera = new GameObject();
        DefCamera.AddComponent<Camera>();
        var mainCamera = GameObject.Instantiate(DefCamera);
        _gameContextWithLogic.SetAMainCamera(mainCamera.GetComponent<Camera>());
        _gameContextWithLogic.SetAScreenShotCamera(mainCamera.GetComponent<Camera>());
        //CreateAssemblyDis
        ErrorCodes error;
        lessonConstructorController.OpenAnUIInitialization();
        yield return new WaitUntil(()=> _gameObject != null);
        Assert.AreEqual(ErrorCodes.None, lessonConstructorController.CheckForErrors());
        lessonConstructorController.SavingAssemblyDis("1 2 3 4 5");
        //CreateLesson
        lessonConstructorController.CreateALesson();
        lessonConstructorController.GiveMeGameObject -= GivingGameObj;
        yield return new WaitForSeconds(10);
        Assert.AreNotEqual(ErrorCodes.None,lessonConstructorController.CheckForErrors());
        yield return null;
    }

    private void GivingGameObj(GameObject obj)
    {
        _gameObject = obj;
    }

    [UnityTest]
    public IEnumerator Test_CreatingLessonOKPass()
    {
        _gameObject = null;
        var DataBaseController = new DataBaseController();
        AssemliesTable assemblies = new AssemliesTable();
        LessonsTable lessons = new LessonsTable();
        TextsTable texts = new TextsTable();
        TypesTable types = new TypesTable();
        UsersTable users = new UsersTable();
        VideosTable videos = new VideosTable();
        List<IDataBase> tables = new List<IDataBase>();
        tables.Add(assemblies); // 0 - assembles
        tables.Add(lessons); // 1 - lessons
        tables.Add(texts); // 2 - text
        tables.Add(types); // 3 - types
        tables.Add(users); // 4 - users
        tables.Add(videos); // 5 - videos
        AssemblyCreator assemblyCreator = new AssemblyCreator();
        var _gameContextWithViews = new GameContextWithViews();
        var _gameContextWithLogic = new GameContextWithLogic();
        LessonConstructorController lessonConstructorController 
            = new LessonConstructorController(DataBaseController,tables,
                _gameContextWithViews,
                new GameContextWithLessons(),
                new GameContextWithUI(),
                _gameContextWithLogic,
                new FileManagerController(),
                new FileManager(),
                assemblyCreator,
                new RectTransform()
                );
        lessonConstructorController.GiveMeGameObject += GivingGameObj;
        lessonConstructorController.Initialization();
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadModel,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadModel].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadModel].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
       
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadVideo,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadVideo].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadVideo].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
       
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadPDF,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadPDF].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadPDF].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
        
        lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadModel,
            $@"D:\Unity-Storage\Diploma_Copy\diploma-unity\AssetBundles\StandaloneWindows\Тормоз\brake",
            @"D:\Unity-Storage\Diploma_Copy\diploma-unity\AssetBundles\StandaloneWindows\Тормоз\brake");
        lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadVideo,
            "Выберите видео-фаил (*.mp4)","Выберите видео-фаил (*.mp4)");
        lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadPDF,
            @"D:\Voprosy_k_GOS_2021.pdf",
            @"D:\Voprosy_k_GOS_2021.pdf");
        
        _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.SetNameToLesson,new GameObject());
        new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].transform);
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].AddComponent<TMP_InputField>();
        _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].
            GetComponent<TMP_InputField>().text = "SomeNewLesson";
        
        
        _gameContextWithViews.AddToggles(0,new GameObject());
        _gameContextWithViews.ChoosenToggles[0].AddComponent<Toggle>();
        _gameContextWithViews.ChoosenToggles[0].GetComponentInChildren<Toggle>().isOn = true;
        var DefCamera = new GameObject();
        DefCamera.AddComponent<Camera>();
        var mainCamera = GameObject.Instantiate(DefCamera);
        _gameContextWithLogic.SetAMainCamera(mainCamera.GetComponent<Camera>());
        _gameContextWithLogic.SetAScreenShotCamera(mainCamera.GetComponent<Camera>());
        
        //CreateAssemblyDis
        ErrorCodes error;
        lessonConstructorController.OpenAnUIInitialization();
        yield return new WaitUntil(()=> _gameObject != null);
        Assert.AreEqual(ErrorCodes.None, lessonConstructorController.CheckForErrors());
        lessonConstructorController.SavingAssemblyDis("1 2 3 4 5");
        //CreateLesson
        lessonConstructorController.CreateALesson();
        lessonConstructorController.GiveMeGameObject -= GivingGameObj;
        yield return new WaitForSeconds(10);
        Assert.AreEqual(ErrorCodes.None,lessonConstructorController.CheckForErrors());
        yield return null;
    }
    }
}
