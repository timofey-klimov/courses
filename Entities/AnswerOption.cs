﻿using Entities.Base;
using System;

namespace Entities
{
    public class AnswerOption : TrackableEntity<Guid>
    {
        public string Content { get; private set; }

        public bool IsCorrect { get; private set; }

        private AnswerOption() { }

        public AnswerOption(string content, bool isCorrect)
        {
            Content = content;
            IsCorrect = isCorrect;
        }
    }
}
