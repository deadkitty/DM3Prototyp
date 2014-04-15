using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;
using Prototype.View;
using Prototype.Resources;
using System.Globalization;

namespace Prototype.Speech
{
    class SpeechCtrl
    {        
        #region Fields
        
        private const String openWindowGrammar = "openWindowGrammar";
        private const String closeWindowGrammar = "closeWindowGrammar";

#if japaneseVersion
        SpeechRecognitionEngine recognitionEngine;
#else
        SpeechRecognizer recognitionEngine;
#endif

        WindowCtrl windowCtrl;

        #endregion

        #region Properties

#if japaneseVersion
        public SpeechRecognitionEngine RecognitionEngine 
#else
        public SpeechRecognizer RecognitionEngine
#endif
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

#if japaneseVersion
            recognitionEngine = new SpeechRecognitionEngine(CultureInfo.GetCultureInfo(ResourceStrings.cultureIdentifier));
#else
            recognitionEngine = new SpeechRecognizer();    
#endif

            recognitionEngine.SpeechRecognized +=new EventHandler<SpeechRecognizedEventArgs>(recognitionEngine_SpeechRecognized);

            LoadOpenWindowGrammar();
            LoadCloseWindowGrammar();

#if japaneseVersion
            recognitionEngine.SetInputToDefaultAudioDevice();
            recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
#endif
        }

        private void LoadOpenWindowGrammar()
        {
            String[] windowStrings =
            {     
                ResourceStrings.grammarExplanation,//ぶんぽうの説明
                ResourceStrings.grammarExercise,   //ぶんぽうの練習
                ResourceStrings.wordsExercise,     //ことばの練習
                ResourceStrings.options,           //オプション
            };
            Choices windowChoices = new Choices(windowStrings);

            String[] openStrings =
            {
                ResourceStrings.openRu,        //開ける
                ResourceStrings.openMasu,      //開けます
                ResourceStrings.openTekudasai, //開けてください
                ResourceStrings.openTe,        //開けて
            };
            Choices openChoices = new Choices(openStrings);

            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = CultureInfo.GetCultureInfo(ResourceStrings.cultureIdentifier);
            gb.Append(windowChoices);
#if japaneseVersion
            gb.Append(ResourceStrings.wo, 0, 1); 
#endif
            gb.Append(openChoices);
            Grammar grammar = new Grammar(gb);
            grammar.Name = openWindowGrammar;

            recognitionEngine.LoadGrammar(grammar);
        }

        private void LoadCloseWindowGrammar()
        {
            String[] closeObjects =
            {
                ResourceStrings.program,    //プログラム
                ResourceStrings.thisWindow, //この窓
                ResourceStrings.back,       //バック
                ResourceStrings.menu,       //メニュー
            };
            Choices objectChoices = new Choices(closeObjects);
#if japaneseVersion
            String[] particleStrings = 
            {
                ResourceStrings.wo,    //を
                ResourceStrings.he,    //へ
            };
            Choices particleChoices = new Choices(particleStrings); 
#endif

            String[] closeStrings =
            {
                ResourceStrings.closeRu,       　//閉める
                ResourceStrings.closeMasu,     　//閉めます
                ResourceStrings.closeTekudasai,　//閉めてください
                ResourceStrings.closeTe,       　//閉めて
                ResourceStrings.goRu,          　//行く
                ResourceStrings.goMasu,        　//行きます
            };
            Choices closeChoices = new Choices(closeStrings);
                        
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = CultureInfo.GetCultureInfo(ResourceStrings.cultureIdentifier);
            gb.Append(objectChoices);
#if japaneseVersion
            gb.Append(particleChoices, 0, 1); 
#endif
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

            switch (result.Words[0].Text)
            {
                case ResourceStrings.grammarExplanation: type = EContentType.grammarExplanationContent; break;
            }
            
            windowCtrl.ChangeWindowContent(type);
        }

        private void ComputeCloseWindowResult(RecognitionResult result)
        {
            EContentType type = EContentType.mainMenuContent;

            switch (result.Words[0].Text)
            {
                case ResourceStrings.program: windowCtrl.CloseApp(); return;
                case ResourceStrings.back: type = EContentType.mainMenuContent; break;
            }

            windowCtrl.ChangeWindowContent(type);


            //if (result.Words.Count == 2)
            //{

            //}
            //else
            //{

            //}

            //if (ResourceStrings.back == result.Words[0].Text ||
            //    ResourceStrings.thisWindow == result.Words[0].Text)
            //{

            //}

        }

        #endregion
    }
}
