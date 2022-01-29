// See https://aka.ms/new-console-template for more information

using Word_Representation.Preprocess;

Console.WriteLine("Hello, World!");

SkipGramDataset dataset = new SkipGramDataset(@"C:\Antoine\Coding\C#\Word Representation\Word Representation\Data\corpus.txt");
dataset.Load();
var enumerable = dataset.Prepare(asEnumerable: true, token: true);

foreach (var line in enumerable)
{
    Console.WriteLine(">>>" + line);
}
