using BenchmarkDotNet.Attributes;
using Baseline = Minakuru.Engine.MoveGenerators;
using Experimental = Minakuru.Engine.MoveGenerators;

namespace Minakuru.Engine.Benchmarks.MoveGenerators;

[HtmlExporter]
public class KingMoveGeneratorBenchmark
{
	private readonly Baseline.IMoveGenerator moveGeneratorBaseline = new Baseline.KingMoveGenerator();
	private readonly Baseline.IMoveGenerator moveGeneratorExperimental = new Experimental.KingMoveGenerator();

	private const string Fen1 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
	private const string Fen2 = "k7/8/5q2/8/3B4/8/8/6K1 w - - 0 1";

	readonly MoveList baselineResult = [];
	readonly MoveList experimentalResult = [];

	string fenCode;

	[Params("Start w", "Start b")]
	public string FenCode
	{
		get { return fenCode; }
		set
		{
			fenCode = value;
			var fen = "";
			switch (value)
			{
				case "Start w":
					fen = Fen1;
					break;
				case "Start b":
					fen = Fen2;
					break;
			}

			Board = fen.ToBoard();
		}
	}

	public Board Board { get; set; }

	//[Benchmark(Baseline = true)]
	public void BaselineMoves()
	{
		moveGeneratorBaseline.GenerateMove(Board, baselineResult);
	}

	// [Benchmark]
	public void ExperimentalMoves()
	{
		moveGeneratorExperimental.GenerateMove(Board, experimentalResult);
	}
}
