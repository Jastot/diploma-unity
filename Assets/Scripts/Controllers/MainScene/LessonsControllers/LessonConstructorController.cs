﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Managers;
using Diploma.Tables;
using Diploma.UI;
using GameObjectCreating;
using Interfaces;
using ListOfLessons;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using Types = Diploma.Tables.Types;

namespace Controllers
{
    public class LessonConstructorController: IInitialization,INeedScreenShoot
    {
        private readonly DataBaseController _dataBaseController;
        private readonly List<IDataBase> _tables;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithLessons _gameContextWithLessons;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly FileManagerController _fileManagerController;
        private string[] _destination = new string[4];
        private readonly FileManager _fileManager;
        private Dictionary<LoadingParts, GameObject> _texts;
        private PlateWithButtonForLessonsFactory _plateWithButtonForLessonsFactory;
        private string[] _localText = new string[4];
        private string[] _localBufferText = new string[4];
        private string[] _massForCopy = new string[3];
        private ErrorCodes _error;
        //Plane[] planes;
        
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/MainScene/LessonPrefab"};
        
        public LessonConstructorController(
            DataBaseController dataBaseController,
            List<IDataBase>  tables,
            GameContextWithViews gameContextWithViews,
            GameContextWithLessons gameContextWithLessons,
            GameContextWithUI gameContextWithUI,
            GameContextWithLogic gameContextWithLogic,
            FileManagerController fileManagerController,
            FileManager fileManager
        )
        {
            _dataBaseController = dataBaseController;
            _tables = tables;
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithLessons = gameContextWithLessons;
            _gameContextWithUI = gameContextWithUI;
            _gameContextWithLogic = gameContextWithLogic;
            _fileManagerController = fileManagerController;
            _fileManager = fileManager;
            _texts = _gameContextWithViews.TextBoxesOnConstructor;
            _plateWithButtonForLessonsFactory = new PlateWithButtonForLessonsFactory(ResourceLoader.LoadPrefab(_viewPath));
        }

        public void Initialization()
        {
            _fileManagerController.newText += SetTextInTextBox;
        }

