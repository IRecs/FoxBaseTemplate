using System;

namespace Engine.Senser
{
    public interface ISenser
    {
        public event Action<bool> onSwitch;

        bool isEnable { get; }

        void SwitchEnable();

        void SetEnable(bool enable);
    }
}
