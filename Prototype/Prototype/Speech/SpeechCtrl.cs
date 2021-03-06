﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;
using Prototype.View;
using System.Globalization;
using Prototype.DataModel;
using Prototype.DataModel.Tables;
using System.Diagnostics;

namespace Prototype.Speech
{
    class SpeechCtrl
    {
        #region Fields
        
        private const String windowOperationsGr = "windowOperationsGrammar";
        private const String chooseLessonsGr = "chooseLessonsGrammar";
        private const String beginLessonsGr = "beginLessonsGrammar";
        private const String nextItemGr = "nextItemGrammar";
        private const String chooseParticleGr = "chooseParticleGrammar";
        private const String showAnswerGr = "showAnswerGrammar";
        private const String logWordGr = "logWordGrammar";
        private const String wordsPracticeGr = "wordPracticeGrammar";
        private const String dictGr = "dictGrammar";

        WindowCtrl windowCtrl;

        Data data;

        SpeechRecognitionEngine recognitionEngine;

        CultureInfo culture;

        IView view;
        ISpeech speechCommandExecuter;

        //window interaction grammars
        Grammar windowOperationsGrammar;
        Grammar chooseLessonsGrammar;
        Grammar beginLessonsGrammar;

        //general practice grammars
        Grammar nextItemGrammar;
        Grammar showAnswerGrammar;

        //grammar practice grammars
        Grammar chooseParticleGrammar;
                
        //words practice grammars
        Grammar wordsGrammar;
        Grammar logWordGrammar;

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
        
        public ISpeech SpeechCommandExecuter
        {
            get { return speechCommandExecuter; }
            set { speechCommandExecuter = value; }
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

            recognitionEngine.SetInputToDefaultAudioDevice();
            recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void DeInitialize()
        {
            recognitionEngine.Dispose();
        }

        #region Window Operations Grammar

        private void LoadWindowOperationsGrammar()
        {
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = culture;

            Choices nounChoices = new Choices();
            nounChoices.Add(new SemanticResultValue(ResourceStrings.back, ENouns.back.ToString()));
            nounChoices.Add(new SemanticResultValue(ResourceStrings.grammarExercise, ENouns.grammarExercise.ToString()));
            nounChoices.Add(new SemanticResultValue(ResourceStrings.grammarExplanation, ENouns.grammarExplanation.ToString()));
            nounChoices.Add(new SemanticResultValue(ResourceStrings.menu, ENouns.menu.ToString()));
            nounChoices.Add(new SemanticResultValue(ResourceStrings.options, ENouns.options.ToString()));
            nounChoices.Add(new SemanticResultValue(ResourceStrings.program, ENouns.program.ToString()));
            nounChoices.Add(new SemanticResultValue(ResourceStrings.thisWindow, ENouns.thisWindow.ToString()));
            nounChoices.Add(new SemanticResultValue(ResourceStrings.wordsExercise, ENouns.wordsExercise.ToString()));
            gb.Append(new SemanticResultKey(ResourceStrings.targetKey, nounChoices));

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
            verbChoices.Add(new SemanticResultValue(ResourceStrings.closeRu, EVerbs.close.ToString())); //閉める
            verbChoices.Add(new SemanticResultValue(ResourceStrings.closeMasu, EVerbs.close.ToString())); //閉めます
            verbChoices.Add(new SemanticResultValue(ResourceStrings.closeTe, EVerbs.close.ToString())); //閉めてください
            verbChoices.Add(new SemanticResultValue(ResourceStrings.closeTekudasai, EVerbs.close.ToString())); //閉めて
            verbChoices.Add(new SemanticResultValue(ResourceStrings.goRu, EVerbs.goTo.ToString()));  //行く
            verbChoices.Add(new SemanticResultValue(ResourceStrings.goMasu, EVerbs.goTo.ToString()));  //行きます
            verbChoices.Add(new SemanticResultValue(ResourceStrings.goTe, EVerbs.goTo.ToString()));  //行って
            verbChoices.Add(new SemanticResultValue(ResourceStrings.goTekudasai, EVerbs.goTo.ToString()));  //行ってください
            verbChoices.Add(new SemanticResultValue(ResourceStrings.openRu, EVerbs.open.ToString()));  //開ける
            verbChoices.Add(new SemanticResultValue(ResourceStrings.openMasu, EVerbs.open.ToString()));  //開けます
            verbChoices.Add(new SemanticResultValue(ResourceStrings.openTe, EVerbs.open.ToString()));  //開けてください
            verbChoices.Add(new SemanticResultValue(ResourceStrings.openTekudasai, EVerbs.open.ToString()));  //開けて
            gb.Append(new SemanticResultKey(ResourceStrings.actionKey, verbChoices));

            windowOperationsGrammar = new Grammar(gb);
            windowOperationsGrammar.Name = windowOperationsGr;
            recognitionEngine.LoadGrammar(windowOperationsGrammar);
        }

        public void UnloadWindowOperationsGrammar()
        {
            recognitionEngine.UnloadGrammar(windowOperationsGrammar);
        }
        
        #endregion

        #region Choose Lessons Grammar

        public void LoadChooseLessonGrammar()
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

            chooseLessonsGrammar = new Grammar(gb);
            chooseLessonsGrammar.Name = chooseLessonsGr;
            recognitionEngine.LoadGrammar(chooseLessonsGrammar);
        }

