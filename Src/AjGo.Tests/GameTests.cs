using System;
using System.Collections.Generic;
using System.Text;

using AjGo;
using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void ShouldCreate()
        {
            Game game = new Game();

            Assert.IsNotNull(game);
            Assert.AreEqual(0, game.Groups.Count);
            Assert.AreEqual(Color.Empty, game.GetColor(0, 0));
            Assert.AreEqual(0, game.Moves.Count);
        }

        [Test]
        public void PlayTest1()
        {
            Game game = new Game();

            game.Play(10, 10, Color.Black);

            Assert.AreEqual(Color.Empty, game.GetColor(0, 0));

            Assert.AreEqual(Color.Black, game.GetColor(10, 10));
            Assert.AreEqual(1, game.Groups.Count);
            Assert.AreEqual(1, game.Moves.Count);

            Assert.AreEqual(4, game.Groups[0].CountLiberties);
        }

        [Test]
        public void PlayTest2()
        {
            Game game = new Game();

            game.Play(12, 10, Color.Black);
            game.Play(12, 11, Color.Black);
            game.Play(12, 12, Color.Black);

            Assert.AreEqual(Color.Empty, game.GetColor(0, 0));

            Assert.AreEqual(Color.Black, game.GetColor(12, 10));
            Assert.AreEqual(Color.Black, game.GetColor(12, 11));
            Assert.AreEqual(Color.Black, game.GetColor(12, 12));

            Assert.AreEqual(1, game.Groups.Count);
            Assert.AreEqual(3, game.Moves.Count);
            Assert.AreEqual(8, game.Groups[0].CountLiberties);
        }

        [Test]
        public void PlayTest3()
        {
            Game game = new Game();

            game.Play(12, 10, Color.Black);
            game.Play(12, 11, Color.Black);
            game.Play(12, 12, Color.Black);

            game.Play(13, 10, Color.White);
            game.Play(13, 11, Color.White);
            game.Play(13, 12, Color.White);

            Assert.AreEqual(Color.Empty, game.GetColor(0, 0));

            Assert.AreEqual(6, game.Moves.Count);

            Assert.AreEqual(Color.Black, game.GetColor(12, 10));
            Assert.AreEqual(Color.Black, game.GetColor(12, 11));
            Assert.AreEqual(Color.Black, game.GetColor(12, 12));

            Assert.AreEqual(Color.White, game.GetColor(13, 10));
            Assert.AreEqual(Color.White, game.GetColor(13, 11));
            Assert.AreEqual(Color.White, game.GetColor(13, 12));

            Assert.AreEqual(2, game.Groups.Count);
            Assert.AreEqual(5, game.Groups[0].CountLiberties);
            Assert.AreEqual(5, game.Groups[1].CountLiberties);
        }

        [Test]
        public void PlayTest4()
        {
            Game game = new Game();

            game.Play(0, 0, Color.Black);
            game.Play(1, 0, Color.White);
            game.Play(0, 1, Color.White);

            Assert.AreEqual(Color.Empty, game.GetColor(0, 0));
            Assert.AreEqual(2, game.Groups.Count);
            Assert.AreEqual(1, game.Groups[0].Count);
            Assert.AreEqual(1, game.Groups[1].Count);
            Assert.AreEqual(3, game.Groups[0].CountLiberties);
            Assert.AreEqual(3, game.Groups[1].CountLiberties);
        }

        [Test]
        public void PlayTest5()
        {
            Game game = new Game();

            game.Play(5, 4, Color.Black);
            game.Play(4, 4, Color.White);
            game.Play(3, 4, Color.Black);
            game.Play(4, 3, Color.White);

            Assert.AreEqual(3, game.Groups.Count);
        }

        [Test]
        public void PlayTest6()
        {
            Game game = new Game();

            game.Play(5, 4, Color.Black);
            game.Play(4, 4, Color.White);
            game.Play(3, 4, Color.Black);
            game.Play(4, 3, Color.White);
            game.Play(5, 3, Color.White);
            game.Play(5, 2, Color.White);

            Assert.AreEqual(3, game.Groups.Count);
        }

        [Test]
        public void PlayTest7()
        {
            Game game = new Game();

            game.Play(0, 1, Color.Black);
            game.Play(1, 0, Color.Black);
            game.Play(1, 1, Color.Black);

            Assert.AreEqual(1, game.Groups.Count);
            Assert.AreEqual(game.GetGroup(0, 1), game.GetGroup(1, 0));
            Assert.AreEqual(game.GetGroup(1, 1), game.GetGroup(1, 0));
            Assert.AreEqual(game.GetGroup(1, 1), game.GetGroup(0, 1));
        }

        [Test]
        public void CloneTest1()
        {
            Game game = new Game();

            Game game2 = game.Clone();

            Assert.IsNotNull(game2);
            Assert.AreEqual(0, game2.Groups.Count);
            Assert.AreEqual(Color.Empty, game2.GetColor(0, 0));
            Assert.AreEqual(0, game2.Moves.Count);
        }

        [Test]
        public void CloneTest2()
        {
            Game game = new Game();

            game.Play(10, 10, Color.Black);

            Game game2 = game.Clone();

            Assert.AreEqual(Color.Empty, game2.GetColor(0, 0));

            Assert.AreEqual(Color.Black, game2.GetColor(10, 10));
            Assert.AreEqual(1, game2.Groups.Count);
            Assert.AreEqual(1, game2.Moves.Count);
        }

        [Test]
        public void CloneTest3()
        {
            Game game = new Game();

            game.Play(12, 10, Color.Black);
            game.Play(12, 11, Color.Black);
            game.Play(12, 12, Color.Black);

            Game game2 = game.Clone();

            Assert.AreEqual(Color.Empty, game2.GetColor(0, 0));

            Assert.AreEqual(Color.Black, game2.GetColor(12, 10));
            Assert.AreEqual(Color.Black, game2.GetColor(12, 11));
            Assert.AreEqual(Color.Black, game2.GetColor(12, 12));

            Assert.AreEqual(1, game2.Groups.Count);
            Assert.AreEqual(3, game2.Moves.Count);
        }

        [Test]
        public void IsValidTest1()
        {
            Game game = new Game();

            Assert.IsTrue(game.IsValid(new Move(0,0,Color.Black)));
            Assert.IsTrue(game.IsValid(new Move(0,0,Color.White)));

            Assert.IsTrue(game.IsValid(new Move(1, 0, Color.Black)));
            Assert.IsTrue(game.IsValid(new Move(1, 0, Color.White)));

            Assert.IsFalse(game.IsValid(new Move(-1, 0, Color.Black)));
            Assert.IsFalse(game.IsValid(new Move(-1, 0, Color.White)));

            Assert.IsFalse(game.IsValid(new Move(19, 0, Color.Black)));
            Assert.IsFalse(game.IsValid(new Move(19, 0, Color.White)));

            Assert.IsFalse(game.IsValid(new Move(0, -1, Color.Black)));
            Assert.IsFalse(game.IsValid(new Move(0, -1, Color.White)));

            Assert.IsFalse(game.IsValid(new Move(0, 19, Color.Black)));
            Assert.IsFalse(game.IsValid(new Move(0, 19, Color.White)));
        }

        [Test]
        public void IsValidTest2()
        {
            Game game = new Game();

            game.Play(0, 1, Color.Black);
            game.Play(1, 0, Color.Black);
            game.Play(2, 1, Color.Black);
            game.Play(1, 2, Color.Black);

            Assert.IsTrue(game.IsValid(1,1,Color.Black));
            Assert.IsFalse(game.IsValid(1, 1, Color.White));

            Assert.IsTrue(game.IsValid(0, 0, Color.Black));
            Assert.IsFalse(game.IsValid(0, 0, Color.White));
        }

        [Test]
        public void IsValidTest3()
        {
            Game game = new Game();

            game.Play(0, 1, Color.Black);
            game.Play(1, 0, Color.Black);
            game.Play(2, 1, Color.Black);

            Assert.IsTrue(game.IsValid(1, 1, Color.Black));
            Assert.IsTrue(game.IsValid(1, 1, Color.White));

            Assert.IsTrue(game.IsValid(0, 0, Color.Black));
            Assert.IsFalse(game.IsValid(0, 0, Color.White));
        }

        [Test]
        public void GetGroupTest1()
        {
            Game game = new Game();
            game.Play(3, 3, Color.Black);

            Group group = game.GetGroup(3, 3);

            Assert.IsNotNull(group);
            Assert.AreEqual(Color.Black, group.Color);
            Assert.AreEqual(1, group.Count);
            Assert.AreEqual(4, group.CountLiberties);
        }

        [Test]
        public void GetGroupTest2()
        {
            Game game = new Game();

            Assert.IsNull(game.GetGroup(3, 3));
        }

        [Test]
        public void GetGroupTest3()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);
            game.Play(3, 4, Color.Black);
            game.Play(3, 5, Color.White);

            Group group1 = game.GetGroup(3, 3);
            Group group2 = game.GetGroup(3, 5);

            Assert.IsNotNull(group1);
            Assert.IsNotNull(group2);

            Assert.AreEqual(Color.Black, group1.Color);
            Assert.AreEqual(Color.White, group2.Color);

            Assert.AreEqual(2, group1.Count);
            Assert.AreEqual(1, group2.Count);
        }

        [Test]
        public void GetColoredGroupTest1()
        {
            Game game = new Game();

            Group group = game.GetColoredGroup(0, 0);
            Group group2 = game.GetColoredGroup((short) (game.Position.Width - 1), (short) (game.Position.Height - 1));

            Assert.IsNotNull(group);
            Assert.IsNotNull(group2);

            Assert.AreEqual(group, group2);

            Assert.AreEqual(game.Position.Size, group.Count);
        }

        [Test]
        public void GetColoredGroupTest2()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);

            Group group = game.GetColoredGroup(3, 3);
            Group group2 = game.GetColoredGroup(2, 2);

            Assert.IsNotNull(group);
            Assert.IsNotNull(group2);

            Assert.AreEqual(Color.Black, group.Color);
            Assert.AreEqual(Color.Blue, group2.Color);

            Assert.AreEqual(1, group.Count);
            Assert.AreEqual(8, group2.Count);

            Group group3 = game.GetColoredGroup(0, 0);

            Assert.IsNotNull(group3);
            Assert.AreEqual(Color.Green, group3.Color);
        }

        [Test]
        public void GetColoredGroupTest3()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);
            game.Play(3, 4, Color.Black);

            Group group = game.GetColoredGroup(3, 3);

            Assert.IsNotNull(group);
            Assert.AreEqual(2, group.Count);

            Group group2 = game.GetColoredGroup(2, 2);

            Assert.IsNotNull(group2);
            Assert.AreEqual(10, group2.Count);
        }

        [Test]
        public void ZoneGroupTest1()
        {
            Game game = new Game();

            game.Play(10, 10, Color.Black);
            game.Play(11, 11, Color.Black);
            game.Play(11, 12, Color.Black);
            game.Play(11, 13, Color.Black);

            Group group = game.GetZoneGroup(10, 10);

            Assert.IsNotNull(group);
            Assert.AreEqual(Color.Black, group.Color);
            Assert.AreEqual(4, group.Count);
        }

        [Test]
        public void ZoneGroupTest2()
        {
            Game game = new Game();

            game.Play(9, 9, Color.Black);
            game.Play(11, 11, Color.Black);
            game.Play(11, 12, Color.Black);
            game.Play(11, 13, Color.Black);

            Group group = game.GetZoneGroup(9, 9);

            Assert.IsNotNull(group);
            Assert.AreEqual(Color.Black, group.Color);
            Assert.AreEqual(4, group.Count);
        }

        [Test]
        public void GroupsTest1()
        {
            Game game = new Game();

            List<Group> groups = game.Groups;

            Assert.IsNotNull(groups);
            Assert.AreEqual(0, groups.Count);

            groups = game.ColoredGroups;

            Assert.IsNotNull(groups);
            Assert.AreEqual(1, groups.Count);
            Assert.AreEqual(Color.Green, groups[0].Color);
        }

        [Test]
        public void GroupsTest2()
        {
            Game game = new Game();
            game.Play(3, 3, Color.Black);

            List<Group> groups = game.Groups;

            Assert.IsNotNull(groups);
            Assert.AreEqual(1, groups.Count);

            groups = game.ColoredGroups;

            Assert.IsNotNull(groups);
            Assert.AreEqual(3, groups.Count);
        }

        [Test]
        public void GroupsTest3()
        {
            Game game = new Game();
            game.Play(3, 3, Color.Black);
            game.Play(18-3, 18-3, Color.White);

            List<Group> groups = game.Groups;

            Assert.IsNotNull(groups);
            Assert.AreEqual(2, groups.Count);

            groups = game.ColoredGroups;

            Assert.IsNotNull(groups);
            Assert.AreEqual(5, groups.Count);
        }

        [Test]
        public void GetLastMoveTest1()
        {
            Game game = new Game();

            Assert.IsNull(game.GetLastMove());
        }

        [Test]
        public void GetLastMoveTest2()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);
            game.Play(5, 5, Color.White);

            Move move = game.GetLastMove();

            Assert.IsNotNull(move);
            Assert.AreEqual(Color.White, move.Color);
            Assert.AreEqual(5, move.Point.X);
            Assert.AreEqual(5, move.Point.Y);
        }

        [Test]
        public void GetZoneTest1()
        {
            Game game = new Game();

            GroupSet zone = game.GetZone(3, 3);

            Assert.IsNotNull(zone);
            Assert.AreEqual(1, zone.Count);
            Assert.AreEqual(Color.Green, zone.Groups[0].Color);
        }

        [Test]
        public void GetZoneTest2()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);

            GroupSet zone = game.GetZone(3, 3);
            Assert.IsNotNull(zone);
            Assert.AreEqual(3, zone.Count);
        }

        [Test]
        public void GetZoneTest3()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);
            game.Play(3, 15, Color.White);

            GroupSet zone = game.GetZone(3, 3);
            Assert.IsNotNull(zone);
            Assert.AreEqual(2, zone.Count);
        }

        [Test]
        public void GetZoneTest4()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);
            game.Play(4, 4, Color.Black);
            game.Play(3, 15, Color.White);

            GroupSet zone = game.GetZone(3, 3);
            Assert.IsNotNull(zone);
            Assert.AreEqual(3, zone.Count);
        }

        [Test]
        public void StoneCountTest1()
        {
            Game game = new Game();

            Assert.AreEqual(0,game.Blacks);
            Assert.AreEqual(0,game.Whites);

            game.Play(3, 3, Color.Black);
            game.Play(3, 4, Color.Black);

            Assert.AreEqual(2, game.Blacks);
            Assert.AreEqual(0, game.Whites);

            game.Play(3, 5, Color.White);

            Assert.AreEqual(2, game.Blacks);
            Assert.AreEqual(1, game.Whites);

            game.Play(3, 5, Color.Empty);

            Assert.AreEqual(2, game.Blacks);
            Assert.AreEqual(0, game.Whites);
        }

        [Test]
        public void StoneCountTest2()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);
            game.Play(2, 3, Color.White);
            game.Play(4, 3, Color.White);
            game.Play(3, 2, Color.White);
            game.Play(3, 4, Color.White);

            Assert.AreEqual(0, game.Blacks);
            Assert.AreEqual(4, game.Whites);
        }

        [Test]
        public void DeathStonesTest1()
        {
            Game game = new Game();

            Assert.AreEqual(0, game.DeadBlacks);
            Assert.AreEqual(0, game.DeadWhites);
        }

        [Test]
        public void DeathStonesTest2()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);
            game.Play(2, 3, Color.White);
            game.Play(4, 3, Color.White);
            game.Play(3, 2, Color.White);
            game.Play(3, 4, Color.White);

            Assert.AreEqual(1, game.DeadBlacks);
            Assert.AreEqual(0, game.DeadWhites);

            Game game2 = game.Clone();

            Assert.AreEqual(1, game2.DeadBlacks);
            Assert.AreEqual(0, game2.DeadWhites);
        }
    }
}

