using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;
using Prototype.View;
using System.Globalization;
using Prototype.DataModel;
using Prototype.DataModel.Tables;

namespace Prototype.Speech
{
    class SpeechCtrl
    {
        #region Fields
        
        private const String windowOperationsGrammar = "windowOperationsGrammar";
        private const String chooseLessonsGrammar = "chooseLessonsGrammar";

        WindowCtrl windowCtrl;

        Data data;

        SpeechRecognitionEngine recognitionEngine;

        CultureInfo culture;

        IView view;

        #endregion

        #region Properties

        public SpeechRecognitionEngine RecognitionEngine 
        {
            get { return recognitionEngine; }
            set { recognitionEngine = value; }
        }
        
        public IView View
        {
            get { return view; }
            set { view = value; }
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
            culture = CultureInfo.GetCultureInfo(ResourceStrings.cultureIdentifier);
        }

        #endregion

        #region Methods
        
        #region Initialize/Deinitialize

        public void Initialize()
        {
            data = Data.GetInstance();
            windowCtrl = WindowCtrl.GetInstance();

    		recognitionEngine = new SpeechRecognitionEngine(culture);

            recognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognitionEngine_SpeechRecognized);

            LoadWindowOperationsGrammar();
            LoadChooseLessonGrammar();

            recognitionEngine.SetInputToDefaultAudioDevice();
            recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void DeInitialize()
        {
            recognitionEngine.Dispose();
        }

        private void LoadWindowOperationsGrammar()
        {
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = culture;

            Choices nounChoices = new Choices();
            nounChoices.Add(new SemanticResultValue(ResourceStrings.back              , ENouns.back.ToString()));               //
            nounChoices.Add(new SemanticResultValue(ResourceStrings.grammarExercise   , ENouns.grammarExercise.ToString()));    //
            nounChoices.Add(new SemanticResultValue(ResourceStrings.grammarExplanation, ENouns.grammarExplanation.ToString())); //
            nounChoices.Add(new SemanticResultValue(ResourceStrings.menu              , ENouns.menu.ToString()));               //
            nounChoices.Add(new SemanticResultValue(ResourceStrings.options           , ENouns.options.ToString()));            //
            nounChoices.Add(new SemanticResultValue(ResourceStrings.program           , ENouns.program.ToString()));            //
            nounChoices.Add(new SemanticResultValue(ResourceStrings.thisWindow        , ENouns.thisWindow.ToString()));         //
            nounChoices.Add(new SemanticResultValue(ResourceStrings.wordsExercise     , ENouns.wordsExercise.ToString()));      //
            gb.Append(new SemanticResultKey("target", nounChoices));

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

            Choices verbChoices = new Choices();
            verbChoices.Add(new SemanticResultValue(ResourceStrings.closeRu       , EVerbs.close.ToString())); //閉める
            verbChoices.Add(new SemanticResultValue(ResourceStrings.closeMasu     , EVerbs.close.ToString())); //閉めます
            verbChoices.Add(new SemanticResultValue(ResourceStrings.closeTe       , EVerbs.close.ToString())); //閉めてください
            verbChoices.Add(new SemanticResultValue(ResourceStrings.closeTekudasai, EVerbs.close.ToString())); //閉めて
            verbChoices.Add(new SemanticResultValue(ResourceStrings.goRu          , EVerbs.goTo.ToString()));  //行く
            verbChoices.Add(new SemanticResultValue(ResourceStrings.goMasu        , EVerbs.goTo.ToString()));  //行きます
            verbChoices.Add(new SemanticResultValue(ResourceStrings.goTe          , EVerbs.goTo.ToString()));  //行って
            verbChoices.Add(new SemanticResultValue(ResourceStrings.goTekudasai   , EVerbs.goTo.ToString()));  //行ってください
            verbChoices.Add(new SemanticResultValue(ResourceStrings.openRu        , EVerbs.open.ToString()));  //開ける
            verbChoices.Add(new SemanticResultValue(ResourceStrings.openMasu      , EVerbs.open.ToString()));  //開けます
            verbChoices.Add(new SemanticResultValue(ResourceStrings.openTe        , EVerbs.open.ToString()));  //開けてください
            verbChoices.Add(new SemanticResultValue(ResourceStrings.openTekudasai , EVerbs.open.ToString()));  //開けて
            gb.Append(new SemanticResultKey("action", verbChoices));

            Grammar grammar = new Grammar(gb);
            grammar.Name = windowOperationsGrammar;
            recognitionEngine.LoadGrammar(grammar);
        }

