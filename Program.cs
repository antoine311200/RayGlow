using Word_Representation.Preprocess;

Console.WriteLine("Hello, World!");

SkipGramDataset dataset = new SkipGramDataset(@"C:\Antoine\Coding\C#\Word Representation\Word Representation\Data\corpus.txt");
dataset.Load();
dataset.Prepare();