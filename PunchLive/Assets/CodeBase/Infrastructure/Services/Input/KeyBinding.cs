using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    [Serializable]
    public class KeyBinding
    {
        [SerializeField] private List<ButtonType> ButtonSequence;

        public string ButtonSequenceCode
        {
            get
            {
                string sequenceCode = "";
                foreach (ButtonType buttonType in ButtonSequence)
                {
                    sequenceCode = string.Concat(sequenceCode, ((int)buttonType).ToString());
                }

                return sequenceCode;
            }
        }
    }
}