        public void UnloadChooseLessonGrammar()
        {
            recognitionEngine.UnloadGrammar(chooseLessonsGrammar);
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
#if japaneseVersion
            fromToBuilder.Append(lessonBuilder);
            fromToBuilder.Append(ResourceStrings.kara);
            fromToBuilder.Append(lessonBuilder);
            fromToBuilder.Append(ResourceStrings.made);
#else
            fromToBuilder.Append(ResourceStrings.kara);
            fromToBuilder.Append(lessonBuilder);
            fromToBuilder.Append(ResourceStrings.made);
            fromToBuilder.Append(lessonBuilder);

#endif
            fromToBuilder.Culture = culture;
            return fromToBuilder;
        }
        
        #endregion

        #region Begin Lesson Grammar

        public void LoadBeginLessonsGrammar()
        {
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = culture;

            Choices verbChoices = new Choices();
            verbChoices.Add(ResourceStrings.begin);
            verbChoices.Add(ResourceStrings.beginMaru);
            verbChoices.Add(ResourceStrings.beginMasu);
            verbChoices.Add(ResourceStrings.beginRu);
            verbChoices.Add(ResourceStrings.yesVerb);
            verbChoices.Add(ResourceStrings.yesCausualVerb);
            gb.Append(verbChoices);

            beginLessonsGrammar = new Grammar(gb);
            beginLessonsGrammar.Name = beginLessonsGr;
            recognitionEngine.LoadGrammar(beginLessonsGrammar);
        }

        public void UnloadBeginLessonsGrammar()
        {
            recognitionEngine.UnloadGrammar(beginLessonsGrammar);
        }

        #endregion

        #region Practice Grammar

        public void LoadNextItemGrammar()
        {
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = culture;

            Choices targetChoices = new Choices();
            targetChoices.Add(new SemanticResultValue(ResourceStrings.sentence, ENouns.sentence.ToString()));
            targetChoices.Add(new SemanticResultValue(ResourceStrings.word, ENouns.word.ToString()));

            gb.Append(ResourceStrings.next);
            gb.Append(new SemanticResultKey(ResourceStrings.targetKey, targetChoices), 0, 1);

            nextItemGrammar = new Grammar(gb);
            nextItemGrammar.Name = nextItemGr;
            recognitionEngine.LoadGrammar(nextItemGrammar);
        }

        public void UnloadNextItemGrammar()
        {
            recognitionEngine.UnloadGrammar(nextItemGrammar);
        }

        public void LoadShowAnswerGrammar()
        {
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = culture;

            Choices wordChoices = new Choices();
            wordChoices.Add(new SemanticResultValue(ResourceStrings.dontKnowNai, EVerbs.show.ToString()));
            wordChoices.Add(new SemanticResultValue(ResourceStrings.dontKnowMasen, EVerbs.show.ToString()));
            wordChoices.Add(new SemanticResultValue(ResourceStrings.answer, ENouns.answer.ToString()));

            GrammarBuilder gbAnswer = new GrammarBuilder();
            gbAnswer.Culture = culture;
            gbAnswer.Append(new SemanticResultKey(ResourceStrings.actionKey, wordChoices));


            Choices nounChoices = new Choices();
            nounChoices.Add(new SemanticResultValue(ResourceStrings.notAtAll, ENouns.notAtAll.ToString()));
            nounChoices.Add(new SemanticResultValue(ResourceStrings.answer, ENouns.answer.ToString()));

            Choices verbChoices = new Choices();
            verbChoices.Add(new SemanticResultValue(ResourceStrings.showRu,        EVerbs.show.ToString()));
            verbChoices.Add(new SemanticResultValue(ResourceStrings.showMasu,      EVerbs.show.ToString()));
            verbChoices.Add(new SemanticResultValue(ResourceStrings.showTe,        EVerbs.show.ToString()));
            verbChoices.Add(new SemanticResultValue(ResourceStrings.showTekudasai, EVerbs.show.ToString()));
            verbChoices.Add(new SemanticResultValue(ResourceStrings.dontKnowNai,   EVerbs.show.ToString()));
            verbChoices.Add(new SemanticResultValue(ResourceStrings.dontKnowMasen, EVerbs.show.ToString()));
            verbChoices.Add(new SemanticResultValue(ResourceStrings.please,        EVerbs.show.ToString()));

            GrammarBuilder gbNounVerb = new GrammarBuilder();
            gbNounVerb.Culture = culture;
            gbNounVerb.Append(new SemanticResultKey(ResourceStrings.targetKey, nounChoices));
            gbNounVerb.Append(ResourceStrings.wo, 0, 1);
            gbNounVerb.Append(new SemanticResultKey(ResourceStrings.actionKey, verbChoices));

            Choices showAnswersChoices = new Choices();
            showAnswersChoices.Add(gbNounVerb);
            showAnswersChoices.Add(gbAnswer);

            gb.Append(showAnswersChoices);

            showAnswerGrammar = new Grammar(gb);
            showAnswerGrammar.Name = showAnswerGr;
            recognitionEngine.LoadGrammar(showAnswerGrammar);
        }

