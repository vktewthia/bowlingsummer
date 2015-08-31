using System;
using System.Collections.Generic;
using BowlingGameLib.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingGameLib;

namespace BowlingGameTest
{
    [TestClass]
    public class TestBowlingGame
    {
        private Game game;
       
        private List<short[]> inputData = new List<short[]>();

        [TestInitialize()]
        public void Arrange()
        {
            game = new Game();
            game.RollTheBowl(1, 4);
            game.RollTheBowl(4, 5);
            game.RollTheBowl(6, 4);
            game.RollTheBowl(5, 5);
            game.RollTheBowl(10, 0);

            game.RollTheBowl(0, 1);
            game.RollTheBowl(7, 3);
            game.RollTheBowl(6, 4);
            game.RollTheBowl(10, 0);
        }

        private void AddLastChanse(bool withStrike)
        {
            if (withStrike)
            {
                game.RollTheLastBowl(2, 8, 6);
            }
            else
            {
                game.RollTheBowl(3, 4);
            }
        }

        [TestMethod]
        public void is_frames_created_correctly_in_game()
        {
            AddLastChanse(false);
            //Frame 1st is not strike or spare
            Assert.IsFalse(game.Frames[1].IsStrike);
            Assert.IsFalse(game.Frames[1].IsSpare);

            //Frame 3 is spare
            Assert.IsFalse(game.Frames[3].IsStrike);
            Assert.IsTrue(game.Frames[3].IsSpare);
            
            //Frame 5 is strike
            Assert.IsTrue(game.Frames[5].IsStrike);

        }

        [TestMethod]
        public void is_frames_created_correctly_in_game_for_10thFrame_without_strike()
        {
            AddLastChanse(false);
            //Frame 1st is not strike or spare
            Assert.IsFalse(game.Frames[10].IsStrike);
            Assert.IsFalse(game.Frames[10].IsSpare);
        }

        [TestMethod]
        public void is_frames_created_correctly_in_game_for_10thFrame_with_strike()
        {
            AddLastChanse(true);
            //Frame 1st is not strike or spare
            Assert.IsFalse(game.Frames[10].IsStrike);
            Assert.IsTrue(game.Frames[10].IsSpare);
        }

        [TestMethod]
        [ExpectedException(typeof(GameExceptions))]
        public void should_exception_when_create_invalid_frame_more_than_10_point()
        {
            game.Reset();
            game.RollTheBowl(8, 8);
            Assert.Fail("Excpected excepiton not thrown");
        }

        [TestMethod]
        [ExpectedException(typeof(GameExceptions))]
        public void should_exception_when_create_invalid_frame_more_than_10_point_in_bowl()
        {
            game.Reset();
            game.RollTheBowl(18, 0);
            Assert.Fail("Excpected excepiton not thrown");
        }

        [TestMethod]
        public void should_not_exception_when_create_10th_frame_more_than_10_point()
        {
            game.RollTheLastBowl(10,10,10);
            var score = game.FinalScore();
            Assert.AreEqual(score, 157);
        }

        [TestMethod]
        [ExpectedException(typeof(GameExceptions))]
        public void should_exception_when_create_invalid_10th_frame_more_than_10_point_in_bowl()
        {
            game.Reset();
            game.RollTheLastBowl(18, 10, 10);
            Assert.Fail("Excpected excepiton not thrown");
           
        }


        [TestMethod]
        public void varify_total_score_of_the_game()
        {
            AddLastChanse(true);
            var score = game.FinalScore();
            Assert.AreEqual(score,133);
        }

        [TestMethod]
        public void varify_score_for_each_frame_of_the_game()
        {
            AddLastChanse(true);
            game.FinalScore();
            var gameFrames = game.Frames;
            Assert.AreEqual(gameFrames[1].Score, 5);
            Assert.AreEqual(gameFrames[2].Score, 14);
            Assert.AreEqual(gameFrames[3].Score, 29);
            Assert.AreEqual(gameFrames[4].Score, 49);
            Assert.AreEqual(gameFrames[5].Score, 60);
            Assert.AreEqual(gameFrames[6].Score, 61);
            Assert.AreEqual(gameFrames[7].Score, 77);
            Assert.AreEqual(gameFrames[8].Score, 97);
            Assert.AreEqual(gameFrames[9].Score, 117);
            Assert.AreEqual(gameFrames[10].Score, 133);
        }

