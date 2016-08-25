using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySoundsUWPApp
{
    public class FunnySoundsCreator
    {
        public FunnySoundModel CreateFunnySounds(FunnySoundTypes funnySoundType, string funnySoundName)
        {
            switch (funnySoundType)
            {
                case FunnySoundTypes.Animals:
                    {
                        if (funnySoundName == "Cat")
                        {
                            return new FunnySoundModel()
                            {
                                Name = "Cat",
                                Type = FunnySoundTypes.Animals,
                                ImageFilePath = "Assets/Images/Animals/Cat.png",
                                SoundFilePath = "Assets/Audio/Animals/Cat.wav",
                            };
                        }
                        else if (funnySoundName == "Cow")
                        {
                            return new FunnySoundModel()
                            {
                                Name = "Cow",
                                Type = FunnySoundTypes.Animals,
                                ImageFilePath = "Assets/Images/Animals/Cow.png",
                                SoundFilePath = "Assets/Audio/Animals/Cow.wav",
                            };
                        }
                        else
                        {
                            throw new InvalidOperationException("This type of funny sound is not supported!");
                        }
                    }
                case FunnySoundTypes.Cartoons:
                    {
                        if (funnySoundName == "Gun")
                        {
                            return new FunnySoundModel()
                            {
                                Name = "Gun",
                                Type = FunnySoundTypes.Cartoons,
                                ImageFilePath = "Assets/Images/Cartoons/Gun.png",
                                SoundFilePath = "Assets/Audio/Cartoons/Gun.wav"
                            };
                        }
                        else if (funnySoundName == "Spring")
                        {
                            return new FunnySoundModel()
                            {
                                Name = "Spring",
                                Type = FunnySoundTypes.Cartoons,
                                ImageFilePath = "Assets/Images/Cartoons/Spring.png",
                                SoundFilePath = "Assets/Audio/Cartoons/Spring.wav"
                            };
                        }
                        else
                        {
                            throw new InvalidOperationException("This type of funny sound is not supported!");
                        }
                    }
                case FunnySoundTypes.Taunts:
                    {
                        if (funnySoundName == "LOL")
                        {
                            return new FunnySoundModel()
                            {
                                Name = "LOL",
                                Type = FunnySoundTypes.Taunts,
                                ImageFilePath = "Assets/Images/Taunts/LOL.png",
                                SoundFilePath = "Assets/Audio/Taunts/LOL.wav"
                            }; 
                        }
                        else if (funnySoundName == "Clock")
                        {
                            return new FunnySoundModel()
                            {
                                Name = "Clock",
                                Type = FunnySoundTypes.Taunts,
                                ImageFilePath = "Assets/Images/Taunts/Clock.png",
                                SoundFilePath = "Assets/Audio/Taunts/Clock.wav"
                            };
                        }
                        else
                        {
                            throw new InvalidOperationException("This type of funny sound is not supported!");
                        }
                    }
                case FunnySoundTypes.Warnings:
                    {
                        if (funnySoundName == "Siren")
                        {
                            return new FunnySoundModel()
                            {
                                Name = "Siren",
                                Type = FunnySoundTypes.Warnings,
                                ImageFilePath = "Assets/Images/Warnings/Siren.png",
                                SoundFilePath = "Assets/Audio/Warnings/Siren.wav"
                            }; 
                        }
                        else if (funnySoundName == "Ship")
                        {
                            return new FunnySoundModel()
                            {
                                Name = "Ship",
                                Type = FunnySoundTypes.Warnings,
                                ImageFilePath = "Assets/Images/Warnings/Ship.png",
                                SoundFilePath = "Assets/Audio/Warnings/Ship.wav"
                            };
                        }
                        else
                        {
                            throw new InvalidOperationException("This type of funny sound is not supported!");
                        }
                    }
                //case FunnySoundTypes.All:
                //    {
                //        if (funnySoundName == "All Sounds")
                //        {
                //            return new FunnySound()
                //            {
                //                Type = FunnySoundTypes.Warnings,
                //                ImageFilePath = "Assets/StoreLogo.png",
                //                SoundFilePath = String.Empty
                //            };
                //        }
                //        else
                //        {
                //            throw new InvalidOperationException("This type of funny sound is not supported!");
                //        }
                //    }
                default:
                    throw new InvalidOperationException("This type of funny sound is not supported!");
            }
        }
    }
}
