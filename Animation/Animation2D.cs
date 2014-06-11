using InfiniteBoxEngine.Object;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Animation
{
    /// <summary>
    /// Stores keyframes and a GameObject that it is tied to. Provides methids to get Translation at animation time.
    /// </summary>
    public class  Animation2D
    {
        LinkedList<Keyframe> timeline = new LinkedList<Keyframe>();
        GameObject obj;
        int animationLength;

        /// <summary>
        /// Initialises Animation and stores related GameObject.
        /// </summary>
        /// <param name="obj"> GameObject affected by the animation.</param>
        public Animation2D(GameObject obj)
        {
            this.obj = obj;
        }

        /// <summary>
        /// Adds keyframe at the position in the timeline relative to its time.
        /// </summary>
        /// <param name="keyframe">Keyframe to be added to the timeline</param>
        /// <returns></returns>
        public bool AddKeyframe(Keyframe keyframe)
        {
            LinkedListNode<Keyframe> firstBefore, firstAfter;

            if (timeline.Count == 0)
                timeline.AddFirst(keyframe);
            else
            {
                foreach (Keyframe frame in timeline)
                {
                    if (frame.FrameTime < keyframe.FrameTime)
                        firstBefore = timeline.Find(frame);
                    else if (frame.FrameTime > keyframe.FrameTime)
                        firstAfter = timeline.Find(frame);
                    else
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Removes specified keyframe from the timeline.
        /// </summary>
        /// <param name="keyframe">Keyframe to be removed</param>
        public void RemoveKeyframe(Keyframe keyframe)
        {
            timeline.Remove(keyframe);
        }

        /// <summary>
        /// Gets the translation of the object based on the given time, will extrapolate position if time is between two frames.
        /// </summary>
        /// <param name="time">Time (ms) of the anaimation to extrapolate from.</param>
        /// <returns>Translation relative to animation time.</returns>
        public Translation GetStateAtTime(float time)
        {
            float offest;
            Translation toReturn = new Translation();;

            foreach (Keyframe keyframe in timeline)
            {
                if (keyframe.FrameTime < time && timeline.Find(keyframe).Next.Value.FrameTime > time)
                {
                    offest = timeline.Find(keyframe).Next.Value.FrameTime - keyframe.FrameTime;
                    toReturn.Position = new Vector2((int)((timeline.Find(keyframe).Next.Value.Position.X - keyframe.Position.X) * offest),
                        (int)((timeline.Find(keyframe).Next.Value.Position.Y - keyframe.Position.Y) * offest));
                    toReturn.Rotation = (timeline.Find(keyframe).Next.Value.Rotation - keyframe.Rotation) * offest;
                    return toReturn;
                }
            }

            return new Translation(timeline.First.Value.Position, timeline.First.Value.Rotation);
        }

        /// <summary>
        /// Length of the animation.
        /// </summary>
        public int AnimationLength
        {
            get { return animationLength; }
            set { animationLength = value; }
        }
    }
}
