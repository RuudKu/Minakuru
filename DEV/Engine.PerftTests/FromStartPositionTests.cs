using System.Diagnostics;
using FluentAssertions;
using Minakuru.Engine.MoveGenerators;

namespace Minakuru.Engine.PerftTests;

[TestClass]
public sealed class FromStartPositionTests
{
	[DataRow(1, 20UL, DisplayName = "Depth 1")]
	[DataRow(2, 400UL, DisplayName = "Depth 2")]
	[DataRow(3, 8_902UL, DisplayName = "Depth 3")]
	// [DataRow(4, 197_281UL, DisplayName = "Depth 4")]
	// [DataRow(5, 4_865_609UL, DisplayName = "Depth 5")]
	[DataTestMethod]
	public void PerftStartpositionTests(int depth, ulong expectedNodes)
	{
		var board = Board.Init();

		var actual = PerftHelper.CountNodes(board, depth);

		actual.Should().Be(expectedNodes);
	}

	[DataRow(1, 48UL, DisplayName = "Depth 1")]
	[DataRow(2, 2_039UL, DisplayName = "Depth 2")]
	[DataRow(3, 97_862UL, DisplayName = "Depth 3")]
	// [DataRow(4, 4_085_603UL, DisplayName = "Depth 4")]
	[DataTestMethod]
	public void PerftPosition2Tests(int depth, ulong expectedNodes)
	{
		// KiwiPeta
		var fen = "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1";
		var board = fen.ToBoard();

		var actual = PerftHelper.CountNodes(board, depth);

		actual.Should().Be(expectedNodes);
	}

	[DataRow(1, 14UL, DisplayName = "Depth 1")]
	[DataRow(2, 191UL, DisplayName = "Depth 2")]
	[DataRow(3, 2_812UL, DisplayName = "Depth 3")]
	[DataRow(4, 43_238UL, DisplayName = "Depth 4")]
	[DataTestMethod]
	public void PerftPosition3Tests(int depth, ulong expectedNodes)
	{
		var fen = "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 1";
		var board = fen.ToBoard();

		var actual = PerftHelper.CountNodes(board, depth);

		actual.Should().Be(expectedNodes);
	}

	[DataRow("8/2p5/3p4/KP5r/1R3p1k/4P3/6P1/8 b - - 0 1", 15, DisplayName = "After e2-e3")]
	[DataRow("8/2p5/3p4/KP5r/1R2Pp1k/8/6P1/8 b - e3 0 1", 16, DisplayName = "After e2-e4")]
	[DataRow("8/2p5/3p4/KP5r/1R3p1k/6P1/4P3/8 b - - 0 1", 4, DisplayName = "After g2-g3")]
	[DataRow("8/2p5/3p4/KP5r/1R3pPk/8/4P3/8 b - g3 0 1", 17, DisplayName = "After g2-g4")]
	[DataRow("8/2p5/3p4/KP5r/R4p1k/8/4P1P1/8 b - - 1 1", 15, DisplayName = "After Rb4-a4")]
	[DataRow("8/2p5/3p4/KP5r/5p1k/8/4P1P1/1R6 b - - 1 1", 16, DisplayName = "After Rb4-b1")]
	[DataRow("8/2p5/3p4/KP5r/5p1k/8/1R2P1P1/8 b - - 1 1", 16, DisplayName = "After Rb4-b2")]
	[DataRow("8/2p5/3p4/KP5r/5p1k/1R6/4P1P1/8 b - - 1 1", 15, DisplayName = "After Rb4-b3")]
	[DataRow("8/2p5/3p4/KP5r/2R2p1k/8/4P1P1/8 b - - 1 1", 15, DisplayName = "After Rb4-c4")]
	[DataRow("8/2p5/3p4/KP5r/3R1p1k/8/4P1P1/8 b - - 1 1", 15, DisplayName = "After Rb4-d4")]
	[DataRow("8/2p5/3p4/KP5r/4Rp1k/8/4P1P1/8 b - - 1 1", 15, DisplayName = "After Rb4-e4")]
	[DataRow("8/2p5/3p4/KP5r/5R1k/8/4P1P1/8 b - - 0 1", 2, DisplayName = "After Rb4xf4+")]
	[DataRow("8/2p5/3p4/1P5r/KR3p1k/8/4P1P1/8 b - - 1 1", 15, DisplayName = "After Ka5-a4")]
	[DataRow("8/2p5/K2p4/1P5r/1R3p1k/8/4P1P1/8 b - - 1 1", 15, DisplayName = "After Ka5-a6")]
	[DataTestMethod]
	public void PerftPosition3DividedTests(string fen, int expectedNodes)
	{
		var board = fen.ToBoard();

		var legalMoves = PerftHelper.LegalMovesGenerator.GenerateMove(board).Select(PerftHelper.ToString).ToArray();

		var actual = legalMoves.Length;

		actual.Should().Be(expectedNodes);
	}

	[DataRow(1, 6UL, DisplayName = "Depth 1")]
	[DataRow(2, 264UL, DisplayName = "Depth 2")]
	[DataRow(3, 9_467UL, DisplayName = "Depth 3")]
	// [DataRow(4, 4_085_603UL, DisplayName = "Depth 4")]
	[DataTestMethod]
	public void PerftPosition4WhiteTests(int depth, ulong expectedNodes)
	{
		var fen = "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1";
		var board = fen.ToBoard();

		var actual = PerftHelper.CountNodes(board, depth);

		actual.Should().Be(expectedNodes);
	}

