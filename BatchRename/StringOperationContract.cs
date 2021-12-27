using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;

namespace BatchRename
{
    public class StringArgs
    {
    }

    public class ChangeExtensionArgs : StringArgs, INotifyPropertyChanged
    {
        public string From { get; set; }
        public string To { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    //public class NewCaseArgs : StringArgs
    //{
    //    public int style { get; set; } //1: All Uppercase, 2: All lowercase, 3: UpperCase the first character each word
    //}

    public class PascalCaseArgs : StringArgs 
    { 
    }

    public class RemovePatternArgs : StringArgs
    {
        public string pattern { get; set; }
    }

    public class TrimArgs : StringArgs
    {
    }

    public class LowerAndRemoveSpaceArgs : StringArgs
    {
    }

    public class AddPrefixArgs : StringArgs
    {
        public string text { get; set; }
    }

    public class AddSuffixArgs : StringArgs
    {
        public string text { get; set; }
    }

    public class ReplaceSpaceToDotArgs : StringArgs
    {
    }

    public class AddCounterArgs : StringArgs
    {
        public string start { get; set; }
        public string step { get; set; }
        public string num { get; set; }
    }

    public abstract class StringMethod : INotifyPropertyChanged
    {
        public StringArgs Args { get; set; }
        public virtual string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public abstract string Operate(string origin, bool isFileName,int count);

        public abstract StringMethod Clone();

        public abstract void Config();

        public virtual string Description { get; set; }
    }

    public class ChangeExtensionMethod : StringMethod, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Name => "Change extension";

        public override string Description
        {
            get
            {
                var args = Args as ChangeExtensionArgs;
                return $"Replace extension '{args.From}' to '{args.To}' ";
            }
        }

        public override StringMethod Clone()
        {
            var oldArgs = Args as ChangeExtensionArgs;
            return new ChangeExtensionMethod()
            {
                Args = new ChangeExtensionArgs()
                {
                    From = oldArgs.From,
                    To = oldArgs.To,
                }
            };
        }

        public override string Operate(string origin, bool isFileName, int count)
        {
            var args = Args as ChangeExtensionArgs;
            var from = args.From;
            var to = args.To;

            if (isFileName)
            {
                string name = Path.GetFileNameWithoutExtension(origin);
                string extension = Path.GetExtension(origin);

                extension = extension.Replace(from, to);

                return (name + extension);
            }
            return origin.Replace(from, to);
        }

        public override void Config()
        {
            var screen = new ChangeExtensionMethodControl(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }
    }

    //public class NewCaseMethod : StringMethod, INotifyPropertyChanged
    //{
    //    public override string Name => "New Case";

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public override StringMethod Clone()
    //    {
    //        var oldArgs = Args as NewCaseArgs;
    //        return new NewCaseMethod()
    //        {
    //            Args = new NewCaseArgs()
    //            {
    //                style = oldArgs.style
    //            }
    //        };
    //    }

    //    public override void Config()
    //    {
    //        var screen = new NewCaseMethodControl(Args);
    //        if (screen.ShowDialog() == true)
    //        {
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
    //        }
    //    }

    //    public override string Operate(string origin, bool isFileName)
    //    {
    //        var args = Args as NewCaseArgs;
    //        if (isFileName == true)
    //        {
    //            string name = Path.GetFileNameWithoutExtension(origin);
    //            string extension = Path.GetExtension(origin);
    //            if (args.style == 1)
    //            {
    //                name = name.ToUpper();
    //            }
    //            else if (args.style == 2)
    //            {
    //                name = name.ToLower();
    //            }
    //            else
    //            {
    //                name = name.ToLower();
    //                TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
    //                name = cultInfo.ToTitleCase(name);
    //            }
    //            return (name + extension);
    //        }
    //        else
    //        {
    //            if (args.style == 1)
    //            {
    //                return origin.ToUpper();
    //            }
    //            else if (args.style == 2)
    //            {
    //                return origin.ToLower();
    //            }
    //            else
    //            {
    //                origin = origin.ToLower();
    //                TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
    //                string res = cultInfo.ToTitleCase(origin);
    //                return res;
    //            }
    //        }
    //    }

    //    public override string Description
    //    {
    //        get
    //        {
    //            var args = Args as NewCaseArgs;

    //            if (args.style == 1)
    //            {
    //                return "Uppercase all letters";
    //            }
    //            if (args.style == 2)
    //            {
    //                return "Lowercase all letters";
    //            }
    //            if (args.style == 3)
    //            {
    //                return "Only Uppercase the first letter of each word";
    //            }
    //            return "error";
    //        }
    //    }
    //}

    public class RemovePatternMethod : StringMethod, INotifyPropertyChanged
    {
        public override string Name => "Remove Pattern";

        public event PropertyChangedEventHandler PropertyChanged;

        public override StringMethod Clone()
        {
            var oldArgs = Args as RemovePatternArgs;
            return new RemovePatternMethod()
            {
                Args = new RemovePatternArgs()
                {
                    pattern = oldArgs.pattern
                }
            };
        }

        public override void Config()
        {
            throw new NotImplementedException();
        }

        public override string Operate(string origin, bool isFileName, int count)
        {
            var args = Args as RemovePatternArgs;
            return origin.Replace(args.pattern, "");
        }
    }

    public class TrimMethod : StringMethod, INotifyPropertyChanged
    {
        public override string Name => "Trim";

        public event PropertyChangedEventHandler PropertyChanged;

        public override StringMethod Clone()
        {
            var oldArgs = Args as TrimArgs;
            return new TrimMethod()
            {
                Args = new TrimArgs()
                {
                }
            };
        }

        public override void Config()
        {
            MessageBox.Show("Dont't need to config this method");
        }

        public override string Operate(string origin, bool isFileName, int count)
        {
            return origin.Trim();
        }

        public override string Description
        {
            get
            {
                return "Remove all space from the beginning and the ending of the filename";
            }
        }
    }

    public class LowerAndRemoveSpaceMethod : StringMethod, INotifyPropertyChanged
    {
        public override string Name => "Lower And Remove Space";

        public event PropertyChangedEventHandler PropertyChanged;

        public override StringMethod Clone()
        {
            var oldArgs = Args as LowerAndRemoveSpaceArgs;
            return new LowerAndRemoveSpaceMethod()
            {
                Args = new LowerAndRemoveSpaceArgs()
                {
                }
            };
        }

        public override void Config()
        {
            MessageBox.Show("Don't need to config this method");
        }

        public override string Operate(string origin, bool isFileName, int count)
        {
            if (isFileName)
            {
                string name = Path.GetFileNameWithoutExtension(origin);
                string extension = Path.GetExtension(origin);

                name = name.ToLower();

                //Xoa bo cac khoang trang dau va cuoi
                name = name.Trim();

                //Xoa bo cac khoang trang thua
                name = System.Text.RegularExpressions.Regex.Replace(name, @"\s+", "");

                return (name + extension);
            }
            else
            {
                origin = origin.ToLower();

                //Xoa bo cac khoang trang dau va cuoi
                origin = origin.Trim();

                //Xoa bo cac khoang trang thua
                origin = System.Text.RegularExpressions.Regex.Replace(origin, @"\s+", "");

                return origin;
            }
        }

        public override string Description
        {
            get
            {
                return "Convert all characters to lowercase, remove all spaces";
            }
        }
    }

    public class ReplaceSpaceToDotMethod : StringMethod, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Name => "Replace Space to Dot";

        public override string Description
        {
            get
            {
                var args = Args as ReplaceSpaceToDotArgs;

                return $"Replace  ' ' to '.'";
            }
        }

        public override StringMethod Clone()
        {
            var oldArgs = Args as ReplaceSpaceToDotArgs;
            return new ReplaceSpaceToDotMethod()
            {
                Args = new ReplaceSpaceToDotArgs()
                {
                }
            };
        }

        public override void Config()
        {
            MessageBox.Show("Dont't need to config this method");
        }

        public override string Operate(string origin, bool isFileName, int count)
        {
            var args = Args as ReplaceSpaceToDotArgs;

            if (isFileName)
            {
                string name = Path.GetFileNameWithoutExtension(origin);
                string extension = Path.GetExtension(origin);
                name = name.Replace(" ", ".");
                return (name + extension);
            }
            return origin;
        }
    }

