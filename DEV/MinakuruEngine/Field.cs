namespace Minakuru.Engine;

public static class Field
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
	public const byte B1FieldNo = ColumnBFieldNo + Row1FieldNo;
	public const byte C1FieldNo = ColumnCFieldNo + Row1FieldNo;
	public const byte D1FieldNo = ColumnDFieldNo + Row1FieldNo;
	public const byte E1FieldNo = ColumnEFieldNo + Row1FieldNo;
	public const byte F1FieldNo = ColumnFFieldNo + Row1FieldNo;
	public const byte G1FieldNo = ColumnGFieldNo + Row1FieldNo;
	public const byte H1FieldNo = ColumnHFieldNo + Row1FieldNo;

	public const byte A2FieldNo = ColumnAFieldNo + Row2FieldNo;
	public const byte B2FieldNo = ColumnBFieldNo + Row2FieldNo;
	public const byte C2FieldNo = ColumnCFieldNo + Row2FieldNo;
	public const byte D2FieldNo = ColumnDFieldNo + Row2FieldNo;
	public const byte E2FieldNo = ColumnEFieldNo + Row2FieldNo;
	public const byte F2FieldNo = ColumnFFieldNo + Row2FieldNo;
	public const byte G2FieldNo = ColumnGFieldNo + Row2FieldNo;
	public const byte H2FieldNo = ColumnHFieldNo + Row2FieldNo;

	public const byte A3FieldNo = ColumnAFieldNo + Row3FieldNo;
	public const byte B3FieldNo = ColumnBFieldNo + Row3FieldNo;
	public const byte C3FieldNo = ColumnCFieldNo + Row3FieldNo;
	public const byte D3FieldNo = ColumnDFieldNo + Row3FieldNo;
	public const byte E3FieldNo = ColumnEFieldNo + Row3FieldNo;
	public const byte F3FieldNo = ColumnFFieldNo + Row3FieldNo;
	public const byte G3FieldNo = ColumnGFieldNo + Row3FieldNo;
	public const byte H3FieldNo = ColumnHFieldNo + Row3FieldNo;

	public const byte A4FieldNo = ColumnAFieldNo + Row4FieldNo;
	public const byte B4FieldNo = ColumnBFieldNo + Row4FieldNo;
	public const byte C4FieldNo = ColumnCFieldNo + Row4FieldNo;
	public const byte D4FieldNo = ColumnDFieldNo + Row4FieldNo;
	public const byte E4FieldNo = ColumnEFieldNo + Row4FieldNo;
	public const byte F4FieldNo = ColumnFFieldNo + Row4FieldNo;
	public const byte G4FieldNo = ColumnGFieldNo + Row4FieldNo;
	public const byte H4FieldNo = ColumnHFieldNo + Row4FieldNo;

	public const byte A5FieldNo = ColumnAFieldNo + Row5FieldNo;
	public const byte B5FieldNo = ColumnBFieldNo + Row5FieldNo;
	public const byte C5FieldNo = ColumnCFieldNo + Row5FieldNo;
	public const byte D5FieldNo = ColumnDFieldNo + Row5FieldNo;
	public const byte E5FieldNo = ColumnEFieldNo + Row5FieldNo;
	public const byte F5FieldNo = ColumnFFieldNo + Row5FieldNo;
	public const byte G5FieldNo = ColumnGFieldNo + Row5FieldNo;
	public const byte H5FieldNo = ColumnHFieldNo + Row5FieldNo;

	public const byte A6FieldNo = ColumnAFieldNo + Row6FieldNo;
	public const byte B6FieldNo = ColumnBFieldNo + Row6FieldNo;
	public const byte C6FieldNo = ColumnCFieldNo + Row6FieldNo;
	public const byte D6FieldNo = ColumnDFieldNo + Row6FieldNo;
	public const byte E6FieldNo = ColumnEFieldNo + Row6FieldNo;
	public const byte F6FieldNo = ColumnFFieldNo + Row6FieldNo;
	public const byte G6FieldNo = ColumnGFieldNo + Row6FieldNo;
	public const byte H6FieldNo = ColumnHFieldNo + Row6FieldNo;

	public const byte A7FieldNo = ColumnAFieldNo + Row7FieldNo;
	public const byte B7FieldNo = ColumnBFieldNo + Row7FieldNo;
	public const byte C7FieldNo = ColumnCFieldNo + Row7FieldNo;
	public const byte D7FieldNo = ColumnDFieldNo + Row7FieldNo;
	public const byte E7FieldNo = ColumnEFieldNo + Row7FieldNo;
	public const byte F7FieldNo = ColumnFFieldNo + Row7FieldNo;
	public const byte G7FieldNo = ColumnGFieldNo + Row7FieldNo;
	public const byte H7FieldNo = ColumnHFieldNo + Row7FieldNo;

	public const byte A8FieldNo = ColumnAFieldNo + Row8FieldNo;
	public const byte B8FieldNo = ColumnBFieldNo + Row8FieldNo;
	public const byte C8FieldNo = ColumnCFieldNo + Row8FieldNo;
	public const byte D8FieldNo = ColumnDFieldNo + Row8FieldNo;
	public const byte E8FieldNo = ColumnEFieldNo + Row8FieldNo;
	public const byte F8FieldNo = ColumnFFieldNo + Row8FieldNo;
	public const byte G8FieldNo = ColumnGFieldNo + Row8FieldNo;
	public const byte H8FieldNo = ColumnHFieldNo + Row8FieldNo;

	public const ulong A1Filter = (ulong)1 << A1FieldNo;
	public const ulong B1Filter = (ulong)1 << B1FieldNo;
	public const ulong C1Filter = (ulong)1 << C1FieldNo;
	public const ulong D1Filter = (ulong)1 << D2FieldNo;
	public const ulong E1Filter = (ulong)1 << E2FieldNo;
	public const ulong F1Filter = (ulong)1 << F7FieldNo;
	public const ulong G1Filter = (ulong)1 << H7FieldNo;
	public const ulong H1Filter = (ulong)1 << H1FieldNo;

	public const ulong A2Filter = (ulong)1 << A1FieldNo;
	public const ulong B2Filter = (ulong)1 << B2FieldNo;
	public const ulong C2Filter = (ulong)1 << C2FieldNo;
	public const ulong D2Filter = (ulong)1 << D2FieldNo;
	public const ulong E2Filter = (ulong)1 << E2FieldNo;
	public const ulong F2Filter = (ulong)1 << F2FieldNo;
	public const ulong G2Filter = (ulong)1 << H2FieldNo;
	public const ulong H2Filter = (ulong)1 << H2FieldNo;

	public const ulong A3Filter = (ulong)1 << A3FieldNo;
	public const ulong B3Filter = (ulong)1 << B3FieldNo;
	public const ulong C3Filter = (ulong)1 << C3FieldNo;
	public const ulong D3Filter = (ulong)1 << D3FieldNo;
	public const ulong E3Filter = (ulong)1 << E3FieldNo;
	public const ulong F3Filter = (ulong)1 << F3FieldNo;
	public const ulong G3Filter = (ulong)1 << H3FieldNo;
	public const ulong H3Filter = (ulong)1 << H3FieldNo;

	public const ulong A4Filter = (ulong)1 << A4FieldNo;
	public const ulong B4Filter = (ulong)1 << B4FieldNo;
	public const ulong C4Filter = (ulong)1 << C4FieldNo;
	public const ulong D4Filter = (ulong)1 << D4FieldNo;
	public const ulong E4Filter = (ulong)1 << E4FieldNo;
	public const ulong F4Filter = (ulong)1 << F4FieldNo;
	public const ulong G4Filter = (ulong)1 << H4FieldNo;
	public const ulong H4Filter = (ulong)1 << H4FieldNo;

	public const ulong A5Filter = (ulong)1 << A5FieldNo;
	public const ulong B5Filter = (ulong)1 << B5FieldNo;
	public const ulong C5Filter = (ulong)1 << C5FieldNo;
	public const ulong D5Filter = (ulong)1 << D5FieldNo;
	public const ulong E5Filter = (ulong)1 << E5FieldNo;
	public const ulong F5Filter = (ulong)1 << F5FieldNo;
	public const ulong G5Filter = (ulong)1 << H5FieldNo;
	public const ulong H5Filter = (ulong)1 << H5FieldNo;

	public const ulong A6Filter = (ulong)1 << A6FieldNo;
	public const ulong B6Filter = (ulong)1 << B6FieldNo;
	public const ulong C6Filter = (ulong)1 << C6FieldNo;
	public const ulong D6Filter = (ulong)1 << D6FieldNo;
	public const ulong E6Filter = (ulong)1 << E6FieldNo;
	public const ulong F6Filter = (ulong)1 << F6FieldNo;
	public const ulong G6Filter = (ulong)1 << H6FieldNo;
	public const ulong H6Filter = (ulong)1 << H6FieldNo;

	public const ulong A7Filter = (ulong)1 << A7FieldNo;
	public const ulong B7Filter = (ulong)1 << B7FieldNo;
	public const ulong C7Filter = (ulong)1 << C7FieldNo;
	public const ulong D7Filter = (ulong)1 << D7FieldNo;
	public const ulong E7Filter = (ulong)1 << E7FieldNo;
	public const ulong F7Filter = (ulong)1 << F7FieldNo;
	public const ulong G7Filter = (ulong)1 << H7FieldNo;
	public const ulong H7Filter = (ulong)1 << H7FieldNo;

	public const ulong A8Filter = (ulong)1 << A8FieldNo;
	public const ulong B8Filter = (ulong)1 << B8FieldNo;
	public const ulong C8Filter = (ulong)1 << C8FieldNo;
	public const ulong D8Filter = (ulong)1 << D8FieldNo;
	public const ulong E8Filter = (ulong)1 << E8FieldNo;
	public const ulong F8Filter = (ulong)1 << F8FieldNo;
	public const ulong G8Filter = (ulong)1 << H8FieldNo;
	public const ulong H8Filter = (ulong)1 << H8FieldNo;
}