	[DataRow(1, 6UL, DisplayName = "Depth 1")]
	[DataRow(2, 264UL, DisplayName = "Depth 2")]
	// [DataRow(3, 9_467UL, DisplayName = "Depth 3")]
	// [DataRow(4, 4_085_603UL, DisplayName = "Depth 4")]
	[DataTestMethod]
	public void PerftPosition4BlackTests(int depth, ulong expectedNodes)
	{
		var fen = "r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1";
		var board = fen.ToBoard();

		var actual = PerftHelper.CountNodes(board, depth);

		actual.Should().Be(expectedNodes);
	}

	[DataRow(1, 44UL, DisplayName = "Depth 1")]
	[DataRow(2, 1_486UL, DisplayName = "Depth 2")]
	// [DataRow(3, 62_379UL, DisplayName = "Depth 3")]
	// [DataRow(4, 4_085_603UL, DisplayName = "Depth 4")]
	[DataTestMethod]
	public void PerftPosition5Tests(int depth, ulong expectedNodes)
	{
		var fen = "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8";
		var board = fen.ToBoard();

		var actual = PerftHelper.CountNodes(board, depth);

		actual.Should().Be(expectedNodes);
	}

	[DataRow(1, 46UL, DisplayName = "Depth 1")]
	// [DataRow(2, 2_079UL, DisplayName = "Depth 2")]
	// [DataRow(3, 89_890UL, DisplayName = "Depth 3")]
	// [DataRow(4, 4_085_603UL, DisplayName = "Depth 4")]
	[DataTestMethod]
	public void PerftPosition6Tests(int depth, ulong expectedNodes)
	{
		var fen = "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10";
		var board = fen.ToBoard();

		var actual = PerftHelper.CountNodes(board, depth);

		actual.Should().Be(expectedNodes);
	}

	[DataRow(1, 24UL, DisplayName = "Depth 1")]
	[DataRow(2, 496UL, DisplayName = "Depth 2")]
	[DataRow(3, 9_483UL, DisplayName = "Depth 3")]
	[DataRow(4, 182_838UL, DisplayName = "Depth 4")]
	[DataTestMethod]
	public void PerftPosition7WhiteTests(int depth, ulong expectedNodes)
	{
		var fen = "n1n5/PPPk4/8/8/8/8/4Kppp/5N1N w - - 0 1";
		var board = fen.ToBoard();

		var actual = PerftHelper.CountNodes(board, depth);

		actual.Should().Be(expectedNodes);
	}

	[DataRow(1, 24UL, DisplayName = "Depth 1")]
	[DataRow(2, 496UL, DisplayName = "Depth 2")]
	[DataRow(3, 9_483UL, DisplayName = "Depth 3")]
	[DataRow(4, 182_838UL, DisplayName = "Depth 4")]
	[DataTestMethod]
	public void PerftPosition7BlackTests(int depth, ulong expectedNodes)
	{
		var fen = "n1n5/PPPk4/8/8/8/8/4Kppp/5N1N b - - 0 1";
		var board = fen.ToBoard();

		var actual = PerftHelper.CountNodes(board, depth);

		actual.Should().Be(expectedNodes);
	}

	//  n1n5/PPPk4/8/8/8/8/4Kppp/5N1k w - - 0 1

	// [TestMethod]
	public void PerftDivideStartpositionTests()
	{
		var board = Board.Init();
		var legalMovesGenerator = new LegalMovesGenerator();

		var actual = CountDividedNodes(board, 3);

		//a2a3: 181046
		//b2b3: 215255
		//c2c3: 222861
		//d2d3: 328511
		//e2e3: 402988
		//f2f3: 178889
		//g2g3: 217210
		//h2h3: 181044
		//a2a4: 217832
		//b2b4: 216145
		//c2c4: 240082
		//d2d4: 361790
		//e2e4: 405385
		//f2f4: 198473
		//g2g4: 214048
		//h2h4: 218829
		//b1a3: 198572
		//b1c3: 234656
		//g1f3: 233491
		//g1h3: 198502

		var expected = new Dictionary<string, ulong>()
		{
			{"a2a3", 181046UL },
			{"b2b3", 215255UL },
			{"c2c3", 222861UL },
			{"d2d3", 328511UL },
			{"e2e3", 402988UL },
			{"f2f3", 178889UL },
			{"g2g3", 217210UL },
			{"h2h3", 181044UL },
			{"a2a4", 217832UL },
			{"b2b4", 216145UL },
			{"c2c4", 240082UL },
			{"d2d4", 361790UL },
			{"e2e4", 405385UL },
			{"f2f4", 198473UL },
			{"g2g4", 214048UL },
			{"h2h4", 218829UL },
			{"b1a3", 198572UL },
			{"b1c3", 234656UL },
			{"g1f3", 233491UL },
			{"g1h3", 198502UL },
		};

		actual.Should().BeEquivalentTo(expected);


		Dictionary<string, ulong> CountDividedNodes(Board board, int remainingDepth)
		{
			var result = new Dictionary<string, ulong>();

			var legalMoves = legalMovesGenerator.GenerateMove(board).ToArray();

			foreach (var legalMove in legalMoves)
			{
				var newBoard = MoveMaker.MakeMove(board, legalMove);
				var count = PerftHelper.CountNodes(newBoard, remainingDepth - 1);
				var readableMove = PerftHelper.ToString(legalMove);
				result.Add(readableMove, count);
			}
			return result;
		}
	}
}
