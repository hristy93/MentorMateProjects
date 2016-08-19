using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySoundsUWPApp
{
    public class FunnySoundsCreator
    {
        public FunnySound CreateFunnySounds(FunnySoundTypes funnySoundType)
        {
            switch (funnySoundType)
            {
                case FunnySoundTypes.Animals:
                    return new FunnySound()
                    {
                        Type = FunnySoundTypes.Animals,
                        ImageFilePath = "Assets/Images/Animals/Cat.png",
                        SoundFilePath = "Assets/Audio/Animals/Cat.wav",
                    };
                case FunnySoundTypes.Cartoons:
                    return new FunnySound()
                    {
                        Type = FunnySoundTypes.Animals,
                        ImageFilePath = "Assets/Images/Cartoons/Gun.png",
                        SoundFilePath = "Assets/Audio/Cartoons/Gun.wav"
                    };
                case FunnySoundTypes.Taunts:
                    return new FunnySound()
                    {
                        Type = FunnySoundTypes.Animals,
                        ImageFilePath = "Assets/Images/Taunts/LOL.png",
                        SoundFilePath = "Assets/Audio/Taunts/LOL.wav"
                    };
                case FunnySoundTypes.Warnings:
                    return new FunnySound()
                    {
                        Type = FunnySoundTypes.Animals,
                        ImageFilePath = "Assets/Images/Warnings/Siren.png",
                        SoundFilePath = "Assets/Audio/Warnings/Siren.wav"
                    };
                default:
                    throw new InvalidOperationException("This type of funny sound is not supported!");
            }
        }
    }
}