        public void UnloadShowAnswerGrammar()
        {
            recognitionEngine.UnloadGrammar(showAnswerGrammar);
        }

        #endregion

        #region Grammar Practice Grammar

        public void LoadChooseParticleGrammar(String[] particle)
        {
            UnloadChooseParticleGrammar();

            Debug.WriteLine("Load chooseParticle Grammar", "prototype");
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = culture;

            Choices particleChoices = new Choices(particle);
            gb.Append(particleChoices);

            String[] selectActions = 
            {
                ResourceStrings.selectRu,
                ResourceStrings.selectMasu,
                ResourceStrings.selectTe,
                ResourceStrings.selectTekudasai,
                ResourceStrings.please,
            };
            Choices selectActionChoices = new Choices(selectActions);
            gb.Append(ResourceStrings.wo, 0, 1);
            gb.Append(selectActionChoices, 0, 1);

            chooseParticleGrammar = new Grammar(gb);
            chooseParticleGrammar.Name = chooseParticleGr;
            chooseParticleGrammar.Priority = 127;
            recognitionEngine.LoadGrammar(chooseParticleGrammar);
        }

        public void UnloadChooseParticleGrammar()
        {
            if (recognitionEngine.Grammars.Contains(chooseParticleGrammar))
            {
                Debug.WriteLine("Unload chooseParticle Grammar", "prototype");
                recognitionEngine.UnloadGrammar(chooseParticleGrammar);
            }
        }

        #endregion

        #region Words Practice Grammar

        public void LoadLogWordGrammar()
        {
            Debug.WriteLine("Load LogWord Grammar", "prototype");
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = culture;

            Choices logChoices = new Choices();
            logChoices.Add(new SemanticResultValue(ResourceStrings.yesVerb, EVerbs.select.ToString()));
            logChoices.Add(new SemanticResultValue(ResourceStrings.yesCausualVerb, EVerbs.select.ToString()));
            logChoices.Add(new SemanticResultValue(ResourceStrings.check, EVerbs.select.ToString()));
            logChoices.Add(new SemanticResultValue(ResourceStrings.no, EVerbs.deselect.ToString()));
            gb.Append(new SemanticResultKey(ResourceStrings.actionKey, logChoices));

            logWordGrammar = new Grammar(gb);
            logWordGrammar.Name = logWordGr;
            logWordGrammar.Priority = 127;
            recognitionEngine.LoadGrammar(logWordGrammar);
        }

        public void UnloadLogWordGrammar()
        {
            recognitionEngine.UnloadGrammar(logWordGrammar);
        }

        public void LoadWordsGrammar()
        {
            Debug.WriteLine("Load Words Practice Grammar", "prototype");
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = culture;

            Choices wordChoices = new Choices();
            foreach (Word w in data.Words)
            {
                wordChoices.Add(w.JWord);
            }
            gb.Append(wordChoices);

            wordsGrammar = new Grammar(gb);
            wordsGrammar.Name = wordsPracticeGr;
            wordsGrammar.Priority = 126;
            recognitionEngine.LoadGrammar(wordsGrammar);
        }

        public void UnloadWordsGrammar()
        {
            recognitionEngine.UnloadGrammar(wordsGrammar);
        }

        #endregion

        #endregion

        #region Recognize Speech

