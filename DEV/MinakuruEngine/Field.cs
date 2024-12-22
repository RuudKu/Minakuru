namespace Engine;

public class Field
{
	public const byte ColumnAFieldNo = 0;
	public const byte ColumnBFieldNo = 1;
	public const byte ColumnCFieldNo = 2;
	public const byte ColumnDFieldNo = 3;
	public const byte ColumnEFieldNo = 4;
	public const byte ColumnFFieldNo = 5;
	public const byte ColumnGFieldNo = 6;
	public const byte ColumnHFieldNo = 7;

	public const byte Row1FieldNo = 0 * 8;
	public const byte Row2FieldNo = 1 * 8;
	public const byte Row3FieldNo = 2 * 8;
	public const byte Row4FieldNo = 3 * 8;
	public const byte Row5FieldNo = 4 * 8;
	public const byte Row6FieldNo = 5 * 8;
	public const byte Row7FieldNo = 6 * 8;
	public const byte Row8FieldNo = 7 * 8;

	public const byte A1FieldNo = ColumnAFieldNo + Row1FieldNo;
	public const byte B1FieldNo = 1;
	public const byte H1FieldNo = 7;
	public const byte A2FieldNo = 1 * 8;
	public const byte H2FieldNo = 1 * 8 + 7;
	public const byte A7FieldNo = 6 * 8;
	public const byte H7FieldNo = 6 * 8 + 7;
	public const byte A8FieldNo = 7 * 8;
	public const byte H8FieldNo = 7 * 8 + 7;

	public const ulong A1Filter = ((ulong)1) << A1FieldNo;
	public const ulong B1Filter = ((ulong)1) << B1FieldNo;
	public const ulong H1Filter = ((ulong)1) << H1FieldNo;
	public const ulong A2Filter = ((ulong)1) << A2FieldNo;
	public const ulong H2Filter = ((ulong)1) << H2FieldNo;
	public const ulong A7Filter = ((ulong)1) << A7FieldNo;
	public const ulong H7Filter = ((ulong)1) << H7FieldNo;
	public const ulong A8Filter = ((ulong)1) << A8FieldNo;
	public const ulong H8Filter = ((ulong)1) << H8FieldNo;

}
