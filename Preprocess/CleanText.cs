using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Word_Representation.Preprocess
{
    internal static class CleanText
    {
        public static System.Collections.Generic.IEnumerable<string> Strip(string[] corpus, bool token = false)
        {
            foreach (string line in corpus)
            {
                string cleanedLine = line.ToLower();

                foreach (string word in cleanedLine.Split(" "))
                {
                    if (CleanPattern.Contractions.ContainsKey(word))
                    {
                        cleanedLine.Replace(word, CleanPattern.Contractions[word]);
                    }
                }

                cleanedLine = Regex.Replace(cleanedLine, "\\b" + string.Join("\\b|\\b", CleanPattern.StopWords) + "\\b", "");

                cleanedLine = CleanPattern.rgSpecialCharacter.Replace(cleanedLine, " ");

                cleanedLine = CleanPattern.rgRecurrentUnderscore.Replace(cleanedLine, " ");
                cleanedLine = CleanPattern.rgRecurrentMinus.Replace(cleanedLine, " ");
                cleanedLine = CleanPattern.rgRecurrentTilde.Replace(cleanedLine, " ");
                cleanedLine = CleanPattern.rgRecurrentPlus.Replace(cleanedLine, " ");
                cleanedLine = CleanPattern.rgRecurrentPoint.Replace(cleanedLine, " ");

                cleanedLine = CleanPattern.rgBrackets.Replace(cleanedLine, " ");
                cleanedLine = CleanPattern.rgPunctuations.Replace(cleanedLine, " ");
                cleanedLine = CleanPattern.rgMoreSpecialCharacter.Replace(cleanedLine, " ");

                cleanedLine = CleanPattern.rgNumInc.Replace(cleanedLine, "INC_NUM");
                cleanedLine = CleanPattern.rgNumCm.Replace(cleanedLine, "CM_NUM");

                cleanedLine = CleanPattern.rgSpaceBetweenPunctuation.Replace(cleanedLine, " ");

                cleanedLine = CleanPattern.rgEOWPunctuationPoint.Replace(cleanedLine, " ");
                cleanedLine = CleanPattern.rgEOWPunctuationMinus.Replace(cleanedLine, " ");
                cleanedLine = CleanPattern.rgEOWPunctuationDoublePoint.Replace(cleanedLine, " ");
                cleanedLine = CleanPattern.rgEOWBPunctuationPoint.Replace(cleanedLine, " ");
                cleanedLine = CleanPattern.rgEOWBPunctuationMinus.Replace(cleanedLine, " ");
                cleanedLine = CleanPattern.rgEOWBPunctuationDoublePoint.Replace(cleanedLine, " ");

                cleanedLine = CleanPattern.rgMultipleSpaces.Replace(cleanedLine, " ");
                cleanedLine = CleanPattern.rgSingleBetweenSpaces.Replace(cleanedLine, " ");

                if (token)
                {
                    cleanedLine = cleanedLine.Trim();
                    cleanedLine = "[START] " + cleanedLine + " [END]";
                }

                if (cleanedLine == "") continue;

                yield return cleanedLine;
            }
        }
    }


    internal static class CleanPattern
    {
        public static string SpecialCharacter = @"((\t)|(\t)|(\n))";

        public static Regex rgSpecialCharacter = new Regex(SpecialCharacter);

        public static string RecurrentUnderscore = @"(__+)";
        public static string RecurrentMinus = @"(--+)";
        public static string RecurrentTilde = @"(~~+)";
        public static string RecurrentPlus = @"(\+\++)";
        public static string RecurrentPoint = @"(\.\.+)";

        public static Regex rgRecurrentUnderscore = new Regex(RecurrentUnderscore);
        public static Regex rgRecurrentMinus = new Regex(RecurrentMinus);
        public static Regex rgRecurrentTilde = new Regex(RecurrentTilde);
        public static Regex rgRecurrentPlus = new Regex(RecurrentPlus);
        public static Regex rgRecurrentPoint = new Regex(RecurrentPoint);

        public static string Brackets = @"[<>()\[\]{}]";
        public static string Punctuations = "[,;?!,\'\"*|]";
        public static string MoreSpecialCharacter = "[&©ø]";

        public static Regex rgBrackets = new Regex(Brackets);
        public static Regex rgPunctuations = new Regex(Punctuations);
        public static Regex rgMoreSpecialCharacter = new Regex(MoreSpecialCharacter);

        public static string Mailto = "(mailto,)";
        public static Regex rgMailto = new Regex(Mailto);

        public static string NumInc = @"([iI][nN][cC]\d+)";
        public static string NumCm = @"([cC][mM]\d+)|([cC][hH][gG]\d+)";
        public static Regex rgNumInc = new Regex(NumInc);
        public static Regex rgNumCm = new Regex(NumCm);

        public static string SpaceBetweenPunctuation = @"(\.\s+)";
        public static Regex rgSpaceBetweenPunctuation = new Regex(SpaceBetweenPunctuation);

        public static string EOWPunctuationPoint = @"(\.\s+)";
        public static string EOWPunctuationMinus = @"(\-\s+)";
        public static string EOWPunctuationDoublePoint = @"(\,\s+)";
        public static Regex rgEOWPunctuationPoint = new Regex(EOWPunctuationPoint);
        public static Regex rgEOWPunctuationMinus = new Regex(EOWPunctuationMinus);
        public static Regex rgEOWPunctuationDoublePoint = new Regex(EOWPunctuationDoublePoint);

        public static string EOWBPunctuationPoint = @"(\s+\.)";
        public static string EOWBPunctuationMinus = @"(\s+\-)";
        public static string EOWBPunctuationDoublePoint = @"(\s+\,)";
        public static Regex rgEOWBPunctuationPoint = new Regex(EOWBPunctuationPoint);
        public static Regex rgEOWBPunctuationMinus = new Regex(EOWBPunctuationMinus);
        public static Regex rgEOWBPunctuationDoublePoint = new Regex(EOWBPunctuationDoublePoint);

        public static string MultipleSpaces = @"(\s+)";
        public static string SingleBetweenSpaces = @"(\s+.\s+)";
        public static Regex rgMultipleSpaces = new Regex(MultipleSpaces);
        public static Regex rgSingleBetweenSpaces = new Regex(SingleBetweenSpaces);

        public static string Url = @"((https*,\/*)([^\/\s]+))(.[^\s]+)";
        public static Regex rgUrl = new Regex(Url);


        public static Dictionary<string, string> Contractions = new Dictionary<string, string>() {
            {"ain't", "is not" },
            {"aren't", "are not"},
            {"can't", "cannot"},
            {"'cause", "because"},
            {"could've", "could have"},
            {"couldn't", "could not"},
            {"didn't", "did not"},
            {"doesn't", "does not"},
            {"don't", "do not"},
            {"hadn't", "had not"},
            {"hasn't", "has not"},
            {"haven't", "have not"},
            {"he'd", "he would"},
            {"he'll", "he will"},
            {"he's", "he is"},
            {"how'd", "how did"},
            {"how'd'y", "how do you"},
            {"how'll", "how will"},
            {"how's", "how is"},
            {"I'd", "I would"},
            {"I'd've", "I would have"},
            {"I'll", "I will"},
            {"I'll've", "I will have"},
            {"I'm", "I am"},
            { "I've", "I have"},
            {"i'd", "i would"},
            {"i'd've", "i would have"},
            {"i'll", "i will"},
            {"i'll've", "i will have"},
            {"i'm", "i am"},
            {"i've", "i have"},
            {"isn't", "is not"},
            {"it'd", "it would"},
            {"it'd've", "it would have"},
            {"it'll", "it will"},
            {"it'll've", "it will have"},
            {"it's", "it is"},
            {"let's", "let us"},
            {"ma'am", "madam"},
            {"mayn't", "may not"},
            {"might've", "might have"},
            {"mightn't", "might not"},
            {"mightn't've", "might not have"},
            {"must've", "must have"},
            {"mustn't", "must not"},
            {"mustn't've", "must not have"},
            {"needn't", "need not"},
            {"needn't've", "need not have"},
            { "o'clock", "of the clock" },
            {"oughtn't", "ought not"},
            {"oughtn't've", "ought not have"},
            {"shan't", "shall not"},
            {"sha'n't", "shall not"},
            {"shan't've", "shall not have"},
            {"she'd", "she would"},
            {"she'd've", "she would have"},
            {"she'll", "she will"},
            {"she'll've", "she will have"},
            {"she's", "she is"},
            {"should've", "should have"},
            {"shouldn't", "should not"},
            {"shouldn't've", "should not have"},
            {"so've", "so have"},
            {"so's", "so as"},
            {"this's", "this is"},
            {"that'd", "that would"},
            {"that'd've", "that would have"},
            {"that's", "that is"},
            {"there'd", "there would"},
            {"there'd've", "there would have"},
            {"there's", "there is"},
            {"here's", "here is"},
            {"they'd", "they would"},
            {"they'd've", "they would have"},
            {"they'll", "they will"},
            {"they'll've", "they will have"},
            {"they're", "they are"},
            {"they've", "they have"},
            {"to've", "to have"},
            {"wasn't", "was not"},
            {"we'd", "we would"},
            {"we'd've", "we would have"},
            {"we'll", "we will"},
            {"we'll've", "we will have"},
            {"we're", "we are"},
            {"we've", "we have"},
            {"weren't", "were not"},
            {"what'll", "what will"},
            {"what'll've", "what will have"},
            {"what're", "what are"},
            {"what's", "what is"},
            {"what've", "what have"},
            {"when's", "when is"},
            {"when've", "when have"},
            {"where'd", "where did"},
            {"where's", "where is"},
            {"where've", "where have"},
            {"who'll", "who will"},
            {"who'll've", "who will have"},
            {"who's", "who is"},
            {"who've", "who have"},
            {"why's", "why is"},
            {"why've", "why have"},
            {"will've", "will have"},
            {"won't", "will not"},
            {"won't've", "will not have"},
            {"would've", "would have"},
            {"wouldn't", "would not"},
            {"wouldn't've", "would not have"},
            {"y'all", "you all"},
            {"y'all'd", "you all would"},
            {"y'all'd've", "you all would have"},
            {"y'all're", "you all are"},
            {"y'all've", "you all have"},
            {"you'd", "you would"},
            {"you'd've", "you would have"},
            {"you'll", "you will"},
            {"you'll've", "you will have"},
            {"you're", "you are"},
            {"you've", "you have" },
        };

        public static string[] StopWords = new string[] {
            "i",
            "me",
            "my",
            "myself",
            "we",
            "our",
            "ours",
            "ourselves",
            "you",
            "your",
            "yours",
            "yourself",
            "yourselves",
            "he",
            "him",
            "his",
            "himself",
            "she",
            "her",
            "hers",
            "herself",
            "it",
            "its",
            "itself",
            "they",
            "them",
            "their",
            "theirs",
            "themselves",
            "what",
            "which",
            "who",
            "whom",
            "this",
            "that",
            "these",
            "those",
            "am",
            "is",
            "are",
            "was",
            "were",
            "be",
            "been",
            "being",
            "have",
            "has",
            "had",
            "having",
            "do",
            "does",
            "did",
            "doing",
            "a",
            "an",
            "the",
            "and",
            "but",
            "if",
            "or",
            "because",
            "as",
            "until",
            "while",
            "of",
            "at",
            "by",
            "for",
            "with",
            "about",
            "against",
            "between",
            "into",
            "through",
            "during",
            "before",
            "after",
            "above",
            "below",
            "to",
            "from",
            "up",
            "down",
            "in",
            "out",
            "on",
            "off",
            "over",
            "under",
            "again",
            "further",
            "then",
            "once",
            "here",
            "there",
            "when",
            "where",
            "why",
            "how",
            "all",
            "any",
            "both",
            "each",
            "few",
            "more",
            "most",
            "other",
            "some",
            "such",
            "no",
            "nor",
            "not",
            "only",
            "own",
            "same",
            "so",
            "than",
            "too",
            "very",
            "s",
            "t",
            "can",
            "will",
            "just",
            "don",
            "should",
            "now",
        };
    }
}
