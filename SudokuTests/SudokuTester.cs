using Sudoku_Solver;
using Sudoku_Solver.SudokuBoardConvertors;
using Sudoku_Solver.Exceptions;
using Sudoku_Solver.Validator;

namespace SudokuTests
{
    // This class is used to test the Sudoku_Solver project
    public class SudokuTester
    {
        // 9*9 boards to test
        private readonly Dictionary<string, string> _sudoku9Boards = new()
        {
            {
                // Empty board
                "000000000000000000000000000000000000000000000000000000000000000000000000000000000",
                "123456789789123456456789123312845967697312845845697312231574698968231574574968231"
            },
            {
                // Easy board
                "004050000900734600003021049035090480090000030076010920310970200009182003000060100",
                "264859317981734652753621849135297486892546731476318925318975264649182573527463198"
            },
            {
                // Medium board
                "200050106030000080010902000004001500080009000950000060049037000703010005001000600",
                "298354176435176982617982354374861529186529743952743861549637218763218495821495637"
            },
            {
                // Hard board
                "900800000000000500000000000020010003010000060000400070708600000000030100400000200",
                "972853614146279538583146729624718953817395462359462871798621345265934187431587296"
            },
            {
                // Evil board
                "000000000000003085001020000000507000004000100090000000500000073002010000000040009",
                "987654321246173985351928746128537694634892157795461832519286473472319568863745219"
            },
            {
                // No solution board
                "000005080000601043000000000010500000000106000300000005530000061000000004000000000",
                "000005080000601043000000000010500000000106000300000005530000061000000004000000000"
            }
        };

        // 16*16 boards to test
        private readonly Dictionary<string, string> _sudoku16Boards = new()
        {
            {
                // Empty board
                "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
                "123456789:;<=>?@9:;<1234=>?@56785678=>?@12349:;<=>?@9:;<5678123431427586;9>:?<@=;9>:3142?<@=75867586?<@=3142;9>:?<@=;9>:7586314224136857:?9;<@=>:?9;2413<@=>68576857<@=>2413:?9;<@=>:?9;6857241343218765>;:9@=<?>;:94321@=<?87658765@=<?4321>;:9@=<?>;:987654321"
            },
            {
                // Easy board
                "10023400<06000700080007003009:6;0<00:0010=0;00>0300?200>000900<0=000800:0<201?000;76000@000?005=000:05?0040800;0@0059<00100000800200000=00<580030=00?0300>80@000580010002000=9?000<406@0=00700050300<0006004;00@0700@050>0010020;1?900=002000>000>000;0200=3500<",
                "15:2349;<@6>?=78>@8=5?7<43129:6;9<47:@618=?;35>236;?2=8>75:94@<1=4>387;:5<261?@98;76412@9:>?<35=<91:=5?634@8>2;7@?259<>31;7=:68462@>;94=?1<587:37=91?235;>8:@<46583;1:<7264@=9?>?:<4>6@8=9372;152358<>:?6794;1=@:7=<@359>8;1642?;1?968=4@25<7>3:4>6@7;12:?=3589<"
            },
            {
                // Medium board
                "0804095020:<06;06000000@9570:00?:000100;000000000000000<00600004?002;1>0@008907=907=0200;100430@008@0=0502000>60060000080907000204305000:020;000000;8040000000000?0000008@40=0070957:0200;00@008;1>030@45000<200000:000130007=957=00?0<000000@000000900=?0000000",
                "38@4=9572?:<>6;16;1>438@957=:<2?:<2?1>6;438@57=957=92?:<1>6;38@4?:<2;1>6@438957=957=<2?:;1>6438@438@7=95<2?:1>6;>6;1@438=957?:<2@43857=9:<2?;1>61>6;8@437=952?:<2?:<6;1>8@43=957=957:<2?6;1>@438;1>638@457=9<2?:<2?:>6;138@47=957=95?:<2>6;18@438@43957=?:<26;1>"
            },
            {
                // Hard board
                "0?00@0000;06=000000400?0@:0>0<070<605=908?000>@0:0000;000=94?1202001>@0000;00=9000=0100?0@007;000;<6450000?000>@0:0>000<00=9000290000108000:00;000;0000002000:00>0:3<070000=200010003>@0<6004500?008000000070000=0050010:300000000000040?00800033000;<0700001000",
                "8?12@:3>7;<6=9455=9428?1@:3>;<67;<675=948?123>@::3>@7;<65=94?12828?1>@:367;<5=9445=9128?>@:37;<67;<645=928?1:3>@@:3>67;<45=98?12945=?1283>@:67;<67;<945=128?@:3>>@:3<67;945=28?1128?3>@:<67;45=9?128:3>@;<67945==9458?12:3>@<67;<67;=945?128>@:33>@:;<67=945128?"
            },
            {
                // Evil Board
                "80010;0@>0=070<00000000040000000=0>07004008?000@:<0018060302009>047:00000;00=0>50018;9@000<00?000030=00500?002000>0000400826000301000030=<00000000090050:0000@080500060:80@09000670?200800000450000007=<060:030000040000000000;010?0@0020000400008200500<07060:?",
                "8?613;2@>5=97:<4;2@35=9>47:<18?6=9>57:<4618?3;2@:<4718?6@3;25=9>?47:82613;9@=<>52618;9@35=<>:?479@3;=<>57:?48261<>5=:?471826;9@3@1829>3;=<45?67:>3;9<45=:?672@1845=<?67:82@19>3;67:?2@18;9>3<45=5;9>47=<?61:@3827=<461:?2@38>5;91:?6@3829>5;47=<382@>5;9<47=61:?"
            },
            {
                // No solution board
                ";0?0=>010690000000710000500:?0;4000000<0400070=005<3000800000000500@000:?80>10004<30>?8;00=20000>?8;270060000000000000900000000?0000?00000>0=000?3:0000>0026000000;>61029@0<00000100<0@00:40000800500:0?;>012600800?0;0000090<0@0;07000005<00?8:00003050:4080709",
                ";0?0=>010690000000710000500:?0;4000000<0400070=005<3000800000000500@000:?80>10004<30>?8;00=20000>?8;270060000000000000900000000?0000?00000>0=000?3:0000>0026000000;>61029@0<00000100<0@00:40000800500:0?;>012600800?0;0000090<0@0;07000005<00?8:00003050:4080709"
            }
        };