        public void SetTextInTextBox(LoadingParts loadingParts, string text,string firstPath)
        {
            switch (loadingParts)
            {
                case LoadingParts.DownloadModel:
                    _localText[0] = text;
                    _texts[loadingParts].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                        _localText[0].Split('\\').Last();
                    _massForCopy[0] = firstPath;
                    break;
                case LoadingParts.DownloadVideo:
                    _localText[2] =  text;
                    _texts[loadingParts].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                        _localText[2].Split('\\').Last();
                    _massForCopy[2] = firstPath;
                    break;
                case LoadingParts.DownloadPDF:
                    _localText[1] = text;
                    _texts[loadingParts].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                        _localText[1].Split('\\').Last();
                    _massForCopy[1] = firstPath;
                    break;
                    // destinationPath[0] = fileManager.CreateFileFolder("Assemblies");
                    // destinationPath[1] = fileManager.CreateFileFolder("Videos");
                    // destinationPath[2] = fileManager.CreateFileFolder("Photos");
                    // destinationPath[3] = fileManager.CreateFileFolder("Texts");
            }
        }
        public ErrorCodes CheckForErrors()
        {
            return _error;
        }
        public bool CreateALesson()
        {
             //это из меню
             // 0 - assembles
             // 1 - lessons
             // 2 - text
             // 3 - types
             // 4 - users
             // 5 - videos
             //порядок заполнения в бд таблицы Lessons
             // 0 - preview
             // 1 - text
             // 2 - video
             // 3 - assembly
             // 4 - type
             // 5 - name
             _error = ErrorCodes.None;
            string[] lessonPacked = new string[6];
            lessonPacked[0] = null;
            lessonPacked[1] = null;
            lessonPacked[2] = null;
            lessonPacked[3] = null;
            lessonPacked[4] = null;
            lessonPacked[5] = null;
            // creating Folder-packed
            // нужно добавить id
            _dataBaseController.SetTable(_tables[1]);
            int id;
            if (_dataBaseController.GetDataFromTable<Lessons>().Count == 0)
            {
                id = 0;
            }
            else
            {
                id = _dataBaseController.GetDataFromTable<Lessons>().Last().Lesson_Id+1;
            }
            _destination[0] = _fileManager.CreateFileFolder(id +"\\"+ "Assemblies");
            _destination[1] = _fileManager.CreateFileFolder(id +"\\"+ "Videos");
            _destination[2] = _fileManager.CreateFileFolder(id +"\\"+ "Photos");
            _destination[3] = _fileManager.CreateFileFolder(id +"\\"+ "Texts");
            
            // add new assembly
            if (_localText[0] != @"Выберите UnityBundle ()")
            {
                _localBufferText[0] =_destination[0]+ "\\" +_localText[0];
                Debug.Log(_localBufferText[0]);
                _dataBaseController.SetTable(_tables[0]);
                string[] assemblyPacked = new string[1];
                assemblyPacked[0] = id +"\\"+ "Assemblies"+ "\\" +_localText[0];
                _dataBaseController.AddNewRecordToTable(assemblyPacked);
                lessonPacked[3] = _dataBaseController.GetDataFromTable<Assemblies>().Last().Assembly_Id.ToString();
            }

            // add new text
            if (_localText[1] != @"Выберите текстовый фаил(*.pdf)")
            {
                _localBufferText[1] = _destination[3] + "\\" + _localText[1];
                Debug.Log(_localBufferText[1]);
                if (_massForCopy[1] != "")
                    //if (!File.Exists(_localBufferText[1]))
                    File.Copy(_massForCopy[1], _localBufferText[1],true);
                _dataBaseController.SetTable(_tables[2]);
                string[] textPacked = new string[1];
                textPacked[0] = id +"\\"+ "Texts"+ "\\" +_localText[1];
                _dataBaseController.AddNewRecordToTable(textPacked);
                lessonPacked[1] = _dataBaseController.GetDataFromTable<Texts>().Last().Text_Id.ToString();  
                //PDFReader pdfReader = new PDFReader();
                //pdfReader.ProcessRequest(textPacked[0],_destination[2]);
            }

            // add new video
            if (_localText[2] != @"Выберите видео-фаил (*.mp4)")
            {
                Debug.Log(_localBufferText[2]);
                _localBufferText[2] = _destination[1]+ "\\" +  _localText[2];
                if(_massForCopy[2]!="")
                    //if (!File.Exists(_localBufferText[2]))
                    File.Copy(_massForCopy[2],_localBufferText[2],true);
                _dataBaseController.SetTable(_tables[5]);
                string[] videoPacked = new string[1];
                videoPacked[0] = id +"\\"+ "Videos"+ "\\" +_localText[2];
                _dataBaseController.AddNewRecordToTable(videoPacked);
                lessonPacked[2] = _dataBaseController.GetDataFromTable<Videos>().Last().Video_Id.ToString();
            }
            //
            
            // add name
            if (_texts[LoadingParts.SetNameToLesson].GetComponent<TMP_InputField>().text != "")
            {
                lessonPacked[5] = _texts[LoadingParts.SetNameToLesson].
                    GetComponent<TMP_InputField>().text;
            }
            else
            {
                _error = ErrorCodes.EmptyInputError;
            }


            
            
            int count = 0;
            _dataBaseController.SetTable(_tables[3]);
            string[] typePacked = new string[1];
            typePacked[0] = null;
            foreach (var key in _gameContextWithViews.ChoosenToggles.Values) {
                if (key.GetComponentInChildren<Toggle>().isOn)
                {
                    typePacked[0] += count + ",";
                }
                count++;
            }

            if (typePacked[0] == null)
            {
                _error = ErrorCodes.EmptyInputError;
            }
            _dataBaseController.AddNewRecordToTable(typePacked);
            lessonPacked[4] = _dataBaseController.GetDataFromTable<Types>().Last().Type_Id.ToString();
            if (CheckForErrors()!= ErrorCodes.None)
            {
                DeleteNotUsingLesson(lessonPacked);
                for (int i = 0; i < 6; i++)
                {
                    lessonPacked[i] = null;
                }
                return false;
            }
            // add screens
            File.Copy(_massForCopy[0],_localBufferText[0],true);
            _dataBaseController.SetTable(_tables[0]);
            Assemblies Assembly = (Assemblies)_dataBaseController.GetRecordFromTableById(Convert.ToInt32(lessonPacked[3]));
            var GameObjectInitilization = new GameObjectInitialization(Assembly, _fileManager);

            SetCameraNearObject(GameObjectInitilization.InstantiateGameObject());
            Debug.Log(_destination[2]+"\\"+lessonPacked[5]+".png");
            TakingScreen(_destination[2]+"\\"+lessonPacked[5]+".png");
            lessonPacked[0] = id +"\\"+ "Photos"+  "\\" + lessonPacked[5]+".png";


            _dataBaseController.SetTable(_tables[1]);
            _dataBaseController.AddNewRecordToTable(lessonPacked);
            _error = ErrorCodes.None;
            return true;
            
        }

