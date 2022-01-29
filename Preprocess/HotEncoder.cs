using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Word_Representation.Utils;

namespace Word_Representation.Preprocess
{
    internal class HotEncoder<T>
    {
        int VocabularySize;

        Dictionary<T, int> Lookup;
        List<T> Vocabulary;
        List<int[]> Encoder;
        public HotEncoder(List<T> vocabulary)
        {
            this.Vocabulary = vocabulary;
            this.VocabularySize = this.Vocabulary.Count;
        }

        public void Fit(bool shuffle = false)
        {

            if (shuffle)
            {
                this.Vocabulary = ListExtended<T>.Shuffle(this.Vocabulary);
            }

            int index = 0;
            foreach (T word in Vocabulary)
            {
                this.Lookup[word] = index++;
            }
        }

        public int[] Encode(T word)
        {
            int[] hot = (from number in Enumerable.Range(0, this.VocabularySize) select 0).ToArray();

            hot[this.Lookup[word]] = 1;

            return hot;
        }

        public List<int[]> Encode(T[] words)
        {
            List<int[]> encoded = new List<int[]>();

            foreach (T word in words)
            {
                encoded.Add(this.Encode(word));
            }

            return encoded;
        }

        public T Decode(int[] hot)
        {
            return;
        }

    }
}
