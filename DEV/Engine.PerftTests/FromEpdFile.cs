using System.Diagnostics;
using System.Text.RegularExpressions;
using FluentAssertions;

namespace Minakuru.Engine.PerftTests;

[TestCategory("Perft")]
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

		var epdLines = new List<EpdLine>();
		foreach (string line in epdContent)
		{
			epdLines.Add(new EpdLine(++lineNumber, line));
		}

		Parallel.ForEach(epdLines, epdLine =>
		{
			RunEpd(maxDepth, epdLine);
		});
		Debug.WriteLine("Testing EPD finished");
	}

	private void RunEpd(int maxDepth, EpdLine epdLine)
	{
		var parts = epdLine.Epd.Split(';');
		var fen = parts[0];
		Debug.WriteLine(epdLine.LineNumber + "  Starting testing FEN " + fen);

		Board board = fen.ToBoard();
		foreach (var part in parts.Skip(1))
		{
			Match mat = Regex.Match(part);
			var depth = int.Parse(mat.Groups["depth"].Value);
			var expected = (ulong)long.Parse(mat.Groups["nodes"].Value);

			if (depth <= maxDepth)
			{
				Debug.WriteLine(epdLine.LineNumber + "  Testing on depth = " + depth);
				var actual = PerftHelper.CountNodes(board, depth);
				Debug.WriteLine(epdLine.LineNumber + "  Expected nodes = " + expected);
				Debug.WriteLine(epdLine.LineNumber + "  Actual nodes = " + actual);
				actual.Should().Be(expected);
			}
		}
		Debug.WriteLine(epdLine.LineNumber + "  Done testing FEN " + fen);
	}

	[GeneratedRegex(@"D(?<depth>\d+)\s+(?<nodes>\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
	partial Regex Regex { get; }


	private record EpdLine(int LineNumber, string Epd) { }
}
