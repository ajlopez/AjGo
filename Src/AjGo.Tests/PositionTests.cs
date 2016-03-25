using System;
using System.Collections.Generic;
using System.Text;

using AjGo;

using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class PositionTests
    {
        [Test]
        public void ShouldCreate()
        {
            Position pos = new Position();
            Assert.IsNotNull(pos);
            Assert.AreEqual(19, pos.Width);
            Assert.AreEqual(19, pos.Height);
            Assert.AreEqual(19 * 19, pos.Size);
        }

        [Test]
        public void ShouldHasSize()
        {
            Position pos = new Position();
            Assert.AreEqual(19, pos.Width);
            Assert.AreEqual(19, pos.Height);
        }

        [Test]
        public void CellsShouldBeEmpty()
        {
            Position pos = new Position();

            for (short x = 0; x < pos.Width; x++)
                for (short y = 0; y < pos.Height; y++)
                    Assert.AreEqual(Color.Empty, pos.GetColor(x, y));
        }

        [Test]
        public void BorderShouldBeBorder()
        {
            Position pos = new Position();

            for (short x = 0; x < pos.Width; x++) {
                Assert.AreEqual(Color.Border,pos.GetColor(x,-1));
                Assert.AreEqual(Color.Border,pos.GetColor(x,pos.Height));
            }

            for (short y = 0; y < pos.Height; y++) {
                Assert.AreEqual(Color.Border,pos.GetColor(-1,y));
                Assert.AreEqual(Color.Border,pos.GetColor(pos.Width,y));
            }
        }

        [Test]
        public void OutOfBoard1()
        {
            Position pos = new Position();
            Assert.AreEqual(Color.Border,pos.GetColor(-2, 0));
        }

        [Test]
        public void OutOfBoard2()
        {
            Position pos = new Position();
            Assert.AreEqual(Color.Border,pos.GetColor(0, -2));
        }

        [Test]
        public void OutOfBoard3()
        {
            Position pos = new Position();
            Assert.AreEqual(Color.Border,pos.GetColor(pos.Width+1, 0));
        }

        [Test]
        public void OutOfBoard4()
        {
            Position pos = new Position();
            Assert.AreEqual(Color.Border,pos.GetColor(0, pos.Height+1));
        }

        [Test]
        public void ShouldSetColor1()
        {
            Position pos = new Position();

            pos.SetColor(0, 0, Color.Black);
            Assert.AreEqual(Color.Black, pos.GetColor(0, 0));
            Assert.AreEqual(Color.Empty, pos.GetColor(0, 1));
            Assert.AreEqual(Color.Empty, pos.GetColor(1, 0));

            pos.SetColor(10, 10, Color.White);
            Assert.AreEqual(Color.White, pos.GetColor(10, 10));
            Assert.AreEqual(Color.Empty, pos.GetColor(10, 11));
            Assert.AreEqual(Color.Empty, pos.GetColor(11, 10));
        }

        [Test][ExpectedException(typeof(IndexOutOfRangeException))]
        public void ShouldSetColor2()
        {
            Position pos = new Position();

            pos.SetColor(-2, 0, Color.Black);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldSetColor3()
        {
            Position pos = new Position();

            pos.SetColor(-1, 0, Color.Black);
        }

        [Test]
        public void EmptyTest1()
        {
            Position pos = new Position();

            pos.SetColor(0, 0, Color.Black);
            Assert.IsFalse(pos.IsEmpty(0, 0));
            Assert.IsTrue(pos.IsEmpty(1, 0));
            Assert.IsTrue(pos.IsEmpty(0, 1));

            pos.SetColor(10, 10, Color.White);
            Assert.IsFalse(pos.IsEmpty(10, 10));
            Assert.IsTrue(pos.IsEmpty(11, 10));
            Assert.IsTrue(pos.IsEmpty(10, 11));
        }


        [Test]
        public void EmptyTest2()
        {
            Position pos = new Position();

            Assert.IsFalse(pos.IsEmpty(-1,0));
            Assert.IsFalse(pos.IsEmpty(0,-1));
            Assert.IsFalse(pos.IsEmpty(-2, 0));
            Assert.IsFalse(pos.IsEmpty(0, -2));

            Assert.IsFalse(pos.IsEmpty(19, 0));
            Assert.IsFalse(pos.IsEmpty(0, 19));
            Assert.IsFalse(pos.IsEmpty(20, 0));
            Assert.IsFalse(pos.IsEmpty(0, 20));
        }

        [Test]
        public void TestColors1()
        {
            Position pos = new Position();

            Assert.AreEqual(2, pos.NoColor(0, 0, Color.Border));
            Assert.AreEqual(2, pos.NoColor(0, 0, Color.Empty));
            Assert.AreEqual(0, pos.NoColor(0, 0, Color.Black));
            Assert.AreEqual(0, pos.NoColor(0, 0, Color.White));

            Assert.AreEqual(0, pos.NoColor(1, 1, Color.Border));
            Assert.AreEqual(4, pos.NoColor(1, 1, Color.Empty));
            Assert.AreEqual(0, pos.NoColor(1, 1, Color.Black));
            Assert.AreEqual(0, pos.NoColor(1, 1, Color.White));
        }

        [Test]
        public void TestColors2()
        {
            Position pos = new Position();

            pos.SetColor(0, 1, Color.White);
            Assert.AreEqual(2, pos.NoColor(0, 0, Color.Border));
            Assert.AreEqual(1, pos.NoColor(0, 0, Color.Empty));
            Assert.AreEqual(1, pos.NoColor(0, 0, Color.White));
            Assert.AreEqual(0, pos.NoColor(0, 0, Color.Black));
        }

        [Test]
        public void TestColors3()
        {
            Position pos = new Position();

            pos.SetColor(0, 1, Color.Black);
            Assert.AreEqual(2, pos.NoColor(0, 0, Color.Border));
            Assert.AreEqual(1, pos.NoColor(0, 0, Color.Empty));
            Assert.AreEqual(0, pos.NoColor(0, 0, Color.White));
            Assert.AreEqual(1, pos.NoColor(0, 0, Color.Black));
        }

        [Test]
        public void MoveTest1()
        {
            Position pos = new Position();
            Move move = new Move(0, 0, Color.Black);

            pos.Play(move);
            Assert.AreEqual(Color.Black, pos.GetColor(0, 0));
            Assert.AreEqual(Color.Empty, pos.GetColor(0, 1));
            Assert.AreEqual(Color.Empty, pos.GetColor(1, 0));

            move = new Move(10, 10, Color.White);
            pos.Play(move);
            Assert.AreEqual(Color.White, pos.GetColor(10, 10));
            Assert.AreEqual(Color.Empty, pos.GetColor(10, 11));
            Assert.AreEqual(Color.Empty, pos.GetColor(11, 10));
        }


        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void MoveTest2()
        {
            Position pos = new Position();

            pos.Play(new Move(-2, 0, Color.Black));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MoveTest3()
        {
            Position pos = new Position();

            pos.Play(new Move(-1, 0, Color.Black));
        }

        [Test]
        public void TestCountColor1()
        {
            Position p = new Position();
            Assert.AreEqual(0, p.CountColor(Color.Black));
            Assert.AreEqual(0, p.CountColor(Color.White));
            Assert.AreEqual(p.Width * p.Height, p.CountColor(Color.Empty));
        }

        [Test]
        public void TestCountColor2()
        {
            Position p = new Position();

            p.Play(new Move(10, 10, Color.Black));

            Assert.AreEqual(1, p.CountColor(Color.Black));
            Assert.AreEqual(0, p.CountColor(Color.White));
            Assert.AreEqual(p.Width * p.Height - 1, p.CountColor(Color.Empty));
        }

        [Test]
        public void TestCountColor3()
        {
            Position p = new Position();

            p.Play(new Move(10, 10, Color.White));

            Assert.AreEqual(0, p.CountColor(Color.Black));
            Assert.AreEqual(1, p.CountColor(Color.White));
            Assert.AreEqual(p.Width * p.Height - 1, p.CountColor(Color.Empty));
        }

        [Test]
        public void TestCountColor4()
        {
            Position p = new Position();

            p.Play(new Move(10, 10, Color.White));
            p.Play(new Move(12, 12, Color.Black));

            Assert.AreEqual(1, p.CountColor(Color.Black));
            Assert.AreEqual(1, p.CountColor(Color.White));
            Assert.AreEqual(p.Width * p.Height - 2, p.CountColor(Color.Empty));
        }

        [Test]
        public void TestExtendedColors1()
        {
            Position p = new Position();
            p.CalculateColors();

            Assert.AreEqual(p.Width * p.Height, p.CountColor(Color.Green));
        }

        [Test]
        public void TestExtendedColors2()
        {
            Position p = new Position();
            p.SetColor(3, 3, Color.Black);
            p.CalculateColors();

            Assert.AreEqual(8, p.CountColor(Color.Blue));
            Assert.AreEqual(1, p.CountColor(Color.Black));
        }

        [Test]
        public void TestExtendedColors3()
        {
            Position p = new Position();
            p.SetColor(3, 3, Color.White);
            p.CalculateColors();

            Assert.AreEqual(8, p.CountColor(Color.Yellow));
            Assert.AreEqual(1, p.CountColor(Color.White));
        }

        [Test]
        public void TestExtendedColors5()
        {
            Position p = new Position();
            p.SetColor(3, 3, Color.Black);
            p.SetColor(4, 4, Color.White);
            p.CalculateColors();

            Assert.AreEqual(5, p.CountColor(Color.Yellow));
            Assert.AreEqual(5, p.CountColor(Color.Blue));
            Assert.AreEqual(2, p.CountColor(Color.Red));
            Assert.AreEqual(1, p.CountColor(Color.Black));
            Assert.AreEqual(1, p.CountColor(Color.White));
        }

        [Test]
        public void TestExtendedColors6()
        {
            Position p = new Position();
            p.SetColor(3, 2, Color.Black);
            p.CalculateColors();

            Assert.AreEqual(11, p.CountColor(Color.Blue));
            Assert.AreEqual(1, p.CountColor(Color.Black));
        }

        [Test]
        public void TestExtendedColors7()
        {
            Position p = new Position();
            p.SetColor(3, 2, Color.White);
            p.CalculateColors();

            Assert.AreEqual(11, p.CountColor(Color.Yellow));
            Assert.AreEqual(1, p.CountColor(Color.White));
        }

        [Test]
        public void TestClone1()
        {
            Position p1 = new Position();
            Position p2 = p1.Clone();

            Assert.IsNotNull(p2);
            Assert.AreEqual(p1.Width, p2.Width);
            Assert.AreEqual(p1.Height, p2.Height);

            Assert.AreEqual(0, p2.CountColor(Color.Black));
            Assert.AreEqual(0, p2.CountColor(Color.White));
            Assert.AreEqual(p2.Width * p2.Height, p2.CountColor(Color.Empty));
        }

        [Test]
        public void TestClone2()
        {
            Position p1 = new Position();

            p1.Play(new Move(10, 10, Color.Black));

            Position p2 = p1.Clone();

            Assert.IsNotNull(p2);
            Assert.AreEqual(p1.Width, p2.Width);
            Assert.AreEqual(p1.Height, p2.Height);

            Assert.AreEqual(Color.Black, p2.GetColor(10, 10));
            Assert.AreEqual(Color.Empty, p2.GetColor(8, 11));

            Assert.AreEqual(1, p2.CountColor(Color.Black));
            Assert.AreEqual(0, p2.CountColor(Color.White));
            Assert.AreEqual(p2.Width * p2.Height - 1, p2.CountColor(Color.Empty));
        }

        [Test]
        public void TestClone3()
        {
            Position p1 = new Position();

            p1.Play(new Move(10, 10, Color.Black));
            p1.Play(new Move(8, 11, Color.White));

            Position p2 = p1.Clone();

            Assert.IsNotNull(p2);
            Assert.AreEqual(p1.Width, p2.Width);
            Assert.AreEqual(p1.Height, p2.Height);

            Assert.AreEqual(Color.Black, p2.GetColor(10, 10));
            Assert.AreEqual(Color.White, p2.GetColor(8, 11));
            Assert.AreEqual(Color.Empty, p2.GetColor(9, 12));

            Assert.AreEqual(1, p2.CountColor(Color.Black));
            Assert.AreEqual(1, p2.CountColor(Color.White));
            Assert.AreEqual(p2.Width * p2.Height - 2, p2.CountColor(Color.Empty));
        }

        [Test]
        public void EqualsTest1()
        {
            Position p1 = new Position();
            Position p2 = new Position();

            Assert.IsTrue(p1.Equals(p2));
        }

        [Test]
        public void EqualsTest2()
        {
            Position p1 = new Position();

            Assert.IsTrue(p1.Equals(p1));
        }

        [Test]
        public void EqualsTest3()
        {
            Position p1 = new Position();
            Position p2 = new Position();

            p1.SetColor(3, 3, Color.Black);

            Assert.IsFalse(p1.Equals(p2));
        }

        [Test]
        public void EqualsTest4()
        {
            Position p1 = new Position();
            Position p2 = new Position();

            p1.SetColor(3, 3, Color.Black);
            p2.SetColor(3, 3, Color.White);

            Assert.IsFalse(p1.Equals(p2));
        }

        [Test]
        public void EqualsTest5()
        {
            Position p1 = new Position();
            Position p2 = new Position();

            p1.SetColor(3, 3, Color.Black);
            p2.SetColor(3, 3, Color.Black);

            Assert.IsTrue(p1.Equals(p2));
        }

        [Test]
        public void CountColorsTest1()
        {
            Position position = new Position();

            ColorCount cc = position.CountFrontierColors(new Point(0, 0));

            Assert.IsNotNull(cc);

            Assert.AreEqual(5, cc.Count[(int) Color.Border]);
            Assert.AreEqual(3, cc.Count[(int)Color.Empty]);
            Assert.AreEqual(0, cc.Count[(int)Color.Black]);
            Assert.AreEqual(0, cc.Count[(int)Color.White]);
        }

        [Test]
        public void CountColorsTest2()
        {
            Position position = new Position();

            ColorCount cc = position.CountFrontierColors(new Point(10, 10));

            Assert.IsNotNull(cc);

            Assert.AreEqual(0, cc.Count[(int)Color.Border]);
            Assert.AreEqual(8, cc.Count[(int)Color.Empty]);
            Assert.AreEqual(0, cc.Count[(int)Color.Black]);
            Assert.AreEqual(0, cc.Count[(int)Color.White]);
        }

        [Test]
        public void CountColorsTest3()
        {
            Position position = new Position();

            position.SetColor(9, 9, Color.Black);
            position.SetColor(10, 11, Color.White);

            ColorCount cc = position.CountFrontierColors(new Point(10, 10));

            Assert.IsNotNull(cc);

            Assert.AreEqual(0, cc.Count[(int)Color.Border]);
            Assert.AreEqual(6, cc.Count[(int)Color.Empty]);
            Assert.AreEqual(1, cc.Count[(int)Color.Black]);
            Assert.AreEqual(1, cc.Count[(int)Color.White]);
        }

        [Test]
        public void CountColorsTest4()
        {
            Position position = new Position();

            position.SetColor(9, 9, Color.Black);
            position.SetColor(10, 11, Color.White);

            PointSet ps = new PointSet();
            ps.Add(9, 9);
            ps.Add(10, 11);

            ColorCount cc = position.CountColors(ps);

            Assert.IsNotNull(cc);

            Assert.AreEqual(0, cc.Count[(int)Color.Border]);
            Assert.AreEqual(0, cc.Count[(int)Color.Empty]);
            Assert.AreEqual(1, cc.Count[(int)Color.Black]);
            Assert.AreEqual(1, cc.Count[(int)Color.White]);
        }

        [Test]
        public void CountColorsTest5()
        {
            Position position = new Position();

            position.SetColor(9, 9, Color.Black);
            position.SetColor(10, 10, Color.White);

            position.CalculateColors();

            PointSet ps = new PointSet();
            ps.Add(9, 9);
            ps.Add(10, 10);
            ps.Add(ps.CalculateFrontier(position));

            ColorCount cc = position.CountColors(ps);

            Assert.IsNotNull(cc);

            Assert.AreEqual(0, cc.Count[(int)Color.Border]);
            Assert.AreEqual(0, cc.Count[(int)Color.Empty]);
            Assert.AreEqual(1, cc.Count[(int)Color.Black]);
            Assert.AreEqual(1, cc.Count[(int)Color.White]);
            Assert.AreEqual(2, cc.Count[(int)Color.Red]);
            Assert.AreEqual(5, cc.Count[(int)Color.Blue]);
            Assert.AreEqual(5, cc.Count[(int)Color.Yellow]);
        }

        [Test]
        public void CountColorsTest6()
        {
            Position position = new Position();
            ColorCount cc = position.CountColors();

            Assert.AreEqual(0, cc.Whites);
            Assert.AreEqual(0, cc.Blacks);
        }

        [Test]
        public void CountColorsTest7()
        {
            Position position = new Position();
            position.SetColor(3, 3, Color.Black);

            ColorCount cc = position.CountColors();

            Assert.AreEqual(0, cc.Whites);
            Assert.AreEqual(1, cc.Blacks);
        }

        [Test]
        public void CountColorsTest8()
        {
            Position position = new Position();
            position.SetColor(3, 3, Color.Black);
            position.SetColor(3, 4, Color.White);

            ColorCount cc = position.CountColors();

            Assert.AreEqual(1, cc.Whites);
            Assert.AreEqual(1, cc.Blacks);
        }

        [Test]
        public void GetPathsFromTest1()
        {
            Position position = new Position();

            List<PointSet> paths = position.GetPathsFrom(3, 3, 1);

            Assert.IsNotNull(paths);
            Assert.AreEqual(1, paths.Count);
        }

        [Test]
        public void GetPathsFromTest2()
        {
            Position position = new Position();

            List<PointSet> paths = position.GetPathsFrom(3, 3, 2);

            Assert.IsNotNull(paths);
            Assert.AreEqual(5, paths.Count);
        }

        [Test]
        public void GetPathsFromTest3()
        {
            Position position = new Position();

            List<PointSet> paths = position.GetPathsFrom(3, 3, 3);

            Assert.IsNotNull(paths);
            Assert.AreEqual(17, paths.Count);
        }

        [Test]
        public void GetPathsFromTest4()
        {
            Position position = new Position();

            List<PointSet> paths = position.GetPathsFrom(3, 3, 4);

            Assert.IsNotNull(paths);
            Assert.AreEqual(17+36, paths.Count);
        }

        [Test]
        public void GetPathsFromToTest1()
        {
            Position position = new Position();

            List<PointSet> paths = position.GetPathsFromTo(3, 3, 3, 4, 2);

            Assert.IsNotNull(paths);
            Assert.AreEqual(1, paths.Count);
        }

        [Test]
        public void GetPathsFromToTest2()
        {
            Position position = new Position();

            List<PointSet> paths = position.GetPathsFromTo(3, 3, 3, 5, 3);

            Assert.IsNotNull(paths);
            Assert.AreEqual(1, paths.Count);
        }

        [Test]
        public void GetPathsFromToTest3()
        {
            Position position = new Position();

            List<PointSet> paths = position.GetPathsFromTo(3, 3, 3, 5, 4);

            Assert.IsNotNull(paths);
            Assert.AreEqual(1, paths.Count);
        }

        [Test]
        public void GetPathsFromToTest4()
        {
            Position position = new Position();

            List<PointSet> paths = position.GetPathsFromTo(3, 3, 3, 5, 5);

            Assert.IsNotNull(paths);
            Assert.AreEqual(3, paths.Count);
        }

        [Test]
        public void GetFreePathsFromToTest1()
        {
            Position position = new Position();

            List<PointSet> paths = position.GetFreePathsFromTo(3, 3, 3, 4, 2);

            Assert.IsNotNull(paths);
            Assert.AreEqual(1, paths.Count);
        }

        [Test]
        public void GetFreePathsFromToTest2()
        {
            Position position = new Position();

            List<PointSet> paths = position.GetFreePathsFromTo(3, 3, 3, 5, 3);

            Assert.IsNotNull(paths);
            Assert.AreEqual(1, paths.Count);
        }

        [Test]
        public void GetFreePathsFromToTest3()
        {
            Position position = new Position();

            List<PointSet> paths = position.GetFreePathsFromTo(3, 3, 3, 5, 4);

            Assert.IsNotNull(paths);
            Assert.AreEqual(1, paths.Count);
        }

        [Test]
        public void GetFreePathsFromToTest4()
        {
            Position position = new Position();

            List<PointSet> paths = position.GetFreePathsFromTo(3, 3, 3, 5, 5);

            Assert.IsNotNull(paths);
            Assert.AreEqual(3, paths.Count);
        }

        [Test]
        public void GetFreePathsFromToTest5()
        {
            Position position = new Position();
            position.SetColor(3, 4, Color.Black);

            List<PointSet> paths = position.GetFreePathsFromTo(3, 3, 3, 5, 5);

            Assert.IsNotNull(paths);
            Assert.AreEqual(2, paths.Count);
        }
    }
}

