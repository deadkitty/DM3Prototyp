using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;
using Prototype.View;
using System.Globalization;
using Prototype.DataModel;

namespace Prototype.Speech
{
    class SpeechCtrl
    {
        #region Fields
        
        private const String windowOperationsGrammar = "windowOperationsGrammar";
        
        WindowCtrl windowCtrl;

        Data data;

#if japaneseVersion
        SpeechRecognitionEngine recognitionEngine;
#else
        SpeechRecognizer recognitionEngine;
#endif

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

        #region Methods
        
        #region Initialize/Deinitialize

        public void Initialize()
        {
            data = Data.GetInstance();
            windowCtrl = WindowCtrl.GetInstance();
            
#if japaneseVersion
    		recognitionEngine = new SpeechRecognitionEngine(CultureInfo.GetCultureInfo(ResourceStrings.cultureIdentifier));
#else
            recognitionEngine = new SpeechRecognizer();    
#endif

            recognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognitionEngine_SpeechRecognized);

            LoadWindowOperationsGrammar();

#if japaneseVersion
            recognitionEngine.SetInputToDefaultAudioDevice();
            recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
#endif
        }

        public void DeInitialize()
        {
            recognitionEngine.Dispose();
        }

        private void LoadWindowOperationsGrammar()
        {
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = CultureInfo.GetCultureInfo(ResourceStrings.cultureIdentifier);

            String[] nouns =
            {
                ResourceStrings.back,              //バック
                ResourceStrings.grammarExplanation,//ぶんぽうの説明
                ResourceStrings.grammarExercise,   //ぶんぽうの練習
                ResourceStrings.wordsExercise,     //ことばの練習
                ResourceStrings.options,           //オプション
                ResourceStrings.program,           //プログラム
                ResourceStrings.thisWindow,        //この窓
                ResourceStrings.menu,              //メニュー
            };
            Choices nounChoices = new Choices(nouns);
            gb.Append(nounChoices);

#if japaneseVersion
            String[] particle = 
            {
                ResourceStrings.wo,    //を
                ResourceStrings.he,    //へ
                ResourceStrings.mo,    //も
            };
            Choices particleChoices = new Choices(particle);
            gb.Append(particleChoices, 0, 1);
#endif

            String[] verbs =
            {
                ResourceStrings.closeRu,       //閉める
                ResourceStrings.closeMasu,     //閉めます
                ResourceStrings.closeTekudasai,//閉めてください
                ResourceStrings.closeTe,       //閉めて
                ResourceStrings.goRu,          //行く
                ResourceStrings.goMasu,        //行きます
                ResourceStrings.goTe,
                ResourceStrings.goTekudasai,
                ResourceStrings.openRu,        //開ける
                ResourceStrings.openMasu,      //開けます
                ResourceStrings.openTekudasai, //開けてください
                ResourceStrings.openTe,        //開けて
            };
            Choices verbChoices = new Choices(verbs);
            gb.Append(verbChoices);

            Grammar grammar = new Grammar(gb);
            grammar.Name = windowOperationsGrammar;
            recognitionEngine.LoadGrammar(grammar);
        }

        #endregion

        #region Recognize Speech

        void recognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            data.CurrentComand = e.Result.Text;

            switch (e.Result.Grammar.Name)
            {
                case windowOperationsGrammar: ComputeCloseWindowResult(e.Result); break;
            }

            windowCtrl.View.UpdateView();
        }

        private void ComputeCloseWindowResult(RecognitionResult result)
        {
            EVerbs action = EVerbs.undefined;
            ENouns noun = ENouns.undefined;
            //traverse recognized words
            for (int i = result.Words.Count - 1; i >= 0; --i)
            {
                if (action == EVerbs.undefined)
                {
                    //traverse resource strings
                    for (int j = 0; j < ResourceStrings.verbs.Length; ++j)
                    {
                        //compare recognized and resource words
                        if (result.Words[i].Text == ResourceStrings.verbs[j])
                        {
                            //if they match check for the matching action and break loops
                            if (j <= (int)EVerbs.close)
                            {
                                action = EVerbs.close;
                                break;
                            }
                            if (j <= (int)EVerbs.goTo)
                            {
                                action = EVerbs.goTo;
                                break;
                            }
                            if (j <= (int)EVerbs.open)
                            {
                                action = EVerbs.open;
                                break;
                            }
                        }
                    }
                }
                else if (noun == ENouns.undefined)
                {
                    for (int j = 0; j < ResourceStrings.nouns.Length; ++j)
                    {
                        if (result.Words[i].Text == ResourceStrings.nouns[j])
                        {
                            if (j <= (int)ENouns.back)
                            {
                                noun = ENouns.back;
                                break;
                            }
                            if (j <= (int)ENouns.grammarExercise)
                            {
                                noun = ENouns.grammarExercise;
                                break;
                            }
                            if (j <= (int)ENouns.grammarExplanation)
                            {
                                noun = ENouns.grammarExplanation;
                                break;
                            }
                            if (j <= (int)ENouns.menu)
                            {
                                noun = ENouns.menu;
                                break;
                            }
                            if (j <= (int)ENouns.options)
                            {
                                noun = ENouns.options;
                                break;
                            }
                            if (j <= (int)ENouns.program)
                            {
                                noun = ENouns.program;
                                break;
                            }
                            if (j <= (int)ENouns.thisWindow)
                            {
                                noun = ENouns.thisWindow;
                                break;
                            }
                            if (j <= (int)ENouns.wordsExercise)
                            {
                                noun = ENouns.wordsExercise;
                                break;
                            }
                        }
                    }
                }
            }

            ComputeComand(noun, action);
        }

        private void ComputeComand(ENouns noun, EVerbs action)
        {
            if (action == EVerbs.close)
            {
                switch (noun)
                {
                    case ENouns.program: windowCtrl.CloseApp(); break;
                    case ENouns.thisWindow: CloseThisWindow(); break;
                }
            }

            if (action == EVerbs.open)
            {
                switch (noun)
                {
                    case ENouns.grammarExercise: windowCtrl.ChangeWindowContent(EContentType.grammarExerciseContent); break;
                    case ENouns.grammarExplanation: windowCtrl.ChangeWindowContent(EContentType.grammarExplanationContent); break;
                    case ENouns.menu: windowCtrl.ChangeWindowContent(EContentType.mainMenuContent); break;
                    case ENouns.options: windowCtrl.ChangeWindowContent(EContentType.optionsContent); break;
                    case ENouns.wordsExercise: windowCtrl.ChangeWindowContent(EContentType.wordsExerciseContent); break;
                }
            }

            if (action == EVerbs.goTo)
            {
                switch (noun)
                {
                    case ENouns.back: windowCtrl.GoBack(); break;
                    case ENouns.grammarExercise: windowCtrl.ChangeWindowContent(EContentType.grammarExerciseContent); break;
                    case ENouns.grammarExplanation: windowCtrl.ChangeWindowContent(EContentType.grammarExplanationContent); break;
                    case ENouns.menu: windowCtrl.ChangeWindowContent(EContentType.mainMenuContent); break;
                    case ENouns.options: windowCtrl.ChangeWindowContent(EContentType.optionsContent); break;
                    case ENouns.wordsExercise: windowCtrl.ChangeWindowContent(EContentType.wordsExerciseContent); break;
                }
            }

        }

        private void CloseThisWindow()
        {
            if (windowCtrl.CurrentContentType == EContentType.mainMenuContent)
            {
                windowCtrl.CloseApp();
            }
            else
            {
                windowCtrl.ChangeWindowContent(EContentType.mainMenuContent);
            }
        }

        #endregion

        #endregion
    }
}
