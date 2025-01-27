﻿using Minakuru.Engine.Evaluators;
using Minakuru.Engine.MoveGenerators;

namespace Minakuru.Engine.MoveSearchers;

public class AlphaBeta(IEvaluator evaluator, LegalMovesGenerator moveGenerator) : IMoveSearcher
{
	private readonly IEvaluator _evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
	private readonly LegalMovesGenerator _moveGenerator = moveGenerator ?? throw new ArgumentNullException(nameof(moveGenerator));

	public Tuple<Move, int> Search(MoveSearchOptions moveSearchOptions, Board board)
	{
		var allMoves = _moveGenerator.GenerateMove(board);
		int max = int.MinValue;
		var bestMove = Move.NullMove;
		foreach (var move in allMoves)
		{
			var newBoard = MoveMaker.MakeMove(board, move);
			int moveScore = -SearchCore(newBoard, moveSearchOptions.MaxDepth - 1);
			if (moveScore > max)
			{
				max = moveScore;
				bestMove = move;
			}
		}
		return new Tuple<Move, int>(bestMove, max);
	}
	private int SearchCore(Board board, int depth)
	{
		if (depth == 0)
		{
			return _evaluator.Evaluate(board);
		}

		var allMoves = _moveGenerator.GenerateMove(board);
		if (!allMoves.Any())
		{
			return -1 * _evaluator.Evaluate(board);
		}
		int max = int.MinValue;
		foreach (var move in allMoves)
		{
			var newBoard = MoveMaker.MakeMove(board, move);
			int moveScore = -SearchCore(newBoard, depth - 1);
			if (moveScore > max)
			{
				max = moveScore;
			}
		}
		return max;
	}

	private int AlphaBetaMax(Board board, int alpha, int beta, int depthleft)
	{
		if (depthleft == 0) return _evaluator.Evaluate(board);
		var bestValue = -int.MaxValue;
		var allMoves = _moveGenerator.GenerateMove(board);
		foreach (var move in allMoves)
		{
			var score = AlphaBetaMin(board, alpha, beta, depthleft - 1);
			if (score > bestValue)
			{
				bestValue = alpha;
				if (score > alpha)
				{
					alpha = score;
				}
			}
			if (score >= beta)
			{
				return score;
			}
		}
		return bestValue;
	}

	private int AlphaBetaMin(Board board, int alpha, int beta, int depthleft)
	{
		if (depthleft == 0) return -_evaluator.Evaluate(board);
		var bestValue = int.MaxValue;
		var allMoves = _moveGenerator.GenerateMove(board);
		foreach (var move in allMoves)
		{
			var score = AlphaBetaMax(board, alpha, beta, depthleft - 1);
			if (score < bestValue)
			{
				bestValue = score;
				if (score < beta)
				{
					beta = score;
				}
					
			}
			if (score <= alpha)
			{
				return score;
			}
		}
		return bestValue;
	}
}
