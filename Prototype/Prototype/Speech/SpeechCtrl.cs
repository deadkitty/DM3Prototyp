using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;
using Prototype.View;
using Prototype.Resources;

namespace Prototype.Speech
{
    class SpeechCtrl
    {
        #region Fields
        
        private const String openWindowGrammar = "openWindowGrammar";
        private const String closeWindowGrammar = "closeWindowGrammar";

        SpeechRecognitionEngine recognitionEngine;

        WindowCtrl windowCtrl;

        #endregion

        #region Properties
        
        public SpeechRecognitionEngine RecognitionEngine
        {
            get { return recognitionEngine; }
            set { recognitionEngine = value; }
        }

        #endregion

        #region Singleton

        private static SpeechCtrl sInstance;

        public static SpeechCtrl GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new SpeechCtrl();
            }
            return sInstance;
        }

        #endregion

        #region Constructor

        private SpeechCtrl()
        {
            
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            windowCtrl = WindowCtrl.GetInstance();

            recognitionEngine = new SpeechRecognitionEngine(System.Globalization.CultureInfo.GetCultureInfo("ja-JP"));
            recognitionEngine.SpeechRecognized +=new EventHandler<SpeechRecognizedEventArgs>(recognitionEngine_SpeechRecognized);

            LoadOpenWindowGrammar();
            LoadCloseWindowGrammar();

            recognitionEngine.SetInputToDefaultAudioDevice();
            recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void LoadOpenWindowGrammar()
        {
            String[] windowStrings =
            {     
                RecognizeStrings.grammarExplanation,//ぶんぽうの説明
                RecognizeStrings.grammarExercise,   //ぶんぽうの練習
                RecognizeStrings.wordsExercise,     //ことばの練習
                RecognizeStrings.options,           //オプション
            };
            Choices windowChoices = new Choices(windowStrings);

            String[] openStrings =
            {
                RecognizeStrings.openRu,        //開ける
                RecognizeStrings.openMasu,      //開けます
                RecognizeStrings.openTekudasai, //開けてください
                RecognizeStrings.openTe,        //開けて
            };
            Choices openChoices = new Choices(openStrings);

            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = System.Globalization.CultureInfo.GetCultureInfo("ja-JP");
            gb.Append(windowChoices);
            gb.Append(RecognizeStrings.wo, 0, 1);
            gb.Append(openChoices);
            Grammar grammar = new Grammar(gb);
            grammar.Name = openWindowGrammar;

            recognitionEngine.LoadGrammar(grammar);
        }

        private void LoadCloseWindowGrammar()
        {
            String[] closeObjects =
            {
                RecognizeStrings.program,    //プログラム
                RecognizeStrings.thisWindow, //この窓
                RecognizeStrings.back,       //バック
                RecognizeStrings.menu,       //メニュー
            };
            Choices objectChoices = new Choices(closeObjects);

            String[] particleStrings = 
            {
                RecognizeStrings.wo,    //を
                RecognizeStrings.he,    //へ
            };
            Choices particleChoices = new Choices(particleStrings);

            String[] closeStrings =
            {
                RecognizeStrings.closeRu,       　//閉める
                RecognizeStrings.closeMasu,     　//閉めます
                RecognizeStrings.closeTekudasai,　//閉めてください
                RecognizeStrings.closeTe,       　//閉めて
                RecognizeStrings.goRu,          　//行く
                RecognizeStrings.goMasu,        　//行きます
            };
            Choices closeChoices = new Choices(closeStrings);
                        
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = System.Globalization.CultureInfo.GetCultureInfo("ja-JP");
            gb.Append(objectChoices);
            gb.Append(particleChoices, 0, 1);
            gb.Append(closeChoices);

            Grammar grammar = new Grammar(gb);
            grammar.Name = closeWindowGrammar;
            recognitionEngine.LoadGrammar(grammar);
        }
        
        public void DeInitialize()
        {
            recognitionEngine.Dispose();
        }
        
        void recognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Grammar.Name)
            {
                case openWindowGrammar: ComputeOpenWindowResult(e.Result); break;
                case closeWindowGrammar: ComputeCloseWindowResult(e.Result); break;
            }
        }

        private void ComputeOpenWindowResult(RecognitionResult result)
        {
            EContentType type = EContentType.mainMenuContent;

            if (RecognizeStrings.grammarExplanation == result.Words[0].Text)
            {
                type = EContentType.grammarExplanationContent;
            }

            windowCtrl.ChangeWindowContent(type);
        }

        private void ComputeCloseWindowResult(RecognitionResult result)
        {
            EContentType type = EContentType.mainMenuContent;

            if (RecognizeStrings.program == result.Words[0].Text)
            {
                windowCtrl.CloseApp();
            }

            //if (result.Words.Count == 2)
            //{

            //}
            //else
            //{

            //}

            //if (RecognizeStrings.back == result.Words[0].Text ||
            //    RecognizeStrings.thisWindow == result.Words[0].Text)
            //{

            //}

        }

        #endregion
    }
}
