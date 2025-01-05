

using System.ComponentModel.DataAnnotations;
using Minakuru.Engine;
using Minakuru.Engine.MoveGenerators;

namespace Minakuru.UciServer;

public class UciServer(Stream InputStream, Stream OutputStream)
{
	private readonly TextReader _textReader = new StreamReader(InputStream);
	private readonly TextWriter _textWriter = new StreamWriter(OutputStream);
	private GameState _gameState = new();

	public async Task RunAsync()
	{
		while (true)
		{
			string? line = await _textReader.ReadLineAsync();
			if (line == null)
			{
				await Task.Delay(50);
			}
			else
			{
				if ("quit".Equals(line, StringComparison.OrdinalIgnoreCase))
				{
					break;
				}
				await ProcessAsync(line!);
			}
		}
	}

	private async Task WriteAsync(string line)
	{
		await _textWriter.WriteLineAsync(line);
		await _textWriter.FlushAsync();
	}

	private async Task WriteInfoAsync(string line)
	{
		await _textWriter.WriteLineAsync("string " + line);
		await _textWriter.FlushAsync();
	}

	private async Task ProcessAsync(string line)
	{
		Func<string, Task<bool>>[] commandProcessorFuncs =
		[
			UciCommandProcessorAsync,
			UciNewGameCommandProcessorAsync,
			PositionCommandProcessorAsync,
			GoCommandProcessorAsync,
			IsReadyCommandProcessorAsync,
			StopCommandProcessorAsync,
			UnknownCommandProcessorAsync // last one
		];

		foreach (var commandProcessorFunc in commandProcessorFuncs)
		{
			if (await commandProcessorFunc(line))
			{
				break;
			}
		}
	}

	private async Task<bool> UciCommandProcessorAsync(string line)
	{
		if ("uci".Equals(line, StringComparison.OrdinalIgnoreCase))
		{
			await WriteAsync("id Minakuru");
			await WriteAsync("name Ruud Kuchler");
			return true;
		}
		return false;
	}

	private async Task<bool> UciNewGameCommandProcessorAsync(string line)
	{
		if ("ucinewgame".Equals(line, StringComparison.OrdinalIgnoreCase))
		{
			_gameState = new();
			await WriteAsync("isready");
			return true;
		}
		return false;
	}
	private async Task<bool> PositionCommandProcessorAsync(string line)
	{
		if (line.StartsWith("position ", StringComparison.OrdinalIgnoreCase))
		{
			await ProcessUciFenAsync(line);

			await WriteInfoAsync("position was processed");
			return true;
		}
		return false;
	}

	private async Task ProcessUciFenAsync(string line)
	{
		var parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

		Board board;
		string[] moveParts;

		switch (parts[1])
		{
			case "startpos":
				board = Board.Init();
				moveParts = parts.Skip(2).ToArray();
				break;
			case "fen":
				var fen = string.Join(' ', parts.Skip(2).Take(6));
				board = fen.ToBoard();
				moveParts = parts.Skip(8).ToArray();
				break;
			default:
				throw new EngineException();
		}
	
		var legalMovesGenerator = MoveGeneratorFactory.Create();

		foreach (var move in moveParts)
		{
			MoveList legalMovesList = legalMovesGenerator.GenerateMove(board);
			var allegedMove = UciMoveToMove(move);
			var actualMove = legalMovesList.Single(m => m.From == allegedMove.From && m.To == allegedMove.To && m.PromotedTo == allegedMove.PromotedTo);
			board = MoveMaker.MakeMove(board, actualMove);
		}

		_gameState.Board = board;

		var currentFen = board.ToFen();

		await WriteInfoAsync("Current FEN = " + currentFen);

		static Move UciMoveToMove(string move)
		{
			byte fromColumn = (byte)(move[0] - 'a');
			byte fromRow = (byte)(move[1] - '1');
			byte toColumn = (byte)(move[2] - 'a');
			byte toRow = (byte)(move[3] - '1');
			byte fromField = (byte)(fromColumn + (8 * fromRow));
			byte toField = (byte)(toColumn + (8 * toRow));
			string promotedTo = "";
			Piece piece = Piece.None;
			if (move.Length > 4)
			{
				promotedTo = move.Substring(4);
				piece = promotedTo switch
				{
					"q" => Piece.Queen,
					"r" => Piece.Rook,
					"b" => Piece.Bishop,
					"n" => Piece.Knight,
					_ => throw new NotImplementedException()
				};
			}
			return new Move(fromField, toField, false, piece);
		}

	}

	private async Task<bool> GoCommandProcessorAsync(string line)
	{
		if (line.StartsWith("go ", StringComparison.OrdinalIgnoreCase))
		{
			await WriteInfoAsync("go received");
			return true;
		}
		return false;
	}

	private async Task<bool> StopCommandProcessorAsync(string line)
	{
		if ("stop".Equals(line, StringComparison.OrdinalIgnoreCase))
		{
			await WriteInfoAsync("stop received");
			return true;
		}
		return false;
	}

	private async Task<bool> IsReadyCommandProcessorAsync(string line)
	{
		if ("isready".Equals(line, StringComparison.OrdinalIgnoreCase))
		{
			await WriteAsync("readyok");
			return true;
		}
		return false;
	}

	private async Task<bool> UnknownCommandProcessorAsync(string line)
	{
		await WriteInfoAsync("UNKNOWN: " + line);
		return true;
	}
}
