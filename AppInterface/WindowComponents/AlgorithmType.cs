using System.Collections.Generic;
using AppInterface.Algorithms;

namespace AppInterface.WindowComponents
{
    readonly struct AlgorithmType
    {
        public static readonly AlgorithmType EmptyInstructions = new AlgorithmType(
            Algorithm.EmptyInstructions,
            "Wstawianie pustych instrukcji",
            false);

        public static readonly AlgorithmType ChangeClassAndMethodNames = new AlgorithmType(
            Algorithm.ChangeClassAndMethodNames,
            "Zmienianie nazw klas i zmiennych na mniej czytelne / przypadkowe",
            true);

        public static readonly AlgorithmType ExtendExpresions = new AlgorithmType(
            Algorithm.ExtendExpresions,
            "Rozszerzanie wyrażeń, zwłaszcza liczbowych",
            false);

        public static readonly AlgorithmType DeadCodeInjection = new AlgorithmType(
            Algorithm.DeadCodeInjection,
            "Wstrzykiwanie martwego kodu",
            true);

        public static readonly AlgorithmType ChangeNumberBase = new AlgorithmType(
            Algorithm.ChangeNumberBase,
            "Zmiana systemu dziesiętnego na inne",
            true);

        public static readonly AlgorithmType ReplaceOperators = new AlgorithmType(
            Algorithm.ReplaceOperators,
            "Zastąpienie literałów liczbowych wyrażeniami bitowymi",
            true);

        public static readonly AlgorithmType CypherComments = new AlgorithmType(
            Algorithm.CypherComments,
            "Szyfrowanie komentarzy", 
            true);

        public static IEnumerable<AlgorithmType> Values
        {
            get
            {
                yield return EmptyInstructions;
                yield return ChangeClassAndMethodNames;
                yield return ExtendExpresions;
                yield return DeadCodeInjection;
                yield return ReplaceOperators;
                yield return ChangeNumberBase;
                yield return CypherComments;
            }
        }

        public Algorithm Algorithm { get; }
        public string Text { get; }
        public bool IsEnabled { get; }

        AlgorithmType(Algorithm algorithm, string text, bool isEnabled) {
            Algorithm = algorithm;
            Text = text;
            IsEnabled = isEnabled;
        } 
    }
}
