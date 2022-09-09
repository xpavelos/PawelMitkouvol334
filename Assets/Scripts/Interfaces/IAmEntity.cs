using System;

namespace TKOU.SimAI
{
    /// <summary>
    /// Basic entity of the game.
    /// </summary>
    public interface IAmEntity
    {
        public int Order { get; }
        /// <summary>
        /// Destroys this entity.
        /// </summary>
        void Destroy();
    }
}