        private void LoadChooseLessonGrammar()
        {
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = culture;
            GrammarBuilder lessonBuilder = GetLessonBuilder();
            GrammarBuilder fromToBuilder = GetFromToBuilder(lessonBuilder);

            Choices selectChoices = new Choices();
            selectChoices.Add(new SemanticResultValue(ResourceStrings.all, ENouns.all.ToString()));
            selectChoices.Add(new SemanticResultValue(ResourceStrings.nothing, ENouns.nothing.ToString()));
            selectChoices.Add(new SemanticResultValue(lessonBuilder, ENouns.lesson.ToString()));
            selectChoices.Add(new SemanticResultValue(fromToBuilder, ENouns.lessonFromTo.ToString()));

            String[] selectActions = 
            {
                ResourceStrings.selectRu,
                ResourceStrings.selectMasu,
                ResourceStrings.selectTe,
                ResourceStrings.selectTekudasai,
            };
            Choices selectActionChoices = new Choices(selectActions);

            gb.Append(new SemanticResultKey(ResourceStrings.targetKey, selectChoices));
            gb.Append(ResourceStrings.wo, 0, 1);
            gb.Append(selectActionChoices);

            Grammar grammar = new Grammar(gb);
            grammar.Name = chooseLessonsGrammar;
            recognitionEngine.LoadGrammar(grammar);
        }

        private GrammarBuilder GetLessonBuilder()
        {
            String[] lessons = new String[data.Lessons.Length];
            for (int i = 0; i < data.Lessons.Length; ++i)
            {
                lessons[i] = ResourceStrings.lesson1 + (i + 1) + ResourceStrings.lesson2;
            }
            Choices lessonChoices = new Choices(lessons);

            GrammarBuilder lessonBuilder = new GrammarBuilder(lessonChoices);
            lessonBuilder.Culture = culture;
            return lessonBuilder;
        }

        private GrammarBuilder GetFromToBuilder(GrammarBuilder lessonBuilder)
        {
            GrammarBuilder fromToBuilder = new GrammarBuilder();
            fromToBuilder.Append(lessonBuilder);
            fromToBuilder.Append(ResourceStrings.kara);
            fromToBuilder.Append(lessonBuilder);
            fromToBuilder.Append(ResourceStrings.made);
            fromToBuilder.Culture = culture;
            return fromToBuilder;
        }

        #endregion

        #region Recognize Speech

        void recognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            data.CurrentComand = ": " + e.Result.Text;

            switch (e.Result.Grammar.Name)
            {
                case windowOperationsGrammar: ComputeWindowResult(e.Result); break;
                case chooseLessonsGrammar: ComputeChooseLessonResult(e.Result); break;
            }

            view.UpdateView();
        }

        private void ComputeWindowResult(RecognitionResult result)
        {
            EVerbs action = (EVerbs)Enum.Parse(typeof(EVerbs), result.Semantics[ResourceStrings.actionKey].Value as String);
            ENouns noun = (ENouns)Enum.Parse(typeof(ENouns), result.Semantics[ResourceStrings.targetKey].Value as String);

            ComputeWindowComand(noun, action);
        }
        
        private void ComputeWindowComand(ENouns noun, EVerbs action)
        {
            if (action == EVerbs.close)
            {
                switch (noun)
                {
                    case ENouns.program: windowCtrl.CloseApp(); break;
                    case ENouns.thisWindow: CloseThisWindow(); break;
                }
                return;
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
                return;
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
                return;
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

        private void ComputeChooseLessonResult(RecognitionResult result)
        {
            ENouns noun = (ENouns)Enum.Parse(typeof(ENouns), result.Semantics[ResourceStrings.targetKey].Value as String);

            if (noun == ENouns.all)
            {
                data.SelectedLessons = data.Lessons;

                view.UpdateView();
                return;
            }

            if (noun == ENouns.nothing)
            {
                data.SelectedLessons = null;

                view.UpdateView();
                return;
            }

            if (noun == ENouns.lesson)
            {
                int l = extractNumber(result.Words[0].Text);
                data.SelectedLessons = new Lesson[] { data.Lessons[l - 1] };

                view.UpdateView();
                return;
            }

            if (noun == ENouns.lessonFromTo)
            {
                int from = extractNumber(result.Words[0].Text) - 1;
                int to = extractNumber(result.Words[2].Text) - 1;

                if (from > to)
                {
                    int hv = from;
                    from = to;
                    to = hv;
                }

                List<Lesson> lessons = new List<Lesson>();
                for (int i = from; i <= to ; ++i)
                {
                    lessons.Add(data.Lessons[i]);
                }
                data.SelectedLessons = lessons.ToArray();
                view.UpdateView();
            }
        }

        private int extractNumber(String text)
        {
            char[] lesson = text.ToCharArray();
            String number = new String(lesson, 1, lesson.Length - 2);
            return Convert.ToInt32(number);
        }

        #endregion

        #endregion
    }
}
