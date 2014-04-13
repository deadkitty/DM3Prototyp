using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;

namespace Controll
{
    class LanguageController
    {
        #region Fields
        
        IView view;

        SpeechRecognitionEngine recognitionEngine;

        public SpeechRecognitionEngine RecognitionEngine
        {
            get { return recognitionEngine; }
            set { recognitionEngine = value; }
        }

        WindowController window;

        #endregion

        #region Properties

        public IView View
        {
            get { return view; }
            set { view = value; }
        }

        public WindowController Window
        {
            get { return window; }
            set { window = value; }
        }

        #endregion

        #region Singleton

        private static LanguageController sInstance;

        public static LanguageController GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new LanguageController();
            }
            return sInstance;
        }

        #endregion

        #region Constructor

        private LanguageController()
        {
            
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            recognitionEngine = new SpeechRecognitionEngine(System.Globalization.CultureInfo.GetCultureInfo("ja-JP"));
                        
            String[] windowStrings =
            {
                "説明",           //test wörter
                "練習",
                "123",
                "青い",
                "１",                
                "３",
                "１０００",
                "百",
                "ひゃく",
                "練習",
                "れんしゅう",
                "ことばの",
                "文法の説明",        //ぶんぽうのせつめい - grammatik erklärung
                "文法の練習",        //ぶんぽうのれんしゅう - grammatik übung
                "ことばの練習",      //Vokabel übungen
                "ぶんぽうのせつめい",
                "ぶんぽうのれんしゅう",
                "ことばのれんしゅう",
                "オプション",        //Einstellungen
            };
            Choices windowChoices = new Choices(windowStrings);

            String[] openStrings =
            {
                "開ける",
                "開けてください",
                "開けて",                
                "開けます",
            };
            Choices openChoices = new Choices(openStrings);

            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = System.Globalization.CultureInfo.GetCultureInfo("ja-JP");
            gb.Append(windowChoices);
            gb.Append("を", 0, 1);
            gb.Append(openChoices);
            
            Grammar grammar = new Grammar(gb);
            
            recognitionEngine.LoadGrammar(grammar);
            recognitionEngine.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(recognitionEngine_RecognizeCompleted);

            recognitionEngine.SetInputToDefaultAudioDevice();
            recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        void recognitionEngine_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            
        }

        void recognitionEngine_LoadGrammarCompleted(object sender, LoadGrammarCompletedEventArgs e)
        {
            
        }
        
        public void DeInitialize()
        {
            recognitionEngine.Dispose();
        }
        
        void recognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            view.UpdateView(e.Result.Text);            
        }

        public void ComputeSpeechRecognition(String speechText)
        {

        }

        #endregion
    }
}