    public class AddPrefixMethod : StringMethod, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Name => "Add Prefix";

        public override string Description
        {
            get
            {
                var args = Args as AddPrefixArgs;

                return $"AddPrefix  '{args.text}'";
            }
        }

        public override StringMethod Clone()
        {
            var oldArgs = Args as AddPrefixArgs;
            return new AddPrefixMethod()
            {
                Args = new AddPrefixArgs()
                {
                    text = oldArgs.text,
                }
            };
        }

        public override void Config()
        {
            var screen = new AddPrefixMethodControl(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }

        public override string Operate(string origin, bool isFileName, int count)
        {
            var args = Args as AddPrefixArgs;
            var text = args.text;

            if (isFileName)
            {
                string name = Path.GetFileNameWithoutExtension(origin);
                string extension = Path.GetExtension(origin);
                name = text + name;
                return (name + extension);
            }
            return origin;
        }
    }

    public class AddSuffixMethod : StringMethod, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Name => "Add Suffix";

        public override string Description
        {
            get
            {
                var args = Args as AddSuffixArgs;

                return $"AddSuffix  '{args.text}'";
            }
        }

        public override StringMethod Clone()
        {
            var oldArgs = Args as AddSuffixArgs;
            return new AddSuffixMethod()
            {
                Args = new AddSuffixArgs()
                {
                    text = oldArgs.text,
                }
            };
        }

