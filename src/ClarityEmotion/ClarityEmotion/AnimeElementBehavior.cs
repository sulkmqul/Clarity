using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.Util;
namespace ClarityEmotion
{
    /// <summary>
    /// 所作基底
    /// </summary>
    public abstract class BaseAnimeElementBehavior
    {
        public abstract EmotionAnimeData ProcBehavior(AnimeElement ae, EmotionAnimeData srcdata);
    }

    /// <summary>
    /// 通常所作
    /// </summary>
    public class AnimeElementBehavior : BaseAnimeElementBehavior
    {
        public override EmotionAnimeData ProcBehavior(AnimeElement ae, EmotionAnimeData srcdata)
        {
            EmotionAnimeData ans = srcdata.DeepClone<EmotionAnimeData>();
            return ans;
        }
    }
}
