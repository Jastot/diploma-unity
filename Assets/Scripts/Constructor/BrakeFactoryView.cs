﻿using System;
using Interfaces;

namespace Diploma.Constructor
{
    public class BrakeFactoryView: IFactoryView
    {
        public void ChoosedNextStage(bool nextStage)
        {
            throw new NotImplementedException();
        }

        public event Action<float, int> NextStage;
    }
}