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

        string Filepath;
        string[] Lines;
        string[] Clean;
        string[] Dataset;

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

        public string[] Prepare()
        {
            this.Preprocess(asEnumerable: false, token: false);

            Dictionary<string, int> data = new Dictionary<string, int>();

            foreach (string line in this.Clean)
            {
                foreach (string word in line.Split(" ") {
                    if (data.Contains(word))
                    {

                    }
                }
            }

            return this.Dataset;
        }
    }
}
