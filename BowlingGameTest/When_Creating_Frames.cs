using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingGameLib;

namespace BowlingGameTest
{
    [TestClass]
    public class When_Creating_Frames
    {
        private Bowl bowl1;
        private Bowl bowl2;
        private Bowl strike;

        private const short INDEX1 = 1;
        private FrameFactory _frameFactory;


        [TestInitialize()]
        public void Arrange()
        {
            _frameFactory = new FrameFactory();
            bowl1 = new Bowl(7);
            bowl2 = new Bowl(3);
            
            //10th Frame Bowl
            strike = new Bowl(10);
        }
        
        [TestMethod]
        public void when_adding_bowls_to_frame()
        {
            Frame frame = new Frame(bowl1,bowl2,1);
            Assert.AreEqual(frame.FirstBowl.PinsDrop, 7);
            Assert.AreEqual(frame.SecondBowl.PinsDrop, 3);
            
            Assert.IsTrue(frame.Index==1);

        }
        
        [TestMethod]
        public void when_frame_is_strike()
        {
            Frame frame = new Frame(strike, bowl1,2);
            Assert.IsTrue(frame.IsStrike);
        }

        [TestMethod]
        public void when_frame_is_spare()
        {
            Frame frame = new Frame(bowl1,bowl2,2);
            Assert.IsTrue(frame.IsSpare);
        }

        [TestMethod]
        public void when_adding_bowls_to_10th_frame()
        {
            LastFrame frame = new LastFrame(10, 3, 7, 2);
            Assert.AreEqual(frame.FirstBowl.PinsDrop, 10);
            Assert.AreEqual(frame.SecondBowl.PinsDrop, 3);
            Assert.AreEqual(frame.ExtraBowl.PinsDrop, 7);
            Assert.IsTrue(frame.Index == 2);
        }

        [TestMethod]
        public void when_calculating_value_of_10th_frame_if_strike()
        {
            AbstractFrame frame = new LastFrame(10, 3, 7, 2);
            var value = frame.Value();
            Assert.AreEqual(value, 20);
        }

        [TestMethod]
        public void when_calculating_value_of_10th_frame_if_spare()
        {
            AbstractFrame frame = new LastFrame(3, 7, 7, 2);
            var value = frame.Value();
            Assert.AreEqual(value, 17);
        }

        [TestMethod]
        public void when_calculating_value_of_10th_frame_if_scratch()
        {
            AbstractFrame frame = new LastFrame(3, 4, 7, 2);
            var value = frame.Value();
            Assert.AreEqual(value, 7);
        }

        [TestMethod]
        public void when_calculating_value_of_frame_if_strike()
        {
            AbstractFrame frame = new Frame(10, 0, INDEX1);
            var value = frame.Value();
            Assert.AreEqual(value, 10);
        }

        [TestMethod]
        public void when_calculating_value_of_frame_if_spare()
        {
            AbstractFrame frame = new Frame(3, 7, INDEX1);
            var value = frame.Value();
            Assert.AreEqual(value, 10);
        }

        [TestMethod]
        public void when_calculating_value_of_frame_if_scratch()
        {
            AbstractFrame frame = new Frame(3, 4, INDEX1);
            var value = frame.Value();
            Assert.AreEqual(value, 7);
        }

        [TestMethod]
        public void should_invalid_when_create_invalid_frame_pinhit_more_than_Max_point()
        {
            Bowl b1 = new Bowl(8);
            Bowl b2 = new Bowl(8);
            Frame frame = new Frame(b1,b2, 2);
            Assert.IsFalse(frame.IsValid);
        }
        
        [TestMethod]
        public void should_valid_when_create_invalid_frame_pinhit_less_than_Max_point()
        {
            Bowl b1 = new Bowl(4);
            Bowl b2 = new Bowl(6);
            Frame frame = new Frame(b1, b2, 2);
            Assert.IsTrue(frame.IsValid);
        }

        [TestMethod]
        public void should_valid_when_create_invalid_10th_frame_pinhit_less_than_Max_point()
        {
            Bowl b2 = new Bowl(10);
            Bowl b3 = new Bowl(10);
            AbstractFrame frame = new LastFrame(strike, b2, b3, 2);
            Assert.IsTrue(frame.IsValid);
        }

        [TestMethod]
        public void when_frame_update_score_for_strike()
        {
            var frameList = GetFrames();
            var strikeFrame =(AbstractFrame)_frameFactory.GetNextFrame(10, 0, 3);
            frameList[3] = strikeFrame;
            var score = strikeFrame.GetScoreForStrikeFrame(frameList);
            Assert.AreEqual(score,17);
            score = ((AbstractFrame)frameList[2]).GetScoreForStrikeFrame(frameList);
            Assert.AreEqual(score, 23);
            
        }

        [TestMethod]
        public void when_frame_update_score_for_spare()
        {
            var frameList = GetFrames();
            var strikeFrame = (AbstractFrame)_frameFactory.GetNextFrame(3, 7, 3);
            frameList[3] = strikeFrame;
            var score = strikeFrame.GetScoreForSpareFrame(frameList);
            Assert.AreEqual(score, 13);
            score = ((AbstractFrame)frameList[5]).GetScoreForSpareFrame(frameList);
            Assert.AreEqual(score, null);

        }

        [TestMethod]
        public void when_frame_update_score_for_10th_strike()
        {
            var frameList = GetFrames();
            var strikeFrame = (AbstractFrame)_frameFactory.GetNextFrame(10,3,10, 3);
            frameList[5] = strikeFrame;
            var score = strikeFrame.GetScoreForStrikeFrame(frameList);
            Assert.AreEqual(score, 23);
           

        }

        [TestMethod]
        public void when_frame_update_score_for_10th_spare()
        {
            var frameList = GetFrames();
            var strikeFrame = (AbstractFrame)_frameFactory.GetNextFrame(2, 8,10, 3);
            frameList[5] = strikeFrame;
            var score = strikeFrame.GetScoreForSpareFrame(frameList);
            Assert.AreEqual(score, 20);

        }

        private Dictionary<int, IFrame> GetFrames()
        {
            var frameList = new Dictionary<int, IFrame>();
            frameList.Add(1, _frameFactory.GetNextFrame(3, 7, 1));
            frameList.Add(2, _frameFactory.GetNextFrame(10, 0, 2));
            frameList.Add(3, _frameFactory.GetNextFrame(3, 4, 3));
            frameList.Add(4, _frameFactory.GetNextFrame(3, 4, 4));
            frameList.Add(5, _frameFactory.GetNextFrame(6, 4, 5));
            return frameList;
        }
    }
}
