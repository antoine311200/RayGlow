using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Word_Representation.Preprocess
{
    internal class SkipGramDataset
    {
        public static string Extension = ".txt";

        int WindowSize;

        string Filepath;
        string[] Lines;
        string[] Clean;

        List<(string, string[])> Dataset;
        Dictionary<string, int> Vocabulary = new Dictionary<string, int>();

        public SkipGramDataset(string filepath, string extension = ".txt")
        {
            this.Filepath = filepath;

            if (extension != SkipGramDataset.Extension)
            {
                SkipGramDataset.Extension = extension;
            }
        }

        public void Load()
        { 
            if (this.Filepath.Substring(this.Filepath.IndexOf('.')) != SkipGramDataset.Extension)
            {
                throw new Exception($"The extension of the SkipGram corpus provided does not match extension {SkipGramDataset.Extension}");
            }

            if (!File.Exists(this.Filepath))
            {
                throw new Exception("Such file does not exist. Please check if you have used the correct path.");
            }
            
            string text = System.IO.File.ReadAllText(this.Filepath);
            this.Lines = text.Split(".");

        }

        public dynamic Preprocess(bool asEnumerable, bool token)
        {
            System.Collections.Generic.IEnumerable<string> stripGenerator = CleanText.Strip(corpus: this.Lines, token: token);
            if (asEnumerable)
            {
                return stripGenerator;
            }
            else
            {
                this.Clean = stripGenerator.ToArray();
                return this.Clean;
            }
        }

        public List<(string, string[])> Prepare(int windowSize)
        {
            this.WindowSize = windowSize;

            this.Preprocess(asEnumerable: false, token: false);

            Dictionary<string, int> data = new Dictionary<string, int>();

            foreach (string line in this.Clean)
            {
                foreach (string word in line.Split(" ")) {
                    string trim = word.Trim();

                    if (trim != " " && trim != "")
                    {
                        if (!data.ContainsKey(trim))
                        {
                            data.Add(trim, 1);
                        }
                        else
                        {
                            data[trim] += 1;
                        }
                    }
                }
            }

            var vocabularySize = data.Count;

            IEnumerable<string> items = from pair in data
                                        orderby pair.Value ascending
                                        select pair.Key;
            

            int index = 0;
            foreach (string item in items)
            {
                this.Vocabulary[item] = index;
                index++;
            }

            foreach (string sentence in this.Clean)
            {
                var i = 0;
                string[] splitSentence = sentence.Split(" ");
                foreach (string word in splitSentence)
                {
                    IEnumerable<int> centerWordEnum = from number in Enumerable.Range(0, vocabularySize) select 0;
                    IEnumerable<int> contextWordsEnum = from number in Enumerable.Range(0, vocabularySize) select 0;
                    
                    int[] centerWord = centerWordEnum.ToArray();
                    int[] contextWords = contextWordsEnum.ToArray();

                    centerWord[this.Vocabulary[word]] = 1;

                    foreach (int j in Enumerable.Range(index-this.WindowSize, index+this.WindowSize))
                    {
                        if (i != j && j >= 0 && j < splitSentence.Length)
                        {
                            contextWords[this.Vocabulary[splitSentence[j]]] += 1;
                        }
                    }



                    i++;
                }
            }

            return this.Dataset;
        }
    }
}