        // 25*25 boards to test
        private readonly Dictionary<string, string> _sudoku25Boards = new()
        {
            {
                // Empty board
                "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
                "123456789:;<=>?@ABCDEFGHI;<=>?12345@ABCDFGEHI6789:6789:HIFEG12345;<=>?@ABCDGFEIH@ABCD6789:12345;<=>?@ABCD;<=>?HEGFI6789:123453152>C@D7;B?EHG8:IF<49A=648:6B31GH2=FI<>9@C;A5?DE7H?I7<459=631@A2BD>EG8:F;C9C;DE8FIA>45:7631?=2BH@<G=GAF@:?EB<89D;C45H7631>I2251?3>=AI8G@C:B<H;D974E6F7@6;42B153<HA8F>EG:=DIC?9<IGE8746;92D153A?FBC>=H:@>DCH9FG:@E746?=2I153<8;ABA:FB=<H?DC>I;E97468@2G153C6412IE;<HDB9G8?>@AF:573=5=937A8214FCHI;:BDGE?6<@>?BDGA5673@:>214C=9<HFEI8;F><@;B:C?=5673EI8214AD9GH:EH8ID9>GF?=<@A5673;CB214B3251?D48IA:F=HG;<@>9C67EI47=693@21CG>D<EF:?8H;5BAD;?<CG>567E3421H9AIB=@:F889@AFECH:BI;567=3421G>?D<EH>:G=;<FA98?B@DC567I3421"
            }
        };

        // illegal boards sizes to test
        private readonly List<string> _illegalSudokuBoardsSize = new()
        {
            "098765432345678", // string with length 15
            "0000", // string with length 4
            "15436", // string with length 5
            "000000000000000000000000000000000000" // string with length 36
        };

        // illegal values in the boards to test
        private readonly List<string> _illegalSudokuBoardsValues = new()
        {
            "4500000000000000", // 4*4 board with 5
            "3", // 1*1 board with 3
            "0000000000000000000000:0000000000000000000000000000000000000000000000000000000000" // 9*9 board with :
        };

        // double values in row to test
        private readonly List<string> _doubleValuesInRow = new()
        {
            "004450000900734600003021049035090480090000030076010920310970200009182003000060100", // double 4 in row 1
            "0000110000000000", // double 1 in row 2
            "8<000407000=00007@00100@02?0:00<=10500000:;0490@00200000@09005=00400000@000103>0060000=1000>;004005=0?0>400007060:?340000000000200805006001200:06070?000;3008<002000030090040@00003>90<40000000?00<00060300?0:;05=@600208000<007000200000000@00=00000000=@0010?3" // double @ in row 2
        };

