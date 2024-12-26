using System.Diagnostics;
using System.Text.RegularExpressions;
using FluentAssertions;

namespace Minakuru.Engine.PerftTests;

[TestClass]
public partial class FromEpdFile
{
	public TestContext TestContext { get; set; }

	[TestMethod]
	public void RunEpdFile()
	{
		var maxDepth = int.Parse(TestContext.Properties["MaxDepth"]?.ToString() ?? "3");

		string[] epdContent = File.ReadAllLines("Files\\PerftTestset.epd");
		Debug.WriteLine("EPD file has " + epdContent.Length + " lines");
		int lineNumber = 0;
		foreach (var epd in epdContent)
		{
			lineNumber++;
			var parts = epd.Split(';');
			var fen = parts[0];
			Debug.WriteLine("Starting testing FEN " + lineNumber + "   " + fen);

			Board board = fen.ToBoard();
			foreach (var part in parts.Skip(1))
			{
				Match mat = Regex.Match(part);
				var depth = int.Parse(mat.Groups["depth"].Value);
				var expected = (ulong)long.Parse(mat.Groups["nodes"].Value);

				if (depth <= maxDepth)
				{
					Debug.WriteLine("Testing on depth = " + depth);
					var actual = PerftHelper.CountNodes(board, depth);
					Debug.WriteLine("Expected nodes = " + expected);
					Debug.WriteLine("Actual nodes = " + actual);
					actual.Should().Be(expected);
				}
			}
			Debug.WriteLine("Done testing FEN " + fen);
		}
		Debug.WriteLine("Testing EPD finished");
	}

	[GeneratedRegex(@"D(?<depth>\d+)\s+(?<nodes>\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
	partial Regex Regex { get; }

}