        public override void Config()
        {
            var screen = new AddSuffixMethodControl(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }

        public override string Operate(string origin, bool isFileName, int count)
        {
            var args = Args as AddSuffixArgs;
            var text = args.text;

            if (isFileName)
            {
                string name = Path.GetFileNameWithoutExtension(origin);
                string extension = Path.GetExtension(origin);
                name = name + text;
                return (name + extension);
            }
            return origin;
        }
    }

    public class PascalCaseMethod : StringMethod, INotifyPropertyChanged
    {
        public override string Name => "Pascal Case";

        public event PropertyChangedEventHandler PropertyChanged;

        public override StringMethod Clone()
        {
            return new PascalCaseMethod()
            {
            };
        }

        public override void Config()
        {
            MessageBox.Show("Don't need to config this method");
        }

        public override string Operate(string origin, bool isFileName, int count)
        {
            var args = Args as PascalCaseArgs;
            if (isFileName == true)
            {
                string name = Path.GetFileNameWithoutExtension(origin);
                string extension = Path.GetExtension(origin);

                name = name.ToLower();
                TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
                name = cultInfo.ToTitleCase(name);

                return (name + extension);
            }
            else
            {
                origin = origin.ToLower();
                TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
                string res = cultInfo.ToTitleCase(origin);
                return res;
            }
        }

        public override string Description
        {
            get
            {
                return "Change filename to Pascal Case (seperated by space)";
            }
        }
    }

    public class AddCounterMethod : StringMethod, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Name => "Add Counter";
        public override string Description
        {
            get
            {
                var args = Args as AddCounterArgs;
                return $"Add Counter start at '{args.start}' with step = '{args.step}' and number of digit = '{args.num}' ";
            }
        }
        public override StringMethod Clone()
        {
            var oldArgs = Args as AddCounterArgs;
            return new AddCounterMethod()
            {
                Args = new AddCounterArgs()
                {
                    start = oldArgs.start,
                    step = oldArgs.step,
                    num=oldArgs.num,
                }
            };
        }

        public override void Config()
        {
            var screen = new AddCounterMethodControl(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }

        public override string Operate(string origin, bool isFileName, int count)
        {
            var args = Args as AddCounterArgs;
            int start = Int32.Parse(args.start);
            int step = Int32.Parse(args.step);
            int num = Int32.Parse(args.num);
            
            if (isFileName)
            {
                string name = Path.GetFileNameWithoutExtension(origin);
                string extension = Path.GetExtension(origin);

                int counter = start + step * count;
                string text = counter.ToString();
                for(int i = 1; i < num; i++)
                {
                    text = "0" + text;
                }

                name = name + text;
                return (name + extension);
            }
            return origin;
        }
    }

}