        // double values in column to test
        private readonly List<string> _doubleValuesInColumn = new()
        {
            "004050000904730600003021049035090480090000030076010920310970200009182003000060100", // double 4 in row column 3
            "3000300000000000", // double 3 in row column 1
            "8<000407000=00007@00100002?0:00<=10500000:;0490@00200000@09005=00400000@0:0103>0060000=1000>;004005=0?0>400007060:?340000000000200805006001200:06070?000;3008<002000030090040@00003>90<40000000?00<00060300?0:;05=@600208000<007000200000000@00=00000000=@0010?3" // double : in column 10
        };

        // double values in box to test
        private readonly List<string> _doubleValuesInBox = new()
        {
            "004050000900734600403021049035090480090000030076010920310970200009182003000060100", // double 4 in box 1
            "0000000000100001", // double 1 in box 4
            "8<000407000=00007@00100002?0:00<=10500000:;0490@00280000@09005=00400000@000103>0060000=1000>;004005=0?0>400007060:?340000000000200805006001200:06070?000;3008<002000030090040@00003>90<40000000?00<00060300?0:;05=@600208000<007000200000000@00=00000000=@0010?3" // double 8 in box 1
        };

        /// <summary>
        /// Testing all the boards in _sudoku9Boards dictionary
        /// </summary>
        [Fact]
        public void TestSudoku9Boards()
        {
            foreach (var board in _sudoku9Boards)
            {
                byte[,] sudoku = SudokuBoardBuilder.BoardBuilder(board.Key);
                string solvedBoard = Solver.Solve(sudoku);
                Assert.Equal(board.Value, solvedBoard);
            }
        }

        /// <summary>
        /// Testing all the boards in _sudoku16Boards dictionary
        /// </summary>
        [Fact]
        public void TestSudoku16Boards()
        {
            foreach (var board in _sudoku16Boards)
            {
                byte[,] sudoku = SudokuBoardBuilder.BoardBuilder(board.Key);
                string solvedBoard = Solver.Solve(sudoku);
                Assert.Equal(board.Value, solvedBoard);
            }
        }

        /// <summary>
        /// Testing all the boards in _sudoku25Boards dictionary
        /// </summary>
        [Fact]
        public void TestSudoku25Boards()
        {
            foreach (var board in _sudoku25Boards)
            {
                byte[,] sudoku = SudokuBoardBuilder.BoardBuilder(board.Key);
                string solvedBoard = Solver.Solve(sudoku);
                Assert.Equal(board.Value, solvedBoard);
            }
        }

        /// <summary>
        /// Test that all the boards in _illegalSudokuBoardsSize are raising an InvalidSudokuBoardSizeException
        /// </summary>
        [Fact]
        public void TestIllegalSudokuBoardsSize()
        {
            foreach (var board in _illegalSudokuBoardsSize)
            {
                Assert.Throws<InvalidSudokuBoardSizeException>(() => SudokuBoardBuilder.BoardBuilder(board));
            }
        }

        /// <summary>
        /// Test that all the boards in _illegalSudokuBoardsValues are raising an AllowedValuesException
        /// </summary>
        [Fact]
        public void TestIllegalSudokuBoardsValues()
        {
            foreach (var board in _illegalSudokuBoardsValues)
            {
                Assert.Throws<AllowedValuesException>(() => SudokuBoardBuilder.BoardBuilder(board));
            }
        }

        /// <summary>
        /// Test that all the boards in _doubleValuesInRow are raising a DuplicateValueInRowException
        /// </summary>
        [Fact]
        public void TestDoubleValuesInRow()
        {
            foreach (var board in _doubleValuesInRow)
            {
                byte[,] sudoku = SudokuBoardBuilder.BoardBuilder(board);
                Assert.Throws<DuplicateValueInRowException>(() => SudokuValidator.Validate(sudoku));
            }
        }

        /// <summary>
        /// Test that all the boards in _doubleValuesInColumn are raising a DuplicateValueInColumnException
        /// </summary>
        [Fact]
        public void TestDoubleValuesInColumn()
        {
            foreach (var board in _doubleValuesInColumn)
            {
                byte[,] sudoku = SudokuBoardBuilder.BoardBuilder(board);
                Assert.Throws<DuplicateValueInColumnException>(() => SudokuValidator.Validate(sudoku));
            }
        }

        /// <summary>
        /// Test that all the boards in _doubleValuesInBox are raising a DuplicateValueInBoxException
        /// </summary>
        [Fact]
        public void TestDoubleValuesInBox()
        {
            foreach (var board in _doubleValuesInBox)
            {
                byte[,] sudoku = SudokuBoardBuilder.BoardBuilder(board);
                Assert.Throws<DuplicateValueInBoxException>(() => SudokuValidator.Validate(sudoku));
            }
        }
    }
}