        [TestMethod]
        public void varify_score_for_each_frame_of_the_game2()
        {
            inputData  = new List<short[]>
                {
                    new short[]{1,4},
                    new short[]{4,5},
                    new short[]{6,4},
                    new short[]{5,5},
                    new short[]{10,0},
                    new short[]{0,1},
                    new short[]{7,3},
                    new short[]{6,4},
                    new short[]{10,0},
                    new short[]{2,8,6}
                };

            game = CreateNewGame();
            var score = game.FinalScore();
            var gameFrames = game.Frames;
            Assert.AreEqual(gameFrames[1].Score, 5);
            Assert.AreEqual(gameFrames[2].Score, 14);
            Assert.AreEqual(gameFrames[3].Score, 29);
            Assert.AreEqual(gameFrames[4].Score, 49);
            Assert.AreEqual(gameFrames[5].Score, 60);
            Assert.AreEqual(gameFrames[6].Score, 61);
            Assert.AreEqual(gameFrames[7].Score, 77);
            Assert.AreEqual(gameFrames[8].Score, 97);
            Assert.AreEqual(gameFrames[9].Score, 117);
            Assert.AreEqual(gameFrames[10].Score, 133);

            Assert.AreEqual(score, 133);

        }

        [TestMethod]
        public void varify_score_for_each_frame_of_the_game3()
        {
            inputData = new List<short[]>
                {
                    new short[]{10,0},
                    new short[]{10,0},
                    new short[]{10,0},
                    new short[]{10,0},
                    new short[]{10,0},
                    new short[]{10,0},
                    new short[]{10,0},
                    new short[]{10,0},
                    new short[]{10,0},
                    new short[]{10,10,10}
                };

            game = CreateNewGame();
            var score = game.FinalScore();
            var gameFrames = game.Frames;
            Assert.AreEqual(gameFrames[1].Score, 30);
            Assert.AreEqual(gameFrames[2].Score, 60);
            Assert.AreEqual(gameFrames[3].Score, 90);
            Assert.AreEqual(gameFrames[4].Score, 120);
            Assert.AreEqual(gameFrames[5].Score, 150);
            Assert.AreEqual(gameFrames[6].Score, 180);
            Assert.AreEqual(gameFrames[7].Score, 210);
            Assert.AreEqual(gameFrames[8].Score, 240);
            Assert.AreEqual(gameFrames[9].Score, 270);
            Assert.AreEqual(gameFrames[10].Score, 300);

            Assert.AreEqual(score, 300);

        }

        [TestMethod]
        public void varify_score_for_each_frame_of_the_game4()
        {
            inputData = new List<short[]>
                {
                    new short[]{10,0},
                    new short[]{10,0},
                    new short[]{7,3},
                    new short[]{8,2},
                    new short[]{10,0},
                    new short[]{9,1},
                    new short[]{10,0},
                    new short[]{10,0},
                    new short[]{10,0},
                    new short[]{10,7,3}
                };

            game = CreateNewGame();
            var score = game.FinalScore();
            var gameFrames = game.Frames;
            Assert.AreEqual(gameFrames[1].Score, 27);
            Assert.AreEqual(gameFrames[2].Score, 47);
            Assert.AreEqual(gameFrames[3].Score, 65);
            Assert.AreEqual(gameFrames[4].Score, 85);
            Assert.AreEqual(gameFrames[5].Score, 105);
            Assert.AreEqual(gameFrames[6].Score, 125);
            Assert.AreEqual(gameFrames[7].Score, 155);
            Assert.AreEqual(gameFrames[8].Score, 185);
            Assert.AreEqual(gameFrames[9].Score, 212);
            Assert.AreEqual(gameFrames[10].Score, 232);

            Assert.AreEqual(score, 232);

        }
        
        [TestCleanup()]
        public void CleanUp()
        {
            game.Reset();
        }
        
        private Game CreateNewGame()
        {
            var game = new Game();
            foreach (var item in inputData)
            {
                if (item.Length < 3)
                {
                    game.RollTheBowl(item[0], item[1]);
                }
                else
                {
                    game.RollTheLastBowl(item[0], item[1],item[2]);
                }
            }
            return game;
        }
        
    }
}