        private void DeleteNotUsingLesson(string[] lessonPacked)
        {
            
            if (lessonPacked[1]!=null)
            {
                _dataBaseController.SetTable(_tables[2]);
                _dataBaseController.DeleteLastRecord(_dataBaseController.GetDataFromTable<Texts>().Last().Text_Id);
            }
            if (lessonPacked[2]!=null)
            {
                _dataBaseController.SetTable(_tables[5]);
                _dataBaseController.DeleteLastRecord(_dataBaseController.GetDataFromTable<Videos>().Last().Video_Id);
            }
            if (lessonPacked[3]!=null)
            {
                _dataBaseController.SetTable(_tables[0]);
                _dataBaseController.DeleteLastRecord(_dataBaseController.GetDataFromTable<Assemblies>().Last().Assembly_Id);
            }
            if (lessonPacked[4]!=null)
            {
                _dataBaseController.SetTable(_tables[3]);
                _dataBaseController.DeleteLastRecord(_dataBaseController.GetDataFromTable<Types>().Last().Type_Id);
            }
        }

        public void SetCameraNearObject(GameObject gameObject)
        {
            gameObject.transform.position = gameObject.transform.position.Change(x:0f,y: 0f,z: 0f);
            _gameContextWithLogic.MainCamera.transform.LookAt(gameObject.transform);
        }
        
        public void AddNewLessonToListOnUI()
        {
            _dataBaseController.SetTable(_tables[1]);
            
            Lessons lastLessonInDb = _dataBaseController.GetDataFromTable<Lessons>().Last();
            
            var lessonToggle = _plateWithButtonForLessonsFactory.Create(
                _gameContextWithUI.UiControllers[LoadingParts.LoadLectures].transform.GetChild(2).GetChild(0).GetChild(0).gameObject.transform);
            lessonToggle.transform.localPosition = new Vector3(0,0,0);
            
            var tex = new Texture2D(5, 5);
            tex.LoadImage(File.ReadAllBytes(_fileManager.GetStorage() +"\\"+ lastLessonInDb.Lesson_Preview));
            lessonToggle.GetComponentInChildren<RawImage>().texture = tex;

            var lessonName = lessonToggle.GetComponentInChildren<TextMeshProUGUI>();
            lessonName.text = lastLessonInDb.Lesson_Name;
                
            _gameContextWithLessons.AddLessonsView(lastLessonInDb.Lesson_Id,
                new ListOfLessonsView(lastLessonInDb.Lesson_Id,
                    lessonToggle));
            _gameContextWithViews.AddLessonsToggles(lastLessonInDb.Lesson_Id,lessonToggle);

            //var lessonChooseButtonsLogic = new LessonChooseButtonsLogic(_gameContextWithViews.ChoosenLessonToggles);
            //lessonChooseButtonsLogic.AddNewButton(lastLessonInDb.Lesson_Id);
            _gameContextWithViews.LessonChooseButtonsLogic.AddNewButton(lastLessonInDb.Lesson_Id);
           
        }
        public event Action<string> TakeScreenShoot;
        public void TakingScreen(string name)
        {
            TakeScreenShoot?.Invoke(name);
        }
    }
}