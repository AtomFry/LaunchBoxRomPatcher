using System;
using System.Collections.Generic;
using System.IO;

namespace LaunchBoxRomPatcher.Models.ModelWrappers
{

    public class RomPatcherWrapper : ModelWrapperBase<RomPatcher>
    {
        public RomPatcherWrapper(RomPatcher model) : base(model)
        {
        }

        public string RomPatcherId
        {
            get => Model.RomPatcherId;
        }

        public string RomPatcherName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }


        public string RomPatcherFilePath
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string RomPatcherCommandLine
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(RomPatcherName):
                    if (string.IsNullOrWhiteSpace(RomPatcherName))
                    {
                        yield return "Name is required";
                    }
                    break;

                case nameof(RomPatcherFilePath):
                    if (string.IsNullOrWhiteSpace(RomPatcherFilePath))
                    {
                        yield return "File path is required";
                    }

                    if(!File.Exists(RomPatcherFilePath))
                    {
                        yield return "File does not exist";
                    }
                    break;

                case nameof(RomPatcherCommandLine):
                    if (string.IsNullOrWhiteSpace(RomPatcherCommandLine))
                    {
                        yield return "Command line is required";
                    }
                    break;

                default:
                    break;
            }
        }
    }
}