        void recognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            data.CurrentComand = ": " + e.Result.Text;
            Debug.WriteLine(e.Result.Grammar.Name + " Recognized");

            switch (e.Result.Grammar.Name)
            {
                case windowOperationsGr : ComputeWindowResult(e.Result); break;
                case chooseLessonsGr    : ComputeChooseLessonResult(e.Result); break;
                case beginLessonsGr     : speechCommandExecuter.ExecuteCommand(ECommand.beginExercise); break;
                case nextItemGr         : speechCommandExecuter.ExecuteCommand(ECommand.skipItem); break;
                case showAnswerGr       : speechCommandExecuter.ExecuteCommand(ECommand.showAnswer); break;
                //case chooseParticleGr: speechCommandExecuter.ExecuteCommand(ECommand.setAnswer, Convert.ToInt32(e.Result.Semantics[ResourceStrings.particleKey].Value as String)); break;
                case chooseParticleGr   : speechCommandExecuter.ExecuteCommand(ECommand.setAnswer, e.Result.Words[0].Text); break;
                case logWordGr          : ComputeLogWordResult(e.Result); break;
                case wordsPracticeGr    : speechCommandExecuter.ExecuteCommand(ECommand.setAnswer, e.Result.Text); break;
                case dictGr             : speechCommandExecuter.ExecuteCommand(ECommand.setAnswer, e.Result.Text); break;
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
                    case ENouns.grammarExercise: windowCtrl.ChangeWindowContent(EContentType.chooseSentenceSetsContent); break;
                    case ENouns.grammarExplanation: windowCtrl.ChangeWindowContent(EContentType.grammarExplanationContent); break;
                    case ENouns.menu: windowCtrl.ChangeWindowContent(EContentType.mainMenuContent); break;
                    case ENouns.options: windowCtrl.ChangeWindowContent(EContentType.settingsContent); break;
                    case ENouns.wordsExercise: windowCtrl.ChangeWindowContent(EContentType.chooseWordSetsContent); break;
                }
                return;
            }

            if (action == EVerbs.goTo)
            {
                switch (noun)
                {
                    case ENouns.back                : windowCtrl.GoBack(); break;
                    case ENouns.grammarExercise     : windowCtrl.ChangeWindowContent(EContentType.chooseSentenceSetsContent); break;
                    case ENouns.wordsExercise       : windowCtrl.ChangeWindowContent(EContentType.chooseWordSetsContent); break;
                    case ENouns.grammarExplanation  : windowCtrl.ChangeWindowContent(EContentType.grammarExplanationContent); break;
                    case ENouns.menu                : windowCtrl.ChangeWindowContent(EContentType.mainMenuContent); break;
                    case ENouns.options             : windowCtrl.ChangeWindowContent(EContentType.settingsContent); break;
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

        private void Exersice(ENouns noun)
        {
            if (noun == ENouns.grammarExercise)
            {
                if (windowCtrl.CurrentContentType != EContentType.chooseSentenceSetsContent)
                {
                    windowCtrl.ChangeWindowContent(EContentType.chooseSentenceSetsContent);
                }
                else
                {
                    windowCtrl.ChangeWindowContent(EContentType.grammarPracticeContent);
                }
                return;
            }
            if (noun == ENouns.wordsExercise)
            {
                if (windowCtrl.CurrentContentType != EContentType.chooseWordSetsContent)
                {
                    windowCtrl.ChangeWindowContent(EContentType.chooseWordSetsContent);
                }
                else
                {
                    windowCtrl.ChangeWindowContent(EContentType.wordsPracticeContent);
                }
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
#if japaneseVersion
                int from = extractNumber(result.Words[0].Text) - 1;
                int to = extractNumber(result.Words[2].Text) - 1;
#else
                int from = extractNumber(result.Words[1].Text) - 1;
                int to = extractNumber(result.Words[3].Text) - 1;
#endif
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
#if japaneseVersion
            char[] lesson = text.ToCharArray();
            String number = new String(lesson, 1, lesson.Length - 2);
#else
            String number = text.Substring(7, text.Length - 7);
#endif
            return Convert.ToInt32(number);

        }

        private void ComputeLogWordResult(RecognitionResult result)
        {
            EVerbs action = (EVerbs)Enum.Parse(typeof(EVerbs), result.Semantics[ResourceStrings.actionKey].Value as String);
            switch (action)
            {
                case EVerbs.select: speechCommandExecuter.ExecuteCommand(ECommand.logAnswer, result.Text); break;
                case EVerbs.deselect: speechCommandExecuter.ExecuteCommand(ECommand.unlogAnswer, result.Text); break;
            }
        }

        #endregion

        #endregion
    }
}
