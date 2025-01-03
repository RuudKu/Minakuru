

namespace Minakuru.UciServer;

public class UciServer(Stream InputStream, Stream OutputStream)
{
	private readonly TextReader _textReader = new StreamReader(InputStream);
	private readonly TextWriter _textWriter = new StreamWriter(OutputStream);

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
		await _textWriter.WriteAsync(line.ToCharArray());
		await _textWriter.WriteAsync('\n');
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
			return true;
		}
		return false;
	}

	private async Task<bool> UciNewGameCommandProcessorAsync(string line)
	{
		if ("ucinewgame".Equals(line, StringComparison.OrdinalIgnoreCase))
		{
			await WriteAsync("isready");
			return true;
		}
		return false;
	}
	private async Task<bool> PositionCommandProcessorAsync(string line)
	{
		if (line.StartsWith("position ", StringComparison.OrdinalIgnoreCase))
		{
			await WriteAsync("posit");
			return true;
		}
		return false;
	}

	private async Task<bool> GoCommandProcessorAsync(string line)
	{
		if (line.StartsWith("go ", StringComparison.OrdinalIgnoreCase))
		{
			await WriteAsync("gogo");
			return true;
		}
		return false;
	}

	private async Task<bool> StopCommandProcessorAsync(string line)
	{
		if ("stop".Equals(line, StringComparison.OrdinalIgnoreCase))
		{
			await WriteAsync("string stopped");
			return true;
		}
		return false;
	}

	private async Task<bool> UnknownCommandProcessorAsync(string line)
	{
		await WriteAsync("string UNKNOWN: " + line);
		return true;
	}
}
